using productionCalculatorLib.components.calculator.exceptions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.calculator;

public class CalculatorEnd
{
    private Worksheet _worksheet;
    private EndNode _endNode;
    private IList<INode> _visitedNodes = new List<INode>();

    public static void ReCalculateAmounts(Worksheet worksheet)
    {
        var calculator = new CalculatorEnd(worksheet);
        calculator.Calculate(calculator._endNode);
    }
    
    private CalculatorEnd(Worksheet worksheet)
    {
        _worksheet = worksheet;
        _endNode = worksheet.Nodes.First(e => e is EndNode) as EndNode ?? throw new LimitRuleError("No end node detected");
        _endNode.Amount = 30;
    }

    private void Calculate(INode node)
    {
        switch (node)
        {
            case ProductionNode nodeIO:
                CalculateProduction(nodeIO);
                break;
            case EndNode nodeI:
                CalculateAllInputs(nodeI);
                break;
            case SpawnNode nodeO:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(node));
        }
    }

    private void CalculateProduction(ProductionNode node)
    {
        foreach (var n in node.InputConnections)
        {
            switch (n.NodeIn)
            {
                case ProductionNode productionNode:
                    productionNode.ProductionAmount = 0;
                    foreach (var throughputO in productionNode.Recipe.OutputThroughPuts)
                    {
                        foreach (var throughPutI in node.Recipe.InputThroughPuts)
                        {
                            if (throughputO.Product.Name != throughPutI.Product.Name) continue;
                            
                            var newAmount = productionNode.ProductionAmount = throughPutI.Amount * node.ProductionAmount / throughputO.Amount;
                            if (productionNode.ProductionAmount < newAmount)
                            {
                                productionNode.ProductionAmount = newAmount;
                            }
                        }
                    }
                    Calculate(productionNode);
                    break;
                case SpawnNode spawnNode:
                    foreach (var throughPutI in node.Recipe.InputThroughPuts)
                    {
                        if (throughPutI.Product.Name == spawnNode.Product.Name)
                        {
                            spawnNode.Amount = throughPutI.Amount * node.ProductionAmount;
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(n));
            }
        }
    }

    private void CalculateAllInputs(EndNode node)
    {
        foreach (var n in node.InputConnections)
        {
            switch (n.NodeIn)
            {
                case ProductionNode productionNode:
                    foreach (var throughput in productionNode.Recipe.OutputThroughPuts)
                    {
                        if (throughput.Product.Name == node?.Product.Name)
                        {
                            productionNode.ProductionAmount = node.Amount / throughput.Amount;
                        }
                    }
                    Calculate(productionNode);
                    break;
                case SpawnNode spawnNode:
                    if (spawnNode.Product.Name == node?.Product.Name)
                    {
                        spawnNode.Amount = node.Amount;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(n));
            }
        }
    }
    
    private struct TempNodeData
    {
    
    }
}