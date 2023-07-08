namespace productionCalculatorLib.components.entities;

public class Machine
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public ICollection<Guid> Recipes { get; private set; } = new HashSet<Guid>();
}
