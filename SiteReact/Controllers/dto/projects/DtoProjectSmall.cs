using productionCalculatorLib.components.project;

namespace SiteReact.Controllers.dto.projects;

public class DtoProjectSmall
{
    public Guid Id { get; }
    public string Name { get; }
    public int AmountWorksheets { get; }
    public Guid EntityContainerId { get; }

    public DtoProjectSmall(Project project)
    {
        Id = project.Id;
        Name = project.Name;
        AmountWorksheets = project.Worksheets.Count;
        EntityContainerId = project.EntityContainerId;
    }
}