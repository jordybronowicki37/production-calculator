namespace ProductionCalculator.Api.Controllers.dto;

public class RecipeCreateDto
{
    public string Name { get; set; } = "";

    public IEnumerable<Guid> Machines { get; set; } = null!;

    public IEnumerable<ThroughPutDto> InputThroughPuts { get; set; } = null!;
    public IEnumerable<ThroughPutDto> OutputThroughPuts { get; set; } = null!;

    public RecipeCreateDto() {}
}