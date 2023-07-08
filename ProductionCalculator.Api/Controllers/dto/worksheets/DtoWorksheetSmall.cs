using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.throughputs;

namespace SiteReact.Controllers.dto.worksheets;

public class DtoWorksheetSmall
{
    public Guid Id { get; }
    public string Name { get; }
    public int AmountNodes { get; }

    public List<DtoThroughPut> InputProducts { get; } = new();
    public List<DtoThroughPut> OutputProducts { get; } = new();

    public DtoWorksheetSmall(EntityContainer container, Worksheet worksheet)
    {
        Id = worksheet.Id;
        Name = worksheet.Name;
        AmountNodes = worksheet.Nodes.Count;

        foreach (var node in worksheet.Nodes)
        {
            switch (node)
            {
                case SpawnNode spawnNode:
                    var foundInput = InputProducts.Find(put => put.Product == spawnNode.ProductId);
                    if (foundInput != null)
                    {
                        foundInput.Amount += spawnNode.Amount;
                    } 
                    else
                    {
                        InputProducts.Add(new DtoThroughPut(container.GetProduct(spawnNode.ProductId)!, spawnNode.Amount));
                    }
                    break;
                case EndNode endNode:
                    var foundOutput = OutputProducts.Find(put => put.Product == endNode.ProductId);
                    if (foundOutput != null)
                    {
                        foundOutput.Amount += endNode.Amount;
                    } 
                    else
                    {
                        OutputProducts.Add(new DtoThroughPut(container.GetProduct(endNode.ProductId)!, endNode.Amount));
                    }
                    break;
            }
        }
    }
}