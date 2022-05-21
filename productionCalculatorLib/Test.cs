using productionCalculatorLib.components;
using productionCalculatorLib.components.nodes.nodeTypes;

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
        
        var node1 = new SpawnNode(productIronOre, 90);
        worksheet.AddNode(node1);
        
        var node2 = new ProductionNode(recipeIronIngot);
        worksheet.AddNode(node2);
        node1.AddOutputNode(node2);

        var node3 = new EndNode(productIronIngot, 30);
        worksheet.AddNode(node3);
        node2.AddOutputNode(node3);

        Console.WriteLine(node2);
    }
}