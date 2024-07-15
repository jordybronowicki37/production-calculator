using ProductionCalculator.Core.components.worksheet;

namespace ProductionCalculator.Core.components.project;

public class Project
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public ICollection<Guid> Worksheets { get; private set; } = new List<Guid>();
    public Guid EntityContainerId { get; init; }

    protected Project() {}

    public Project(string name, Guid entityContainerId)
    {
        Name = name;
        EntityContainerId = entityContainerId;
    }

    public Worksheet CreateWorksheet(string name)
    {
        var w = new Worksheet(name, EntityContainerId);
        Worksheets.Add(w.Id);
        return w;
    }
}