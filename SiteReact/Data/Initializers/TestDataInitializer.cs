using productionCalculatorLib.components.calculator;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;
using SiteReact.Data.DbContexts;

namespace SiteReact.Data.Initializers;

public static class TestDataInitializer
{
    public static void InitializeAllData(DocumentContext context)
    {
        InitializeSimpleOneWay(out var w1, out var e1);
        InitializeDoubleSpawn(out var w2, out var e2);
        
        context.EntityContainers.InsertMany(new []{e1, e2});
        context.Worksheets.InsertMany(new []{w1, w2});
    }
    
    public static void InitializeSimpleOneWay(out Worksheet worksheet, out EntityContainer entityContainer)
    {
        entityContainer = new EntityContainer();
        worksheet = new Worksheet(entityContainer)
        {
            Name = "Iron ingot smelting"
        };

        var productIronOre = entityContainer.GetOrGenerateProduct("Iron ore");
        var productIronIngot = entityContainer.GetOrGenerateProduct("Iron ingot");

        var recipeIronIngot = entityContainer.GenerateRecipe("Iron ingot");
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 30));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productIronIngot, 10));

        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot).Build();
        var node3 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productIronIngot).SetExactTarget(20).Build();
        
        worksheet.GetConnectionBuilder(node1, node2, productIronOre).Build();
        worksheet.GetConnectionBuilder(node2, node3, productIronIngot).Build();
        
        CalculatorLimit.ReCalculateAmounts(worksheet, entityContainer);
    }

    public static void InitializeDoubleSpawn(out Worksheet worksheet, out EntityContainer entityContainer)
    {
        entityContainer = new EntityContainer();
        worksheet = new Worksheet(entityContainer)
        {
            Name = "Steel ingot smelting"
        };

        var productIronOre = entityContainer.GetOrGenerateProduct("Iron ore");
        var productCoal = entityContainer.GetOrGenerateProduct("Coal");
        var productSteelIngot = entityContainer.GetOrGenerateProduct("Steel ingot");
        
        var recipeIronIngot = entityContainer.GenerateRecipe("Steel ingot");
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 30));
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productCoal, 5));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productSteelIngot, 10));
        
        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productCoal).Build();
        var node3 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot).SetExactTarget(2).Build();
        var node4 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productSteelIngot).Build();
        
        worksheet.GetConnectionBuilder(node1, node3, productIronOre).Build();
        worksheet.GetConnectionBuilder(node2, node3, productCoal).Build();
        worksheet.GetConnectionBuilder(node3, node4, productSteelIngot).Build();
        
        CalculatorLimit.ReCalculateAmounts(worksheet, entityContainer);
    }
}