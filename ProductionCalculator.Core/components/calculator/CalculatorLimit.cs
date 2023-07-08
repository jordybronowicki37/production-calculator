using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.entities;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
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
                    var spawnConnections = FilterConnections(GetConnections(Side.Out, spawnNode), spawnNode.ProductId);
                    var sumSpawnConnections = spawnConnections.Sum(connection => connection.Amount);
                    if (!CompareFloatingPointNumbers(spawnNode.Amount, sumSpawnConnections)) return false;
                    break;
                case ProductionNode productionNode:
                    foreach (var throughPut in GetRecipe(productionNode.RecipeId).InputThroughPuts)
                    {
                        var prodConnections = FilterConnections(GetConnections(Side.In, productionNode), throughPut.ProductId);
                        var prodConnectionsSum = prodConnections.Sum(connection => connection.Amount);
                        if (!CompareFloatingPointNumbers(productionNode.Amount * throughPut.Amount, prodConnectionsSum)) return false;
                    }
                    
                    foreach (var throughPut in GetRecipe(productionNode.RecipeId).OutputThroughPuts)
                    {
                        var prodConnections = FilterConnections(GetConnections(Side.Out, productionNode), throughPut.ProductId);
                        var prodConnectionsSum = prodConnections.Sum(connection => connection.Amount);
                        if (!CompareFloatingPointNumbers(productionNode.Amount * throughPut.Amount, prodConnectionsSum)) return false;
                    }
                    break;
                case EndNode endNode:
                    var endConnections = FilterConnections(GetConnections(Side.In, endNode), endNode.ProductId);
                    var sumEndConnections = endConnections.Sum(connection => connection.Amount);
                    if (!CompareFloatingPointNumbers(endNode.Amount, sumEndConnections)) return false;
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
                connections = FilterConnections(GetConnections(Side.Out, spawnNode), spawnNode.ProductId).ToList();
                if (exactTarget == null) productNode.Amount = connections.Sum(v => v.Amount);
                else DistributeOutputConnections(connections, productNode.Amount);
                break;
            case EndNode endNode:
                connections = FilterConnections(GetConnections(Side.In, endNode), endNode.ProductId).ToList();
                if (exactTarget == null) productNode.Amount = connections.Sum(v => v.Amount);
                else DistributeInputConnections(connections, productNode.Amount);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(productNode));
        }
    }

    private void DistributeInputConnections(IEnumerable<Connection> connections, float amount)
    {
        // Remove nodes with exact targets
        var connectionsCopy = new List<Connection>(connections).Where(c =>
        {
            var nodeIn = GetNode(c.NodeInId);
            var nodeOut = GetNode(c.NodeOutId);
            var exactTargetIn = GetTarget(nodeIn, TargetProductionTypes.ExactAmount);
            var exactTargetOut = GetTarget(nodeOut, TargetProductionTypes.ExactAmount);
            
            // Stop if exact target is not present
            if (exactTargetIn == null) return true;
            
            // Check if two exact nodes are connected
            if (exactTargetOut != null)
            {
                var num1 = GetRealAmount(nodeIn, c.ProductId, Side.Out);
                var num2 = GetRealAmount(nodeOut, c.ProductId, Side.In);
                var minAmount = Math.Min(num1, num2);
                c.Amount = minAmount;
                amount -= minAmount;
            }
            else
            {
                amount -= GetRealAmount(nodeIn, c.ProductId, Side.Out);
            }

            return false;
        }).ToList();

        // Distribute remaining amounts
        foreach (var connection in connectionsCopy)
        {
            var newAmount = amount / connectionsCopy.Count;
            if (newAmount > connection.Amount) 
                connection.Amount = newAmount;
        }
    }

    private void DistributeOutputConnections(IEnumerable<Connection> connections, float amount)
    {
        // Remove nodes with exact targets
        var connectionsCopy = new List<Connection>(connections).Where(c =>
        {
            var nodeIn = GetNode(c.NodeInId);
            var nodeOut = GetNode(c.NodeOutId);
            var exactTargetIn = GetTarget(nodeIn, TargetProductionTypes.ExactAmount);
            var exactTargetOut = GetTarget(nodeOut, TargetProductionTypes.ExactAmount);
            
            // Stop if exact target is not present
            if (exactTargetOut == null) return true;
            
            // Check if two exact nodes are connected
            if (exactTargetIn != null)
            {
                var num1 = GetRealAmount(nodeIn, c.ProductId, Side.Out);
                var num2 = GetRealAmount(nodeOut, c.ProductId, Side.In);
                var minAmount = Math.Min(num1, num2);
                c.Amount = minAmount;
                amount -= minAmount;
            }
            else
            {
                amount -= GetRealAmount(nodeOut, c.ProductId, Side.In);
            }

            return false;
        }).ToList();

        // Distribute remaining amounts
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
                    var conn = GetConnections(Side.In, productionNode);
                    var connectionsFiltered = FilterConnections(conn, inputThroughPut.ProductId);
                    if (!hasExactTarget)
                    {
                        var newAmount = connectionsFiltered.Sum(v => v.Amount) / inputThroughPut.Amount;
                        if (productionNode.Amount < newAmount) productionNode.Amount = newAmount;
                    }
                    DistributeInputConnections(connectionsFiltered, inputThroughPut.Amount * productionNode.Amount);
                }
                foreach (var outputThroughPut in GetRecipe(productionNode.RecipeId).OutputThroughPuts)
                {
                    var connectionsFiltered = FilterConnections(GetConnections(Side.Out, productionNode), outputThroughPut.ProductId).ToList();
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

    private float GetRealAmount(INode node, Guid productId, Side side)
    {
        switch (node)
        {
            case IHasProduct:
                return node.Amount;
            case IHasRecipe recipeNode:
                var recipe = GetRecipe(recipeNode.RecipeId);
                var throughPut = GetThroughput(side, recipe, productId);
                return node.Amount * throughPut.Amount;
            default:
                throw new ArgumentOutOfRangeException(nameof(node));
        }
    }

    private Recipe GetRecipe(Guid id)
    {
        return _entityContainer.GetRecipe(id)!;
    }

    private INode GetNode(Guid id)
    {
        return _worksheet.Nodes.First(n => n.Id == id);
    }

    private TargetProduction? GetTarget(INode node, TargetProductionTypes type)
    {
        return node.Targets.FirstOrDefault(v => v.Type == type);
    }

    private ThroughPut GetThroughput(Side side, Recipe recipe, Guid productId)
    {
        return side == Side.In
            ? recipe.InputThroughPuts.Find(t => t.ProductId == productId)!
            : recipe.OutputThroughPuts.Find(t => t.ProductId == productId)!;
    }
    
    private IEnumerable<Connection> GetConnections(Side side, INode node)
    {
        return side == Side.Out
            ? _worksheet.Connections.Where(connection => connection.NodeInId == node.Id)
            : _worksheet.Connections.Where(connection => connection.NodeOutId == node.Id);
    }

    private static IEnumerable<Connection> FilterConnections(IEnumerable<Connection> connections, Guid productId)
    {
        return connections.Where(connection => connection.ProductId.Equals(productId));
    }

    public static bool CompareFloatingPointNumbers(float num1, float num2)
    {
        return Math.Abs(num1 - num2) < 0.1;
    }
    
    private enum Side { In, Out }
}