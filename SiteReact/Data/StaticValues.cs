using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;

namespace SiteReact.Data;

public class StaticValues
{
    private static StaticValues? _singleTon;
    public static StaticValues Get() => _singleTon ??= new StaticValues();

    public IList<Product> Products { get; }
    public IList<Recipe> Recipes { get; }
    public IList<INode> Nodes { get; }
    public Worksheet Worksheet { get; }

    private StaticValues()
    {
        Worksheet = new Worksheet();

        var productIronOre = new Product("Iron ore");
        var productIronIngot = new Product("Iron ingot");
        
        var recipeIronIngot = new Recipe("Iron ingot");
        recipeIronIngot.InputThroughPuts.Add(new ThroughPut(productIronOre, 90));
        recipeIronIngot.OutputThroughPuts.Add(new ThroughPut(productIronIngot, 30));
        
        var node1 = new SpawnNode(productIronOre, 90);
        Worksheet.AddNode(node1);
        
        var node2 = new ProductionNode(recipeIronIngot);
        Worksheet.AddNode(node2);
        node1.AddOutputNode(node2);

        var node3 = new EndNode(productIronIngot, 30);
        Worksheet.AddNode(node3);
        node2.AddOutputNode(node3);

        Products = new[] {productIronOre, productIronIngot};
        Recipes = new[] {recipeIronIngot};
    }
}