using productionCalculatorLib.components;
using productionCalculatorLib.components.nodes.nodeTypes;

namespace productionCalculatorLib;

public class Test
{
    public static void Main(string[] args)
    {
        var worksheet = new Worksheet();

        var productIronOre = new Product("Iron ore");
        var recipeIronOre = new Recipe("Iron ore mine");
        recipeIronOre.OutputThroughPuts.Add(new ThroughPut(productIronOre, 90));
        var node1 = new ProductionNode(recipeIronOre);
        worksheet.AddNode(node1);

        var productIronIngot = new Product("Iron ingot");
        var recipeIronIngot = new Recipe("Iron ingot");
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 90));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productIronIngot, 30));
        var node2 = new ProductionNode(recipeIronIngot);
        worksheet.AddNode(node2);
        
        node1.AddOutputNode(node2);

        Console.WriteLine(node2);
    }
}