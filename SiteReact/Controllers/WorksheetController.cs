﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.calculator;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.worksheets;
using SiteReact.Data.DbContexts;
using SiteReact.Data.GameDataPresets;

namespace SiteReact.Controllers;

[ApiController]
[Route("[controller]")]
public class WorksheetController : ControllerBase
{
    private readonly ILogger<WorksheetController> _logger;
    private readonly DocumentContext _context;
    
    public WorksheetController(
        ILogger<WorksheetController> logger, 
        DocumentContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var ws = GetAllWorksheets();
        return Ok(ws.Select(w =>
        {
            var e = GetEntityContainer(w.EntityContainerId);
            return new DtoWorksheetSmall(w, e);
        }));
    }

    [HttpGet("{id:Guid}")]
    public IActionResult Get(Guid id)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        if (w.Nodes.Count != 0) CalculatorLimit.ReCalculateAmounts(w, e);
        return Ok(new DtoWorksheet(w, e));
    }

    [HttpPost("")]
    public IActionResult CreateNew(DtoWorksheetCreate dto)
    {
        var e = new EntityContainer();
        var w = new Worksheet(e){Name = dto.Name};

        switch (dto.DataPreset)
        {
            case "":
            case "none":
                break;
            case "dysonSphereProgram":
                DSPData.addData(e);
                break;
            case "satisfactoryEarlyAccess":
                SatisfactoryData.addData(e);
                break;
            case "satisfactoryExperimental":
                SatisfactoryExperimentalData.addData(e);
                break;
            case "satisfactoryFICSMAS":
                SatisfactoryFicsMasData.addData(e);
                break;
            default:
                return NotFound("Data preset is not found");
        }
        
        _context.Worksheets.InsertOne(w);
        
        return Ok(new DtoWorksheet(w, e));
    }

    [HttpPatch("{id:Guid}")]
    public IActionResult Edit(Guid id, DtoWorksheetCreate dto)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        w.Name = dto.Name;
        
        _context.Worksheets.InsertOne(w);
        
        return Ok(new DtoWorksheet(w, e));
    }
    
    [HttpPost("{id:Guid}/calculate")]
    public IActionResult Calculate(Guid id)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");

        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        CalculatorLimit.ReCalculateAmounts(w, e);
        
        _context.Worksheets.InsertOne(w);
        
        return Ok(new DtoWorksheet(w, e));
    }
    
    private IEnumerable<Worksheet> GetAllWorksheets()
    {
        var filter = Builders<Worksheet>.Filter.Empty;
        return _context.Worksheets.Find(filter).ToList();
    }
    
    private Worksheet? GetWorksheet(Guid id)
    {
        var filter = Builders<Worksheet>.Filter.Eq(w => w.Id, id);
        return _context.Worksheets.Find(filter).FirstOrDefault();
    }
    
    private EntityContainer? GetEntityContainer(Guid id)
    {
        var filter = Builders<EntityContainer>.Filter.Eq(w => w.Id, id);
        return _context.EntityContainers.Find(filter).FirstOrDefault();
    }
}