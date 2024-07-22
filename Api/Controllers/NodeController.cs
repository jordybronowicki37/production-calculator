using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductionCalculator.Api.Controllers.dto;
using ProductionCalculator.Api.Data.DbContexts;
using ProductionCalculator.Core.components.entityContainer;
using ProductionCalculator.Core.components.nodes;
using ProductionCalculator.Core.components.nodes.abstractions;
using ProductionCalculator.Core.components.nodes.enums;
using ProductionCalculator.Core.components.nodes.interfaces;
using ProductionCalculator.Core.components.nodes.nodeTypes;
using ProductionCalculator.Core.components.targets;
using ProductionCalculator.Core.components.worksheet;

namespace ProductionCalculator.Api.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/worksheet/{worksheetId:Guid}/[controller]")]
public class NodeController : ControllerBase
{
    private readonly ILogger<NodeController> _logger;
    private readonly DocumentContext _context;
    
    public NodeController(
        ILogger<NodeController> logger, 
        DocumentContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpPost("")]
    public IActionResult AddNode(Guid worksheetId, NodeCreateDto nodeCreateDto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        if (!Enum.TryParse(nodeCreateDto.Type, out ENodeTypes type)) return BadRequest("Could not parse type");
        INode node;
        
        switch (type)
        {
            case ENodeTypes.Spawn:
            {
                if (nodeCreateDto.Product == null) return BadRequest("Product field is empty");
                var product = e.GetProduct(nodeCreateDto.Product);
                if (product == null) return NotFound("ProductId not found");
                node = w.GetNodeBuilder<SpawnNode>().SetPosition(nodeCreateDto.Position).SetProduct(product).Build();
                break;
            }
            case ENodeTypes.Production:
            {
                if (nodeCreateDto.Recipe == null) return BadRequest("Recipe field is empty");
                if (nodeCreateDto.Machine == null) return BadRequest("Machine field is empty");
                var recipe = e.GetRecipe(nodeCreateDto.Recipe);
                var machine = e.GetMachine(nodeCreateDto.Machine);
                if (recipe == null) return NotFound("Recipe not found");
                if (machine == null) return NotFound("Machine not found");
                node = w.GetNodeBuilder<ProductionNode>().SetPosition(nodeCreateDto.Position).SetRecipe(recipe, machine).Build();
                break;
            }
            case ENodeTypes.End:
            {
                if (nodeCreateDto.Product == null) return BadRequest("Product field is empty");
                var product = e.GetProduct(nodeCreateDto.Product);
                if (product == null) return NotFound("ProductId not found");
                node = w.GetNodeBuilder<EndNode>().SetPosition(nodeCreateDto.Position).SetProduct(product).Build();
                break;
            }
            default:
                return BadRequest();
        }
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Nodes, w.Nodes);
        _context.Worksheets.UpdateOne(filter, update);

        return Ok(NodeDto.GenerateNode(node));
    }

    [HttpPut("{nodeId:Guid}/position")]
    public IActionResult EditNodePosition(Guid nodeId, Guid worksheetId, NodePosition dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");

        node.Position = dto;
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Nodes, w.Nodes);
        _context.Worksheets.UpdateOne(filter, update);
        
        return Ok(NodeDto.GenerateNode(node));
    }

    [HttpPut("{nodeId:Guid}/product")]
    public IActionResult EditNodeProduct(Guid nodeId, Guid worksheetId, NodeSetProductDto nodeSetProductDto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");
        if (node is not IHasProduct productNode) return BadRequest("Node does not support products");

        var product = e.GetProduct(nodeSetProductDto.Product);
        if (product == null) return NotFound("ProductId not found");
        
        productNode.ProductId = product.Id;

        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Nodes, w.Nodes);
        _context.Worksheets.UpdateOne(filter, update);
        
        return Ok(NodeDto.GenerateNode(productNode));
    }
    
    [HttpPut("{nodeId:Guid}/recipe")]
    public IActionResult EditNodeRecipe(Guid nodeId, Guid worksheetId, NodeSetRecipeDto nodeSetRecipeDto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");
        if (node is not IHasRecipe recipeNode) return BadRequest("Node does not support recipes");
        
        var recipe = e.GetRecipe(nodeSetRecipeDto.Recipe);
        if (recipe == null) return NotFound("ProductId not found");
        recipeNode.RecipeId = recipe.Id;
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Nodes, w.Nodes);
        _context.Worksheets.UpdateOne(filter, update);
        
        return Ok(NodeDto.GenerateNode(recipeNode));
    }
    
    [HttpPut("{nodeId:Guid}/targets")]
    public IActionResult EditNodeTargets(Guid nodeId, Guid worksheetId, IEnumerable<ProductionTargetDto> dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");

        var targetTypes = new List<TargetProduction>();
        foreach (var dtoTarget in dto)
        {
            if (!Enum.TryParse(dtoTarget.Type, out TargetProductionTypes type)) return BadRequest("Could not parse type " + dtoTarget.Type);
            targetTypes.Add(new TargetProduction(type, dtoTarget.Amount));
        }

        if (targetTypes.Count == 0)
        {
            node.ClearTargets();
        }
        else if (targetTypes[0].Type == TargetProductionTypes.ExactAmount)
        {
            node.SetExactTarget(targetTypes[0].Amount);
        }
        else
        {
            var minTarget = targetTypes.FirstOrDefault(v => v.Type == TargetProductionTypes.MinAmount);
            float? minAmount = minTarget == null ? null : minTarget.Amount;
            var maxTarget = targetTypes.FirstOrDefault(v => v.Type == TargetProductionTypes.MaxAmount);
            float? maxAmount = maxTarget == null ? null : maxTarget.Amount;
            node.SetMinMaxTarget(minAmount, maxAmount);
        }
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Nodes, w.Nodes);
        _context.Worksheets.UpdateOne(filter, update);
        
        return Ok(NodeDto.GenerateNode(node));
    }
    
    [HttpDelete("{nodeId:Guid}")]
    public IActionResult DeleteNode(Guid nodeId, Guid worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");
        
        w.RemoveNode(node);
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Nodes, w.Nodes);
        _context.Worksheets.UpdateOne(filter, update);
        
        return NoContent();
    }

    private EntityContainer? GetEntityContainer(Guid id)
    {
        var filter = Builders<EntityContainer>.Filter.Eq(w => w.Id, id);
        return _context.EntityContainers.Find(filter).FirstOrDefault();
    }

    private Worksheet? GetWorksheet(Guid id)
    {
        var filter = Builders<Worksheet>.Filter.Eq(w => w.Id, id);
        return _context.Worksheets.Find(filter).FirstOrDefault();
    }

    private ANode? GetNode(Worksheet worksheet, Guid id)
    {
        return worksheet.Nodes.FirstOrDefault(n => n.Id == id);
    }
}