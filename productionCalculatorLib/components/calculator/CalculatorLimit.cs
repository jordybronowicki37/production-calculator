using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.calculator;

public class CalculatorLimit
{
    private Worksheet _worksheet;
    private EntityContainer _entityContainer;
    private int _amountOfTimesCalculated;
    
    private CalculatorLimit(Worksheet worksheet, EntityContainer entityContainer)
    {
        _worksheet = worksheet;
        _entityContainer = entityContainer;
    }

    public static void ReCalculateAmounts(Worksheet worksheet, EntityContainer entityContainer)
    {
        var w = new CalculatorLimit(worksheet, entityContainer);
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
            node.Targets.Any(target =>
                target.Type == TargetProductionTypes.ExactAmount));
    }
    
    private void ResetAmounts()
    {
        foreach (var node in _worksheet.Nodes)
        {
            if (node is IHasProduct productNode)
            {
                var exactTarget = productNode.Targets.FirstOrDefault(v => v.Type == TargetProductionTypes.ExactAmount);
                var minTarget = productNode.Targets.FirstOrDefault(v => v.Type == TargetProductionTypes.MinAmount);
                if (exactTarget != null) productNode.Amount = exactTarget.Amount;
                else if (minTarget != null) productNode.Amount = minTarget.Amount;
                else productNode.Amount = 0;
            };
            if (node is IHasRecipe recipeNode)
            {
                var exactTarget = recipeNode.Targets.FirstOrDefault(v => v.Type == TargetProductionTypes.ExactAmount);
                var minTarget = recipeNode.Targets.FirstOrDefault(v => v.Type == TargetProductionTypes.MinAmount);
                if (exactTarget != null) recipeNode.ProductionAmount = exactTarget.Amount;
                else if (minTarget != null) recipeNode.ProductionAmount = minTarget.Amount;
                else recipeNode.ProductionAmount = 0;
            };
            if (node is INodeIn inNode) foreach (var connection in GetInputConnections(inNode))connection.Amount = 0;
        }
    }
    
    private bool CheckResult()
    {
        foreach (var node in _worksheet.Nodes)
        {
            switch (node)
            {
                case SpawnNode spawnNode:
                    if (CompareFloatingPointNumbers(spawnNode.Amount, FilterConnections(GetOutputConnections(spawnNode),
                                GetProduct(spawnNode.ProductId))
                            .Sum(connection => connection.Amount))) return false;
                    break;
                case ProductionNode productionNode:
                    if ((from throughPut in GetRecipe(productionNode.RecipeId).InputThroughPuts 
                         let amountRequired = GetInputConnections(productionNode)
                             .Where(c => c.Product.Id.Equals(throughPut.ProductId))
                             .Sum(connection => connection.Amount) 
                         where CompareFloatingPointNumbers(productionNode.ProductionAmount * throughPut.Amount, amountRequired)
                         select throughPut).Any()) return false;
                    
                    if ((from throughPut in GetRecipe(productionNode.RecipeId).OutputThroughPuts 
                         let amountProvided = GetOutputConnections(productionNode)
                             .Where(c => c.Product.Id.Equals(throughPut.ProductId))
                             .Sum(connection => connection.Amount) 
                         where CompareFloatingPointNumbers(productionNode.ProductionAmount * throughPut.Amount, amountProvided)
                         select throughPut).Any()) return false;
                    break;
                case EndNode endNode:
                    if (CompareFloatingPointNumbers(endNode.Amount, FilterConnections(GetInputConnections(endNode), 
                                GetProduct(endNode.ProductId))
                            .Sum(connection => connection.Amount))) return false;
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
        var hasExactTarget = productNode.Targets.FirstOrDefault(v => v.Type == TargetProductionTypes.ExactAmount) != null;
        var connections = productNode switch
        {
            SpawnNode spawnNode => FilterConnections(GetOutputConnections(spawnNode), GetProduct(spawnNode.ProductId)).ToList(),
            EndNode endNode => FilterConnections(GetInputConnections(endNode), GetProduct(endNode.ProductId)).ToList(),
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
                var hasExactTarget = productionNode.Targets.FirstOrDefault(v => v.Type == TargetProductionTypes.ExactAmount) != null;

                foreach (var inputThroughPut in GetRecipe(productionNode.RecipeId).InputThroughPuts)
                {
                    var connectionsFiltered = FilterConnections(GetInputConnections(productionNode),
                        GetProduct(inputThroughPut.ProductId)).ToList();
                    if (!hasExactTarget)
                    {
                        var newAmount = connectionsFiltered.Sum(v => v.Amount) / inputThroughPut.Amount;
                        if (productionNode.ProductionAmount < newAmount) productionNode.ProductionAmount = newAmount;
                    }
                    DistributeConnections(connectionsFiltered, inputThroughPut.Amount * productionNode.ProductionAmount);
                }
                foreach (var outputThroughPut in GetRecipe(productionNode.RecipeId).OutputThroughPuts)
                {
                    var connectionsFiltered = FilterConnections(GetOutputConnections(productionNode),
                        GetProduct(outputThroughPut.ProductId)).ToList();
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

    private Product GetProduct(Guid id)
    {
        return _entityContainer.GetProduct(id);
    }

    private Recipe GetRecipe(Guid id)
    {
        return _entityContainer.GetRecipe(id);
    }

    private IEnumerable<Connection> GetOutputConnections(INodeOut node)
    {
        return _worksheet.Connections.Where(connection => connection.NodeInId == node.Id);
    }
    
    private IEnumerable<Connection> GetInputConnections(INodeIn node)
    {
        return _worksheet.Connections.Where(connection => connection.NodeOutId == node.Id);
    }

    private static IEnumerable<Connection> FilterConnections(IEnumerable<Connection> connections, Product product)
    {
        return connections.Where(connection => connection.Product.Equals(product));
    }

    private static bool CompareFloatingPointNumbers(float num1, float num2)
    {
        return Math.Abs(num1 - num2) > 0.1;
    }
}