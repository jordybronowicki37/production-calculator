using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.interfaces;
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
        
        var node1 = worksheet.GetNodeBuilder(NodeTypes.Spawn).SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder(NodeTypes.Production).SetRecipe(recipeIronIngot).AddInputNode(node1 as INodeOut).Build();
        var node3 = worksheet.GetNodeBuilder(NodeTypes.End).SetProduct(productIronOre).AddInputNode(node2 as INodeOut).Build();
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
        
        var node0 = worksheet.GetNodeBuilder(NodeTypes.Spawn).SetProduct(productIronOre).Build();
        var node1 = worksheet.GetNodeBuilder(NodeTypes.Spawn).SetProduct(productCoal).Build();
        var node2 = worksheet.GetNodeBuilder(NodeTypes.Production).SetRecipe(recipeIronIngot).AddInputNode(node0 as INodeOut).AddInputNode(node1 as INodeOut).Build();
        var node3 = worksheet.GetNodeBuilder(NodeTypes.End).SetProduct(productIronOre).AddInputNode(node2 as INodeOut).Build();
    }
}