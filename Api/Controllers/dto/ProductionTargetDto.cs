﻿using ProductionCalculator.Core.components.targets;

namespace ProductionCalculator.Api.Controllers.dto;

public class ProductionTargetDto
{
    public float Amount { get; set; }
    public string Type { get; set; } = string.Empty;

    public ProductionTargetDto() {}

    public ProductionTargetDto(TargetProduction target)
    {
        Amount = target.Amount;
        Type = target.Type.ToString();
    }
}