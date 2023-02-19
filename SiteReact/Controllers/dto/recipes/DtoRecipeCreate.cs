﻿using SiteReact.Controllers.dto.throughputs;

namespace SiteReact.Controllers.dto.recipes;

public class DtoRecipeCreate
{
    public string Name { get; set; } = "";

    public IEnumerable<DtoThroughPut> InputThroughPuts { get; set; } = null!;
    public IEnumerable<DtoThroughPut> OutputThroughPuts { get; set; } = null!;

    public DtoRecipeCreate() {}
}