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
    
    public CalculatorLimit(Worksheet worksheet, EntityContainer entityContainer)
    {
        _worksheet = worksheet;
        _entityContainer = entityContainer;
    }

    public void ReCalculateAmounts()
    {
        if (!CheckLimits())
        {
            _worksheet.CalculationSucceeded = false;
            _worksheet.CalculationError = "Worksheet must have at least 1 'ExactAmount' limit";
            return;
        }
        
        if (CheckResult())
        {
            _worksheet.CalculationSucceeded = true;
            _worksheet.CalculationError = "";
            return;
        }
        
        ResetAmounts();
        while (_amountOfTimesCalculated < _worksheet.Nodes.Count*5)
        {
            CalculateStep();
            if (CheckResult())
            {
                _worksheet.CalculationSucceeded = true;
                _worksheet.CalculationError = "";
                return;
            }
            _amountOfTimesCalculated++;
        }
        _worksheet.CalculationSucceeded = false;
        _worksheet.CalculationError = "Calculator could not find stable solution";
    }

    private bool CheckLimits()
    {
        return _worksheet.Nodes.Any(node => GetTarget(node, TargetProductionTypes.ExactAmount) != null);
    }
    
    private void ResetAmounts()
    {
        foreach (var node in _worksheet.Nodes)
        {
            if (node is IHasProduct productNode)
            {
                var exactTarget = GetTarget(node, TargetProductionTypes.ExactAmount);
                var minTarget = GetTarget(node, TargetProductionTypes.MinAmount);
                
                if (exactTarget != null) productNode.Amount = exactTarget.Amount;
                else if (minTarget != null) productNode.Amount = minTarget.Amount;
                else productNode.Amount = 0;
            }
            if (node is IHasRecipe recipeNode)
            {
                var exactTarget = GetTarget(node, TargetProductionTypes.ExactAmount);
                var minTarget = GetTarget(node, TargetProductionTypes.MinAmount);
                
                if (exactTarget != null) recipeNode.Amount = exactTarget.Amount;
                else if (minTarget != null) recipeNode.Amount = minTarget.Amount;
                else recipeNode.Amount = 0;
            }
            foreach (var connection in _worksheet.Connections) connection.Amount = 0;
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
                         where CompareFloatingPointNumbers(productionNode.Amount * throughPut.Amount, amountRequired)
                         select throughPut).Any()) return false;
                    
                    if ((from throughPut in GetRecipe(productionNode.RecipeId).OutputThroughPuts 
                         let amountProvided = GetOutputConnections(productionNode)
                             .Where(c => c.Product.Id.Equals(throughPut.ProductId))
                             .Sum(connection => connection.Amount) 
                         where CompareFloatingPointNumbers(productionNode.Amount * throughPut.Amount, amountProvided)
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

    private void CalculateStep()
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
        var exactTarget = GetTarget(productNode, TargetProductionTypes.ExactAmount);
        ICollection<Connection> connections;

        switch (productNode)
        {
            case SpawnNode spawnNode:
                connections = FilterConnections(GetOutputConnections(spawnNode), GetProduct(spawnNode.ProductId)).ToList();
                if (exactTarget == null) productNode.Amount = connections.Sum(v => v.Amount);
                else DistributeOutputConnections(connections, productNode.Amount);
                break;
            case EndNode endNode:
                connections = FilterConnections(GetInputConnections(endNode), GetProduct(endNode.ProductId)).ToList();
                if (exactTarget == null) productNode.Amount = connections.Sum(v => v.Amount);
                else DistributeInputConnections(connections, productNode.Amount);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(productNode));
        }
    }

    private void DistributeInputConnections(IEnumerable<Connection> connections, float amount)
    {
        var connectionsCopy = new List<Connection>(connections).Where(c =>
        {
            var node = GetNode(c.NodeInId);
            var exactTarget = GetTarget(node, TargetProductionTypes.ExactAmount);
            if (exactTarget != null) amount -= node.Amount;
            return exactTarget == null;
        }).ToList();

        foreach (var connection in connectionsCopy)
        {
            var newAmount = amount / connectionsCopy.Count;
            if (newAmount > connection.Amount) 
                connection.Amount = newAmount;
        }
    }

    private void DistributeOutputConnections(IEnumerable<Connection> connections, float amount)
    {
        var connectionsCopy = new List<Connection>(connections).Where(c =>
        {
            var node = GetNode(c.NodeOutId);
            var exactTarget = GetTarget(node, TargetProductionTypes.ExactAmount);
            if (exactTarget != null) amount -= node.Amount;
            return exactTarget == null;
        }).ToList();

        foreach (var connection in connectionsCopy)
        {
            var newAmount = amount / connectionsCopy.Count;
            if (newAmount > connection.Amount) 
                connection.Amount = newAmount;
        }
    }

    private void CalculateRecipeAmounts(IHasRecipe recipeNode)
    {
        switch (recipeNode)
        {
            case ProductionNode productionNode:
                var hasExactTarget = GetTarget(recipeNode, TargetProductionTypes.ExactAmount) != null;

                foreach (var inputThroughPut in GetRecipe(productionNode.RecipeId).InputThroughPuts)
                {
                    var connectionsFiltered = FilterConnections(GetInputConnections(productionNode),
                        GetProduct(inputThroughPut.ProductId)).ToList();
                    if (!hasExactTarget)
                    {
                        var newAmount = connectionsFiltered.Sum(v => v.Amount) / inputThroughPut.Amount;
                        if (productionNode.Amount < newAmount) productionNode.Amount = newAmount;
                    }
                    DistributeInputConnections(connectionsFiltered, inputThroughPut.Amount * productionNode.Amount);
                }
                foreach (var outputThroughPut in GetRecipe(productionNode.RecipeId).OutputThroughPuts)
                {
                    var connectionsFiltered = FilterConnections(GetOutputConnections(productionNode),
                        GetProduct(outputThroughPut.ProductId)).ToList();
                    if (!hasExactTarget)
                    {
                        var newAmount = connectionsFiltered.Sum(v => v.Amount) / outputThroughPut.Amount;
                        if (productionNode.Amount < newAmount) productionNode.Amount = newAmount;
                    }
                    DistributeOutputConnections(connectionsFiltered, outputThroughPut.Amount*productionNode.Amount);
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

    private INode GetNode(Guid id)
    {
        return _worksheet.Nodes.First(n => n.Id == id);
    }

    private TargetProduction? GetTarget(INode node, TargetProductionTypes type)
    {
        return node.Targets.FirstOrDefault(v => v.Type == type);
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

    public static bool CompareFloatingPointNumbers(float num1, float num2)
    {
        return Math.Abs(num1 - num2) < 0.1;
    }
}