using ProductionCalculator.Core.components.entityContainer;

namespace ProductionCalculator.Api.Data.GameDataPresets;

public static class SatisfactoryFicsMasData
{
    public static void AddData(EntityContainer e)
    {
        SatisfactoryData.AddData(e);
        e.Name = "Satisfactory FICSMAS";
        
        // Tier 0
        var PCopIng = e.GetOrGenerateProduct("Copper Ingot");
        var PIroIng = e.GetOrGenerateProduct("Iron Ingot");

        // Fics Mas
        var PFicGif = e.GetOrGenerateProduct("FICSMAS Gift");
        var PBluFicOrn = e.GetOrGenerateProduct("Blue FICSMAS Ornament");
        var PRedFicOrn = e.GetOrGenerateProduct("Red FICSMAS Ornament");
        var PCopFicOrn = e.GetOrGenerateProduct("Copper FICSMAS Ornament");
        var PIroFicOrn = e.GetOrGenerateProduct("Iron FICSMAS Ornament");
        var PActSno = e.GetOrGenerateProduct("Actual Snow");
        var PCanCan = e.GetOrGenerateProduct("Candy Cane");
        var PFicBow = e.GetOrGenerateProduct("FICSMAS Bow");
        var PFicTreBra = e.GetOrGenerateProduct("FICSMAS Tree Branch");
        var PSno = e.GetOrGenerateProduct("Snowball");
        var PFanFir = e.GetOrGenerateProduct("Fancy Fireworks");
        var PFicDec = e.GetOrGenerateProduct("FICSMAS Decoration");
        var PFicOrnBun = e.GetOrGenerateProduct("FICSMAS Ornament Bundle");
        var PFicWonSta = e.GetOrGenerateProduct("FICSMAS Wonder Star");
        var PSpaFir = e.GetOrGenerateProduct("Sparkly Fireworks");
        var PSweFir = e.GetOrGenerateProduct("Sweet Fireworks");
        
        // Machines
        var MSme = e.GetMachine("Smelter")!;
        var MFou = e.GetMachine("Foundry")!;
        var MCon = e.GetMachine("Constructor")!;
        var MAss = e.GetMachine("Assembler")!;
        
        // Smelter
        e.GetRecipeBuilder("Blue FICSMAS Ornament", MSme)
            .AddInput(PFicGif, 5)
            .AddOutput(PBluFicOrn, 10).Build();
        e.GetRecipeBuilder("Red FICSMAS Ornament", MSme)
            .AddInput(PFicGif, 5)
            .AddOutput(PRedFicOrn, 5).Build();
        
        // Foundry
        e.GetRecipeBuilder("Copper FICSMAS Ornament", MFou)
            .AddInput(PRedFicOrn, 10)
            .AddInput(PCopIng, 10)
            .AddOutput(PCopFicOrn, 5).Build();
        e.GetRecipeBuilder("Iron FICSMAS Ornament", MFou)
            .AddInput(PBluFicOrn, 15)
            .AddInput(PIroIng, 15)
            .AddOutput(PIroFicOrn, 5).Build();
        
        // Constructor
        e.GetRecipeBuilder("Actual Snow", MCon)
            .AddInput(PFicGif, 25)
            .AddOutput(PActSno, 10).Build();
        e.GetRecipeBuilder("Candy Cane", MCon)
            .AddInput(PFicGif, 15)
            .AddOutput(PCanCan, 5).Build();
        e.GetRecipeBuilder("FICSMAS Bow", MCon)
            .AddInput(PFicGif, 10)
            .AddOutput(PFicBow, 5).Build();
        e.GetRecipeBuilder("FICSMAS Tree Branch", MCon)
            .AddInput(PFicGif, 10)
            .AddOutput(PFicTreBra, 10).Build();
        e.GetRecipeBuilder("Snowball", MCon)
            .AddInput(PActSno, 15)
            .AddOutput(PSno, 5).Build();
        
        // Assembler
        e.GetRecipeBuilder("Fancy Fireworks", MAss)
            .AddInput(PFicTreBra, 10)
            .AddInput(PFicBow, 7.5f)
            .AddOutput(PFanFir, 2.5f).Build();
        e.GetRecipeBuilder("FICSMAS Decoration", MAss)
            .AddInput(PFicTreBra, 15)
            .AddInput(PFicOrnBun, 6)
            .AddOutput(PFicDec, 2).Build();
        e.GetRecipeBuilder("FICSMAS Ornament Bundle", MAss)
            .AddInput(PCopFicOrn, 5)
            .AddInput(PIroFicOrn, 5)
            .AddOutput(PFicOrnBun, 5).Build();
        e.GetRecipeBuilder("FICSMAS Wonder Star", MAss)
            .AddInput(PFicDec, 5)
            .AddInput(PCanCan, 20)
            .AddOutput(PFicWonSta, 1).Build();
        e.GetRecipeBuilder("Sparkly Fireworks", MAss)
            .AddInput(PFicTreBra, 7.5f)
            .AddInput(PActSno, 5)
            .AddOutput(PSpaFir, 2.5f).Build();
        e.GetRecipeBuilder("Sweet Fireworks", MAss)
            .AddInput(PFicTreBra, 15)
            .AddInput(PCanCan, 7.5f)
            .AddOutput(PSweFir, 2.5f).Build();
        
    }
}