using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.calculator.targets;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using SiteReact.Controllers.dto.connections;
using SiteReact.Controllers.dto.nodes;
using SiteReact.Controllers.dto.targets;
using SiteReact.Data;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:int}/[controller]")]
public class NodeController : ControllerBase
{
    private readonly ILogger<NodeController> _logger;
    
    public NodeController(ILogger<NodeController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost]
    public IActionResult AddNode(int worksheetId, DtoNodeCreate dto)
    {
        var w = StaticValues.Get().Worksheet[worksheetId];
        if (!Enum.TryParse(dto.Type, out ENodeTypes type)) return BadRequest("Could not parse type");
        INode node;
        
        switch (type)
        {
            case ENodeTypes.Spawn:
            {
                if (dto.Product == null) return BadRequest("Product field was empty");
                var product = w.GetProduct(dto.Product);
                if (product == null) return NotFound("Product not found");
                node = w.GetNodeBuilder<SpawnNode>().SetProduct(product).Build();
                break;
            }
            case ENodeTypes.Production:
            {
                if (dto.Recipe == null) return BadRequest("Recipe field was empty");
                var recipe = w.GetRecipe(dto.Recipe);
                if (recipe == null) return NotFound("Recipe not found");
                node = w.GetNodeBuilder<ProductionNode>().SetRecipe(recipe).Build();
                break;
            }
            case ENodeTypes.End:
            {
                if (dto.Product == null) return BadRequest("Product field was empty");
                var product = w.GetProduct(dto.Product);
                if (product == null) return NotFound("Product not found");
                node = w.GetNodeBuilder<EndNode>().SetProduct(product).Build();
                break;
            }
            default:
                return BadRequest();
        }

        return Ok(NodeDto.GenerateNode(node));
    }
    
    [HttpPut("{nodeId:int}/product")]
    public IActionResult EditNodeProduct(int nodeId, int worksheetId, DtoNodeSetProduct dto)
    {
        var w = StaticValues.Get().Worksheet[worksheetId];
        
        var node = w.Nodes.FirstOrDefault(n => n.Id == nodeId);
        if (node == null) return NotFound("Node is not found");
        if (node is not IHasProduct productNode) return BadRequest("Node does not support products");

        var product = w.GetProduct(dto.Product);
        if (product == null) return NotFound("Product not found");
        productNode.Product = product;

        if (node is INodeOut nodeOut)
        {
            foreach (var connection in nodeOut.OutputConnections) connection.Product = product;
        }
        if (node is INodeIn nodeIn)
        {
            foreach (var connection in nodeIn.InputConnections) connection.Product = product;
        }
        
        return Ok(NodeDto.GenerateNode(productNode));
    }
    
    [HttpPut("{nodeId:int}/recipe")]
    public IActionResult EditNodeRecipe(int nodeId, int worksheetId, DtoNodeSetRecipe dto)
    {
        var w = StaticValues.Get().Worksheet[worksheetId];
        
        var node = w.Nodes.FirstOrDefault(n => n.Id == nodeId);
        if (node == null) return NotFound("Node is not found");
        if (node is not IHasRecipe recipeNode) return BadRequest("Node does not support recipes");
        
        var recipe = w.GetRecipe(dto.Recipe);
        if (recipe == null) return NotFound("Product not found");
        recipeNode.Recipe = recipe;
        
        return Ok(NodeDto.GenerateNode(recipeNode));
    }
    
    [HttpPut("{nodeId:int}/targets")]
    public IActionResult EditNodeTargets(int nodeId, int worksheetId, IEnumerable<DtoProductionTarget> dto)
    {
        var w = StaticValues.Get().Worksheet[worksheetId];
        
        var node = w.Nodes.FirstOrDefault(n => n.Id == nodeId);
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
        
        return Ok(NodeDto.GenerateNode(node));
    }
    
    [HttpDelete("{nodeId:int}")]
    public IActionResult DeleteNode(int nodeId, int worksheetId)
    {
        return Ok();
    }

    [HttpPost("connection")]
    public IActionResult AddNode(int worksheetId, DtoConnectionCreate dto)
    {
        var w = StaticValues.Get().Worksheet[worksheetId];
        
        var node1 = w.Nodes.FirstOrDefault(n => n.Id == dto.InputNodeId);
        if (node1 == null) return NotFound("Node is not found");
        if (node1 is not INodeOut source) return BadRequest("Source node is not an output");
        
        var node2 = w.Nodes.FirstOrDefault(n => n.Id == dto.OutputNodeId);
        if (node2 == null) return NotFound("Node is not found");
        if (node2 is not INodeIn target) return BadRequest("Target node is not an input");
        
        var product = w.GetProduct(dto.Product);
        if (product == null) return BadRequest();

        var connection = new Connection(source, target, product);
        source.AddOutputConnection(connection);
        target.AddInputConnection(connection);
        
        return Ok(new DtoConnectionDouble(connection));
    }

    [HttpDelete("connection/{connectionId:int}")]
    public IActionResult RemoveNode(int worksheetId, int connectionId)
    {
        var w = StaticValues.Get().Worksheet[worksheetId];
        
        foreach (var node in w.Nodes)
        {
            if (node is INodeOut nodeOut) nodeOut.RemoveConnnection(connectionId);
            if (node is INodeIn nodeIn) nodeIn.RemoveConnnection(connectionId);
        }

        return NoContent();
    }
}