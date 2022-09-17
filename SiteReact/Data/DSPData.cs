using productionCalculatorLib.components.worksheet;

namespace SiteReact.Data;

public static class DSPData
{
    public static void addData(Worksheet worksheet)
    {
        var a = worksheet.EntityContainer;
        
        // Natural Resources
        var pCoa = a.GetOrGenerateProduct("Coal");
        var pCopOre = a.GetOrGenerateProduct("Copper Ore");
        var pCruOil = a.GetOrGenerateProduct("Crude Oil");
        var pDeu = a.GetOrGenerateProduct("Deuterium");
        var pFirIce = a.GetOrGenerateProduct("Fire Ice");
        var pFraSil = a.GetOrGenerateProduct("Fractal Silicon");
        var pHyd = a.GetOrGenerateProduct("Hydrogen");
        var pIroOre = a.GetOrGenerateProduct("Iron Ore");
        var pKimOre = a.GetOrGenerateProduct("Kimberlite Ore");
        var pLog = a.GetOrGenerateProduct("Log");
        var pOptGraCry = a.GetOrGenerateProduct("Optical Grating Crystal");
        var pOrgCry = a.GetOrGenerateProduct("Organic Crystal");
        var pPlaFue = a.GetOrGenerateProduct("Plant Fuel");
        var pSilOre = a.GetOrGenerateProduct("Silicon Ore");
        var pSpiStaCry = a.GetOrGenerateProduct("Spiniform Stalagmite Crystal");
        var pSto = a.GetOrGenerateProduct("Stone");
        var pSulAci = a.GetOrGenerateProduct("Sulfuric Acid");
        var pTitOre = a.GetOrGenerateProduct("Titanium Ore");
        var pUniMag = a.GetOrGenerateProduct("Unipolar Magnet");
        var pWat = a.GetOrGenerateProduct("Water");
        
        // Intermediate Products
        var pAnnConSph = a.GetOrGenerateProduct("Annihilation Constraint Sphere");
        var pAnt = a.GetOrGenerateProduct("Antimatter");
        var pCarNan = a.GetOrGenerateProduct("Carbon Nanotube");
        var pCasCry = a.GetOrGenerateProduct("Casimir Crystal");
        var pCirBoa = a.GetOrGenerateProduct("Circuit Board");
        var pCriPho = a.GetOrGenerateProduct("Critical Photon");
        var pCrySil = a.GetOrGenerateProduct("Crystal Silicon");
        var pCopIng = a.GetOrGenerateProduct("Copper Ingot");
        var pDia = a.GetOrGenerateProduct("Diamond");
        var pDysSphCom = a.GetOrGenerateProduct("Dyson Sphere Component");
        var pEleMot = a.GetOrGenerateProduct("Electric Motor");
        var pEleTur = a.GetOrGenerateProduct("Electromagnetic Turbine");
        var pEneGra = a.GetOrGenerateProduct("Energetic Graphite");
        var pFraMat = a.GetOrGenerateProduct("Frame Material");
        var pGea = a.GetOrGenerateProduct("Gear");
        var pGla = a.GetOrGenerateProduct("Glass");
        var pGra = a.GetOrGenerateProduct("Graphene");
        var pGraLen = a.GetOrGenerateProduct("Graviton Lens");
        var pHigPurSil = a.GetOrGenerateProduct("High-Purity Silicon");
        var pIroIng = a.GetOrGenerateProduct("Iron Ingot");
        var pMag = a.GetOrGenerateProduct("Magnet");
        var pMagCoi = a.GetOrGenerateProduct("Magnetic Coil");
        var pMicCom = a.GetOrGenerateProduct("Microcrystalline Component");
        var pPlaFil = a.GetOrGenerateProduct("Plane Filter");
        var pPlaExc = a.GetOrGenerateProduct("Plasma Exciter");
        var pPla = a.GetOrGenerateProduct("Plastic");
        var pPri = a.GetOrGenerateProduct("Prism");
        var pPro = a.GetOrGenerateProduct("Processor");
        var pProMkI = a.GetOrGenerateProduct("Proliferator Mk.I");
        var pProMkII = a.GetOrGenerateProduct("Proliferator Mk.II");
        var pProMkIII = a.GetOrGenerateProduct("Proliferator Mk.III");
        var pQuaChi = a.GetOrGenerateProduct("Quantum Chip");
        var pReiThr = a.GetOrGenerateProduct("Reinforced Thruster");
        var pParBro = a.GetOrGenerateProduct("Particle Broadband");
        var pParCon = a.GetOrGenerateProduct("Particle Container");
        var pPho = a.GetOrGenerateProduct("Photon Combiner");
        var pRefOil = a.GetOrGenerateProduct("Refined Oil");
        var pSte = a.GetOrGenerateProduct("Steel");
        var pStoBri = a.GetOrGenerateProduct("Stone Brick");
        var pStraMat = a.GetOrGenerateProduct("Strange Matter");
        var pSupMagRin = a.GetOrGenerateProduct("Super-Magnetic Ring");
        var pTitAll = a.GetOrGenerateProduct("Titanium Alloy");
        var pTitCry = a.GetOrGenerateProduct("Titanium Crystal");
        var pTitGla = a.GetOrGenerateProduct("Titanium Glass");
        var pTitIng = a.GetOrGenerateProduct("Titanium Ingot");
        var pThr = a.GetOrGenerateProduct("Thruster");
        
        // Consumables
        var pAntFueRod = a.GetOrGenerateProduct("Antimatter Fuel Rod");
        var pDeuFueRod = a.GetOrGenerateProduct("Deuteron Fuel Rod");
        var pFou = a.GetOrGenerateProduct("Foundation");
        var pHydFueRod = a.GetOrGenerateProduct("Hydrogen Fuel Rod");
        var pSpaWar = a.GetOrGenerateProduct("Space Warper");
        
        // Science
        var pEleMat = a.GetOrGenerateProduct("Electromagnetic Matrix");
        var pEneMat = a.GetOrGenerateProduct("Energy Matrix");
        var pStruMat = a.GetOrGenerateProduct("Structure Matrix");
        var pInfMat = a.GetOrGenerateProduct("Information Matrix");
        var pGraMat = a.GetOrGenerateProduct("Gravity Matrix");
        var pUniMat = a.GetOrGenerateProduct("Universe Matrix");
        
        // Building Consumables
        var pLogDro = a.GetOrGenerateProduct("Logistic Drone");
        var pLogVes = a.GetOrGenerateProduct("Logistic Vessel");
        var pSmaCarRoc = a.GetOrGenerateProduct("Small Carrier Rocket");
        var pSolSai = a.GetOrGenerateProduct("Solar Sail");
        
        // Power
        var pAcc = a.GetOrGenerateProduct("Accumulator");
        var pArtSta = a.GetOrGenerateProduct("Artificial Star");
        var pEneExc = a.GetOrGenerateProduct("Energy Exchanger");
        var pFulAcc = a.GetOrGenerateProduct("Full Accumulator");
        var pGeoPowSta = a.GetOrGenerateProduct("Geothermal Power Station");
        var pMinFusPowPla = a.GetOrGenerateProduct("Mini Fusion Power Plant");
        var pSatSub = a.GetOrGenerateProduct("Satellite Substation");
        var pSolPan = a.GetOrGenerateProduct("Solar Panel");
        var pTesTow = a.GetOrGenerateProduct("Tesla Tower");
        var pThePowPla = a.GetOrGenerateProduct("Thermal Power Plant");
        var pWinTur = a.GetOrGenerateProduct("Wind Turbine");
        var pWirPowTow = a.GetOrGenerateProduct("Wireless Power Tower");
        
        // Mining
        var pAdvMinMac = a.GetOrGenerateProduct("Advanced Mining Machine");
        var pMinMac = a.GetOrGenerateProduct("Mining Machine");
        var pOilExt = a.GetOrGenerateProduct("Oil Extractor");
        var pOrbCol = a.GetOrGenerateProduct("Orbital Collector");
        var pWatPum = a.GetOrGenerateProduct("Water Pump");
        
        // Logistics
        var pAutPil = a.GetOrGenerateProduct("Automatic Piler");
        var pConBel1 = a.GetOrGenerateProduct("Conveyor Belt Mk.I");
        var pConBel2 = a.GetOrGenerateProduct("Conveyor Belt Mk.II");
        var pConBel3 = a.GetOrGenerateProduct("Conveyor Belt Mk.III");
        var pSor1 = a.GetOrGenerateProduct("Sorter Mk.I");
        var pSor2 = a.GetOrGenerateProduct("Sorter Mk.II");
        var pSor3 = a.GetOrGenerateProduct("Sorter Mk.III");
        var pSpl = a.GetOrGenerateProduct("Splitter");
        var pTraMon = a.GetOrGenerateProduct("Traffic Monitor");
        var pSto1 = a.GetOrGenerateProduct("Storage Mk.I");
        var pSto2 = a.GetOrGenerateProduct("Storage Mk.II");
        var pSto3 = a.GetOrGenerateProduct("Storage Tank");
        var pPlaLogSta = a.GetOrGenerateProduct("Planetary Logistics Station");
        var pIntLogSta = a.GetOrGenerateProduct("Interstellar Logistics Station");
        var pArcSme = a.GetOrGenerateProduct("Arc Smelter");
        var pPlaSme = a.GetOrGenerateProduct("Plane Smelter");
        var pAssMac1 = a.GetOrGenerateProduct("Assembling Machine Mk.I");
        var pAssMac2 = a.GetOrGenerateProduct("Assembling Machine Mk.II");
        var pAssMac3 = a.GetOrGenerateProduct("Assembling Machine Mk.III");
        var pSprCoa = a.GetOrGenerateProduct("Spray Coater");
        var pOilRef = a.GetOrGenerateProduct("Oil Refinery");
        var pChePla = a.GetOrGenerateProduct("Chemical Plant");
        var pFra = a.GetOrGenerateProduct("Fractionator");
        var pMinParCol = a.GetOrGenerateProduct("Miniature Particle Collider");
        
        // Dyson Sphere
        var pEMRailEje = a.GetOrGenerateProduct("EM-Rail Ejector");
        var pMatLab = a.GetOrGenerateProduct("Matrix Lab");
        var pRayRec = a.GetOrGenerateProduct("Ray Receiver");
        var pVerLauSil = a.GetOrGenerateProduct("Vertical Launching Silo");
    }
}