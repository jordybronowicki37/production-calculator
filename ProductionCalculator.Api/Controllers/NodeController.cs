using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.targets;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.nodes;
using SiteReact.Controllers.dto.targets;
using SiteReact.Data.DbContexts;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:Guid}/[controller]")]
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
    public IActionResult AddNode(Guid worksheetId, DtoNodeCreate dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        if (!Enum.TryParse(dto.Type, out ENodeTypes type)) return BadRequest("Could not parse type");
        INode node;
        
        switch (type)
        {
            case ENodeTypes.Spawn:
            {
                if (dto.Product == null) return BadRequest("Product field is empty");
                var product = e.GetProduct(dto.Product);
                if (product == null) return NotFound("ProductId not found");
                node = w.GetNodeBuilder<SpawnNode>().SetProduct(product).Build();
                break;
            }
            case ENodeTypes.Production:
            {
                if (dto.Recipe == null) return BadRequest("Recipe field is empty");
                if (dto.Machine == null) return BadRequest("Machine field is empty");
                var recipe = e.GetRecipe(dto.Recipe);
                var machine = e.GetMachine(dto.Machine);
                if (recipe == null) return NotFound("Recipe not found");
                if (machine == null) return NotFound("Machine not found");
                node = w.GetNodeBuilder<ProductionNode>().SetRecipe(recipe, machine).Build();
                break;
            }
            case ENodeTypes.End:
            {
                if (dto.Product == null) return BadRequest("Product field is empty");
                var product = e.GetProduct(dto.Product);
                if (product == null) return NotFound("ProductId not found");
                node = w.GetNodeBuilder<EndNode>().SetProduct(product).Build();
                break;
            }
            default:
                return BadRequest();
        }
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Nodes, w.Nodes);
        _context.Worksheets.UpdateOne(filter, update);

        return Ok(DtoNode.GenerateNode(node));
    }
    
    [HttpPut("{nodeId:Guid}/product")]
    public IActionResult EditNodeProduct(Guid nodeId, Guid worksheetId, DtoNodeSetProduct dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");
        if (node is not IHasProduct productNode) return BadRequest("Node does not support products");

        var product = e.GetProduct(dto.Product);
        if (product == null) return NotFound("ProductId not found");
        
        productNode.ProductId = product.Id;

        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Nodes, w.Nodes);
        _context.Worksheets.UpdateOne(filter, update);
        
        return Ok(DtoNode.GenerateNode(productNode));
    }
    
    [HttpPut("{nodeId:Guid}/recipe")]
    public IActionResult EditNodeRecipe(Guid nodeId, Guid worksheetId, DtoNodeSetRecipe dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");
        if (node is not IHasRecipe recipeNode) return BadRequest("Node does not support recipes");
        
        var recipe = e.GetRecipe(dto.Recipe);
        if (recipe == null) return NotFound("ProductId not found");
        recipeNode.RecipeId = recipe.Id;
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Nodes, w.Nodes);
        _context.Worksheets.UpdateOne(filter, update);
        
        return Ok(DtoNode.GenerateNode(recipeNode));
    }
    
    [HttpPut("{nodeId:Guid}/targets")]
    public IActionResult EditNodeTargets(Guid nodeId, Guid worksheetId, IEnumerable<DtoProductionTarget> dto)
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
        
        return Ok(DtoNode.GenerateNode(node));
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