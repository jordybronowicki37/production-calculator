using productionCalculatorLib.components.worksheet;

namespace SiteReact.Data.GameDataPresets;

public static class SatisfactoryFicsMasData
{
    // ReSharper disable InconsistentNaming
    public static void addData(Worksheet worksheet)
    {
        SatisfactoryData.addData(worksheet);
        var e = worksheet.EntityContainer;
        
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
        
        // Smelter
        e.GetRecipeBuilder("Blue FICSMAS Ornament")
            .AddInput(PFicGif, 5)
            .AddOutput(PBluFicOrn, 10).Build();
        e.GetRecipeBuilder("Red FICSMAS Ornament")
            .AddInput(PFicGif, 5)
            .AddOutput(PRedFicOrn, 5).Build();
        
        // Foundry
        e.GetRecipeBuilder("Copper FICSMAS Ornament")
            .AddInput(PRedFicOrn, 10)
            .AddInput(PCopIng, 10)
            .AddOutput(PCopFicOrn, 5).Build();
        e.GetRecipeBuilder("Iron FICSMAS Ornament")
            .AddInput(PBluFicOrn, 15)
            .AddInput(PIroIng, 15)
            .AddOutput(PIroFicOrn, 5).Build();
        
        // Constructor
        e.GetRecipeBuilder("Actual Snow")
            .AddInput(PFicGif, 25)
            .AddOutput(PActSno, 10).Build();
        e.GetRecipeBuilder("Candy Cane")
            .AddInput(PFicGif, 15)
            .AddOutput(PCanCan, 5).Build();
        e.GetRecipeBuilder("FICSMAS Bow")
            .AddInput(PFicGif, 10)
            .AddOutput(PFicBow, 5).Build();
        e.GetRecipeBuilder("FICSMAS Tree Branch")
            .AddInput(PFicGif, 10)
            .AddOutput(PFicTreBra, 10).Build();
        e.GetRecipeBuilder("Snowball")
            .AddInput(PActSno, 15)
            .AddOutput(PSno, 5).Build();
        
        // Assembler
        e.GetRecipeBuilder("Fancy Fireworks")
            .AddInput(PFicTreBra, 10)
            .AddInput(PFicBow, 7.5f)
            .AddOutput(PFanFir, 2.5f).Build();
        e.GetRecipeBuilder("FICSMAS Decoration")
            .AddInput(PFicTreBra, 15)
            .AddInput(PFicOrnBun, 6)
            .AddOutput(PFicDec, 2).Build();
        e.GetRecipeBuilder("FICSMAS Ornament Bundle")
            .AddInput(PCopFicOrn, 5)
            .AddInput(PIroFicOrn, 5)
            .AddOutput(PFicOrnBun, 5).Build();
        e.GetRecipeBuilder("FICSMAS Wonder Star")
            .AddInput(PFicDec, 5)
            .AddInput(PCanCan, 20)
            .AddOutput(PFicWonSta, 1).Build();
        e.GetRecipeBuilder("Sparkly Fireworks")
            .AddInput(PFicTreBra, 7.5f)
            .AddInput(PActSno, 5)
            .AddOutput(PSpaFir, 2.5f).Build();
        e.GetRecipeBuilder("Sweet Fireworks")
            .AddInput(PFicTreBra, 15)
            .AddInput(PCanCan, 7.5f)
            .AddOutput(PSweFir, 2.5f).Build();
        
    }
}