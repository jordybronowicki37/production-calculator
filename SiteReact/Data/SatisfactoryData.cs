using productionCalculatorLib.components.worksheet;

namespace SiteReact.Data;

public static class SatisfactoryData
{
    // ReSharper disable InconsistentNaming
    public static void addData(Worksheet worksheet)
    {
        // Ores
        var PBau = worksheet.GetOrGenerateProduct("Bauxite");
        var PCatOre = worksheet.GetOrGenerateProduct("Caterium Ore");
        var PCoa = worksheet.GetOrGenerateProduct("Coal");
        var PCopOre = worksheet.GetOrGenerateProduct("Copper Ore");
        var PCruOil = worksheet.GetOrGenerateProduct("Crude Oil");
        var PPacCruOil = worksheet.GetOrGenerateProduct("Packaged Crude Oil");
        var PIroOre = worksheet.GetOrGenerateProduct("Iron Ore");
        var PLim = worksheet.GetOrGenerateProduct("Limestone");
        var PNitGas = worksheet.GetOrGenerateProduct("Nitrogen Gas");
        var PPacNitGas = worksheet.GetOrGenerateProduct("Packaged Nitrogen Gas");
        var PRawQua = worksheet.GetOrGenerateProduct("Raw Quartz");
        var PSamOre = worksheet.GetOrGenerateProduct("SAM Ore");
        var PSul = worksheet.GetOrGenerateProduct("Sulfur");
        var PUra = worksheet.GetOrGenerateProduct("Uranium");
        var PWat = worksheet.GetOrGenerateProduct("Water");
        var PPacWat = worksheet.GetOrGenerateProduct("Packaged Water");
        
        // Flora & Fauna
        var PAliCar = worksheet.GetOrGenerateProduct("Alien Carapace");
        var PAliOrg = worksheet.GetOrGenerateProduct("Alien Organs");
        var PFloPet = worksheet.GetOrGenerateProduct("Flower Petals");
        var PLea = worksheet.GetOrGenerateProduct("Leaves");
        var PMyc = worksheet.GetOrGenerateProduct("Mycelia");
        var PBluPowSlu = worksheet.GetOrGenerateProduct("Blue Power Slug");
        var PYelPowSlu = worksheet.GetOrGenerateProduct("Yellow Power Slug");
        var PPurPowSlu = worksheet.GetOrGenerateProduct("Purple Power Slug");
        var PWoo = worksheet.GetOrGenerateProduct("Wood");

        // Tier 0
        var PBio = worksheet.GetOrGenerateProduct("Biomass");
        var PCab = worksheet.GetOrGenerateProduct("Cable");
        var PCon = worksheet.GetOrGenerateProduct("Concrete");
        var PCopIng = worksheet.GetOrGenerateProduct("Copper Ingot");
        var PIroIng = worksheet.GetOrGenerateProduct("Iron Ingot");
        var PIroPla = worksheet.GetOrGenerateProduct("Iron Plate");
        var PIroRod = worksheet.GetOrGenerateProduct("Iron Rod");
        var PReiIroPla = worksheet.GetOrGenerateProduct("Reinforced Iron Plate");
        var PScr = worksheet.GetOrGenerateProduct("Screw");
        var PWir = worksheet.GetOrGenerateProduct("Wire");
        
        // Tier 2
        var PColCar = worksheet.GetOrGenerateProduct("Color Cartridge");
        var PCopShe = worksheet.GetOrGenerateProduct("Copper Sheet");
        var PSolBio = worksheet.GetOrGenerateProduct("Solid Biofuel");
        var PModFra = worksheet.GetOrGenerateProduct("Modular Frame");
        var PRot = worksheet.GetOrGenerateProduct("Rotor");
        var PSmaPla = worksheet.GetOrGenerateProduct("Smart Plating");
        
        // Tier 3
        var PSteBea = worksheet.GetOrGenerateProduct("Steel Beam");
        var PSteIng = worksheet.GetOrGenerateProduct("Steel Ingot");
        var PStePip = worksheet.GetOrGenerateProduct("Steel Pipe");
        var PVerFra = worksheet.GetOrGenerateProduct("Versatile Framework");
        
        // Tier 4
        var PAutWir = worksheet.GetOrGenerateProduct("Automated Wiring");
        var PEncIndBea = worksheet.GetOrGenerateProduct("Encased Industrial Beam");
        var PHeaModFra = worksheet.GetOrGenerateProduct("Heavy Modular Frame");
        var PMot = worksheet.GetOrGenerateProduct("Motor");
        var PSta = worksheet.GetOrGenerateProduct("Stator");
        
        // Tier 5
        var PAdaConUni = worksheet.GetOrGenerateProduct("Adaptive Control Unit");
        var PCirBoa = worksheet.GetOrGenerateProduct("Circuit Board");
        var PCom = worksheet.GetOrGenerateProduct("Computer");
        var PEmpCan = worksheet.GetOrGenerateProduct("Empty Canister");
        var PFue = worksheet.GetOrGenerateProduct("Fuel");
        var PPacFue = worksheet.GetOrGenerateProduct("Packaged Fuel");
        var PHeaOilRes = worksheet.GetOrGenerateProduct("Heavy Oil Residue");
        var PPacHeaOilRes = worksheet.GetOrGenerateProduct("Packaged Heavy Oil Residue");
        var PLiqBio = worksheet.GetOrGenerateProduct("Liquid Biofuel");
        var PPacLiqBio = worksheet.GetOrGenerateProduct("Packaged Liquid Biofuel");
        var PModEng = worksheet.GetOrGenerateProduct("Modular Engine");
        var PPetCok = worksheet.GetOrGenerateProduct("Petroleum Coke");
        var PPla = worksheet.GetOrGenerateProduct("Plastic");
        var PPolRes = worksheet.GetOrGenerateProduct("Polymer Resin");
        var PRub = worksheet.GetOrGenerateProduct("Rubber");
        
        // Tier 7
        var PAluSol = worksheet.GetOrGenerateProduct("Alumina Solution");
        var PPacAluSol = worksheet.GetOrGenerateProduct("Packaged Alumina Solution");
        var PAluCas = worksheet.GetOrGenerateProduct("Aluminum Casing");
        var PAlcAluShe = worksheet.GetOrGenerateProduct("Alclad Aluminum Sheet");
        var PAluIng = worksheet.GetOrGenerateProduct("Aluminum Ingot");
        var PAluScr = worksheet.GetOrGenerateProduct("Aluminum Scrap");
        var PAssDirSys = worksheet.GetOrGenerateProduct("Assembly Director System");
        var PBat = worksheet.GetOrGenerateProduct("Battery");
        var PRadConUni = worksheet.GetOrGenerateProduct("Radio Control Unit");
        var PSulAci = worksheet.GetOrGenerateProduct("Sulfuric Acid");
        var PPacSulAci = worksheet.GetOrGenerateProduct("Packaged Sulfuric Acid");
        
        // Tier 8
        var PCooSys = worksheet.GetOrGenerateProduct("Cooling System");
        var PCopPow = worksheet.GetOrGenerateProduct("Copper Powder");
        var PEleConRod = worksheet.GetOrGenerateProduct("Electromagnetic Control Rod");
        var PEmpFluTan = worksheet.GetOrGenerateProduct("Empty Fluid Tank");
        var PEncPluCel = worksheet.GetOrGenerateProduct("Encased Plutonium Cell");
        var PEncUraCel = worksheet.GetOrGenerateProduct("Encased Uranium Cell");
        var PFusModFra = worksheet.GetOrGenerateProduct("Fused Modular Frame");
        var PHeaSin = worksheet.GetOrGenerateProduct("Heat Sink");
        var PMagFieGen = worksheet.GetOrGenerateProduct("Magnetic Field Generator");
        var PNitAci = worksheet.GetOrGenerateProduct("Nitric Acid");
        var PPacNitAci = worksheet.GetOrGenerateProduct("Packaged Nitric Acid");
        var PNonUra = worksheet.GetOrGenerateProduct("Non-fissle Uranium");
        var PNucPas = worksheet.GetOrGenerateProduct("Nuclear Pasta");
        var PPluFueRod = worksheet.GetOrGenerateProduct("Plutonium Fuel Rod");
        var PPluPel = worksheet.GetOrGenerateProduct("Plutonium Pellet");
        var PPluWas = worksheet.GetOrGenerateProduct("Plutonium Waste");
        var PPreConCub = worksheet.GetOrGenerateProduct("Pressure Conversion Cube");
        var PTheProRoc = worksheet.GetOrGenerateProduct("Thermal Propulsion Rocket");
        var PTurMot = worksheet.GetOrGenerateProduct("Turbo Motor");
        var PUraFueRod = worksheet.GetOrGenerateProduct("Uranium Fuel Rod");
        var PUraWas = worksheet.GetOrGenerateProduct("Uranium Waste");
        
        // MAM
        var PAiLim = worksheet.GetOrGenerateProduct("AI Limiter");
        var PBlaPow = worksheet.GetOrGenerateProduct("Black Powder");
        var PCatIng = worksheet.GetOrGenerateProduct("Caterium Ingot");
        var PComCoa = worksheet.GetOrGenerateProduct("Compacted Coal");
        var PCryOsc = worksheet.GetOrGenerateProduct("Crystal Oscillator");
        var PFab = worksheet.GetOrGenerateProduct("Fabric");
        var PHigSpeCon = worksheet.GetOrGenerateProduct("High-Speed Connector");
        var PQui = worksheet.GetOrGenerateProduct("Quickwire");
        var PPowSha = worksheet.GetOrGenerateProduct("Power Shard");
        var PQuaCry = worksheet.GetOrGenerateProduct("Quartz Crystal");
        var PSil = worksheet.GetOrGenerateProduct("Silica");
        var PSup = worksheet.GetOrGenerateProduct("Supercomputer");
        var PTur = worksheet.GetOrGenerateProduct("Turbofuel");
        var PPacTur = worksheet.GetOrGenerateProduct("Packaged Turbofuel");
        
        // Equipment
        var PBea = worksheet.GetOrGenerateProduct("Beacon");
        var PGasFil = worksheet.GetOrGenerateProduct("Gas Filter");
        var PIodInfFil = worksheet.GetOrGenerateProduct("Iodine Infused Filter");
        var PNob = worksheet.GetOrGenerateProduct("Nobelisk");
        var PSpiReb = worksheet.GetOrGenerateProduct("Spiked Rebar");
        var PRifCar = worksheet.GetOrGenerateProduct("Rifle Cartridge");
        
        // Smelter Recipes
        worksheet.GetRecipeBuilder("Caterium Ingot")
            .AddInput(PCatOre, 45)
            .AddOutput(PCatIng, 15).Build();
        worksheet.GetRecipeBuilder("Copper Ingot")
            .AddInput(PCopOre, 30)
            .AddOutput(PCopIng, 30).Build();
        worksheet.GetRecipeBuilder("Iron Ingot")
            .AddInput(PIroOre, 30)
            .AddOutput(PIroIng, 30).Build();
        worksheet.GetRecipeBuilder("Pure Aluminum Ingot")
            .AddInput(PAluScr, 60)
            .AddOutput(PAluIng, 30).Build();
        
        // Foundry Recipes
        worksheet.GetRecipeBuilder("Aluminum Ingot")
            .AddInput(PAluScr, 90)
            .AddInput(PSil, 75)
            .AddOutput(PAluIng, 60).Build();
        worksheet.GetRecipeBuilder("Coke Steel Ingot")
            .AddInput(PIroOre, 75)
            .AddInput(PPetCok, 75)
            .AddOutput(PSteIng, 100).Build();
        worksheet.GetRecipeBuilder("Compacted Steel Ingot")
            .AddInput(PIroOre, 22.5f)
            .AddInput(PComCoa, 11.25f)
            .AddOutput(PSteIng, 37.5f).Build();
        worksheet.GetRecipeBuilder("Copper Alloy Ingot")
            .AddInput(PCopOre, 50)
            .AddInput(PIroOre, 25)
            .AddOutput(PCopIng, 100).Build();
        worksheet.GetRecipeBuilder("Iron Alloy Ingot")
            .AddInput(PIroOre, 20)
            .AddInput(PCopOre, 20)
            .AddOutput(PIroIng, 50).Build();
        worksheet.GetRecipeBuilder("Solid Steel Ingot")
            .AddInput(PIroIng, 40)
            .AddInput(PCoa, 40)
            .AddOutput(PSteIng, 60).Build();
        worksheet.GetRecipeBuilder("Steel Ingot")
            .AddInput(PIroOre, 45)
            .AddInput(PCoa, 45)
            .AddOutput(PSteIng, 45).Build();
        
        // Constructor Recipes
        worksheet.GetRecipeBuilder("AluminumCasing")
            .AddInput(PAluIng, 90)
            .AddOutput(PAluCas, 60).Build();
        worksheet.GetRecipeBuilder("BioCoal")
            .AddInput(PBio, 37.5f)
            .AddOutput(PCoa, 45).Build();
        worksheet.GetRecipeBuilder("Biomass (Alien Carapace)")
            .AddInput(PAliCar, 15)
            .AddOutput(PBio, 1500).Build();
        worksheet.GetRecipeBuilder("Biomass (Alien Organs)")
            .AddInput(PAliOrg, 7.5f)
            .AddOutput(PBio, 1500).Build();
        worksheet.GetRecipeBuilder("Biomass (Leaves)")
            .AddInput(PLea, 120)
            .AddOutput(PBio, 60).Build();
        worksheet.GetRecipeBuilder("Biomass (Mycelia)")
            .AddInput(PMyc, 150)
            .AddOutput(PBio, 150).Build();
        worksheet.GetRecipeBuilder("Biomass (Wood)")
            .AddInput(PWoo, 60)
            .AddOutput(PBio, 300).Build();
        worksheet.GetRecipeBuilder("Cable")
            .AddInput(PWir, 60)
            .AddOutput(PCab, 30).Build();
        worksheet.GetRecipeBuilder("Cast Screw")
            .AddInput(PIroIng, 12.5f)
            .AddOutput(PScr, 50).Build();
        worksheet.GetRecipeBuilder("Caterium Wire")
            .AddInput(PCatIng, 15)
            .AddOutput(PWir, 120).Build();
        worksheet.GetRecipeBuilder("Charcoal")
            .AddInput(PWoo, 15)
            .AddOutput(PCoa, 150).Build();
        worksheet.GetRecipeBuilder("Color Cartridge")
            .AddInput(PFloPet, 37.5f)
            .AddOutput(PColCar, 75).Build();
        worksheet.GetRecipeBuilder("Concrete")
            .AddInput(PLim, 45)
            .AddOutput(PCon, 15).Build();
        worksheet.GetRecipeBuilder("Copper Powder")
            .AddInput(PCopIng, 300)
            .AddOutput(PCopPow, 50).Build();
        worksheet.GetRecipeBuilder("Copper Sheet")
            .AddInput(PCopIng, 20)
            .AddOutput(PCopShe, 10).Build();
        worksheet.GetRecipeBuilder("Empty Canister")
            .AddInput(PPla, 30)
            .AddOutput(PEmpCan, 60).Build();
        worksheet.GetRecipeBuilder("Empty Fluid Tank")
            .AddInput(PAluIng, 60)
            .AddOutput(PEmpFluTan, 60).Build();
        worksheet.GetRecipeBuilder("Iron Plate")
            .AddInput(PIroIng, 30)
            .AddOutput(PIroPla, 20).Build();
        worksheet.GetRecipeBuilder("Iron Rod")
            .AddInput(PIroIng, 15)
            .AddOutput(PIroRod, 15).Build();
        worksheet.GetRecipeBuilder("Iron Wire")
            .AddInput(PIroIng, 12.5f)
            .AddOutput(PWir, 22.5f).Build();
        worksheet.GetRecipeBuilder("Power Shard Blue")
            .AddInput(PBluPowSlu, 7.5f)
            .AddOutput(PPowSha, 7.5f).Build();
        worksheet.GetRecipeBuilder("Power Shard Yellow")
            .AddInput(PYelPowSlu, 5)
            .AddOutput(PPowSha, 10).Build();
        worksheet.GetRecipeBuilder("Power Shard Purple")
            .AddInput(PPurPowSlu, 2.5f)
            .AddOutput(PPowSha, 12.5f).Build();
        worksheet.GetRecipeBuilder("Quartz Crystal")
            .AddInput(PRawQua, 37.5f)
            .AddOutput(PQuaCry, 22.5f).Build();
        worksheet.GetRecipeBuilder("Quickwire")
            .AddInput(PCatIng, 12)
            .AddOutput(PQui, 60).Build();
        worksheet.GetRecipeBuilder("Screw")
            .AddInput(PIroRod, 10)
            .AddOutput(PScr, 40).Build();
        worksheet.GetRecipeBuilder("Silica")
            .AddInput(PRawQua, 22.5f)
            .AddOutput(PSil, 37.5f).Build();
        worksheet.GetRecipeBuilder("Solid Biofuel")
            .AddInput(PBio, 120)
            .AddOutput(PSolBio, 60).Build();
        worksheet.GetRecipeBuilder("Iron Rebar")
            .AddInput(PIroRod, 15)
            .AddOutput(PSpiReb, 15).Build();
        worksheet.GetRecipeBuilder("Steel Beam")
            .AddInput(PSteIng, 60)
            .AddOutput(PSteBea, 15).Build();
        worksheet.GetRecipeBuilder("Steel Canister")
            .AddInput(PSteIng, 60)
            .AddOutput(PEmpCan, 40).Build();
        worksheet.GetRecipeBuilder("Steel Pipe")
            .AddInput(PSteIng, 30)
            .AddOutput(PStePip, 20).Build();
        worksheet.GetRecipeBuilder("Steel Rod")
            .AddInput(PSteIng, 12)
            .AddOutput(PIroRod, 48).Build();
        worksheet.GetRecipeBuilder("Steel Screw")
            .AddInput(PSteIng, 5)
            .AddOutput(PScr, 260).Build();
        worksheet.GetRecipeBuilder("Wire")
            .AddInput(PCopIng, 15)
            .AddOutput(PWir, 30).Build();
        
        // Assembler Recipes
        worksheet.GetRecipeBuilder("Adhered Iron Plate")
            .AddInput(PIroPla, 11.25f)
            .AddInput(PRub, 3.75f)
            .AddOutput(PReiIroPla, 3.8f).Build();
        worksheet.GetRecipeBuilder("AI Limiter")
            .AddInput(PCopShe, 25)
            .AddInput(PQui, 100)
            .AddOutput(PAiLim, 5).Build();
        worksheet.GetRecipeBuilder("Alclad Aluminum Sheet")
            .AddInput(PAluIng, 30)
            .AddInput(PCopIng, 10)
            .AddOutput(PAlcAluShe, 30).Build();
        worksheet.GetRecipeBuilder("Alclad Casting")
            .AddInput(PAluIng, 150)
            .AddInput(PCopIng, 75)
            .AddOutput(PAluCas, 112.5f).Build();
        worksheet.GetRecipeBuilder("Assembly Director System")
            .AddInput(PAdaConUni, 1.5f)
            .AddInput(PSup, 0.75f)
            .AddOutput(PAssDirSys, 0.8f).Build();
        worksheet.GetRecipeBuilder("Automated Wiring")
            .AddInput(PSta, 2.5f)
            .AddInput(PCab, 50)
            .AddOutput(PAutWir, 2.5f).Build();
        worksheet.GetRecipeBuilder("Black Powder")
            .AddInput(PCoa, 7.5f)
            .AddInput(PSul, 15)
            .AddOutput(PBlaPow, 7.5f).Build();
        worksheet.GetRecipeBuilder("Bolted Frame")
            .AddInput(PReiIroPla, 7.5f)
            .AddInput(PScr, 140)
            .AddOutput(PModFra, 5).Build();
        worksheet.GetRecipeBuilder("Bolted Iron Plate")
            .AddInput(PIroPla, 90)
            .AddInput(PScr, 250)
            .AddOutput(PReiIroPla, 15).Build();
        worksheet.GetRecipeBuilder("Caterium Circuit Board")
            .AddInput(PPla, 12.5f)
            .AddInput(PQui, 37.5f)
            .AddOutput(PCirBoa, 8.8f).Build();
        worksheet.GetRecipeBuilder("Cheap Silica")
            .AddInput(PRawQua, 11.25f)
            .AddInput(PLim, 18.75f)
            .AddOutput(PSil, 26.3f).Build();
        worksheet.GetRecipeBuilder("Circuit Board")
            .AddInput(PCopShe, 15)
            .AddInput(PPla, 30)
            .AddOutput(PCirBoa, 7.5f).Build();
        worksheet.GetRecipeBuilder("Coated Iron Canister")
            .AddInput(PIroPla, 30)
            .AddInput(PCopShe, 15)
            .AddOutput(PEmpCan, 60).Build();
        worksheet.GetRecipeBuilder("Coated Iron Plate")
            .AddInput(PIroIng, 50)
            .AddInput(PPla, 10)
            .AddOutput(PIroPla, 75).Build();
        worksheet.GetRecipeBuilder("Compacted Coal")
            .AddInput(PCoa, 25)
            .AddInput(PSul, 25)
            .AddOutput(PRot, 11.3f).Build();
        worksheet.GetRecipeBuilder("Crystal Computer")
            .AddInput(PCirBoa, 7.5f)
            .AddInput(PCryOsc, 2.8125f)
            .AddOutput(PCom, 2.8f).Build();
        worksheet.GetRecipeBuilder("Electric Motor")
            .AddInput(PEleConRod, 3.75f)
            .AddInput(PRot, 7.5f)
            .AddOutput(PMot, 7.5f).Build();
        worksheet.GetRecipeBuilder("Electrode Circuit Board")
            .AddInput(PRub, 30)
            .AddInput(PPetCok, 45)
            .AddOutput(PCirBoa, 5).Build();
        worksheet.GetRecipeBuilder("Electromagnetic Connection Rod")
            .AddInput(PSta, 8)
            .AddInput(PHigSpeCon, 4)
            .AddOutput(PEleConRod, 8).Build();
        worksheet.GetRecipeBuilder("Electromagnetic Control Rod")
            .AddInput(PSta, 6)
            .AddInput(PAiLim, 4)
            .AddOutput(PEleConRod, 4).Build();
        worksheet.GetRecipeBuilder("Encased Industrial Beam")
            .AddInput(PSteBea, 24)
            .AddInput(PCon, 30)
            .AddOutput(PEncIndBea, 6).Build();
        worksheet.GetRecipeBuilder("Encased Industrial Pipe")
            .AddInput(PStePip, 28)
            .AddInput(PCon, 20)
            .AddOutput(PEncIndBea, 4).Build();
        worksheet.GetRecipeBuilder("Encased Plutoniom Cell")
            .AddInput(PPluPel, 10)
            .AddInput(PCon, 20)
            .AddOutput(PEncPluCel, 5).Build();
        worksheet.GetRecipeBuilder("Fabric")
            .AddInput(PMyc, 15)
            .AddInput(PBio, 75)
            .AddOutput(PFab, 15).Build();
        worksheet.GetRecipeBuilder("Fine Black Powder")
            .AddInput(PSul, 7.5f)
            .AddInput(PComCoa, 3.75f)
            .AddOutput(PBlaPow, 15).Build();
        worksheet.GetRecipeBuilder("Fine Concrete")
            .AddInput(PSil, 7.5f)
            .AddInput(PLim, 30)
            .AddOutput(PCon, 25).Build();
        worksheet.GetRecipeBuilder("Fused Quickwire")
            .AddInput(PCatIng, 7.5f)
            .AddInput(PCopIng, 37.5f)
            .AddOutput(PQui, 90).Build();
        worksheet.GetRecipeBuilder("Fused Wire")
            .AddInput(PCopIng, 12)
            .AddInput(PCatIng, 3)
            .AddOutput(PWir, 90).Build();
        worksheet.GetRecipeBuilder("Heat Exchanger")
            .AddInput(PAluCas, 30)
            .AddInput(PRub, 30)
            .AddOutput(PHeaSin, 10).Build();
        worksheet.GetRecipeBuilder("Heat Sink")
            .AddInput(PAlcAluShe, 37.5f)
            .AddInput(PCopShe, 22.5f)
            .AddOutput(PHeaSin, 7.5f).Build();
        worksheet.GetRecipeBuilder("Insulated Cable")
            .AddInput(PWir, 45)
            .AddInput(PRub, 30)
            .AddOutput(PCab, 100).Build();
        worksheet.GetRecipeBuilder("Modular Frame")
            .AddInput(PReiIroPla, 3)
            .AddInput(PIroRod, 12)
            .AddOutput(PModFra, 2).Build();
        worksheet.GetRecipeBuilder("Motor")
            .AddInput(PRot, 10)
            .AddInput(PSta, 10)
            .AddOutput(PMot, 5).Build();
        worksheet.GetRecipeBuilder("Nobelisk")
            .AddInput(PBlaPow, 15)
            .AddInput(PStePip, 30)
            .AddOutput(PNob, 3).Build();
        worksheet.GetRecipeBuilder("OC Computer")
            .AddInput(PRadConUni, 9)
            .AddInput(PCooSys, 9)
            .AddOutput(PSup, 3).Build();
        worksheet.GetRecipeBuilder("Plutonium Fuel Unit")
            .AddInput(PEncPluCel, 10)
            .AddInput(PPreConCub, 0.5f)
            .AddOutput(PPluFueRod, 0.5f).Build();
        worksheet.GetRecipeBuilder("Pressure Conversion Cube")
            .AddInput(PFusModFra, 1)
            .AddInput(PRadConUni, 2)
            .AddOutput(PPreConCub, 1).Build();
        worksheet.GetRecipeBuilder("Quickwire Cable")
            .AddInput(PQui, 7.5f)
            .AddInput(PRub, 5)
            .AddOutput(PCab, 27.5f).Build();
        worksheet.GetRecipeBuilder("Quickwire Stator")
            .AddInput(PStePip, 16)
            .AddInput(PQui, 60)
            .AddOutput(PSta, 8).Build();
        worksheet.GetRecipeBuilder("Reinforced Iron Plate")
            .AddInput(PIroPla, 30)
            .AddInput(PScr, 60)
            .AddOutput(PReiIroPla, 5).Build();
        worksheet.GetRecipeBuilder("Rotor")
            .AddInput(PIroRod, 20)
            .AddInput(PScr, 100)
            .AddOutput(PRot, 4).Build();
        worksheet.GetRecipeBuilder("Rubber Concrete")
            .AddInput(PLim, 50)
            .AddInput(PRub, 10)
            .AddOutput(PCon, 45).Build();
        worksheet.GetRecipeBuilder("Silicon Circuit Board")
            .AddInput(PCopShe, 27.5f)
            .AddInput(PSil, 27.5f)
            .AddOutput(PCirBoa, 12.5f).Build();
        worksheet.GetRecipeBuilder("Smart Plating")
            .AddInput(PReiIroPla, 2)
            .AddInput(PRot, 2)
            .AddOutput(PSmaPla, 2).Build();
        worksheet.GetRecipeBuilder("Stator")
            .AddInput(PStePip, 15)
            .AddInput(PWir, 40)
            .AddOutput(PSta, 5).Build();
        worksheet.GetRecipeBuilder("Steel Coated Plate")
            .AddInput(PSteIng, 7.5f)
            .AddInput(PPla, 5)
            .AddOutput(PIroPla, 45).Build();
        worksheet.GetRecipeBuilder("Steel Rotor")
            .AddInput(PStePip, 10)
            .AddInput(PWir, 30)
            .AddOutput(PRot, 5).Build();
        worksheet.GetRecipeBuilder("Steeled Frame")
            .AddInput(PReiIroPla, 2)
            .AddInput(PStePip, 10)
            .AddOutput(PModFra, 3).Build();
        worksheet.GetRecipeBuilder("Stitched Iron Plate")
            .AddInput(PIroPla, 18.75f)
            .AddInput(PWir, 37.5f)
            .AddOutput(PReiIroPla, 5.6f).Build();
        worksheet.GetRecipeBuilder("Versitile Framework")
            .AddInput(PModFra, 2.5f)
            .AddInput(PSteBea, 30)
            .AddOutput(PVerFra, 5).Build();

        // Manufacturer Recipes
        worksheet.GetRecipeBuilder("Adaptive Control Unit")
            .AddInput(PAutWir, 7.5f)
            .AddInput(PCirBoa, 5)
            .AddInput(PHeaModFra, 1)
            .AddInput(PCom, 1)
            .AddOutput(PAdaConUni, 1).Build();
        worksheet.GetRecipeBuilder("Automated Speed Wiring")
            .AddInput(PSta, 3.75f)
            .AddInput(PWir, 75)
            .AddInput(PHigSpeCon, 1.875f)
            .AddOutput(PAutWir, 7.5f).Build();
        worksheet.GetRecipeBuilder("Beacon")
            .AddInput(PIroPla, 22.5f)
            .AddInput(PIroRod, 7.5f)
            .AddInput(PWir, 112.5f)
            .AddInput(PCab, 15)
            .AddOutput(PBea, 7.5f).Build();
        worksheet.GetRecipeBuilder("Caterium Computer")
            .AddInput(PCirBoa, 26.25f)
            .AddInput(PQui, 105)
            .AddInput(PRub, 45)
            .AddOutput(PCom, 3.8f).Build();
        worksheet.GetRecipeBuilder("Classic Battery")
            .AddInput(PSul, 45)
            .AddInput(PAlcAluShe, 52.5f)
            .AddInput(PPla, 60)
            .AddInput(PWir, 90)
            .AddOutput(PBat, 30).Build();
        worksheet.GetRecipeBuilder("Computer")
            .AddInput(PCirBoa, 25)
            .AddInput(PCab, 22.5f)
            .AddInput(PPla, 45)
            .AddInput(PScr, 130)
            .AddOutput(PCom, 2.5f).Build();
        worksheet.GetRecipeBuilder("Crystal Beacon")
            .AddInput(PSteBea, 2)
            .AddInput(PStePip, 8)
            .AddInput(PCryOsc, 0.5f)
            .AddOutput(PBea, 1).Build();
        worksheet.GetRecipeBuilder("Flexible Framework")
            .AddInput(PModFra, 3.75f)
            .AddInput(PSteBea, 22.5f)
            .AddInput(PRub, 30)
            .AddOutput(PVerFra, 7.5f).Build();
        worksheet.GetRecipeBuilder("Gas Filter")
            .AddInput(PCoa, 37.5f)
            .AddInput(PRub, 15)
            .AddInput(PFab, 15)
            .AddOutput(PGasFil, 7.5f).Build();
        worksheet.GetRecipeBuilder("Heavy Encased Frame")
            .AddInput(PModFra, 7.5f)
            .AddInput(PEncIndBea, 9.375f)
            .AddInput(PStePip, 33.75f)
            .AddInput(PCon, 20.625f)
            .AddOutput(PHeaModFra, 2.8f).Build();
        worksheet.GetRecipeBuilder("Heavy Flexible Frame")
            .AddInput(PModFra, 18.75f)
            .AddInput(PEncIndBea, 11.25f)
            .AddInput(PRub, 75)
            .AddInput(PScr, 390)
            .AddOutput(PHeaModFra, 3.8f).Build();
        worksheet.GetRecipeBuilder("Heavy Modular Frame")
            .AddInput(PModFra, 10)
            .AddInput(PStePip, 30)
            .AddInput(PEncIndBea, 10)
            .AddInput(PScr, 200)
            .AddOutput(PHeaModFra, 2).Build();
        worksheet.GetRecipeBuilder("High-Speed Connector")
            .AddInput(PQui, 210)
            .AddInput(PCab, 37.5f)
            .AddInput(PCirBoa, 3.75f)
            .AddOutput(PHigSpeCon, 3.8f).Build();
        worksheet.GetRecipeBuilder("Infused Uranium Cell")
            .AddInput(PUra, 25)
            .AddInput(PSil, 15)
            .AddInput(PSul, 25)
            .AddInput(PQui, 75)
            .AddOutput(PEncUraCel, 20).Build();
        worksheet.GetRecipeBuilder("Insulated Crystal Oscillator")
            .AddInput(PQuaCry, 18.75f)
            .AddInput(PRub, 13.125f)
            .AddInput(PAiLim, 1.875f)
            .AddOutput(PCryOsc, 1.9f).Build();
        worksheet.GetRecipeBuilder("Iodine Infused Filter")
            .AddInput(PGasFil, 3.75f)
            .AddInput(PQui, 30)
            .AddInput(PAluCas, 3.75f)
            .AddOutput(PIodInfFil, 3.8f).Build();
        worksheet.GetRecipeBuilder("Magnetic Field Generator")
            .AddInput(PVerFra, 2.5f)
            .AddInput(PEleConRod, 1)
            .AddInput(PBat, 5)
            .AddOutput(PMagFieGen, 1).Build();
        worksheet.GetRecipeBuilder("Modular Engine")
            .AddInput(PMot, 2)
            .AddInput(PRub, 15)
            .AddInput(PSmaPla, 2)
            .AddOutput(PModEng, 1).Build();
        worksheet.GetRecipeBuilder("Plastic Smart Plating")
            .AddInput(PReiIroPla, 2.5f)
            .AddInput(PRot, 2.5f)
            .AddInput(PPla, 7.5f)
            .AddOutput(PSmaPla, 5).Build();
        worksheet.GetRecipeBuilder("Plutonium Fuel Rod")
            .AddInput(PEncPluCel, 7.5f)
            .AddInput(PSteBea, 4.5f)
            .AddInput(PEleConRod, 1.5f)
            .AddInput(PHeaSin, 2.5f)
            .AddOutput(PPluFueRod, 0.3f).Build();
        worksheet.GetRecipeBuilder("Radio Connection Unit")
            .AddInput(PHeaSin, 15)
            .AddInput(PHigSpeCon, 7.5f)
            .AddInput(PQuaCry, 45)
            .AddOutput(PRadConUni, 3.8f).Build();
        worksheet.GetRecipeBuilder("Radio Control System")
            .AddInput(PCryOsc, 1.5f)
            .AddInput(PCirBoa, 15)
            .AddInput(PAluCas, 90)
            .AddInput(PRub, 45)
            .AddOutput(PRadConUni, 4.5f).Build();
        worksheet.GetRecipeBuilder("Radio Control Unit")
            .AddInput(PAluCas, 40)
            .AddInput(PCryOsc, 1.25f)
            .AddInput(PCom, 1.25f)
            .AddOutput(PRadConUni, 2.5f).Build();
        worksheet.GetRecipeBuilder("Rifle Cartridge")
            .AddInput(PBea, 3)
            .AddInput(PSteBea, 30)
            .AddInput(PBlaPow, 30)
            .AddInput(PRub, 30)
            .AddOutput(PRifCar, 15).Build();
        worksheet.GetRecipeBuilder("Rigour Motor")
            .AddInput(PRot, 3.75f)
            .AddInput(PSta, 3.75f)
            .AddInput(PCryOsc, 1.25f)
            .AddOutput(PMot, 7.5f).Build();
        worksheet.GetRecipeBuilder("Seismic Nobelisk")
            .AddInput(PBlaPow, 12)
            .AddInput(PStePip, 12)
            .AddInput(PCryOsc, 1.5f)
            .AddOutput(PNob, 6).Build();
        worksheet.GetRecipeBuilder("Silicon High-Speed Connector")
            .AddInput(PQui, 90)
            .AddInput(PSil, 37.5f)
            .AddInput(PCirBoa, 3)
            .AddOutput(PHigSpeCon, 3).Build();
        worksheet.GetRecipeBuilder("Super-State Computer")
            .AddInput(PCom, 3.6f)
            .AddInput(PEleConRod, 2.4f)
            .AddInput(PBat, 24)
            .AddInput(PWir, 54)
            .AddOutput(PSup, 2.4f).Build();
        worksheet.GetRecipeBuilder("Supercomputer")
            .AddInput(PCom, 3.75f)
            .AddInput(PAiLim, 3.75f)
            .AddInput(PHigSpeCon, 5.625f)
            .AddInput(PPla, 52.5f)
            .AddOutput(PSup, 1.9f).Build();
        worksheet.GetRecipeBuilder("Thermal Propulsion Rocket")
            .AddInput(PModEng, 2.5f)
            .AddInput(PTurMot, 1)
            .AddInput(PCooSys, 3)
            .AddInput(PFusModFra, 1)
            .AddOutput(PTheProRoc, 1).Build();
        worksheet.GetRecipeBuilder("Turbo Electric Motor")
            .AddInput(PMot, 6.5625f)
            .AddInput(PRadConUni, 8.4375f)
            .AddInput(PEleConRod, 4.6875f)
            .AddInput(PRot, 6.5625f)
            .AddOutput(PTurMot, 2.8f).Build();
        worksheet.GetRecipeBuilder("Turbo Motor")
            .AddInput(PCooSys, 7.5f)
            .AddInput(PRadConUni, 3.75f)
            .AddInput(PMot, 7.5f)
            .AddInput(PRub, 45)
            .AddOutput(PTurMot, 1.9f).Build();
        worksheet.GetRecipeBuilder("Turbo Pressure Motor")
            .AddInput(PMot, 7.5f)
            .AddInput(PPreConCub, 1.875f)
            .AddInput(PPacNitGas, 45)
            .AddInput(PSta, 15)
            .AddOutput(PTurMot, 3.8f).Build();
        worksheet.GetRecipeBuilder("Uranium Fuel Rod")
            .AddInput(PEncUraCel, 20)
            .AddInput(PEncIndBea, 1.2f)
            .AddInput(PEleConRod, 2)
            .AddOutput(PUraFueRod, 0.4f).Build();
        worksheet.GetRecipeBuilder("Uranium Fuel Unit")
            .AddInput(PEncUraCel, 20)
            .AddInput(PEleConRod, 2)
            .AddInput(PCryOsc, 0.6f)
            .AddInput(PBea, 1.2f)
            .AddOutput(PUraFueRod, 0.6f).Build();
        
        // Packager Recipes
        worksheet.GetRecipeBuilder("Packaged Alumina Solution")
            .AddInput(PAluSol, 120)
            .AddInput(PEmpCan, 120)
            .AddOutput(PPacAluSol, 120).Build();
        worksheet.GetRecipeBuilder("Packaged Fuel")
            .AddInput(PFue, 40)
            .AddInput(PEmpCan, 40)
            .AddOutput(PPacFue, 40).Build();
        worksheet.GetRecipeBuilder("Packaged Heavy Oil Residue")
            .AddInput(PHeaOilRes, 30)
            .AddInput(PEmpCan, 30)
            .AddOutput(PPacHeaOilRes, 30).Build();
        worksheet.GetRecipeBuilder("Packaged Liquid Biofuel")
            .AddInput(PLiqBio, 40)
            .AddInput(PEmpCan, 40)
            .AddOutput(PPacLiqBio, 40).Build();
        worksheet.GetRecipeBuilder("Packaged Nitric Acid")
            .AddInput(PNitAci, 30)
            .AddInput(PEmpCan, 30)
            .AddOutput(PPacNitAci, 30).Build();
        worksheet.GetRecipeBuilder("Packaged Nitrogen Gas")
            .AddInput(PNitGas, 240)
            .AddInput(PEmpCan, 60)
            .AddOutput(PPacNitGas, 60).Build();
        worksheet.GetRecipeBuilder("Packaged Oil")
            .AddInput(PCruOil, 30)
            .AddInput(PEmpCan, 30)
            .AddOutput(PPacCruOil, 30).Build();
        worksheet.GetRecipeBuilder("Packaged Sulfuric Acid")
            .AddInput(PSulAci, 40)
            .AddInput(PEmpCan, 40)
            .AddOutput(PPacSulAci, 40).Build();
        worksheet.GetRecipeBuilder("Packaged Turbofuel")
            .AddInput(PTur, 20)
            .AddInput(PEmpCan, 20)
            .AddOutput(PPacTur, 20).Build();
        worksheet.GetRecipeBuilder("Packaged Water")
            .AddInput(PWat, 60)
            .AddInput(PEmpCan, 60)
            .AddOutput(PPacWat, 60).Build();
        worksheet.GetRecipeBuilder("Unpackaged Alumina Solution")
            .AddInput(PPacAluSol, 120)
            .AddOutput(PAluSol, 120)
            .AddOutput(PEmpCan, 120).Build();
        worksheet.GetRecipeBuilder("Unpackaged Fuel")
            .AddInput(PPacFue, 60)
            .AddOutput(PFue, 60)
            .AddOutput(PEmpCan, 60).Build();
        worksheet.GetRecipeBuilder("Unpackaged Heavy Oil Residue")
            .AddInput(PPacHeaOilRes, 20)
            .AddOutput(PHeaOilRes, 20)
            .AddOutput(PEmpCan, 20).Build();
        worksheet.GetRecipeBuilder("Unpackaged Liquid Biofuel")
            .AddInput(PPacLiqBio, 60)
            .AddOutput(PLiqBio, 60)
            .AddOutput(PEmpCan, 60).Build();
        worksheet.GetRecipeBuilder("Unpackaged Nitric Acid")
            .AddInput(PPacNitAci, 20)
            .AddOutput(PNitAci, 20)
            .AddOutput(PEmpCan, 20).Build();
        worksheet.GetRecipeBuilder("Unpackaged Nitrogen Gas")
            .AddInput(PPacNitGas, 60)
            .AddOutput(PNitGas, 240)
            .AddOutput(PEmpCan, 60).Build();
        worksheet.GetRecipeBuilder("Unpackaged Oil")
            .AddInput(PPacCruOil, 60)
            .AddOutput(PCruOil, 60)
            .AddOutput(PEmpCan, 60).Build();
        worksheet.GetRecipeBuilder("Unpackaged Sulfuric Acid")
            .AddInput(PPacSulAci, 60)
            .AddOutput(PSulAci, 60)
            .AddOutput(PEmpCan, 60).Build();
        worksheet.GetRecipeBuilder("Unpackaged Turbofuel")
            .AddInput(PPacTur, 20)
            .AddOutput(PTur, 20)
            .AddOutput(PEmpCan, 20).Build();
        worksheet.GetRecipeBuilder("Unpackaged Water")
            .AddInput(PPacWat, 120)
            .AddOutput(PWat, 120)
            .AddOutput(PEmpCan, 120).Build();

        // Refinery Recipes
        worksheet.GetRecipeBuilder("Alumina Solution")
            .AddInput(PBau, 120)
            .AddInput(PWat, 180)
            .AddOutput(PAluSol, 120)
            .AddOutput(PSil, 50).Build();
        worksheet.GetRecipeBuilder("Aluminum Scrap")
            .AddInput(PAluSol, 240)
            .AddInput(PCoa, 120)
            .AddOutput(PAluScr, 360)
            .AddOutput(PWat, 120).Build();
        worksheet.GetRecipeBuilder("Coated Cable")
            .AddInput(PWir, 37.5f)
            .AddInput(PHeaOilRes, 15)
            .AddOutput(PCab, 67.5f).Build();
        worksheet.GetRecipeBuilder("Diluted Packaged Fuel")
            .AddInput(PHeaOilRes, 30)
            .AddInput(PPacWat, 60)
            .AddOutput(PPacFue, 60).Build();
        worksheet.GetRecipeBuilder("Electrode - Aluminum Scrap")
            .AddInput(PAluSol, 180)
            .AddInput(PPetCok, 60)
            .AddOutput(PAluScr, 300)
            .AddOutput(PWat, 105).Build();
        worksheet.GetRecipeBuilder("Fuel")
            .AddInput(PCruOil, 60)
            .AddOutput(PFue, 40)
            .AddOutput(PPolRes, 30).Build();
        worksheet.GetRecipeBuilder("Heavy Oil Residue")
            .AddInput(PCruOil, 30)
            .AddOutput(PHeaOilRes, 40)
            .AddOutput(PPolRes, 20).Build();
        worksheet.GetRecipeBuilder("Liquid Biofuel")
            .AddInput(PSolBio, 90)
            .AddInput(PWat, 45)
            .AddOutput(PLiqBio, 60).Build();
        worksheet.GetRecipeBuilder("Petroleum Coke")
            .AddInput(PHeaOilRes, 40)
            .AddOutput(PPetCok, 120).Build();
        worksheet.GetRecipeBuilder("Plastic")
            .AddInput(PCruOil, 30)
            .AddOutput(PPla, 20)
            .AddOutput(PHeaOilRes, 10).Build();
        worksheet.GetRecipeBuilder("Polyester Fabric")
            .AddInput(PPolRes, 30)
            .AddInput(PWat, 30)
            .AddOutput(PFab, 30).Build();
        worksheet.GetRecipeBuilder("Polyester Fabric 2")
            .AddInput(PPolRes, 80)
            .AddInput(PWat, 50)
            .AddOutput(PFab, 5).Build();
        worksheet.GetRecipeBuilder("Polymer Resin")
            .AddInput(PCruOil, 60)
            .AddOutput(PPolRes, 130)
            .AddOutput(PHeaOilRes, 20).Build();
        worksheet.GetRecipeBuilder("Pure Caterium")
            .AddInput(PCatOre, 24)
            .AddInput(PWat, 24)
            .AddOutput(PCatIng, 12).Build();
        worksheet.GetRecipeBuilder("Pure Copper Ingot")
            .AddInput(PCopOre, 15)
            .AddInput(PWat, 10)
            .AddOutput(PCopIng, 37.5f).Build();
        worksheet.GetRecipeBuilder("Pure Iron Ingot")
            .AddInput(PIroOre, 35)
            .AddInput(PWat, 20)
            .AddOutput(PIroIng, 65).Build();
        worksheet.GetRecipeBuilder("Pure Quartz Crystal")
            .AddInput(PRawQua, 67.5f)
            .AddInput(PWat, 37.5f)
            .AddOutput(PQuaCry, 52.5f).Build();
        worksheet.GetRecipeBuilder("Recycled Plastic")
            .AddInput(PRub, 30)
            .AddInput(PFue, 30)
            .AddOutput(PPla, 60).Build();
        worksheet.GetRecipeBuilder("Recycled Rubber")
            .AddInput(PPla, 30)
            .AddInput(PFue, 30)
            .AddOutput(PRub, 60).Build();
        worksheet.GetRecipeBuilder("Residual Fuel")
            .AddInput(PHeaOilRes, 60)
            .AddOutput(PFue, 40).Build();
        worksheet.GetRecipeBuilder("Residual Plastic")
            .AddInput(PPolRes, 60)
            .AddInput(PWat, 20)
            .AddOutput(PPla, 20).Build();
        worksheet.GetRecipeBuilder("Residual Rubber")
            .AddInput(PPolRes, 40)
            .AddInput(PWat, 40)
            .AddOutput(PRub, 20).Build();
        worksheet.GetRecipeBuilder("Rubber")
            .AddInput(PCruOil, 30)
            .AddOutput(PRub, 20)
            .AddOutput(PHeaOilRes, 20).Build();
        worksheet.GetRecipeBuilder("Sloppy Alumina")
            .AddInput(PBau, 200)
            .AddInput(PWat, 200)
            .AddOutput(PAluSol, 240).Build();
        worksheet.GetRecipeBuilder("Steamed Copper Sheet")
            .AddInput(PCopIng, 22.5f)
            .AddInput(PWat, 22.5f)
            .AddOutput(PCopShe, 22.5f).Build();
        worksheet.GetRecipeBuilder("Sulfuric Acid")
            .AddInput(PSul, 50)
            .AddInput(PWat, 50)
            .AddOutput(PSulAci, 50).Build();
        worksheet.GetRecipeBuilder("Turbo Heavy Fuel")
            .AddInput(PHeaOilRes, 37.5f)
            .AddInput(PComCoa, 30)
            .AddOutput(PTur, 30).Build();
        worksheet.GetRecipeBuilder("Turbofuel")
            .AddInput(PFue, 22.5f)
            .AddInput(PComCoa, 15)
            .AddOutput(PTur, 18.8f).Build();
        worksheet.GetRecipeBuilder("Wet Concrete")
            .AddInput(PLim, 120)
            .AddInput(PWat, 100)
            .AddOutput(PCon, 80).Build();
        
        // Blender Recipes
        worksheet.GetRecipeBuilder("Battery")
            .AddInput(PSulAci, 50)
            .AddInput(PAluSol, 40)
            .AddInput(PAluCas, 20)
            .AddOutput(PBat, 20)
            .AddOutput(PWat, 30).Build();
        worksheet.GetRecipeBuilder("Cooling Device")
            .AddInput(PHeaSin, 9.375f)
            .AddInput(PMot, 1.875f)
            .AddInput(PNitGas, 45)
            .AddOutput(PCooSys, 3.8f).Build();
        worksheet.GetRecipeBuilder("Cooling System")
            .AddInput(PHeaSin, 12)
            .AddInput(PRub, 12)
            .AddInput(PWat, 30)
            .AddInput(PNitGas, 150)
            .AddOutput(PCooSys, 6).Build();
        worksheet.GetRecipeBuilder("Diluted Fuel")
            .AddInput(PHeaOilRes, 50)
            .AddInput(PWat, 100)
            .AddOutput(PFue, 100).Build();
        worksheet.GetRecipeBuilder("Encased Uranium Cell")
            .AddInput(PUra, 50)
            .AddInput(PCon, 15)
            .AddInput(PSulAci, 40)
            .AddOutput(PEncUraCel, 25)
            .AddOutput(PSulAci, 10).Build();
        worksheet.GetRecipeBuilder("Fertile Uranium")
            .AddInput(PUra, 25)
            .AddInput(PUraWas, 25)
            .AddInput(PNitAci, 15)
            .AddInput(PSulAci, 25)
            .AddOutput(PNonUra, 100)
            .AddOutput(PWat, 40).Build();
        worksheet.GetRecipeBuilder("Fused Modular Frame")
            .AddInput(PHeaModFra, 1.5f)
            .AddInput(PAluCas, 75)
            .AddInput(PNitGas, 37.5f)
            .AddOutput(PFusModFra, 1.5f).Build();
        worksheet.GetRecipeBuilder("Heat-Fused Frame")
            .AddInput(PHeaModFra, 3)
            .AddInput(PAluIng, 150)
            .AddInput(PNitAci, 24)
            .AddInput(PFue, 30)
            .AddOutput(PFusModFra, 3).Build();
        worksheet.GetRecipeBuilder("Instant Scrap")
            .AddInput(PBau, 150)
            .AddInput(PCoa, 100)
            .AddInput(PSulAci, 50)
            .AddInput(PWat, 60)
            .AddOutput(PAluScr, 300)
            .AddOutput(PWat, 50).Build();
        worksheet.GetRecipeBuilder("Nitrac Acid")
            .AddInput(PNitGas, 120)
            .AddInput(PWat, 30)
            .AddInput(PIroPla, 10)
            .AddOutput(PNitAci, 30).Build();
        worksheet.GetRecipeBuilder("Non-fissle Uranium")
            .AddInput(PUraWas, 37.5f)
            .AddInput(PSil, 25)
            .AddInput(PNitAci, 15)
            .AddInput(PSulAci, 15)
            .AddOutput(PNonUra, 50)
            .AddOutput(PWat, 15).Build();
        worksheet.GetRecipeBuilder("Turbo Blend Fuel")
            .AddInput(PFue, 15)
            .AddInput(PHeaOilRes, 30)
            .AddInput(PSul, 22.5f)
            .AddInput(PPetCok, 22.5f)
            .AddOutput(PTur, 45).Build();

        // Particle Accelerator Recipes
        worksheet.GetRecipeBuilder("Instant Plutonium Cell")
            .AddInput(PNonUra, 75)
            .AddInput(PAluCas, 10)
            .AddOutput(PEncPluCel, 10).Build();
        worksheet.GetRecipeBuilder("Nuclear Pasta")
            .AddInput(PCopPow, 100)
            .AddInput(PPreConCub, 0.5f)
            .AddOutput(PNucPas, 0.5f).Build();
        worksheet.GetRecipeBuilder("Plutonium Pellet")
            .AddInput(PNonUra, 100)
            .AddInput(PUraWas, 25)
            .AddOutput(PPluPel, 30).Build();
    }
}