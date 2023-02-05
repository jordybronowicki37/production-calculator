using productionCalculatorLib.components.entityContainer;

namespace SiteReact.Data.GameDataPresets;

public static class DSPData
{
    public static void addData(EntityContainer e)
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
        var pPho = e.GetOrGenerateProduct("Photon Combiner");
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
        var pConBel1 = e.GetOrGenerateProduct("Conveyor Belt Mk.I");
        var pConBel2 = e.GetOrGenerateProduct("Conveyor Belt Mk.II");
        var pConBel3 = e.GetOrGenerateProduct("Conveyor Belt Mk.III");
        var pSor1 = e.GetOrGenerateProduct("Sorter Mk.I");
        var pSor2 = e.GetOrGenerateProduct("Sorter Mk.II");
        var pSor3 = e.GetOrGenerateProduct("Sorter Mk.III");
        var pSpl = e.GetOrGenerateProduct("Splitter");
        var pTraMon = e.GetOrGenerateProduct("Traffic Monitor");
        var pSto1 = e.GetOrGenerateProduct("Storage Mk.I");
        var pSto2 = e.GetOrGenerateProduct("Storage Mk.II");
        var pSto3 = e.GetOrGenerateProduct("Storage Tank");
        var pPlaLogSta = e.GetOrGenerateProduct("Planetary Logistics Station");
        var pIntLogSta = e.GetOrGenerateProduct("Interstellar Logistics Station");
        var pArcSme = e.GetOrGenerateProduct("Arc Smelter");
        var pPlaSme = e.GetOrGenerateProduct("Plane Smelter");
        var pAssMac1 = e.GetOrGenerateProduct("Assembling Machine Mk.I");
        var pAssMac2 = e.GetOrGenerateProduct("Assembling Machine Mk.II");
        var pAssMac3 = e.GetOrGenerateProduct("Assembling Machine Mk.III");
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
    }
}