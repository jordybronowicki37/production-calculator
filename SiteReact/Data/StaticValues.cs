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
        
        generateSimpleOneWay();
        generateDoubleSpawn();
    }

    private void generateSimpleOneWay()
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
        
        var node1 = new SpawnNode(productIronOre, 0);
        worksheet.AddNode(node1);
        
        var node2 = new ProductionNode(recipeIronIngot);
        worksheet.AddNode(node2);
        node1.AddOutputNode(node2);

        var node3 = new EndNode(productIronIngot, 30);
        worksheet.AddNode(node3);
        node2.AddOutputNode(node3);
    }

    private void generateDoubleSpawn()
    {
        var worksheet = new Worksheet
        {
            Name = "Steel ingot smelting"
        };
        Worksheet.Add(worksheet);

        var productIronOre = worksheet.GetOrGenerateProduct("Iron ore");
        var productCoal = worksheet.GetOrGenerateProduct("Coal");
        var productIronIngot = worksheet.GetOrGenerateProduct("Steel ingot");
        
        var recipeIronIngot = worksheet.GenerateRecipe("Steel ingot");
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 30));
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productCoal, 5));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productIronIngot, 10));
        
        var node0 = new SpawnNode(productIronOre, 0);
        worksheet.AddNode(node0);
        
        var node1 = new SpawnNode(productCoal, 0);
        worksheet.AddNode(node1);
        
        var node2 = new ProductionNode(recipeIronIngot);
        worksheet.AddNode(node2);
        node0.AddOutputNode(node2);
        node1.AddOutputNode(node2);

        var node3 = new EndNode(productIronIngot, 30);
        worksheet.AddNode(node3);
        node2.AddOutputNode(node3);
    }
}