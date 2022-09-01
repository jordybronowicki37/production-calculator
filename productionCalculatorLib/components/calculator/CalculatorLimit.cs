using productionCalculatorLib.components.calculator.targets;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.calculator;

public class CalculatorLimit
{
    private Worksheet _worksheet;
    private int _amountOfTimesCalculated;
    
    private CalculatorLimit(Worksheet worksheet)
    {
        _worksheet = worksheet;
    }

    public static void ReCalculateAmounts(Worksheet worksheet)
    {
        var w = new CalculatorLimit(worksheet);
        if (!w.CheckLimits())
        {
            worksheet.CalculationSucceeded = false;
            worksheet.CalculationError = "Worksheet must have at least 1 'ExactAmount' limit";
            return;
        }
        
        if (w.CheckResult())
        {
            worksheet.CalculationSucceeded = true;
            worksheet.CalculationError = "";
            return;
        }
        
        w.ResetAmounts();
        while (w._amountOfTimesCalculated < worksheet.Nodes.Count*5)
        {
            w.Calculate();
            if (w.CheckResult())
            {
                worksheet.CalculationSucceeded = true;
                worksheet.CalculationError = "";
                return;
            }
            w._amountOfTimesCalculated++;
        }
        worksheet.CalculationSucceeded = false;
        worksheet.CalculationError = "Calculator could not find stable solution";
    }

    private bool CheckLimits()
    {
        return _worksheet.Nodes.Any(node =>
            node.ProductionTargets.Any(target =>
                target.Type == TargetProductionTypes.ExactAmount));
    }
    
    private void ResetAmounts()
    {
        foreach (var node in _worksheet.Nodes)
        {
            if (node is IHasProduct productNode)
            {
                var exactTarget = productNode.ProductionTargets.FirstOrDefault(v => v.Type == TargetProductionTypes.ExactAmount);
                var minTarget = productNode.ProductionTargets.FirstOrDefault(v => v.Type == TargetProductionTypes.MinAmount);
                if (exactTarget != null) productNode.Amount = exactTarget.Amount;
                else if (minTarget != null) productNode.Amount = minTarget.Amount;
                else productNode.Amount = 0;
            };
            if (node is IHasRecipe recipeNode)
            {
                var exactTarget = recipeNode.ProductionTargets.FirstOrDefault(v => v.Type == TargetProductionTypes.ExactAmount);
                var minTarget = recipeNode.ProductionTargets.FirstOrDefault(v => v.Type == TargetProductionTypes.MinAmount);
                if (exactTarget != null) recipeNode.ProductionAmount = exactTarget.Amount;
                else if (minTarget != null) recipeNode.ProductionAmount = minTarget.Amount;
                else recipeNode.ProductionAmount = 0;
            };
            if (node is INodeIn inNode) foreach (var connection in inNode.InputConnections)connection.Amount = 0;
        }
    }
    
    private bool CheckResult()
    {
        foreach (var node in _worksheet.Nodes)
        {
            switch (node)
            {
                case SpawnNode spawnNode:
                    if (spawnNode.Amount - spawnNode.OutputConnections.Sum(connection => connection.Amount) > 0.1) return false;
                    break;
                case ProductionNode productionNode:
                    if ((from throughPut in productionNode.Recipe.InputThroughPuts 
                         let amountRequired = productionNode.InputConnections
                             .Where(c => c.Product.Equals(throughPut.Product))
                             .Sum(connection => connection.Amount) 
                         where productionNode.ProductionAmount * throughPut.Amount - amountRequired > 0.1 
                         select throughPut).Any()) return false;
                    
                    if ((from throughPut in productionNode.Recipe.OutputThroughPuts 
                         let amountProvided = productionNode.OutputConnections
                             .Where(c => c.Product.Equals(throughPut.Product))
                             .Sum(connection => connection.Amount) 
                         where productionNode.ProductionAmount * throughPut.Amount - amountProvided > 0.1 
                         select throughPut).Any()) return false;
                    break;
                case EndNode endNode:
                    if (endNode.Amount - endNode.InputConnections.Sum(connection => connection.Amount) > 0.1) return false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(node));
            }
        }
        return true;
    }

    private void Calculate()
    {
        foreach (var node in _worksheet.Nodes)
        {
            switch (node)
            {
                case IHasProduct productNode:
                    CalculateProductAmounts(productNode);
                    break;
                case IHasRecipe recipeNode:
                    CalculateRecipeAmounts(recipeNode);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(node));
            }
        }
    }

    private void CalculateProductAmounts(IHasProduct productNode)
    {
        var hasExactTarget = productNode.ProductionTargets.FirstOrDefault(v => v.Type == TargetProductionTypes.ExactAmount) != null;
        var connections = productNode switch
        {
            SpawnNode spawnNode => FilterConnections(spawnNode.OutputConnections, spawnNode.Product).ToList(),
            EndNode endNode => FilterConnections(endNode.InputConnections, endNode.Product).ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(productNode))
        };
        
        if (!hasExactTarget) productNode.Amount = connections.Sum(v => v.Amount);
        else DistributeConnections(connections, productNode.Amount);
    }

    private void DistributeConnections(ICollection<Connection> connections, float amount)
    {
        foreach (var connection in connections)
        {
            var newAmount = amount / connections.Count;
            if (newAmount > connection.Amount) 
                connection.Amount = newAmount;
        }
    }

    private void CalculateRecipeAmounts(IHasRecipe recipeNode)
    {
        switch (recipeNode)
        {
            case ProductionNode productionNode:
                var hasExactTarget = productionNode.ProductionTargets.FirstOrDefault(v => v.Type == TargetProductionTypes.ExactAmount) != null;

                foreach (var inputThroughPut in productionNode.Recipe.InputThroughPuts)
                {
                    var connectionsFiltered = FilterConnections(productionNode.InputConnections, inputThroughPut.Product).ToList();
                    if (!hasExactTarget)
                    {
                        var newAmount = connectionsFiltered.Sum(v => v.Amount) / inputThroughPut.Amount;
                        if (productionNode.ProductionAmount < newAmount) productionNode.ProductionAmount = newAmount;
                    }
                    DistributeConnections(connectionsFiltered, inputThroughPut.Amount * productionNode.ProductionAmount);
                }
                foreach (var outputThroughPut in productionNode.Recipe.OutputThroughPuts)
                {
                    var connectionsFiltered = FilterConnections(productionNode.OutputConnections, outputThroughPut.Product).ToList();
                    if (!hasExactTarget)
                    {
                        var newAmount = connectionsFiltered.Sum(v => v.Amount) / outputThroughPut.Amount;
                        if (productionNode.ProductionAmount < newAmount) productionNode.ProductionAmount = newAmount;
                    }
                    DistributeConnections(connectionsFiltered, outputThroughPut.Amount*productionNode.ProductionAmount);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(recipeNode));
        }
    }

    private static IEnumerable<Connection> FilterConnections(IEnumerable<Connection> connections, Product product)
    {
        return connections.Where(connection => connection.Product.Equals(product));
    }
}