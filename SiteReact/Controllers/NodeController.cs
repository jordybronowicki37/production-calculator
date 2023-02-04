using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.targets;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.connections;
using SiteReact.Controllers.dto.nodes;
using SiteReact.Controllers.dto.targets;
using SiteReact.Data.DbContexts;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:long}/[controller]")]
public class NodeController : ControllerBase
{
    private readonly ILogger<NodeController> _logger;
    private readonly MainContext _context;
    
    public NodeController(
        ILogger<NodeController> logger, 
        MainContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpPost]
    public IActionResult AddNode(long worksheetId, DtoNodeCreate dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        if (!Enum.TryParse(dto.Type, out ENodeTypes type)) return BadRequest("Could not parse type");
        INode node;
        
        switch (type)
        {
            case ENodeTypes.Spawn:
            {
                if (dto.Product == null) return BadRequest("Product field was empty");
                var product = w.EntityContainer.GetProduct(dto.Product);
                if (product == null) return NotFound("Product not found");
                node = w.GetNodeBuilder<SpawnNode>().SetProduct(product).Build();
                break;
            }
            case ENodeTypes.Production:
            {
                if (dto.Recipe == null) return BadRequest("Recipe field was empty");
                var recipe = w.EntityContainer.GetRecipe(dto.Recipe);
                if (recipe == null) return NotFound("Recipe not found");
                node = w.GetNodeBuilder<ProductionNode>().SetRecipe(recipe).Build();
                break;
            }
            case ENodeTypes.End:
            {
                if (dto.Product == null) return BadRequest("Product field was empty");
                var product = w.EntityContainer.GetProduct(dto.Product);
                if (product == null) return NotFound("Product not found");
                node = w.GetNodeBuilder<EndNode>().SetProduct(product).Build();
                break;
            }
            default:
                return BadRequest();
        }
        
        _context.SaveChanges();

        return Ok(DtoNode.GenerateNode(node));
    }
    
    [HttpPut("{nodeId:long}/product")]
    public IActionResult EditNodeProduct(long nodeId, long worksheetId, DtoNodeSetProduct dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");
        if (node is not IHasProduct productNode) return BadRequest("Node does not support products");

        var product = w.EntityContainer.GetProduct(dto.Product);
        if (product == null) return NotFound("Product not found");
        productNode.Product = product;

        _context.SaveChanges();
        
        return Ok(DtoNode.GenerateNode(productNode));
    }
    
    [HttpPut("{nodeId:long}/recipe")]
    public IActionResult EditNodeRecipe(long nodeId, long worksheetId, DtoNodeSetRecipe dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");
        if (node is not IHasRecipe recipeNode) return BadRequest("Node does not support recipes");
        
        var recipe = w.EntityContainer.GetRecipe(dto.Recipe);
        if (recipe == null) return NotFound("Product not found");
        recipeNode.Recipe = recipe;
        
        _context.SaveChanges();
        
        return Ok(DtoNode.GenerateNode(recipeNode));
    }
    
    [HttpPut("{nodeId:long}/targets")]
    public IActionResult EditNodeTargets(long nodeId, long worksheetId, IEnumerable<DtoProductionTarget> dto)
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

        foreach (var target in node.ProductionTargets)
        {
            target.NodeId = node.Id;
            _context.Add(target);
        }
        _context.SaveChanges();
        
        return Ok(DtoNode.GenerateNode(node));
    }
    
    [HttpDelete("{nodeId:long}")]
    public IActionResult DeleteNode(long nodeId, long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var node = GetNode(w, nodeId);
        if (node == null) return NotFound("Node is not found");
        
        w.RemoveNode(node);
        
        _context.SaveChanges();
        
        return Ok();
    }

    [HttpPost("connection")]
    public IActionResult AddNode(long worksheetId, DtoConnectionCreate dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var node1 = GetNode(w, dto.InputNodeId);
        if (node1 == null) return NotFound("Node is not found");
        if (node1 is not INodeOut source) return BadRequest("Source node is not an output");
        
        var node2 = GetNode(w, dto.OutputNodeId);
        if (node2 == null) return NotFound("Node is not found");
        if (node2 is not INodeIn target) return BadRequest("Target node is not an input");
        
        var product = w.EntityContainer.GetProduct(dto.Product);
        if (product == null) return BadRequest();

        var connection = w.GetConnectionBuilder(source, target, product).Build();

        _context.SaveChanges();
        
        return Ok(new DtoConnectionDouble(connection));
    }

    [HttpDelete("connection/{connectionId:long}")]
    public IActionResult RemoveNode(long worksheetId, long connectionId)
    {
        var worksheet = GetWorksheet(worksheetId);
        if (worksheet == null) return NotFound("Worksheet is not found");

        var connection = worksheet.Connections.FirstOrDefault(c => c.Id == connectionId);
        if (connection == null) return NotFound("Connection is not found");
        
        worksheet.RemoveConnection(connection);
        
        _context.SaveChanges();

        return NoContent();
    }

    private Worksheet? GetWorksheet(long worksheetId)
    {
        return _context.Worksheets.FirstOrDefault(w => w.Id == worksheetId);
    }

    private ANode? GetNode(Worksheet worksheet, long nodeId)
    {
        return worksheet.Nodes.FirstOrDefault(n => n.Id == nodeId);
    }
}