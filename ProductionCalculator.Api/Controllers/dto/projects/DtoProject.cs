using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.project;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.entityContainers;
using SiteReact.Controllers.dto.worksheets;

namespace SiteReact.Controllers.dto.projects;

public class DtoProject
{
    public Guid Id { get; }
    public string Name { get; }
    public IEnumerable<DtoWorksheet> Worksheets { get; }
    public DtoEntityContainer EntityContainer { get; }

    public DtoProject(Project project, EntityContainer entityContainer, IEnumerable<Worksheet> worksheets)
    {
        Id = project.Id;
        Name = project.Name;
        Worksheets = worksheets.Select(w => new DtoWorksheet(w));
        EntityContainer = new DtoEntityContainer(entityContainer);
    }
}
