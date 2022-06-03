using Microsoft.AspNetCore.Mvc;

namespace SiteReact.Controllers;

[ApiController]
[Route("[controller]")]
public class NodeController : ControllerBase
{
    private readonly ILogger<NodeController> _logger;
    
    public NodeController(ILogger<NodeController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost("worksheet/{worksheetId:int}")]
    public IActionResult AddNode(int worksheetId)
    {
        return Ok();
    }
    
    [HttpPatch("{nodeId:int}/worksheet/{worksheetId:int}")]
    public IActionResult EditNode(int nodeId, int worksheetId)
    {
        return Ok();
    }
    
    [HttpDelete("{nodeId:int}/worksheet/{worksheetId:int}")]
    public IActionResult DeleteNode(int nodeId, int worksheetId)
    {
        return Ok();
    }
}