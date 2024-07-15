using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductionCalculator.Api.Controllers.dto;
using ProductionCalculator.Api.Data.DbContexts;
using ProductionCalculator.Api.Data.GameDataPresets;
using ProductionCalculator.Core.components.entityContainer;
using ProductionCalculator.Core.components.project;
using ProductionCalculator.Core.components.worksheet;

namespace ProductionCalculator.Api.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("project")]
public class ProjectController : ControllerBase
{
    private readonly ILogger<ProjectController> _logger;
    private readonly DocumentContext _context;
    
    public ProjectController(
        ILogger<ProjectController> logger, 
        DocumentContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet("")]
    public IActionResult GetAll()
    {
        return Ok(GetAllProjects().Select(p =>
        {
            var ec = GetEntityContainer(p.EntityContainerId);
            var worksheets = GetWorksheets(p.Worksheets);
            return new ProjectDto(p, ec, worksheets);
        }));
    }
    
    [HttpGet("{id:Guid}")]
    public IActionResult Get(Guid id)
    {
        var p = GetProject(id);
        if (p == null) return NotFound("Project is not found");
        
        var e = GetEntityContainer(p.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");

        var ws = GetWorksheets(p.Worksheets);
        
        return Ok(new ProjectDto(p, e, ws));
    }
    
    [HttpPost("")]
    public IActionResult CreateNew(ProjectCreateDto projectCreateDto)
    {
        var e = new EntityContainer();
        var p = new Project(projectCreateDto.Name, e.Id);

        switch (projectCreateDto.DataPreset)
        {
            case "":
            case "none":
                break;
            case "dysonSphereProgram":
                DspData.AddData(e);
                break;
            case "satisfactoryEarlyAccess":
                SatisfactoryData.AddData(e);
                break;
            case "satisfactoryExperimental":
                SatisfactoryExperimentalData.AddData(e);
                break;
            case "satisfactoryFICSMAS":
                SatisfactoryFicsMasData.AddData(e);
                break;
            default:
                return NotFound("Data preset is not found");
        }
        
        _context.EntityContainers.InsertOne(e);
        _context.Projects.InsertOne(p);
        
        return Ok(new ProjectDto(p, e, new List<Worksheet>()));
    }
    
    [HttpPost("{id:Guid}")]
    public IActionResult CreateNewWorksheet(Guid id, WorksheetCreateDto worksheetCreateDto)
    {
        var p = GetProject(id);
        if (p == null) return NotFound("Project is not found");

        var w = p.CreateWorksheet(worksheetCreateDto.Name);
        
        _context.Worksheets.InsertOne(w);
        
        var filter = Builders<Project>.Filter.Eq(f => f.Id, p.Id);
        _context.Projects.ReplaceOne(filter, p);
        
        return Ok(new WorksheetDto(w));
    }
    
    private IEnumerable<Project> GetAllProjects()
    {
        var filter = Builders<Project>.Filter.Empty;
        return _context.Projects.Find(filter).ToList();
    }
    
    private Project? GetProject(Guid id)
    {
        var filter = Builders<Project>.Filter.Eq(w => w.Id, id);
        return _context.Projects.Find(filter).FirstOrDefault();
    }
    
    private IEnumerable<Worksheet> GetWorksheets(IEnumerable<Guid> worksheetIds)
    {
        var filter = Builders<Worksheet>.Filter.In(w => w.Id, worksheetIds);
        return _context.Worksheets.Find(filter).ToList();
    }
    
    private EntityContainer? GetEntityContainer(Guid id)
    {
        var filter = Builders<EntityContainer>.Filter.Eq(w => w.Id, id);
        return _context.EntityContainers.Find(filter).FirstOrDefault();
    }
}