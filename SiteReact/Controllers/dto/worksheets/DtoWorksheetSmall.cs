using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.throughputs;

namespace SiteReact.Controllers.dto.worksheets;

public class DtoWorksheetSmall
{
    public Guid Id { get; }
    public string Name { get; }
    public int AmountProducts { get; }
    public int AmountRecipes { get; }
    public int AmountNodes { get; }

    public List<DtoThroughPut> InputProducts { get; } = new();
    public List<DtoThroughPut> OutputProducts { get; } = new();

    public DtoWorksheetSmall(Worksheet worksheet, EntityContainer entityContainer)
    {
        Id = worksheet.Id;
        Name = worksheet.Name;
        AmountProducts = entityContainer.Products.Count;
        AmountRecipes = entityContainer.Recipes.Count;
        AmountNodes = worksheet.Nodes.Count;

        foreach (var node in worksheet.Nodes)
        {
            switch (node)
            {
                case SpawnNode spawnNode:
                    var foundInput = InputProducts.Find(put => put.Product.Id == spawnNode.ProductId);
                    if (foundInput != null)
                    {
                        foundInput.Amount += spawnNode.Amount;
                    } 
                    else
                    {
                        var product = entityContainer.GetProduct(spawnNode.ProductId);
                        if (product == null) continue;
                        InputProducts.Add(new DtoThroughPut(product, spawnNode.Amount));
                    }
                    break;
                case EndNode endNode:
                    var foundOutput = OutputProducts.Find(put => put.Product.Id == endNode.ProductId);
                    if (foundOutput != null)
                    {
                        foundOutput.Amount += endNode.Amount;
                    } 
                    else
                    {
                        var product = entityContainer.GetProduct(endNode.ProductId);
                        if (product == null) continue;
                        OutputProducts.Add(new DtoThroughPut(product, endNode.Amount));
                    }
                    break;
            }
        }
    }
}