using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto;
using SiteReact.Data.DbContexts;

namespace SiteReact.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("worksheet/{worksheetId:Guid}/[controller]")]
public class ConnectionController : ControllerBase
{
    private readonly ILogger<ConnectionController> _logger;
    private readonly DocumentContext _context;
    
    public ConnectionController(
        ILogger<ConnectionController> logger, 
        DocumentContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("")]
    public IActionResult AddConnection(Guid worksheetId, ConnectionCreateDto connectionCreateDto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var node1 = GetNode(w, connectionCreateDto.InputNodeId);
        if (node1 == null) return NotFound("Node is not found");
        if (node1 is not INodeOut source) return BadRequest("Source node is not an output");
        
        var node2 = GetNode(w, connectionCreateDto.OutputNodeId);
        if (node2 == null) return NotFound("Node is not found");
        if (node2 is not INodeIn target) return BadRequest("Target node is not an input");
        
        var product = e.GetProduct(connectionCreateDto.Product);
        if (product == null) return BadRequest("Product is not found");

        var connection = w.GetConnectionBuilder(source, target, product).Build();

        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Connections, w.Connections);
        _context.Worksheets.UpdateOne(filter, update);
        
        return Ok(new ConnectionDto(connection));
    }

    [HttpPut("{connectionId:Guid}")]
    public IActionResult EditConnection(Guid worksheetId, Guid connectionId, ConnectionEditDto connectionEditDto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");

        var connection = w.Connections.FirstOrDefault(c => c.Id == connectionId);
        if (connection == null) return NotFound("Connection is not found");

        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        if (e.GetProduct(connectionEditDto.ProductId) == null) return NotFound("Product is not found");
        
        connection.ProductId = connectionEditDto.ProductId;
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Connections, w.Connections);
        _context.Worksheets.UpdateOne(filter, update);

        return Ok(new ConnectionDto(connection));
    }

    [HttpDelete("{connectionId:Guid}")]
    public IActionResult RemoveConnection(Guid worksheetId, Guid connectionId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");

        var connection = w.Connections.FirstOrDefault(c => c.Id == connectionId);
        if (connection == null) return NotFound("Connection is not found");
        
        w.RemoveConnection(connection);
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Connections, w.Connections);
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