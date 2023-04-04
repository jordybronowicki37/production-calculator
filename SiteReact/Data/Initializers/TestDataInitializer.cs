using MongoDB.Driver;
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
        
        InsertOrReplace(context, w1, e1);
        InsertOrReplace(context, w2, e2);
    }

    private static void InsertOrReplace(DocumentContext c, Worksheet w, EntityContainer e)
    {
        var findEFilter = Builders<EntityContainer>.Filter.Eq(f => f.Id, e.Id);
        c.EntityContainers.DeleteOne(findEFilter);
        c.EntityContainers.InsertOne(e);
        
        var findWFilter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        c.Worksheets.DeleteOne(findWFilter);
        c.Worksheets.InsertOne(w);
    }
    
    public static void InitializeSimpleOneWay(out Worksheet worksheet, out EntityContainer entityContainer)
    {
        entityContainer = new EntityContainer()
        {
            Id = Guid.Parse("dff3c380-3e78-418c-af70-bbc955140aca")
        };
        worksheet = new Worksheet(entityContainer)
        {
            Id = Guid.Parse("9fd8a83b-de70-4124-9dfa-64f350ceb743"),
            Name = "Iron ingot smelting"
        };

        var productIronOre = entityContainer.GetOrGenerateProduct("Iron ore");
        var productIronIngot = entityContainer.GetOrGenerateProduct("Iron ingot");
        
        var machineSmelter = entityContainer.GenerateMachine("Smelter");
        
        var recipeIronIngot = entityContainer.GenerateRecipe("Iron ingot", machineSmelter);
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 30));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productIronIngot, 10));

        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot, machineSmelter).Build();
        var node3 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productIronIngot).SetExactTarget(20).Build();
        
        worksheet.GetConnectionBuilder(node1, node2, productIronOre).Build();
        worksheet.GetConnectionBuilder(node2, node3, productIronIngot).Build();
        
        new CalculatorLimit(worksheet, entityContainer).ReCalculateAmounts();
    }

    public static void InitializeDoubleSpawn(out Worksheet worksheet, out EntityContainer entityContainer)
    {
        entityContainer = new EntityContainer
        {
            Id = Guid.Parse("7671dd58-077d-4922-935b-4ed08782b70c")
        };
        worksheet = new Worksheet(entityContainer)
        {
            Id = Guid.Parse("c75b35b8-c2af-486f-bf06-a9ece3de7923"),
            Name = "Steel ingot smelting"
        };

        var productIronOre = entityContainer.GetOrGenerateProduct("Iron ore");
        var productCoal = entityContainer.GetOrGenerateProduct("Coal");
        var productSteelIngot = entityContainer.GetOrGenerateProduct("Steel ingot");
        
        var machineSmelter = entityContainer.GenerateMachine("Smelter");
        
        var recipeIronIngot = entityContainer.GenerateRecipe("Steel ingot", machineSmelter);
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 30));
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productCoal, 5));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productSteelIngot, 10));
        
        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productCoal).Build();
        var node3 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot, machineSmelter).SetExactTarget(2).Build();
        var node4 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productSteelIngot).Build();
        
        worksheet.GetConnectionBuilder(node1, node3, productIronOre).Build();
        worksheet.GetConnectionBuilder(node2, node3, productCoal).Build();
        worksheet.GetConnectionBuilder(node3, node4, productSteelIngot).Build();
        
        new CalculatorLimit(worksheet, entityContainer).ReCalculateAmounts();
    }
}