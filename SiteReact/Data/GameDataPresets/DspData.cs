using productionCalculatorLib.components.entityContainer;

namespace SiteReact.Data.GameDataPresets;

public static class DspData
{
    public static void AddData(EntityContainer e)
    {
        // Natural Resources
        var pCoa = e.GetOrGenerateProduct("Coal");
        var pCopOre = e.GetOrGenerateProduct("Copper Ore");
        var pCruOil = e.GetOrGenerateProduct("Crude Oil");
        var pDeu = e.GetOrGenerateProduct("Deuterium");
        var pFirIce = e.GetOrGenerateProduct("Fire Ice");
        var pFraSil = e.GetOrGenerateProduct("Fractal Silicon");
        var pHyd = e.GetOrGenerateProduct("Hydrogen");
        var pIroOre = e.GetOrGenerateProduct("Iron Ore");
        var pKimOre = e.GetOrGenerateProduct("Kimberlite Ore");
        var pLog = e.GetOrGenerateProduct("Log");
        var pOptGraCry = e.GetOrGenerateProduct("Optical Grating Crystal");
        var pOrgCry = e.GetOrGenerateProduct("Organic Crystal");
        var pPlaFue = e.GetOrGenerateProduct("Plant Fuel");
        var pSilOre = e.GetOrGenerateProduct("Silicon Ore");
        var pSpiStaCry = e.GetOrGenerateProduct("Spiniform Stalagmite Crystal");
        var pSto = e.GetOrGenerateProduct("Stone");
        var pSulAci = e.GetOrGenerateProduct("Sulfuric Acid");
        var pTitOre = e.GetOrGenerateProduct("Titanium Ore");
        var pUniMag = e.GetOrGenerateProduct("Unipolar Magnet");
        var pWat = e.GetOrGenerateProduct("Water");
        
        // Intermediate Products
        var pAnnConSph = e.GetOrGenerateProduct("Annihilation Constraint Sphere");
        var pAnt = e.GetOrGenerateProduct("Antimatter");
        var pCarNan = e.GetOrGenerateProduct("Carbon Nanotube");
        var pCasCry = e.GetOrGenerateProduct("Casimir Crystal");
        var pCirBoa = e.GetOrGenerateProduct("Circuit Board");
        var pCriPho = e.GetOrGenerateProduct("Critical Photon");
        var pCrySil = e.GetOrGenerateProduct("Crystal Silicon");
        var pCopIng = e.GetOrGenerateProduct("Copper Ingot");
        var pDia = e.GetOrGenerateProduct("Diamond");
        var pDysSphCom = e.GetOrGenerateProduct("Dyson Sphere Component");
        var pEleMot = e.GetOrGenerateProduct("Electric Motor");
        var pEleTur = e.GetOrGenerateProduct("Electromagnetic Turbine");
        var pEneGra = e.GetOrGenerateProduct("Energetic Graphite");
        var pFraMat = e.GetOrGenerateProduct("Frame Material");
        var pGea = e.GetOrGenerateProduct("Gear");
        var pGla = e.GetOrGenerateProduct("Glass");
        var pGra = e.GetOrGenerateProduct("Graphene");
        var pGraLen = e.GetOrGenerateProduct("Graviton Lens");
        var pHigPurSil = e.GetOrGenerateProduct("High-Purity Silicon");
        var pIroIng = e.GetOrGenerateProduct("Iron Ingot");
        var pMag = e.GetOrGenerateProduct("Magnet");
        var pMagCoi = e.GetOrGenerateProduct("Magnetic Coil");
        var pMicCom = e.GetOrGenerateProduct("Microcrystalline Component");
        var pPlaFil = e.GetOrGenerateProduct("Plane Filter");
        var pPlaExc = e.GetOrGenerateProduct("Plasma Exciter");
        var pPla = e.GetOrGenerateProduct("Plastic");
        var pPri = e.GetOrGenerateProduct("Prism");
        var pPro = e.GetOrGenerateProduct("Processor");
        var pProMkI = e.GetOrGenerateProduct("Proliferator Mk.I");
        var pProMkII = e.GetOrGenerateProduct("Proliferator Mk.II");
        var pProMkIII = e.GetOrGenerateProduct("Proliferator Mk.III");
        var pQuaChi = e.GetOrGenerateProduct("Quantum Chip");
        var pReiThr = e.GetOrGenerateProduct("Reinforced Thruster");
        var pParBro = e.GetOrGenerateProduct("Particle Broadband");
        var pParCon = e.GetOrGenerateProduct("Particle Container");
        var pPhoCom = e.GetOrGenerateProduct("Photon Combiner");
        var pRefOil = e.GetOrGenerateProduct("Refined Oil");
        var pSte = e.GetOrGenerateProduct("Steel");
        var pStoBri = e.GetOrGenerateProduct("Stone Brick");
        var pStraMat = e.GetOrGenerateProduct("Strange Matter");
        var pSupMagRin = e.GetOrGenerateProduct("Super-Magnetic Ring");
        var pTitAll = e.GetOrGenerateProduct("Titanium Alloy");
        var pTitCry = e.GetOrGenerateProduct("Titanium Crystal");
        var pTitGla = e.GetOrGenerateProduct("Titanium Glass");
        var pTitIng = e.GetOrGenerateProduct("Titanium Ingot");
        var pThr = e.GetOrGenerateProduct("Thruster");
        
        // Consumables
        var pAntFueRod = e.GetOrGenerateProduct("Antimatter Fuel Rod");
        var pDeuFueRod = e.GetOrGenerateProduct("Deuteron Fuel Rod");
        var pFou = e.GetOrGenerateProduct("Foundation");
        var pHydFueRod = e.GetOrGenerateProduct("Hydrogen Fuel Rod");
        var pSpaWar = e.GetOrGenerateProduct("Space Warper");
        
        // Science
        var pEleMat = e.GetOrGenerateProduct("Electromagnetic Matrix");
        var pEneMat = e.GetOrGenerateProduct("Energy Matrix");
        var pStruMat = e.GetOrGenerateProduct("Structure Matrix");
        var pInfMat = e.GetOrGenerateProduct("Information Matrix");
        var pGraMat = e.GetOrGenerateProduct("Gravity Matrix");
        var pUniMat = e.GetOrGenerateProduct("Universe Matrix");
        
        // Building Consumables
        var pLogDro = e.GetOrGenerateProduct("Logistic Drone");
        var pLogVes = e.GetOrGenerateProduct("Logistic Vessel");
        var pLogBot = e.GetOrGenerateProduct("Logistics Bot");
        var pSmaCarRoc = e.GetOrGenerateProduct("Small Carrier Rocket");
        var pSolSai = e.GetOrGenerateProduct("Solar Sail");
        
        // Power
        var pAcc = e.GetOrGenerateProduct("Accumulator");
        var pArtSta = e.GetOrGenerateProduct("Artificial Star");
        var pEneExc = e.GetOrGenerateProduct("Energy Exchanger");
        var pFulAcc = e.GetOrGenerateProduct("Full Accumulator");
        var pGeoPowSta = e.GetOrGenerateProduct("Geothermal Power Station");
        var pMinFusPowPla = e.GetOrGenerateProduct("Mini Fusion Power Plant");
        var pSatSub = e.GetOrGenerateProduct("Satellite Substation");
        var pSolPan = e.GetOrGenerateProduct("Solar Panel");
        var pTesTow = e.GetOrGenerateProduct("Tesla Tower");
        var pThePowPla = e.GetOrGenerateProduct("Thermal Power Plant");
        var pWinTur = e.GetOrGenerateProduct("Wind Turbine");
        var pWirPowTow = e.GetOrGenerateProduct("Wireless Power Tower");
        
        // Mining
        var pAdvMinMac = e.GetOrGenerateProduct("Advanced Mining Machine");
        var pMinMac = e.GetOrGenerateProduct("Mining Machine");
        var pOilExt = e.GetOrGenerateProduct("Oil Extractor");
        var pOrbCol = e.GetOrGenerateProduct("Orbital Collector");
        var pWatPum = e.GetOrGenerateProduct("Water Pump");
        
        // Logistics
        var pAutPil = e.GetOrGenerateProduct("Automatic Piler");
        var pConBelMk1 = e.GetOrGenerateProduct("Conveyor Belt Mk.I");
        var pConBelMk2 = e.GetOrGenerateProduct("Conveyor Belt Mk.II");
        var pConBelMk3 = e.GetOrGenerateProduct("Conveyor Belt Mk.III");
        var pSor1 = e.GetOrGenerateProduct("Sorter Mk.I");
        var pSor2 = e.GetOrGenerateProduct("Sorter Mk.II");
        var pSor3 = e.GetOrGenerateProduct("Sorter Mk.III");
        var pSpl = e.GetOrGenerateProduct("Splitter");
        var pTraMon = e.GetOrGenerateProduct("Traffic Monitor");
        var pStoMk1 = e.GetOrGenerateProduct("Storage Mk.I");
        var pStoMk2 = e.GetOrGenerateProduct("Storage Mk.II");
        var pStoTan = e.GetOrGenerateProduct("Storage Tank");
        var pPlaLogSta = e.GetOrGenerateProduct("Planetary Logistics Station");
        var pIntLogSta = e.GetOrGenerateProduct("Interstellar Logistics Station");
        var pArcSme = e.GetOrGenerateProduct("Arc Smelter");
        var pPlaSme = e.GetOrGenerateProduct("Plane Smelter");
        var pAssMacMk1 = e.GetOrGenerateProduct("Assembling Machine Mk.I");
        var pAssMacMk2 = e.GetOrGenerateProduct("Assembling Machine Mk.II");
        var pAssMacMk3 = e.GetOrGenerateProduct("Assembling Machine Mk.III");
        var pSprCoa = e.GetOrGenerateProduct("Spray Coater");
        var pOilRef = e.GetOrGenerateProduct("Oil Refinery");
        var pChePla = e.GetOrGenerateProduct("Chemical Plant");
        var pFra = e.GetOrGenerateProduct("Fractionator");
        var pMinParCol = e.GetOrGenerateProduct("Miniature Particle Collider");
        
        // Dyson Sphere
        var pEMRailEje = e.GetOrGenerateProduct("EM-Rail Ejector");
        var pMatLab = e.GetOrGenerateProduct("Matrix Lab");
        var pRayRec = e.GetOrGenerateProduct("Ray Receiver");
        var pVerLauSil = e.GetOrGenerateProduct("Vertical Launching Silo");
        
        // Smelting
        e.GetRecipeBuilder("Iron Ingot")
            .AddInput(pIroOre, 1)
            .AddOutput(pIroIng, 1).Build();
        e.GetRecipeBuilder("Copper Ingot")
            .AddInput(pCopOre, 1)
            .AddOutput(pCopIng, 1).Build();
        e.GetRecipeBuilder("Magnet")
            .AddInput(pIroOre, 2/3f)
            .AddOutput(pMag, 2/3f).Build();
        e.GetRecipeBuilder("Steel")
            .AddInput(pIroIng, 1)
            .AddOutput(pSte, 1/3f).Build();
        e.GetRecipeBuilder("Stone Brick")
            .AddInput(pSto, 1)
            .AddOutput(pStoBri, 1).Build();
        e.GetRecipeBuilder("Glass")
            .AddInput(pSto, 1)
            .AddOutput(pGla, 0.5f).Build();
        e.GetRecipeBuilder("Energetic Graphite")
            .AddInput(pCoa, 1)
            .AddOutput(pEneGra, 0.5f).Build();
        e.GetRecipeBuilder("Silicon Ore")
            .AddInput(pSto, 1)
            .AddOutput(pSilOre, 0.1f).Build();
        e.GetRecipeBuilder("High-Purity Silicon")
            .AddInput(pSilOre, 1)
            .AddOutput(pHigPurSil, 0.5f).Build();
        e.GetRecipeBuilder("Crystal Silicon")
            .AddInput(pHigPurSil, 0.5f)
            .AddOutput(pCrySil, 0.5f).Build();
        e.GetRecipeBuilder("Diamond")
            .AddInput(pEneGra, 0.5f)
            .AddOutput(pDia, 0.5f).Build();
        e.GetRecipeBuilder("Diamond (advanced)")
            .AddInput(pKimOre, 2/3f)
            .AddOutput(pDia, 4/3f).Build();
        e.GetRecipeBuilder("Titanium")
            .AddInput(pTitOre, 1)
            .AddOutput(pTitIng, 0.5f).Build();
        e.GetRecipeBuilder("Titanium Alloy")
            .AddInput(pSulAci, 2/3f)
            .AddInput(pSte, 1/3f)
            .AddInput(pTitIng, 1/3f)
            .AddOutput(pTitAll, 1/3f).Build();
        
        // Assembling
        e.GetRecipeBuilder("Gear")
            .AddInput(pIroIng, 1)
            .AddOutput(pGea, 1).Build();
        e.GetRecipeBuilder("Plasma Exciter")
            .AddInput(pMagCoi, 2)
            .AddInput(pPri, 1)
            .AddOutput(pPlaExc, 0.5f).Build();
        e.GetRecipeBuilder("Photon Combiner")
            .AddInput(pCirBoa, 1/3f)
            .AddInput(pPri, 2/3f)
            .AddOutput(pPhoCom, 1/3f).Build();
        e.GetRecipeBuilder("Photon Combiner (advanced)")
            .AddInput(pCirBoa, 1/3f)
            .AddInput(pOptGraCry, 1/3f)
            .AddOutput(pPhoCom, 1/3f).Build();
        e.GetRecipeBuilder("Super-magnetic Ring")
            .AddInput(pEneGra, 1/3f)
            .AddInput(pMag, 1)
            .AddInput(pEleTur, 2/3f)
            .AddOutput(pSupMagRin, 1/3f).Build();
        e.GetRecipeBuilder("Electromagnetic Turbine")
            .AddInput(pMagCoi, 1)
            .AddInput(pEleMot, 1)
            .AddOutput(pEleTur, 0.5f).Build();
        e.GetRecipeBuilder("Electric Motor")
            .AddInput(pIroIng, 1)
            .AddInput(pGea, 0.5f)
            .AddInput(pMagCoi, 0.5f)
            .AddOutput(pEleMot, 0.5f).Build();
        e.GetRecipeBuilder("Magnetic Coil")
            .AddInput(pMag, 2)
            .AddInput(pCopIng, 1)
            .AddOutput(pMagCoi, 2).Build();
        e.GetRecipeBuilder("Crystal Silicon (advanced)")
            .AddInput(pFraSil, 2/3f)
            .AddOutput(pCrySil, 4/3f).Build();
        e.GetRecipeBuilder("Particle Broadband")
            .AddInput(pPla, 0.125f)
            .AddInput(pCrySil, 0.25f)
            .AddInput(pCarNan, 0.25f)
            .AddOutput(pParBro, 0.125f).Build();
        e.GetRecipeBuilder("Microcrystalline Component")
            .AddInput(pCopIng, 0.5f)
            .AddInput(pHigPurSil, 1)
            .AddOutput(pMicCom, 0.5f).Build();
        e.GetRecipeBuilder("Titanium Glass")
            .AddInput(pWat, 0.4f)
            .AddInput(pTitIng, 0.4f)
            .AddInput(pGla, 0.4f)
            .AddOutput(pTitGla, 0.4f).Build();
        e.GetRecipeBuilder("Circuit Board")
            .AddInput(pCopIng, 1)
            .AddInput(pIroIng, 2)
            .AddOutput(pCirBoa, 2).Build();
        e.GetRecipeBuilder("Processor")
            .AddInput(pMicCom, 2/3f)
            .AddInput(pCirBoa, 2/3f)
            .AddOutput(pPro, 1/3f).Build();
        e.GetRecipeBuilder("Quantum Chip")
            .AddInput(pPro, 1/3f)
            .AddInput(pPlaFil, 1/3f)
            .AddOutput(pQuaChi, 1/6f).Build();
        e.GetRecipeBuilder("Prism")
            .AddInput(pGla, 1.5f)
            .AddOutput(pPri, 1).Build();
        e.GetRecipeBuilder("Graviton Lens")
            .AddInput(pDia, 2/3f)
            .AddInput(pStraMat, 1/6f)
            .AddOutput(pGraLen, 1/6f).Build();
        e.GetRecipeBuilder("Casimir Crystal")
            .AddInput(pHyd, 3)
            .AddInput(pGra, 0.5f)
            .AddInput(pDia, 0.25f)
            .AddOutput(pCasCry, 0.25f).Build();
        e.GetRecipeBuilder("Casimir Crystal (advanced)")
            .AddInput(pHyd, 3)
            .AddInput(pGra, 0.5f)
            .AddInput(pOptGraCry, 2)
            .AddOutput(pCasCry, 0.25f).Build();
        e.GetRecipeBuilder("Particle Container")
            .AddInput(pCopIng, 0.5f)
            .AddInput(pGra, 0.5f)
            .AddInput(pEleTur, 0.5f)
            .AddOutput(pParCon, 0.25f).Build();
        e.GetRecipeBuilder("Particle Container (advanced)")
            .AddInput(pCopIng, 0.5f)
            .AddInput(pUniMag, 2.5f)
            .AddOutput(pParCon, 0.25f).Build();
        e.GetRecipeBuilder("Space Warper")
            .AddInput(pGraLen, 0.1f)
            .AddOutput(pSpaWar, 0.1f).Build();
        e.GetRecipeBuilder("Space Warper (advanced)")
            .AddInput(pGraMat, 0.1f)
            .AddOutput(pSpaWar, 0.8f).Build();
        e.GetRecipeBuilder("Titanium Crystal")
            .AddInput(pTitIng, 0.75f)
            .AddInput(pOrgCry, 0.25f)
            .AddOutput(pTitCry, 0.25f).Build();
        e.GetRecipeBuilder("Organic Crystal (original)")
            .AddInput(pLog, 10/3f)
            .AddInput(pPlaFue, 5)
            .AddInput(pWat, 5/3f)
            .AddOutput(pOrgCry, 1/6f).Build();
        e.GetRecipeBuilder("Plane Filter")
            .AddInput(pTitGla, 1/6f)
            .AddInput(pCasCry, 1/12f)
            .AddOutput(pPlaFil, 1/12f).Build();
        e.GetRecipeBuilder("Solar Sail")
            .AddInput(pGra, 0.25f)
            .AddInput(pPhoCom, 0.25f)
            .AddOutput(pSolSai, 0.5f).Build();
        e.GetRecipeBuilder("Frame Material")
            .AddInput(pHigPurSil, 1/6f)
            .AddInput(pTitAll, 1/6f)
            .AddInput(pCarNan, 2/3f)
            .AddOutput(pFraMat, 1/6f).Build();
        e.GetRecipeBuilder("Dyson Sphere Component")
            .AddInput(pPro, 0.375f)
            .AddInput(pSolSai, 0.375f)
            .AddInput(pFraMat, 0.375f)
            .AddOutput(pDysSphCom, 0.125f).Build();
        e.GetRecipeBuilder("Small Carrier Rocket")
            .AddInput(pFraMat, 1/3f)
            .AddInput(pDeuFueRod, 2/3f)
            .AddInput(pQuaChi, 1/3f)
            .AddOutput(pSmaCarRoc, 1/6f).Build();
        e.GetRecipeBuilder("Foundation")
            .AddInput(pSte, 1)
            .AddInput(pStoBri, 3)
            .AddOutput(pFou, 1).Build();
        e.GetRecipeBuilder("Annihilation Constraint Sphere")
            .AddInput(pPro, 0.05f)
            .AddInput(pParCon, 0.05f)
            .AddOutput(pAnnConSph, 0.05f).Build();
        e.GetRecipeBuilder("Proliferator Mk.I")
            .AddInput(pCoa, 2)
            .AddOutput(pProMkI, 2).Build();
        e.GetRecipeBuilder("Proliferator Mk.II")
            .AddInput(pProMkI, 2)
            .AddInput(pDia, 1)
            .AddOutput(pProMkII, 1).Build();
        e.GetRecipeBuilder("Proliferator Mk.III")
            .AddInput(pProMkII, 1)
            .AddInput(pCarNan, 0.5f)
            .AddOutput(pProMkIII, 0.5f).Build();
        e.GetRecipeBuilder("Hydrogen Fuel Rod")
            .AddInput(pHyd, 5/3f)
            .AddInput(pTitIng, 1/6f)
            .AddOutput(pHydFueRod, 1/3f).Build();
        e.GetRecipeBuilder("Deuteron Fuel Rod")
            .AddInput(pSupMagRin, 1/12f)
            .AddInput(pDeu, 5/3f)
            .AddInput(pTitAll, 1/12f)
            .AddOutput(pDeuFueRod, 1/6f).Build();
        e.GetRecipeBuilder("Antimatter Fuel Rod")
            .AddInput(pTitAll, 1/24f)
            .AddInput(pAnnConSph, 1/24f)
            .AddInput(pHyd, 0.5f)
            .AddInput(pAnt, 0.5f)
            .AddOutput(pAntFueRod, 1/12f).Build();
        e.GetRecipeBuilder("Thruster")
            .AddInput(pCopIng, 0.75f)
            .AddInput(pSte, 0.5f)
            .AddOutput(pThr, 0.25f).Build();
        e.GetRecipeBuilder("Reinforced Thruster")
            .AddInput(pTitAll, 5/6f)
            .AddInput(pEleTur, 5/6f)
            .AddOutput(pReiThr, 1/6f).Build();
        e.GetRecipeBuilder("Logistics Drone")
            .AddInput(pThr, 0.5f)
            .AddInput(pPro, 0.5f)
            .AddInput(pIroIng, 1.25f)
            .AddOutput(pLogDro, 0.25f).Build();
        e.GetRecipeBuilder("Logistics Vessel")
            .AddInput(pReiThr, 1/3f)
            .AddInput(pPro, 5/3f)
            .AddInput(pTitAll, 5/3f)
            .AddOutput(pLogVes, 1/6f).Build();
        
        // Refinery
        e.GetRecipeBuilder("Plasma Refining")
            .AddInput(pCruOil, 0.5f)
            .AddOutput(pRefOil, 0.5f)
            .AddOutput(pHyd, 0.25f).Build();
        e.GetRecipeBuilder("X-ray Cracking")
            .AddInput(pHyd, 0.5f)
            .AddInput(pRefOil, 0.25f)
            .AddOutput(pEneGra, 0.25f)
            .AddOutput(pHyd, 0.75f).Build();
        e.GetRecipeBuilder("Reforming Refine")
            .AddInput(pCoa, 0.25f)
            .AddInput(pHyd, 0.25f)
            .AddInput(pRefOil, 0.5f)
            .AddOutput(pRefOil, 0.75f).Build();
        
        // Chemical
        e.GetRecipeBuilder("Sulfuric Acid")
            .AddInput(pWat, 2/3f)
            .AddInput(pSto, 4/3f)
            .AddInput(pRefOil, 1)
            .AddOutput(pSulAci, 3/2f).Build();
        e.GetRecipeBuilder("Graphene")
            .AddInput(pEneGra, 1)
            .AddInput(pSulAci, 1/3f)
            .AddOutput(pGra, 2/3f).Build();
        e.GetRecipeBuilder("Graphene (advanced)")
            .AddInput(pFirIce, 1)
            .AddOutput(pHyd, 0.5f)
            .AddOutput(pGra, 1).Build();
        e.GetRecipeBuilder("Carbon Nanotube")
            .AddInput(pTitIng, 0.25f)
            .AddInput(pGra, 0.75f)
            .AddOutput(pCarNan, 0.5f).Build();
        e.GetRecipeBuilder("Carbon Nanotube (advanced)")
            .AddInput(pSpiStaCry, 1.5f)
            .AddOutput(pCarNan, 0.5f).Build();
        e.GetRecipeBuilder("Plastic")
            .AddInput(pGra, 1/3f)
            .AddInput(pRefOil, 2/3f)
            .AddOutput(pPla, 1/3f).Build();
        e.GetRecipeBuilder("Organic Crystal")
            .AddInput(pWat, 1/6f)
            .AddInput(pRefOil, 1/6f)
            .AddInput(pPla, 1/3f)
            .AddOutput(pOrgCry, 1/6f).Build();
        
        // Particle collider
        e.GetRecipeBuilder("Deuterium")
            .AddInput(pHyd, 4)
            .AddOutput(pDeu, 2).Build();
        e.GetRecipeBuilder("Mass-energy Storage")
            .AddInput(pCriPho, 1)
            .AddOutput(pHyd, 1)
            .AddOutput(pAnt, 1).Build();
        e.GetRecipeBuilder("Strange Matter")
            .AddInput(pDeu, 1.25f)
            .AddInput(pIroIng, 0.25f)
            .AddInput(pParCon, 0.25f)
            .AddOutput(pStraMat, 0.125f).Build();
        
        // Matrix
        e.GetRecipeBuilder("Electromagnetic Matrix")
            .AddInput(pCirBoa, 1/3f)
            .AddInput(pMagCoi, 1/3f)
            .AddOutput(pEleMat, 1/3f).Build();
        e.GetRecipeBuilder("Structure Matrix")
            .AddInput(pDia, 0.125f)
            .AddInput(pTitCry, 0.125f)
            .AddOutput(pStruMat, 0.125f).Build();
        e.GetRecipeBuilder("Energy Matrix")
            .AddInput(pEneGra, 1/3f)
            .AddInput(pHyd, 1/3f)
            .AddOutput(pEneMat, 1/6f).Build();
        e.GetRecipeBuilder("Information Matrix")
            .AddInput(pPro, 0.2f)
            .AddInput(pParBro, 0.1f)
            .AddOutput(pInfMat, 0.1f).Build();
        e.GetRecipeBuilder("Gravity Matrix")
            .AddInput(pGraLen, 1/24f)
            .AddInput(pQuaChi, 1/24f)
            .AddOutput(pGraMat, 1/12f).Build();
        e.GetRecipeBuilder("Universe Matrix")
            .AddInput(pEleMat, 1/15f)
            .AddInput(pStruMat, 1/15f)
            .AddInput(pEneMat, 1/15f)
            .AddInput(pInfMat, 1/15f)
            .AddInput(pGraMat, 1/15f)
            .AddInput(pAnt, 1/15f)
            .AddOutput(pUniMat, 1/15f).Build();
    }
}