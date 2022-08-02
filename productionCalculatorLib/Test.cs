using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.interfaces;
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

        var node1 = worksheet.GetNodeBuilder(NodeTypes.Spawn).SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder(NodeTypes.Production).SetRecipe(recipeIronIngot).AddInputNode((node1 as INodeOut)!, productIronOre).Build();
        var node3 = worksheet.GetNodeBuilder(NodeTypes.End).SetProduct(productIronOre).AddInputNode((node2 as INodeOut)!, productIronIngot).Build();

        Console.WriteLine(node2);
    }
}