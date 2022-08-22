using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using SiteReact.Controllers.dto.nodes;
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
    
    [HttpPatch("{nodeId:int}/product")]
    public IActionResult EditNodeProduct(int nodeId, int worksheetId, DtoNodeSetProduct dto)
    {
        var w = StaticValues.Get().Worksheet[worksheetId];
        
        var node = w.Nodes.First(n => n.Id == nodeId);
        if (node is not IHasProduct productNode) return BadRequest();

        var product = w.GetProduct(dto.Product);
        if (product == null) return BadRequest();
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
    
    [HttpPatch("{nodeId:int}/recipe")]
    public IActionResult EditNodeRecipe(int nodeId, int worksheetId, DtoNodeSetRecipe dto)
    {
        var w = StaticValues.Get().Worksheet[worksheetId];
        
        var node = w.Nodes.First(n => n.Id == nodeId);
        if (node is not IHasRecipe recipeNode) return BadRequest();
        
        var recipe = w.GetRecipe(dto.Recipe);
        if (recipe == null) return BadRequest();
        recipeNode.Recipe = recipe;
        
        return Ok(NodeDto.GenerateNode(recipeNode));
    }
    
    [HttpDelete("{nodeId:int}")]
    public IActionResult DeleteNode(int nodeId, int worksheetId)
    {
        return Ok();
    }
}