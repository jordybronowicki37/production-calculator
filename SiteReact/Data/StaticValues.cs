using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;

namespace SiteReact.Data;

public class StaticValues
{
    private static StaticValues? _singleTon;
    public static StaticValues Get() => _singleTon ??= new StaticValues();
    public IList<Worksheet> Worksheet { get; }

    private StaticValues()
    {
        Worksheet = new List<Worksheet>();
        
        GenerateSimpleOneWay();
        GenerateDoubleSpawn();
    }

    private void GenerateSimpleOneWay()
    {
        var worksheet = new Worksheet
        {
            Name = "Iron ingot smelting"
        };
        Worksheet.Add(worksheet);

        var productIronOre = worksheet.GetOrGenerateProduct("Iron ore");
        var productIronIngot = worksheet.GetOrGenerateProduct("Iron ingot");
        
        var recipeIronIngot = worksheet.GenerateRecipe("Iron ingot");
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 30));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productIronIngot, 10));
        
        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot).AddInputNode(node1, productIronOre).Build();
        var node3 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productIronIngot).AddInputNode(node2, productIronIngot).SetExactTarget(20).Build();
    }

    private void GenerateDoubleSpawn()
    {
        var worksheet = new Worksheet
        {
            Name = "Steel ingot smelting"
        };
        Worksheet.Add(worksheet);

        var productIronOre = worksheet.GetOrGenerateProduct("Iron ore");
        var productCoal = worksheet.GetOrGenerateProduct("Coal");
        var productSteelIngot = worksheet.GetOrGenerateProduct("Steel ingot");
        
        var recipeIronIngot = worksheet.GenerateRecipe("Steel ingot");
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 30));
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productCoal, 5));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productSteelIngot, 10));
        
        var node0 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productCoal).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot).AddInputNode(node0, productIronOre).AddInputNode(node1, productCoal).SetExactTarget(2).Build();
        var node3 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productSteelIngot).AddInputNode(node2, productSteelIngot).Build();
        node3.Amount = 30;
    }
}