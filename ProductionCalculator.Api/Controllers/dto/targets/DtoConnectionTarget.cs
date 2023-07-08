﻿using productionCalculatorLib.components.targets;

namespace SiteReact.Controllers.dto.targets;

public class DtoConnectionTarget
{
    public float Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    
    public DtoConnectionTarget() {}
    
    public DtoConnectionTarget(TargetConnection target)
    {
        Amount = target.Amount;
        Type = target.Type.ToString();
    }
}