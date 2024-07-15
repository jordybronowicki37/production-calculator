using ProductionCalculator.Core.components.entityContainer;

namespace ProductionCalculator.Api.Data.GameDataPresets;

public static class SatisfactoryExperimentalData
{
    public static void AddData(EntityContainer e)
    {
        e.Name = "Satisfactory Experimental";
        
        // Ores
        var PBau = e.GetOrGenerateProduct("Bauxite");
        var PCatOre = e.GetOrGenerateProduct("Caterium Ore");
        var PCoa = e.GetOrGenerateProduct("Coal");
        var PCopOre = e.GetOrGenerateProduct("Copper Ore");
        var PCruOil = e.GetOrGenerateProduct("Crude Oil");
        var PPacCruOil = e.GetOrGenerateProduct("Packaged Crude Oil");
        var PIroOre = e.GetOrGenerateProduct("Iron Ore");
        var PLim = e.GetOrGenerateProduct("Limestone");
        var PNitGas = e.GetOrGenerateProduct("Nitrogen Gas");
        var PPacNitGas = e.GetOrGenerateProduct("Packaged Nitrogen Gas");
        var PRawQua = e.GetOrGenerateProduct("Raw Quartz");
        var PSamOre = e.GetOrGenerateProduct("SAM Ore");
        var PSul = e.GetOrGenerateProduct("Sulfur");
        var PUra = e.GetOrGenerateProduct("Uranium");
        var PWat = e.GetOrGenerateProduct("Water");
        var PPacWat = e.GetOrGenerateProduct("Packaged Water");
        
        // Flora & Fauna
        var PHogRem = e.GetOrGenerateProduct("Hog Remains");
        var PSpiRem = e.GetOrGenerateProduct("Spitter Remains");
        var PStiRem = e.GetOrGenerateProduct("Stinger Remains");
        var PHatRem = e.GetOrGenerateProduct("Hatcher Remains");
        var PFloPet = e.GetOrGenerateProduct("Flower Petals");
        var PLea = e.GetOrGenerateProduct("Leaves");
        var PMyc = e.GetOrGenerateProduct("Mycelia");
        var PBluPowSlu = e.GetOrGenerateProduct("Blue Power Slug");
        var PYelPowSlu = e.GetOrGenerateProduct("Yellow Power Slug");
        var PPurPowSlu = e.GetOrGenerateProduct("Purple Power Slug");
        var PWoo = e.GetOrGenerateProduct("Wood");
        
        // Tier 0
        var PBio = e.GetOrGenerateProduct("Biomass");
        var PCab = e.GetOrGenerateProduct("Cable");
        var PCon = e.GetOrGenerateProduct("Concrete");
        var PCopIng = e.GetOrGenerateProduct("Copper Ingot");
        var PIroIng = e.GetOrGenerateProduct("Iron Ingot");
        var PIroPla = e.GetOrGenerateProduct("Iron Plate");
        var PIroRod = e.GetOrGenerateProduct("Iron Rod");
        var PReiIroPla = e.GetOrGenerateProduct("Reinforced Iron Plate");
        var PScr = e.GetOrGenerateProduct("Screw");
        var PWir = e.GetOrGenerateProduct("Wire");
        
        // Tier 2
        var PColCar = e.GetOrGenerateProduct("Color Cartridge");
        var PCopShe = e.GetOrGenerateProduct("Copper Sheet");
        var PSolBio = e.GetOrGenerateProduct("Solid Biofuel");
        var PModFra = e.GetOrGenerateProduct("Modular Frame");
        var PRot = e.GetOrGenerateProduct("Rotor");
        var PSmaPla = e.GetOrGenerateProduct("Smart Plating");
        
        // Tier 3
        var PSteBea = e.GetOrGenerateProduct("Steel Beam");
        var PSteIng = e.GetOrGenerateProduct("Steel Ingot");
        var PStePip = e.GetOrGenerateProduct("Steel Pipe");
        var PVerFra = e.GetOrGenerateProduct("Versatile Framework");
        
        // Tier 4
        var PAutWir = e.GetOrGenerateProduct("Automated Wiring");
        var PEncIndBea = e.GetOrGenerateProduct("Encased Industrial Beam");
        var PHeaModFra = e.GetOrGenerateProduct("Heavy Modular Frame");
        var PMot = e.GetOrGenerateProduct("Motor");
        var PSta = e.GetOrGenerateProduct("Stator");
        
        // Tier 5
        var PAdaConUni = e.GetOrGenerateProduct("Adaptive Control Unit");
        var PCirBoa = e.GetOrGenerateProduct("Circuit Board");
        var PCom = e.GetOrGenerateProduct("Computer");
        var PEmpCan = e.GetOrGenerateProduct("Empty Canister");
        var PFue = e.GetOrGenerateProduct("Fuel");
        var PPacFue = e.GetOrGenerateProduct("Packaged Fuel");
        var PHeaOilRes = e.GetOrGenerateProduct("Heavy Oil Residue");
        var PPacHeaOilRes = e.GetOrGenerateProduct("Packaged Heavy Oil Residue");
        var PLiqBio = e.GetOrGenerateProduct("Liquid Biofuel");
        var PPacLiqBio = e.GetOrGenerateProduct("Packaged Liquid Biofuel");
        var PModEng = e.GetOrGenerateProduct("Modular Engine");
        var PPetCok = e.GetOrGenerateProduct("Petroleum Coke");
        var PPla = e.GetOrGenerateProduct("Plastic");
        var PPolRes = e.GetOrGenerateProduct("Polymer Resin");
        var PRub = e.GetOrGenerateProduct("Rubber");
        
        // Tier 7
        var PAluSol = e.GetOrGenerateProduct("Alumina Solution");
        var PPacAluSol = e.GetOrGenerateProduct("Packaged Alumina Solution");
        var PAluCas = e.GetOrGenerateProduct("Aluminum Casing");
        var PAlcAluShe = e.GetOrGenerateProduct("Alclad Aluminum Sheet");
        var PAluIng = e.GetOrGenerateProduct("Aluminum Ingot");
        var PAluScr = e.GetOrGenerateProduct("Aluminum Scrap");
        var PAssDirSys = e.GetOrGenerateProduct("Assembly Director System");
        var PBat = e.GetOrGenerateProduct("Battery");
        var PRadConUni = e.GetOrGenerateProduct("Radio Control Unit");
        var PSulAci = e.GetOrGenerateProduct("Sulfuric Acid");
        var PPacSulAci = e.GetOrGenerateProduct("Packaged Sulfuric Acid");
        
        // Tier 8
        var PCooSys = e.GetOrGenerateProduct("Cooling System");
        var PCopPow = e.GetOrGenerateProduct("Copper Powder");
        var PEleConRod = e.GetOrGenerateProduct("Electromagnetic Control Rod");
        var PEmpFluTan = e.GetOrGenerateProduct("Empty Fluid Tank");
        var PEncPluCel = e.GetOrGenerateProduct("Encased Plutonium Cell");
        var PEncUraCel = e.GetOrGenerateProduct("Encased Uranium Cell");
        var PFusModFra = e.GetOrGenerateProduct("Fused Modular Frame");
        var PHeaSin = e.GetOrGenerateProduct("Heat Sink");
        var PMagFieGen = e.GetOrGenerateProduct("Magnetic Field Generator");
        var PNitAci = e.GetOrGenerateProduct("Nitric Acid");
        var PPacNitAci = e.GetOrGenerateProduct("Packaged Nitric Acid");
        var PNonUra = e.GetOrGenerateProduct("Non-fissle Uranium");
        var PNucPas = e.GetOrGenerateProduct("Nuclear Pasta");
        var PPluFueRod = e.GetOrGenerateProduct("Plutonium Fuel Rod");
        var PPluPel = e.GetOrGenerateProduct("Plutonium Pellet");
        var PPluWas = e.GetOrGenerateProduct("Plutonium Waste");
        var PPreConCub = e.GetOrGenerateProduct("Pressure Conversion Cube");
        var PTheProRoc = e.GetOrGenerateProduct("Thermal Propulsion Rocket");
        var PTurMot = e.GetOrGenerateProduct("Turbo Motor");
        var PUraFueRod = e.GetOrGenerateProduct("Uranium Fuel Rod");
        var PUraWas = e.GetOrGenerateProduct("Uranium Waste");
        
        // MAM
        var PAiLim = e.GetOrGenerateProduct("AI Limiter");
        var PAliPro = e.GetOrGenerateProduct("Alien Protein");
        var PBlaPow = e.GetOrGenerateProduct("Black Powder");
        var PCatIng = e.GetOrGenerateProduct("Caterium Ingot");
        var PComCoa = e.GetOrGenerateProduct("Compacted Coal");
        var PCryOsc = e.GetOrGenerateProduct("Crystal Oscillator");
        var PFab = e.GetOrGenerateProduct("Fabric");
        var PHigSpeCon = e.GetOrGenerateProduct("High-Speed Connector");
        var POrgDatCap = e.GetOrGenerateProduct("Organic Data Capsule");
        var PQui = e.GetOrGenerateProduct("Quickwire");
        var PPowSha = e.GetOrGenerateProduct("Power Shard");
        var PQuaCry = e.GetOrGenerateProduct("Quartz Crystal");
        var PSil = e.GetOrGenerateProduct("Silica");
        var PSmoPow = e.GetOrGenerateProduct("Smokeless Powder");
        var PSup = e.GetOrGenerateProduct("Supercomputer");
        var PTur = e.GetOrGenerateProduct("Turbofuel");
        var PPacTur = e.GetOrGenerateProduct("Packaged Turbofuel");
        
        // Equipment
        var PBea = e.GetOrGenerateProduct("Beacon");
        var PGasFil = e.GetOrGenerateProduct("Gas Filter");
        var PIodInfFil = e.GetOrGenerateProduct("Iodine Infused Filter");
        var PNob = e.GetOrGenerateProduct("Nobelisk");
        var PGasNob = e.GetOrGenerateProduct("Gas Nobelisk");
        var PPulNob = e.GetOrGenerateProduct("Pulse Nobelisk");
        var PCluNob = e.GetOrGenerateProduct("Cluster Nobelisk");
        var PNukNob = e.GetOrGenerateProduct("Nuke Nobelisk");
        var PIroReb = e.GetOrGenerateProduct("Iron Rebar");
        var PStuReb = e.GetOrGenerateProduct("Stun Rebar");
        var PShaReb = e.GetOrGenerateProduct("Shatter Rebar");
        var PExpReb = e.GetOrGenerateProduct("Explosive Rebar");
        var PRifAmm = e.GetOrGenerateProduct("Rifle Ammo");
        var PHomRifAmm = e.GetOrGenerateProduct("Homing Rifle Ammo");
        var PTurRifAmm = e.GetOrGenerateProduct("Turbo Rifle Ammo");
        
        // Machines
        var MSme = e.GenerateMachine("Smelter");
        var MFou = e.GenerateMachine("Foundry");
        var MCon = e.GenerateMachine("Constructor");
        var MAss = e.GenerateMachine("Assembler");
        var MMan = e.GenerateMachine("Manufacturer");
        var MPac = e.GenerateMachine("Packager");
        var MRef = e.GenerateMachine("Refinery");
        var MBle = e.GenerateMachine("Blender");
        var MParAcc = e.GenerateMachine("Particle Accelerator");
        
        // Smelter Recipes
        e.GetRecipeBuilder("Caterium Ingot", MSme)
            .AddInput(PCatOre, 45)
            .AddOutput(PCatIng, 15).Build();
        e.GetRecipeBuilder("Copper Ingot", MSme)
            .AddInput(PCopOre, 30)
            .AddOutput(PCopIng, 30).Build();
        e.GetRecipeBuilder("Iron Ingot", MSme)
            .AddInput(PIroOre, 30)
            .AddOutput(PIroIng, 30).Build();
        e.GetRecipeBuilder("Pure Aluminum Ingot", MSme)
            .AddInput(PAluScr, 60)
            .AddOutput(PAluIng, 30).Build();
        
        // Foundry Recipes
        e.GetRecipeBuilder("Aluminum Ingot", MFou)
            .AddInput(PAluScr, 90)
            .AddInput(PSil, 75)
            .AddOutput(PAluIng, 60).Build();
        e.GetRecipeBuilder("Coke Steel Ingot", MFou)
            .AddInput(PIroOre, 75)
            .AddInput(PPetCok, 75)
            .AddOutput(PSteIng, 100).Build();
        e.GetRecipeBuilder("Compacted Steel Ingot", MFou)
            .AddInput(PIroOre, 22.5f)
            .AddInput(PComCoa, 11.25f)
            .AddOutput(PSteIng, 37.5f).Build();
        e.GetRecipeBuilder("Copper Alloy Ingot", MFou)
            .AddInput(PCopOre, 50)
            .AddInput(PIroOre, 25)
            .AddOutput(PCopIng, 100).Build();
        e.GetRecipeBuilder("Iron Alloy Ingot", MFou)
            .AddInput(PIroOre, 20)
            .AddInput(PCopOre, 20)
            .AddOutput(PIroIng, 50).Build();
        e.GetRecipeBuilder("Solid Steel Ingot", MFou)
            .AddInput(PIroIng, 40)
            .AddInput(PCoa, 40)
            .AddOutput(PSteIng, 60).Build();
        e.GetRecipeBuilder("Steel Ingot", MFou)
            .AddInput(PIroOre, 45)
            .AddInput(PCoa, 45)
            .AddOutput(PSteIng, 45).Build();
        
        // Constructor Recipes
        e.GetRecipeBuilder("AluminumCasing", MCon)
            .AddInput(PAluIng, 90)
            .AddOutput(PAluCas, 60).Build();
        e.GetRecipeBuilder("BioCoal", MCon)
            .AddInput(PBio, 37.5f)
            .AddOutput(PCoa, 45).Build();
        e.GetRecipeBuilder("Biomass (Alien Protein)", MCon)
            .AddInput(PAliPro, 15)
            .AddOutput(PBio, 1500).Build();
        e.GetRecipeBuilder("Biomass (Leaves)", MCon)
            .AddInput(PLea, 120)
            .AddOutput(PBio, 60).Build();
        e.GetRecipeBuilder("Biomass (Mycelia)", MCon)
            .AddInput(PMyc, 150)
            .AddOutput(PBio, 150).Build();
        e.GetRecipeBuilder("Biomass (Wood)", MCon)
            .AddInput(PWoo, 60)
            .AddOutput(PBio, 300).Build();
        e.GetRecipeBuilder("Cable", MCon)
            .AddInput(PWir, 60)
            .AddOutput(PCab, 30).Build();
        e.GetRecipeBuilder("Cast Screw", MCon)
            .AddInput(PIroIng, 12.5f)
            .AddOutput(PScr, 50).Build();
        e.GetRecipeBuilder("Caterium Wire", MCon)
            .AddInput(PCatIng, 15)
            .AddOutput(PWir, 120).Build();
        e.GetRecipeBuilder("Charcoal", MCon)
            .AddInput(PWoo, 15)
            .AddOutput(PCoa, 150).Build();
        e.GetRecipeBuilder("Color Cartridge", MCon)
            .AddInput(PFloPet, 50)
            .AddOutput(PColCar,10075).Build();
        e.GetRecipeBuilder("Concrete", MCon)
            .AddInput(PLim, 45)
            .AddOutput(PCon, 15).Build();
        e.GetRecipeBuilder("Copper Powder", MCon)
            .AddInput(PCopIng, 300)
            .AddOutput(PCopPow, 50).Build();
        e.GetRecipeBuilder("Copper Sheet", MCon)
            .AddInput(PCopIng, 20)
            .AddOutput(PCopShe, 10).Build();
        e.GetRecipeBuilder("Empty Canister", MCon)
            .AddInput(PPla, 30)
            .AddOutput(PEmpCan, 60).Build();
        e.GetRecipeBuilder("Empty Fluid Tank", MCon)
            .AddInput(PAluIng, 60)
            .AddOutput(PEmpFluTan, 60).Build();
        e.GetRecipeBuilder("Hatcher Protein", MCon)
            .AddInput(PHatRem, 20)
            .AddOutput(PAliPro, 20).Build();
        e.GetRecipeBuilder("Hog Protein", MCon)
            .AddInput(PHogRem, 20)
            .AddOutput(PAliPro, 20).Build();
        e.GetRecipeBuilder("Iron Plate", MCon)
            .AddInput(PIroIng, 30)
            .AddOutput(PIroPla, 20).Build();
        e.GetRecipeBuilder("Iron Rod", MCon)
            .AddInput(PIroIng, 15)
            .AddOutput(PIroRod, 15).Build();
        e.GetRecipeBuilder("Iron Wire", MCon)
            .AddInput(PIroIng, 12.5f)
            .AddOutput(PWir, 22.5f).Build();
        e.GetRecipeBuilder("Organic Data Capsule", MCon)
            .AddInput(PAliPro, 10)
            .AddOutput(POrgDatCap, 10).Build();
        e.GetRecipeBuilder("Power Shard Blue", MCon)
            .AddInput(PBluPowSlu, 7.5f)
            .AddOutput(PPowSha, 7.5f).Build();
        e.GetRecipeBuilder("Power Shard Yellow", MCon)
            .AddInput(PYelPowSlu, 5)
            .AddOutput(PPowSha, 10).Build();
        e.GetRecipeBuilder("Power Shard Purple", MCon)
            .AddInput(PPurPowSlu, 2.5f)
            .AddOutput(PPowSha, 12.5f).Build();
        e.GetRecipeBuilder("Quartz Crystal", MCon)
            .AddInput(PRawQua, 37.5f)
            .AddOutput(PQuaCry, 22.5f).Build();
        e.GetRecipeBuilder("Quickwire", MCon)
            .AddInput(PCatIng, 12)
            .AddOutput(PQui, 60).Build();
        e.GetRecipeBuilder("Screw", MCon)
            .AddInput(PIroRod, 10)
            .AddOutput(PScr, 40).Build();
        e.GetRecipeBuilder("Silica", MCon)
            .AddInput(PRawQua, 22.5f)
            .AddOutput(PSil, 37.5f).Build();
        e.GetRecipeBuilder("Solid Biofuel", MCon)
            .AddInput(PBio, 120)
            .AddOutput(PSolBio, 60).Build();
        e.GetRecipeBuilder("Iron Rebar", MCon)
            .AddInput(PIroRod, 15)
            .AddOutput(PIroReb, 15).Build();
        e.GetRecipeBuilder("Spitter Protein", MCon)
            .AddInput(PSpiRem, 20)
            .AddOutput(PAliPro, 20).Build();
        e.GetRecipeBuilder("Steel Beam", MCon)
            .AddInput(PSteIng, 60)
            .AddOutput(PSteBea, 15).Build();
        e.GetRecipeBuilder("Steel Canister", MCon)
            .AddInput(PSteIng, 60)
            .AddOutput(PEmpCan, 40).Build();
        e.GetRecipeBuilder("Steel Pipe", MCon)
            .AddInput(PSteIng, 30)
            .AddOutput(PStePip, 20).Build();
        e.GetRecipeBuilder("Steel Rod", MCon)
            .AddInput(PSteIng, 12)
            .AddOutput(PIroRod, 48).Build();
        e.GetRecipeBuilder("Steel Screw", MCon)
            .AddInput(PSteIng, 5)
            .AddOutput(PScr, 260).Build();
        e.GetRecipeBuilder("Stinger Protein", MCon)
            .AddInput(PStiRem, 20)
            .AddOutput(PAliPro, 20).Build();
        e.GetRecipeBuilder("Wire", MCon)
            .AddInput(PCopIng, 15)
            .AddOutput(PWir, 30).Build();
        
        // Assembler Recipes
        e.GetRecipeBuilder("Adhered Iron Plate", MAss)
            .AddInput(PIroPla, 11.25f)
            .AddInput(PRub, 3.75f)
            .AddOutput(PReiIroPla, 3.8f).Build();
        e.GetRecipeBuilder("AI Limiter", MAss)
            .AddInput(PCopShe, 25)
            .AddInput(PQui, 100)
            .AddOutput(PAiLim, 5).Build();
        e.GetRecipeBuilder("Alclad Aluminum Sheet", MAss)
            .AddInput(PAluIng, 30)
            .AddInput(PCopIng, 10)
            .AddOutput(PAlcAluShe, 30).Build();
        e.GetRecipeBuilder("Alclad Casting", MAss)
            .AddInput(PAluIng, 150)
            .AddInput(PCopIng, 75)
            .AddOutput(PAluCas, 112.5f).Build();
        e.GetRecipeBuilder("Assembly Director System", MAss)
            .AddInput(PAdaConUni, 1.5f)
            .AddInput(PSup, 0.75f)
            .AddOutput(PAssDirSys, 0.8f).Build();
        e.GetRecipeBuilder("Automated Wiring", MAss)
            .AddInput(PSta, 2.5f)
            .AddInput(PCab, 50)
            .AddOutput(PAutWir, 2.5f).Build();
        e.GetRecipeBuilder("Black Powder", MAss)
            .AddInput(PCoa, 15)
            .AddInput(PSul, 15)
            .AddOutput(PBlaPow, 30).Build();
        e.GetRecipeBuilder("Bolted Frame", MAss)
            .AddInput(PReiIroPla, 7.5f)
            .AddInput(PScr, 140)
            .AddOutput(PModFra, 5).Build();
        e.GetRecipeBuilder("Bolted Iron Plate", MAss)
            .AddInput(PIroPla, 90)
            .AddInput(PScr, 250)
            .AddOutput(PReiIroPla, 15).Build();
        e.GetRecipeBuilder("Caterium Circuit Board", MAss)
            .AddInput(PPla, 12.5f)
            .AddInput(PQui, 37.5f)
            .AddOutput(PCirBoa, 8.8f).Build();
        e.GetRecipeBuilder("Cheap Silica", MAss)
            .AddInput(PRawQua, 11.25f)
            .AddInput(PLim, 18.75f)
            .AddOutput(PSil, 26.3f).Build();
        e.GetRecipeBuilder("Circuit Board", MAss)
            .AddInput(PCopShe, 15)
            .AddInput(PPla, 30)
            .AddOutput(PCirBoa, 7.5f).Build();
        e.GetRecipeBuilder("Cluster Nobelisk", MAss)
            .AddInput(PNob, 7.5f)
            .AddInput(PSmoPow, 10)
            .AddOutput(PCluNob, 2.5f).Build();
        e.GetRecipeBuilder("Coated Iron Canister", MAss)
            .AddInput(PIroPla, 30)
            .AddInput(PCopShe, 15)
            .AddOutput(PEmpCan, 60).Build();
        e.GetRecipeBuilder("Coated Iron Plate", MAss)
            .AddInput(PIroIng, 50)
            .AddInput(PPla, 10)
            .AddOutput(PIroPla, 75).Build();
        e.GetRecipeBuilder("Compacted Coal", MAss)
            .AddInput(PCoa, 25)
            .AddInput(PSul, 25)
            .AddOutput(PRot, 11.3f).Build();
        e.GetRecipeBuilder("Crystal Computer", MAss)
            .AddInput(PCirBoa, 7.5f)
            .AddInput(PCryOsc, 2.8125f)
            .AddOutput(PCom, 2.8f).Build();
        e.GetRecipeBuilder("Electric Motor", MAss)
            .AddInput(PEleConRod, 3.75f)
            .AddInput(PRot, 7.5f)
            .AddOutput(PMot, 7.5f).Build();
        e.GetRecipeBuilder("Electrode Circuit Board", MAss)
            .AddInput(PRub, 30)
            .AddInput(PPetCok, 45)
            .AddOutput(PCirBoa, 5).Build();
        e.GetRecipeBuilder("Electromagnetic Connection Rod", MAss)
            .AddInput(PSta, 8)
            .AddInput(PHigSpeCon, 4)
            .AddOutput(PEleConRod, 8).Build();
        e.GetRecipeBuilder("Electromagnetic Control Rod", MAss)
            .AddInput(PSta, 6)
            .AddInput(PAiLim, 4)
            .AddOutput(PEleConRod, 4).Build();
        e.GetRecipeBuilder("Encased Industrial Beam", MAss)
            .AddInput(PSteBea, 24)
            .AddInput(PCon, 30)
            .AddOutput(PEncIndBea, 6).Build();
        e.GetRecipeBuilder("Encased Industrial Pipe", MAss)
            .AddInput(PStePip, 28)
            .AddInput(PCon, 20)
            .AddOutput(PEncIndBea, 4).Build();
        e.GetRecipeBuilder("Encased Plutoniom Cell", MAss)
            .AddInput(PPluPel, 10)
            .AddInput(PCon, 20)
            .AddOutput(PEncPluCel, 5).Build();
        e.GetRecipeBuilder("Fabric", MAss)
            .AddInput(PMyc, 15)
            .AddInput(PBio, 75)
            .AddOutput(PFab, 15).Build();
        e.GetRecipeBuilder("Fine Black Powder", MAss)
            .AddInput(PSul, 7.5f)
            .AddInput(PComCoa, 3.75f)
            .AddOutput(PBlaPow, 15).Build();
        e.GetRecipeBuilder("Fine Concrete", MAss)
            .AddInput(PSil, 7.5f)
            .AddInput(PLim, 30)
            .AddOutput(PCon, 25).Build();
        e.GetRecipeBuilder("Fused Quickwire", MAss)
            .AddInput(PCatIng, 7.5f)
            .AddInput(PCopIng, 37.5f)
            .AddOutput(PQui, 90).Build();
        e.GetRecipeBuilder("Fused Wire", MAss)
            .AddInput(PCopIng, 12)
            .AddInput(PCatIng, 3)
            .AddOutput(PWir, 90).Build();
        e.GetRecipeBuilder("Gas Nobelisk", MAss)
            .AddInput(PNob, 5)
            .AddInput(PBio, 50)
            .AddOutput(PGasNob, 5).Build();
        e.GetRecipeBuilder("Heat Exchanger", MAss)
            .AddInput(PAluCas, 30)
            .AddInput(PRub, 30)
            .AddOutput(PHeaSin, 10).Build();
        e.GetRecipeBuilder("Heat Sink", MAss)
            .AddInput(PAlcAluShe, 37.5f)
            .AddInput(PCopShe, 22.5f)
            .AddOutput(PHeaSin, 7.5f).Build();
        e.GetRecipeBuilder("Homing Rifle Ammo", MAss)
            .AddInput(PRifAmm, 50)
            .AddInput(PHigSpeCon, 2.5f)
            .AddOutput(PHomRifAmm, 25).Build();
        e.GetRecipeBuilder("Insulated Cable", MAss)
            .AddInput(PWir, 45)
            .AddInput(PRub, 30)
            .AddOutput(PCab, 100).Build();
        e.GetRecipeBuilder("Modular Frame", MAss)
            .AddInput(PReiIroPla, 3)
            .AddInput(PIroRod, 12)
            .AddOutput(PModFra, 2).Build();
        e.GetRecipeBuilder("Motor", MAss)
            .AddInput(PRot, 10)
            .AddInput(PSta, 10)
            .AddOutput(PMot, 5).Build();
        e.GetRecipeBuilder("Nobelisk", MAss)
            .AddInput(PBlaPow, 20)
            .AddInput(PStePip, 20)
            .AddOutput(PNob, 10).Build();
        e.GetRecipeBuilder("OC Computer", MAss)
            .AddInput(PRadConUni, 9)
            .AddInput(PCooSys, 9)
            .AddOutput(PSup, 3).Build();
        e.GetRecipeBuilder("Plutonium Fuel Unit", MAss)
            .AddInput(PEncPluCel, 10)
            .AddInput(PPreConCub, 0.5f)
            .AddOutput(PPluFueRod, 0.5f).Build();
        e.GetRecipeBuilder("Pressure Conversion Cube", MAss)
            .AddInput(PFusModFra, 1)
            .AddInput(PRadConUni, 2)
            .AddOutput(PPreConCub, 1).Build();
        e.GetRecipeBuilder("Pulse Nobelisk", MAss)
            .AddInput(PNob, 5)
            .AddInput(PCryOsc, 1)
            .AddOutput(PPulNob, 5).Build();
        e.GetRecipeBuilder("Quickwire Cable", MAss)
            .AddInput(PQui, 7.5f)
            .AddInput(PRub, 5)
            .AddOutput(PCab, 27.5f).Build();
        e.GetRecipeBuilder("Quickwire Stator", MAss)
            .AddInput(PStePip, 16)
            .AddInput(PQui, 60)
            .AddOutput(PSta, 8).Build();
        e.GetRecipeBuilder("Reinforced Iron Plate", MAss)
            .AddInput(PIroPla, 30)
            .AddInput(PScr, 60)
            .AddOutput(PReiIroPla, 5).Build();
        e.GetRecipeBuilder("Rifle Ammo", MAss)
            .AddInput(PCopShe, 15)
            .AddInput(PSmoPow, 10)
            .AddOutput(PRifAmm, 75).Build();
        e.GetRecipeBuilder("Rotor", MAss)
            .AddInput(PIroRod, 20)
            .AddInput(PScr, 100)
            .AddOutput(PRot, 4).Build();
        e.GetRecipeBuilder("Rubber Concrete", MAss)
            .AddInput(PLim, 50)
            .AddInput(PRub, 10)
            .AddOutput(PCon, 45).Build();
        e.GetRecipeBuilder("Shatter Rebar", MAss)
            .AddInput(PIroReb, 20)
            .AddInput(PQuaCry, 30)
            .AddOutput(PShaReb, 10).Build();
        e.GetRecipeBuilder("Silicon Circuit Board", MAss)
            .AddInput(PCopShe, 27.5f)
            .AddInput(PSil, 27.5f)
            .AddOutput(PCirBoa, 12.5f).Build();
        e.GetRecipeBuilder("Smart Plating", MAss)
            .AddInput(PReiIroPla, 2)
            .AddInput(PRot, 2)
            .AddOutput(PSmaPla, 2).Build();
        e.GetRecipeBuilder("Stator", MAss)
            .AddInput(PStePip, 15)
            .AddInput(PWir, 40)
            .AddOutput(PSta, 5).Build();
        e.GetRecipeBuilder("Steel Coated Plate", MAss)
            .AddInput(PSteIng, 7.5f)
            .AddInput(PPla, 5)
            .AddOutput(PIroPla, 45).Build();
        e.GetRecipeBuilder("Steel Rotor", MAss)
            .AddInput(PStePip, 10)
            .AddInput(PWir, 30)
            .AddOutput(PRot, 5).Build();
        e.GetRecipeBuilder("Steeled Frame", MAss)
            .AddInput(PReiIroPla, 2)
            .AddInput(PStePip, 10)
            .AddOutput(PModFra, 3).Build();
        e.GetRecipeBuilder("Stitched Iron Plate", MAss)
            .AddInput(PIroPla, 18.75f)
            .AddInput(PWir, 37.5f)
            .AddOutput(PReiIroPla, 5.6f).Build();
        e.GetRecipeBuilder("Stun Rebar", MAss)
            .AddInput(PIroReb, 10)
            .AddInput(PQui, 50)
            .AddOutput(PStuReb, 10).Build();
        e.GetRecipeBuilder("Versitile Framework", MAss)
            .AddInput(PModFra, 2.5f)
            .AddInput(PSteBea, 30)
            .AddOutput(PVerFra, 5).Build();

        // Manufacturer Recipes
        e.GetRecipeBuilder("Adaptive Control Unit", MMan)
            .AddInput(PAutWir, 7.5f)
            .AddInput(PCirBoa, 5)
            .AddInput(PHeaModFra, 1)
            .AddInput(PCom, 1)
            .AddOutput(PAdaConUni, 1).Build();
        e.GetRecipeBuilder("Automated Speed Wiring", MMan)
            .AddInput(PSta, 3.75f)
            .AddInput(PWir, 75)
            .AddInput(PHigSpeCon, 1.875f)
            .AddOutput(PAutWir, 7.5f).Build();
        e.GetRecipeBuilder("Beacon", MMan)
            .AddInput(PIroPla, 22.5f)
            .AddInput(PIroRod, 7.5f)
            .AddInput(PWir, 112.5f)
            .AddInput(PCab, 15)
            .AddOutput(PBea, 7.5f).Build();
        e.GetRecipeBuilder("Caterium Computer", MMan)
            .AddInput(PCirBoa, 26.25f)
            .AddInput(PQui, 105)
            .AddInput(PRub, 45)
            .AddOutput(PCom, 3.8f).Build();
        e.GetRecipeBuilder("Classic Battery", MMan)
            .AddInput(PSul, 45)
            .AddInput(PAlcAluShe, 52.5f)
            .AddInput(PPla, 60)
            .AddInput(PWir, 90)
            .AddOutput(PBat, 30).Build();
        e.GetRecipeBuilder("Computer", MMan)
            .AddInput(PCirBoa, 25)
            .AddInput(PCab, 22.5f)
            .AddInput(PPla, 45)
            .AddInput(PScr, 130)
            .AddOutput(PCom, 2.5f).Build();
        e.GetRecipeBuilder("Crystal Beacon", MMan)
            .AddInput(PSteBea, 2)
            .AddInput(PStePip, 8)
            .AddInput(PCryOsc, 0.5f)
            .AddOutput(PBea, 1).Build();
        e.GetRecipeBuilder("Explosive Rebar", MMan)
            .AddInput(PIroReb, 20)
            .AddInput(PSmoPow, 20)
            .AddInput(PStePip, 20)
            .AddOutput(PExpReb, 10).Build();
        e.GetRecipeBuilder("Flexible Framework", MMan)
            .AddInput(PModFra, 3.75f)
            .AddInput(PSteBea, 22.5f)
            .AddInput(PRub, 30)
            .AddOutput(PVerFra, 7.5f).Build();
        e.GetRecipeBuilder("Gas Filter", MMan)
            .AddInput(PCoa, 37.5f)
            .AddInput(PRub, 15)
            .AddInput(PFab, 15)
            .AddOutput(PGasFil, 7.5f).Build();
        e.GetRecipeBuilder("Heavy Encased Frame", MMan)
            .AddInput(PModFra, 7.5f)
            .AddInput(PEncIndBea, 9.375f)
            .AddInput(PStePip, 33.75f)
            .AddInput(PCon, 20.625f)
            .AddOutput(PHeaModFra, 2.8f).Build();
        e.GetRecipeBuilder("Heavy Flexible Frame", MMan)
            .AddInput(PModFra, 18.75f)
            .AddInput(PEncIndBea, 11.25f)
            .AddInput(PRub, 75)
            .AddInput(PScr, 390)
            .AddOutput(PHeaModFra, 3.8f).Build();
        e.GetRecipeBuilder("Heavy Modular Frame", MMan)
            .AddInput(PModFra, 10)
            .AddInput(PStePip, 30)
            .AddInput(PEncIndBea, 10)
            .AddInput(PScr, 200)
            .AddOutput(PHeaModFra, 2).Build();
        e.GetRecipeBuilder("High-Speed Connector", MMan)
            .AddInput(PQui, 210)
            .AddInput(PCab, 37.5f)
            .AddInput(PCirBoa, 3.75f)
            .AddOutput(PHigSpeCon, 3.8f).Build();
        e.GetRecipeBuilder("Infused Uranium Cell", MMan)
            .AddInput(PUra, 25)
            .AddInput(PSil, 15)
            .AddInput(PSul, 25)
            .AddInput(PQui, 75)
            .AddOutput(PEncUraCel, 20).Build();
        e.GetRecipeBuilder("Insulated Crystal Oscillator", MMan)
            .AddInput(PQuaCry, 18.75f)
            .AddInput(PRub, 13.125f)
            .AddInput(PAiLim, 1.875f)
            .AddOutput(PCryOsc, 1.9f).Build();
        e.GetRecipeBuilder("Iodine Infused Filter", MMan)
            .AddInput(PGasFil, 3.75f)
            .AddInput(PQui, 30)
            .AddInput(PAluCas, 3.75f)
            .AddOutput(PIodInfFil, 3.8f).Build();
        e.GetRecipeBuilder("Magnetic Field Generator", MMan)
            .AddInput(PVerFra, 2.5f)
            .AddInput(PEleConRod, 1)
            .AddInput(PBat, 5)
            .AddOutput(PMagFieGen, 1).Build();
        e.GetRecipeBuilder("Modular Engine", MMan)
            .AddInput(PMot, 2)
            .AddInput(PRub, 15)
            .AddInput(PSmaPla, 2)
            .AddOutput(PModEng, 1).Build();
        e.GetRecipeBuilder("Nuke Nobelisk", MMan)
            .AddInput(PNob, 2.5f)
            .AddInput(PEncUraCel, 10)
            .AddInput(PSmoPow, 5)
            .AddInput(PAiLim, 3)
            .AddOutput(PNukNob, 0.5f).Build();
        e.GetRecipeBuilder("Plastic Smart Plating", MMan)
            .AddInput(PReiIroPla, 2.5f)
            .AddInput(PRot, 2.5f)
            .AddInput(PPla, 7.5f)
            .AddOutput(PSmaPla, 5).Build();
        e.GetRecipeBuilder("Plutonium Fuel Rod", MMan)
            .AddInput(PEncPluCel, 7.5f)
            .AddInput(PSteBea, 4.5f)
            .AddInput(PEleConRod, 1.5f)
            .AddInput(PHeaSin, 2.5f)
            .AddOutput(PPluFueRod, 0.3f).Build();
        e.GetRecipeBuilder("Radio Connection Unit", MMan)
            .AddInput(PHeaSin, 15)
            .AddInput(PHigSpeCon, 7.5f)
            .AddInput(PQuaCry, 45)
            .AddOutput(PRadConUni, 3.8f).Build();
        e.GetRecipeBuilder("Radio Control System", MMan)
            .AddInput(PCryOsc, 1.5f)
            .AddInput(PCirBoa, 15)
            .AddInput(PAluCas, 90)
            .AddInput(PRub, 45)
            .AddOutput(PRadConUni, 4.5f).Build();
        e.GetRecipeBuilder("Radio Control Unit", MMan)
            .AddInput(PAluCas, 40)
            .AddInput(PCryOsc, 1.25f)
            .AddInput(PCom, 1.25f)
            .AddOutput(PRadConUni, 2.5f).Build();
        e.GetRecipeBuilder("Rifle Cartridge", MMan)
            .AddInput(PBea, 3)
            .AddInput(PSteBea, 30)
            .AddInput(PBlaPow, 30)
            .AddInput(PRub, 30)
            .AddOutput(PRifAmm, 15).Build();
        e.GetRecipeBuilder("Rigour Motor", MMan)
            .AddInput(PRot, 3.75f)
            .AddInput(PSta, 3.75f)
            .AddInput(PCryOsc, 1.25f)
            .AddOutput(PMot, 7.5f).Build();
        e.GetRecipeBuilder("Seismic Nobelisk", MMan)
            .AddInput(PBlaPow, 12)
            .AddInput(PStePip, 12)
            .AddInput(PCryOsc, 1.5f)
            .AddOutput(PNob, 6).Build();
        e.GetRecipeBuilder("Silicon High-Speed Connector", MMan)
            .AddInput(PQui, 90)
            .AddInput(PSil, 37.5f)
            .AddInput(PCirBoa, 3)
            .AddOutput(PHigSpeCon, 3).Build();
        e.GetRecipeBuilder("Super-State Computer", MMan)
            .AddInput(PCom, 3.6f)
            .AddInput(PEleConRod, 2.4f)
            .AddInput(PBat, 24)
            .AddInput(PWir, 54)
            .AddOutput(PSup, 2.4f).Build();
        e.GetRecipeBuilder("Supercomputer", MMan)
            .AddInput(PCom, 3.75f)
            .AddInput(PAiLim, 3.75f)
            .AddInput(PHigSpeCon, 5.625f)
            .AddInput(PPla, 52.5f)
            .AddOutput(PSup, 1.9f).Build();
        e.GetRecipeBuilder("Thermal Propulsion Rocket", MMan)
            .AddInput(PModEng, 2.5f)
            .AddInput(PTurMot, 1)
            .AddInput(PCooSys, 3)
            .AddInput(PFusModFra, 1)
            .AddOutput(PTheProRoc, 1).Build();
        e.GetRecipeBuilder("Turbo Electric Motor", MMan)
            .AddInput(PMot, 6.5625f)
            .AddInput(PRadConUni, 8.4375f)
            .AddInput(PEleConRod, 4.6875f)
            .AddInput(PRot, 6.5625f)
            .AddOutput(PTurMot, 2.8f).Build();
        e.GetRecipeBuilder("Turbo Motor", MMan)
            .AddInput(PCooSys, 7.5f)
            .AddInput(PRadConUni, 3.75f)
            .AddInput(PMot, 7.5f)
            .AddInput(PRub, 45)
            .AddOutput(PTurMot, 1.9f).Build();
        e.GetRecipeBuilder("Turbo Pressure Motor", MMan)
            .AddInput(PMot, 7.5f)
            .AddInput(PPreConCub, 1.875f)
            .AddInput(PPacNitGas, 45)
            .AddInput(PSta, 15)
            .AddOutput(PTurMot, 3.8f).Build();
        e.GetRecipeBuilder("Turbo Rifle Ammo", MMan)
            .AddInput(PRifAmm, 125)
            .AddInput(PAluCas, 15)
            .AddInput(PPacTur, 15)
            .AddOutput(PTurRifAmm, 250).Build();
        e.GetRecipeBuilder("Uranium Fuel Rod", MMan)
            .AddInput(PEncUraCel, 20)
            .AddInput(PEncIndBea, 1.2f)
            .AddInput(PEleConRod, 2)
            .AddOutput(PUraFueRod, 0.4f).Build();
        e.GetRecipeBuilder("Uranium Fuel Unit", MMan)
            .AddInput(PEncUraCel, 20)
            .AddInput(PEleConRod, 2)
            .AddInput(PCryOsc, 0.6f)
            .AddInput(PBea, 1.2f)
            .AddOutput(PUraFueRod, 0.6f).Build();
        
        // Packager Recipes
        e.GetRecipeBuilder("Packaged Alumina Solution", MPac)
            .AddInput(PAluSol, 120)
            .AddInput(PEmpCan, 120)
            .AddOutput(PPacAluSol, 120).Build();
        e.GetRecipeBuilder("Packaged Fuel", MPac)
            .AddInput(PFue, 40)
            .AddInput(PEmpCan, 40)
            .AddOutput(PPacFue, 40).Build();
        e.GetRecipeBuilder("Packaged Heavy Oil Residue", MPac)
            .AddInput(PHeaOilRes, 30)
            .AddInput(PEmpCan, 30)
            .AddOutput(PPacHeaOilRes, 30).Build();
        e.GetRecipeBuilder("Packaged Liquid Biofuel", MPac)
            .AddInput(PLiqBio, 40)
            .AddInput(PEmpCan, 40)
            .AddOutput(PPacLiqBio, 40).Build();
        e.GetRecipeBuilder("Packaged Nitric Acid", MPac)
            .AddInput(PNitAci, 30)
            .AddInput(PEmpCan, 30)
            .AddOutput(PPacNitAci, 30).Build();
        e.GetRecipeBuilder("Packaged Nitrogen Gas", MPac)
            .AddInput(PNitGas, 240)
            .AddInput(PEmpCan, 60)
            .AddOutput(PPacNitGas, 60).Build();
        e.GetRecipeBuilder("Packaged Oil", MPac)
            .AddInput(PCruOil, 30)
            .AddInput(PEmpCan, 30)
            .AddOutput(PPacCruOil, 30).Build();
        e.GetRecipeBuilder("Packaged Sulfuric Acid", MPac)
            .AddInput(PSulAci, 40)
            .AddInput(PEmpCan, 40)
            .AddOutput(PPacSulAci, 40).Build();
        e.GetRecipeBuilder("Packaged Turbofuel", MPac)
            .AddInput(PTur, 20)
            .AddInput(PEmpCan, 20)
            .AddOutput(PPacTur, 20).Build();
        e.GetRecipeBuilder("Packaged Water", MPac)
            .AddInput(PWat, 60)
            .AddInput(PEmpCan, 60)
            .AddOutput(PPacWat, 60).Build();
        e.GetRecipeBuilder("Unpackaged Alumina Solution", MPac)
            .AddInput(PPacAluSol, 120)
            .AddOutput(PAluSol, 120)
            .AddOutput(PEmpCan, 120).Build();
        e.GetRecipeBuilder("Unpackaged Fuel", MPac)
            .AddInput(PPacFue, 60)
            .AddOutput(PFue, 60)
            .AddOutput(PEmpCan, 60).Build();
        e.GetRecipeBuilder("Unpackaged Heavy Oil Residue", MPac)
            .AddInput(PPacHeaOilRes, 20)
            .AddOutput(PHeaOilRes, 20)
            .AddOutput(PEmpCan, 20).Build();
        e.GetRecipeBuilder("Unpackaged Liquid Biofuel", MPac)
            .AddInput(PPacLiqBio, 60)
            .AddOutput(PLiqBio, 60)
            .AddOutput(PEmpCan, 60).Build();
        e.GetRecipeBuilder("Unpackaged Nitric Acid", MPac)
            .AddInput(PPacNitAci, 20)
            .AddOutput(PNitAci, 20)
            .AddOutput(PEmpCan, 20).Build();
        e.GetRecipeBuilder("Unpackaged Nitrogen Gas", MPac)
            .AddInput(PPacNitGas, 60)
            .AddOutput(PNitGas, 240)
            .AddOutput(PEmpCan, 60).Build();
        e.GetRecipeBuilder("Unpackaged Oil", MPac)
            .AddInput(PPacCruOil, 60)
            .AddOutput(PCruOil, 60)
            .AddOutput(PEmpCan, 60).Build();
        e.GetRecipeBuilder("Unpackaged Sulfuric Acid", MPac)
            .AddInput(PPacSulAci, 60)
            .AddOutput(PSulAci, 60)
            .AddOutput(PEmpCan, 60).Build();
        e.GetRecipeBuilder("Unpackaged Turbofuel", MPac)
            .AddInput(PPacTur, 20)
            .AddOutput(PTur, 20)
            .AddOutput(PEmpCan, 20).Build();
        e.GetRecipeBuilder("Unpackaged Water", MPac)
            .AddInput(PPacWat, 120)
            .AddOutput(PWat, 120)
            .AddOutput(PEmpCan, 120).Build();

        // Refinery Recipes
        e.GetRecipeBuilder("Alumina Solution", MRef)
            .AddInput(PBau, 120)
            .AddInput(PWat, 180)
            .AddOutput(PAluSol, 120)
            .AddOutput(PSil, 50).Build();
        e.GetRecipeBuilder("Aluminum Scrap", MRef)
            .AddInput(PAluSol, 240)
            .AddInput(PCoa, 120)
            .AddOutput(PAluScr, 360)
            .AddOutput(PWat, 120).Build();
        e.GetRecipeBuilder("Coated Cable", MRef)
            .AddInput(PWir, 37.5f)
            .AddInput(PHeaOilRes, 15)
            .AddOutput(PCab, 67.5f).Build();
        e.GetRecipeBuilder("Diluted Packaged Fuel", MRef)
            .AddInput(PHeaOilRes, 30)
            .AddInput(PPacWat, 60)
            .AddOutput(PPacFue, 60).Build();
        e.GetRecipeBuilder("Electrode - Aluminum Scrap", MRef)
            .AddInput(PAluSol, 180)
            .AddInput(PPetCok, 60)
            .AddOutput(PAluScr, 300)
            .AddOutput(PWat, 105).Build();
        e.GetRecipeBuilder("Fuel", MRef)
            .AddInput(PCruOil, 60)
            .AddOutput(PFue, 40)
            .AddOutput(PPolRes, 30).Build();
        e.GetRecipeBuilder("Heavy Oil Residue", MRef)
            .AddInput(PCruOil, 30)
            .AddOutput(PHeaOilRes, 40)
            .AddOutput(PPolRes, 20).Build();
        e.GetRecipeBuilder("Liquid Biofuel", MRef)
            .AddInput(PSolBio, 90)
            .AddInput(PWat, 45)
            .AddOutput(PLiqBio, 60).Build();
        e.GetRecipeBuilder("Petroleum Coke", MRef)
            .AddInput(PHeaOilRes, 40)
            .AddOutput(PPetCok, 120).Build();
        e.GetRecipeBuilder("Plastic", MRef)
            .AddInput(PCruOil, 30)
            .AddOutput(PPla, 20)
            .AddOutput(PHeaOilRes, 10).Build();
        e.GetRecipeBuilder("Polyester Fabric", MRef)
            .AddInput(PPolRes, 30)
            .AddInput(PWat, 30)
            .AddOutput(PFab, 30).Build();
        e.GetRecipeBuilder("Polyester Fabric 2", MRef)
            .AddInput(PPolRes, 80)
            .AddInput(PWat, 50)
            .AddOutput(PFab, 5).Build();
        e.GetRecipeBuilder("Polymer Resin", MRef)
            .AddInput(PCruOil, 60)
            .AddOutput(PPolRes, 130)
            .AddOutput(PHeaOilRes, 20).Build();
        e.GetRecipeBuilder("Pure Caterium", MRef)
            .AddInput(PCatOre, 24)
            .AddInput(PWat, 24)
            .AddOutput(PCatIng, 12).Build();
        e.GetRecipeBuilder("Pure Copper Ingot", MRef)
            .AddInput(PCopOre, 15)
            .AddInput(PWat, 10)
            .AddOutput(PCopIng, 37.5f).Build();
        e.GetRecipeBuilder("Pure Iron Ingot", MRef)
            .AddInput(PIroOre, 35)
            .AddInput(PWat, 20)
            .AddOutput(PIroIng, 65).Build();
        e.GetRecipeBuilder("Pure Quartz Crystal", MRef)
            .AddInput(PRawQua, 67.5f)
            .AddInput(PWat, 37.5f)
            .AddOutput(PQuaCry, 52.5f).Build();
        e.GetRecipeBuilder("Recycled Plastic", MRef)
            .AddInput(PRub, 30)
            .AddInput(PFue, 30)
            .AddOutput(PPla, 60).Build();
        e.GetRecipeBuilder("Recycled Rubber", MRef)
            .AddInput(PPla, 30)
            .AddInput(PFue, 30)
            .AddOutput(PRub, 60).Build();
        e.GetRecipeBuilder("Residual Fuel", MRef)
            .AddInput(PHeaOilRes, 60)
            .AddOutput(PFue, 40).Build();
        e.GetRecipeBuilder("Residual Plastic", MRef)
            .AddInput(PPolRes, 60)
            .AddInput(PWat, 20)
            .AddOutput(PPla, 20).Build();
        e.GetRecipeBuilder("Residual Rubber", MRef)
            .AddInput(PPolRes, 40)
            .AddInput(PWat, 40)
            .AddOutput(PRub, 20).Build();
        e.GetRecipeBuilder("Rubber", MRef)
            .AddInput(PCruOil, 30)
            .AddOutput(PRub, 20)
            .AddOutput(PHeaOilRes, 20).Build();
        e.GetRecipeBuilder("Sloppy Alumina", MRef)
            .AddInput(PBau, 200)
            .AddInput(PWat, 200)
            .AddOutput(PAluSol, 240).Build();
        e.GetRecipeBuilder("Smokeless Powder", MRef)
            .AddInput(PBlaPow, 20)
            .AddInput(PHeaOilRes, 10)
            .AddOutput(PSmoPow, 20).Build();
        e.GetRecipeBuilder("Steamed Copper Sheet", MRef)
            .AddInput(PCopIng, 22.5f)
            .AddInput(PWat, 22.5f)
            .AddOutput(PCopShe, 22.5f).Build();
        e.GetRecipeBuilder("Sulfuric Acid", MRef)
            .AddInput(PSul, 50)
            .AddInput(PWat, 50)
            .AddOutput(PSulAci, 50).Build();
        e.GetRecipeBuilder("Turbo Heavy Fuel", MRef)
            .AddInput(PHeaOilRes, 37.5f)
            .AddInput(PComCoa, 30)
            .AddOutput(PTur, 30).Build();
        e.GetRecipeBuilder("Turbofuel", MRef)
            .AddInput(PFue, 22.5f)
            .AddInput(PComCoa, 15)
            .AddOutput(PTur, 18.8f).Build();
        e.GetRecipeBuilder("Wet Concrete", MRef)
            .AddInput(PLim, 120)
            .AddInput(PWat, 100)
            .AddOutput(PCon, 80).Build();
        
        // Blender Recipes
        e.GetRecipeBuilder("Battery", MBle)
            .AddInput(PSulAci, 50)
            .AddInput(PAluSol, 40)
            .AddInput(PAluCas, 20)
            .AddOutput(PBat, 20)
            .AddOutput(PWat, 30).Build();
        e.GetRecipeBuilder("Cooling Device", MBle)
            .AddInput(PHeaSin, 9.375f)
            .AddInput(PMot, 1.875f)
            .AddInput(PNitGas, 45)
            .AddOutput(PCooSys, 3.8f).Build();
        e.GetRecipeBuilder("Cooling System", MBle)
            .AddInput(PHeaSin, 12)
            .AddInput(PRub, 12)
            .AddInput(PWat, 30)
            .AddInput(PNitGas, 150)
            .AddOutput(PCooSys, 6).Build();
        e.GetRecipeBuilder("Diluted Fuel", MBle)
            .AddInput(PHeaOilRes, 50)
            .AddInput(PWat, 100)
            .AddOutput(PFue, 100).Build();
        e.GetRecipeBuilder("Encased Uranium Cell", MBle)
            .AddInput(PUra, 50)
            .AddInput(PCon, 15)
            .AddInput(PSulAci, 40)
            .AddOutput(PEncUraCel, 25)
            .AddOutput(PSulAci, 10).Build();
        e.GetRecipeBuilder("Fertile Uranium", MBle)
            .AddInput(PUra, 25)
            .AddInput(PUraWas, 25)
            .AddInput(PNitAci, 15)
            .AddInput(PSulAci, 25)
            .AddOutput(PNonUra, 100)
            .AddOutput(PWat, 40).Build();
        e.GetRecipeBuilder("Fused Modular Frame", MBle)
            .AddInput(PHeaModFra, 1.5f)
            .AddInput(PAluCas, 75)
            .AddInput(PNitGas, 37.5f)
            .AddOutput(PFusModFra, 1.5f).Build();
        e.GetRecipeBuilder("Heat-Fused Frame", MBle)
            .AddInput(PHeaModFra, 3)
            .AddInput(PAluIng, 150)
            .AddInput(PNitAci, 24)
            .AddInput(PFue, 30)
            .AddOutput(PFusModFra, 3).Build();
        e.GetRecipeBuilder("Instant Scrap", MBle)
            .AddInput(PBau, 150)
            .AddInput(PCoa, 100)
            .AddInput(PSulAci, 50)
            .AddInput(PWat, 60)
            .AddOutput(PAluScr, 300)
            .AddOutput(PWat, 50).Build();
        e.GetRecipeBuilder("Nitrac Acid", MBle)
            .AddInput(PNitGas, 120)
            .AddInput(PWat, 30)
            .AddInput(PIroPla, 10)
            .AddOutput(PNitAci, 30).Build();
        e.GetRecipeBuilder("Non-fissle Uranium", MBle)
            .AddInput(PUraWas, 37.5f)
            .AddInput(PSil, 25)
            .AddInput(PNitAci, 15)
            .AddInput(PSulAci, 15)
            .AddOutput(PNonUra, 50)
            .AddOutput(PWat, 15).Build();
        e.GetRecipeBuilder("Turbo Blend Fuel", MBle)
            .AddInput(PFue, 15)
            .AddInput(PHeaOilRes, 30)
            .AddInput(PSul, 22.5f)
            .AddInput(PPetCok, 22.5f)
            .AddOutput(PTur, 45).Build();
        e.GetRecipeBuilder("Turbo Rifle Ammo Blend", MBle)
            .AddInput(PRifAmm, 125)
            .AddInput(PAluCas, 15)
            .AddInput(PTur, 15)
            .AddOutput(PTurRifAmm, 250).Build();
        
        // Particle Accelerator Recipes
        e.GetRecipeBuilder("Instant Plutonium Cell", MParAcc)
            .AddInput(PNonUra, 75)
            .AddInput(PAluCas, 10)
            .AddOutput(PEncPluCel, 10).Build();
        e.GetRecipeBuilder("Nuclear Pasta", MParAcc)
            .AddInput(PCopPow, 100)
            .AddInput(PPreConCub, 0.5f)
            .AddOutput(PNucPas, 0.5f).Build();
        e.GetRecipeBuilder("Plutonium Pellet", MParAcc)
            .AddInput(PNonUra, 100)
            .AddInput(PUraWas, 25)
            .AddOutput(PPluPel, 30).Build();
    }
}