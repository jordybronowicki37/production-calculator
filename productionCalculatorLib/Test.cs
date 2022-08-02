using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib;

public class Test
{
    public static void Main(string[] args)
    {
        var worksheet = new Worksheet();

        var productIronOre = new Product("Iron ore");
        var productIronIngot = new Product("Iron ingot");
        
        var recipeIronIngot = new Recipe("Iron ingot");
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 90));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productIronIngot, 30));

        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot).AddInputNode(node1, productIronOre).Build();
        var node3 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productIronOre).AddInputNode(node2, productIronIngot).Build();

        Console.WriteLine(node2);
    }
}