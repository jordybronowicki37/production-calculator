using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.project;
using SiteReact.Controllers.dto.projects;
using SiteReact.Controllers.dto.worksheets;
using SiteReact.Data.DbContexts;
using SiteReact.Data.GameDataPresets;

namespace SiteReact.Controllers;

[ApiController]
[Route("[controller]")]
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
        return Ok(GetAllProjects().Select(p => new DtoProject(p)));
    }
    
    [HttpGet("{id:Guid}")]
    public IActionResult Get(Guid id)
    {
        var p = GetProject(id);
        if (p == null) return NotFound("Project is not found");
        return Ok(new DtoProject(p));
    }
    
    [HttpPost("")]
    public IActionResult CreateNew(DtoProjectCreate dto)
    {
        var e = new EntityContainer();
        var p = new Project(dto.Name, e.Id);

        switch (dto.DataPreset)
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
        
        return Ok(new DtoProject(p));
    }
    
    [HttpPost("{id:Guid}")]
    public IActionResult CreateNewWorksheet(Guid id, DtoWorksheetCreate dto)
    {
        var p = GetProject(id);
        if (p == null) return NotFound("Project is not found");

        var w = p.CreateWorksheet(dto.Name);
        
        _context.Worksheets.InsertOne(w);
        
        var filter = Builders<Project>.Filter.Eq(f => f.Id, p.Id);
        _context.Projects.ReplaceOne(filter, p);
        
        return Ok(new DtoWorksheet(w));
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
    
    private EntityContainer? GetEntityContainer(Guid id)
    {
        var filter = Builders<EntityContainer>.Filter.Eq(w => w.Id, id);
        return _context.EntityContainers.Find(filter).FirstOrDefault();
    }
}