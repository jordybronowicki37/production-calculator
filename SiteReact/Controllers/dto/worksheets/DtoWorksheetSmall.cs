using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.throughputs;

namespace SiteReact.Controllers.dto.worksheets;

public class DtoWorksheetSmall
{
    public string Name { get; }
    public int AmountProducts { get; }
    public int AmountRecipes { get; }
    public int AmountNodes { get; }

    public List<DtoThroughPut> InputProducts { get; } = new();
    public List<DtoThroughPut> OutputProducts { get; } = new();

    public DtoWorksheetSmall(Worksheet worksheet)
    {
        Name = worksheet.Name;
        AmountProducts = worksheet.Products.Count;
        AmountRecipes = worksheet.Recipes.Count;
        AmountNodes = worksheet.Nodes.Count;

        foreach (var node in worksheet.Nodes)
        {
            switch (node)
            {
                case SpawnNode spawnNode:
                    var foundInput = InputProducts.Find(put => put.Product.Name == spawnNode.Product.Name);
                    if (foundInput != null)
                    {
                        foundInput.Amount += spawnNode.Amount;
                    } 
                    else
                    {
                        InputProducts.Add(new DtoThroughPut(spawnNode.Product, spawnNode.Amount));
                    }
                    break;
                case EndNode endNode:
                    var foundOutput = InputProducts.Find(put => put.Product.Name == endNode.Product.Name);
                    if (foundOutput != null)
                    {
                        foundOutput.Amount += endNode.Amount;
                    } 
                    else
                    {
                        OutputProducts.Add(new DtoThroughPut(endNode.Product, endNode.Amount));
                    }
                    break;
            }
        }
    }
}