using productionCalculatorLib.components.worksheet;

namespace SiteReact.Data;

public static class SatisfactoryFicsMasData
{
    // ReSharper disable InconsistentNaming
    public static void addData(Worksheet worksheet)
    {
        SatisfactoryData.addData(worksheet);
        // Tier 0
        var PCopIng = worksheet.GetOrGenerateProduct("Copper Ingot");
        var PIroIng = worksheet.GetOrGenerateProduct("Iron Ingot");

        // Fics Mas
        var PFicGif = worksheet.GetOrGenerateProduct("FICSMAS Gift");
        var PBluFicOrn = worksheet.GetOrGenerateProduct("Blue FICSMAS Ornament");
        var PRedFicOrn = worksheet.GetOrGenerateProduct("Red FICSMAS Ornament");
        var PCopFicOrn = worksheet.GetOrGenerateProduct("Copper FICSMAS Ornament");
        var PIroFicOrn = worksheet.GetOrGenerateProduct("Iron FICSMAS Ornament");
        var PActSno = worksheet.GetOrGenerateProduct("Actual Snow");
        var PCanCan = worksheet.GetOrGenerateProduct("Candy Cane");
        var PFicBow = worksheet.GetOrGenerateProduct("FICSMAS Bow");
        var PFicTreBra = worksheet.GetOrGenerateProduct("FICSMAS Tree Branch");
        var PSno = worksheet.GetOrGenerateProduct("Snowball");
        var PFanFir = worksheet.GetOrGenerateProduct("Fancy Fireworks");
        var PFicDec = worksheet.GetOrGenerateProduct("FICSMAS Decoration");
        var PFicOrnBun = worksheet.GetOrGenerateProduct("FICSMAS Ornament Bundle");
        var PFicWonSta = worksheet.GetOrGenerateProduct("FICSMAS Wonder Star");
        var PSpaFir = worksheet.GetOrGenerateProduct("Sparkly Fireworks");
        var PSweFir = worksheet.GetOrGenerateProduct("Sweet Fireworks");
        
        // Smelter
        worksheet.GetRecipeBuilder("Blue FICSMAS Ornament")
            .AddInput(PFicGif, 5)
            .AddOutput(PBluFicOrn, 10).Build();
        worksheet.GetRecipeBuilder("Red FICSMAS Ornament")
            .AddInput(PFicGif, 5)
            .AddOutput(PRedFicOrn, 5).Build();
        
        // Foundry
        worksheet.GetRecipeBuilder("Copper FICSMAS Ornament")
            .AddInput(PRedFicOrn, 10)
            .AddInput(PCopIng, 10)
            .AddOutput(PCopFicOrn, 5).Build();
        worksheet.GetRecipeBuilder("Iron FICSMAS Ornament")
            .AddInput(PBluFicOrn, 15)
            .AddInput(PIroIng, 15)
            .AddOutput(PIroFicOrn, 5).Build();
        
        // Constructor
        worksheet.GetRecipeBuilder("Actual Snow")
            .AddInput(PFicGif, 25)
            .AddOutput(PActSno, 10).Build();
        worksheet.GetRecipeBuilder("Candy Cane")
            .AddInput(PFicGif, 15)
            .AddOutput(PCanCan, 5).Build();
        worksheet.GetRecipeBuilder("FICSMAS Bow")
            .AddInput(PFicGif, 10)
            .AddOutput(PFicBow, 5).Build();
        worksheet.GetRecipeBuilder("FICSMAS Tree Branch")
            .AddInput(PFicGif, 10)
            .AddOutput(PFicTreBra, 10).Build();
        worksheet.GetRecipeBuilder("Snowball")
            .AddInput(PActSno, 15)
            .AddOutput(PSno, 5).Build();
        
        // Assembler
        worksheet.GetRecipeBuilder("Fancy Fireworks")
            .AddInput(PFicTreBra, 10)
            .AddInput(PFicBow, 7.5f)
            .AddOutput(PFanFir, 2.5f).Build();
        worksheet.GetRecipeBuilder("FICSMAS Decoration")
            .AddInput(PFicTreBra, 15)
            .AddInput(PFicOrnBun, 6)
            .AddOutput(PFicDec, 2).Build();
        worksheet.GetRecipeBuilder("FICSMAS Ornament Bundle")
            .AddInput(PCopFicOrn, 5)
            .AddInput(PIroFicOrn, 5)
            .AddOutput(PFicOrnBun, 5).Build();
        worksheet.GetRecipeBuilder("FICSMAS Wonder Star")
            .AddInput(PFicDec, 5)
            .AddInput(PCanCan, 20)
            .AddOutput(PFicWonSta, 1).Build();
        worksheet.GetRecipeBuilder("Sparkly Fireworks")
            .AddInput(PFicTreBra, 7.5f)
            .AddInput(PActSno, 5)
            .AddOutput(PSpaFir, 2.5f).Build();
        worksheet.GetRecipeBuilder("Sweet Fireworks")
            .AddInput(PFicTreBra, 15)
            .AddInput(PCanCan, 7.5f)
            .AddOutput(PSweFir, 2.5f).Build();
        
    }
}