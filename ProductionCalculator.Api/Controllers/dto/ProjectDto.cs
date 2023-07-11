using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.project;
using productionCalculatorLib.components.worksheet;

namespace SiteReact.Controllers.dto;

public class ProjectDto
{
    public Guid Id { get; }
    public string Name { get; }
    public IEnumerable<WorksheetDto> Worksheets { get; }
    public EntityContainerDto EntityContainer { get; }

    public ProjectDto(Project project, EntityContainer entityContainer, IEnumerable<Worksheet> worksheets)
    {
        Id = project.Id;
        Name = project.Name;
        Worksheets = worksheets.Select(w => new WorksheetDto(w));
        EntityContainer = new EntityContainerDto(entityContainer);
    }
}
