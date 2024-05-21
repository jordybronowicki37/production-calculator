using productionCalculatorLib.components.calculator.linkedDomain;
using productionCalculatorLib.components.entities;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.targets;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.calculator;

public class Calculator
{
    private readonly Worksheet _worksheet;
    private int _amountOfTimesCalculated;
    private ICollection<INode> _linkedNodes;
    private ICollection<LinkedConnection> _linkedConnections;
    
    public Calculator(Worksheet worksheet, EntityContainer entityContainer)
    {
        _worksheet = worksheet;
        var linked = WorksheetLinker.LinkWorksheet(_worksheet, entityContainer);
        _linkedNodes = linked.Nodes;
        _linkedConnections = linked.Connections;
    }

    public void ReCalculateAmounts()
    {
        ExecuteCalculation();
        foreach (var node in _worksheet.Nodes)
        {
            var linkedNode = _linkedNodes.First(n => n.Id == node.Id);
            node.Amount = linkedNode.Amount;
        }
        foreach (var connection in _worksheet.Connections)
        {
            var linkedConnection = _linkedConnections.First(c => c.Id == connection.Id);
            connection.Amount = linkedConnection.Amount;
        }
    }

    private void ExecuteCalculation()
    {
        _worksheet.Alerts.Clear();
        
        // Check if worksheet is empty
        if (_worksheet.Nodes.Count == 0)
        {
            _worksheet.CalculationSucceeded = false;
            _worksheet.Alerts.Add(new WorksheetAlert(WorksheetAlertType.WorksheetEmpty));
            return;
        }
        
        // Check if target is missing
        if (!CheckForExactLimit())
        {
            _worksheet.CalculationSucceeded = false;
            _worksheet.Alerts.Add(new WorksheetAlert(WorksheetAlertType.WorksheetTargetMissing));
            return;
        }
        
        if (CheckResult())
        {
            _worksheet.CalculationSucceeded = true;
            return;
        }
        
        ResetAmounts();
        while (_amountOfTimesCalculated < _worksheet.Nodes.Count*5)
        {
            CalculateStep();
            if (CheckResult())
            {
                _worksheet.CalculationSucceeded = true;
                return;
            }
            _amountOfTimesCalculated++;
        }
        _worksheet.CalculationSucceeded = false;
        _worksheet.Alerts.Add(new WorksheetAlert(WorksheetAlertType.CalculationOverflow));
    }

    private bool CheckForExactLimit()
    {
        return _linkedNodes.Any(node => GetTarget(node, TargetProductionTypes.ExactAmount) != null);
    }
    
    private void ResetAmounts()
    {
        foreach (var node in _linkedNodes)
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
            foreach (var connection in _linkedConnections) connection.Amount = 0;
        }
    }
    
    private bool CheckResult()
    {
        foreach (var node in _linkedNodes)
        {
            switch (node)
            {
                case LinkedSpawnNode spawnNode:
                    var spawnConnections = FilterConnections(spawnNode.OutConnections, spawnNode.ProductId);
                    var sumSpawnConnections = spawnConnections.Sum(connection => connection.Amount);
                    if (!CompareFloatingPointNumbers(spawnNode.Amount, sumSpawnConnections)) return false;
                    break;
                case LinkedProductionNode productionNode:
                    foreach (var throughPut in productionNode.Recipe.InputThroughPuts)
                    {
                        var prodConnections = FilterConnections(productionNode.InConnections, throughPut.ProductId);
                        var prodConnectionsSum = prodConnections.Sum(connection => connection.Amount);
                        if (!CompareFloatingPointNumbers(productionNode.Amount * throughPut.Amount, prodConnectionsSum)) return false;
                    }
                    foreach (var throughPut in productionNode.Recipe.OutputThroughPuts)
                    {
                        var prodConnections = FilterConnections(productionNode.OutConnections, throughPut.ProductId);
                        var prodConnectionsSum = prodConnections.Sum(connection => connection.Amount);
                        if (!CompareFloatingPointNumbers(productionNode.Amount * throughPut.Amount, prodConnectionsSum)) return false;
                    }
                    break;
                case LinkedEndNode endNode:
                    var endConnections = FilterConnections(endNode.InConnections, endNode.ProductId);
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
        foreach (var node in _linkedNodes)
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
        ICollection<LinkedConnection> connections;

        switch (productNode)
        {
            case LinkedSpawnNode spawnNode:
                connections = FilterConnections(spawnNode.OutConnections, spawnNode.ProductId).ToList();
                if (exactTarget == null) productNode.Amount = connections.Sum(v => v.Amount);
                else DistributeOutputConnections(connections, productNode.Amount);
                break;
            case LinkedEndNode endNode:
                connections = FilterConnections(endNode.InConnections, endNode.ProductId).ToList();
                if (exactTarget == null) productNode.Amount = connections.Sum(v => v.Amount);
                else DistributeInputConnections(connections, productNode.Amount);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(productNode));
        }
    }

    private void DistributeInputConnections(IEnumerable<LinkedConnection> connections, float amount)
    {
        // Remove nodes with exact targets
        var connectionsCopy = new List<LinkedConnection>(connections).Where(c =>
        {
            var exactTargetIn = GetTarget(c.NodeIn, TargetProductionTypes.ExactAmount);
            var exactTargetOut = GetTarget(c.NodeOut, TargetProductionTypes.ExactAmount);
            
            // Stop if exact target is not present
            if (exactTargetIn == null) return true;
            
            // Check if two exact nodes are connected
            if (exactTargetOut != null)
            {
                var num1 = GetRealAmount(c.NodeIn, c.ProductId, Side.Out);
                var num2 = GetRealAmount(c.NodeOut, c.ProductId, Side.In);
                var minAmount = Math.Min(num1, num2);
                c.Amount = minAmount;
                amount -= minAmount;
            }
            else
            {
                amount -= GetRealAmount(c.NodeIn, c.ProductId, Side.Out);
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

    private void DistributeOutputConnections(IEnumerable<LinkedConnection> connections, float amount)
    {
        // Remove nodes with exact targets
        var connectionsCopy = new List<LinkedConnection>(connections).Where(c =>
        {
            var exactTargetIn = GetTarget(c.NodeIn, TargetProductionTypes.ExactAmount);
            var exactTargetOut = GetTarget(c.NodeOut, TargetProductionTypes.ExactAmount);
            
            // Stop if exact target is not present
            if (exactTargetOut == null) return true;
            
            // Check if two exact nodes are connected
            if (exactTargetIn != null)
            {
                var num1 = GetRealAmount(c.NodeIn, c.ProductId, Side.Out);
                var num2 = GetRealAmount(c.NodeOut, c.ProductId, Side.In);
                var minAmount = Math.Min(num1, num2);
                c.Amount = minAmount;
                amount -= minAmount;
            }
            else
            {
                amount -= GetRealAmount(c.NodeOut, c.ProductId, Side.In);
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
            case LinkedProductionNode productionNode:
                var hasExactTarget = GetTarget(recipeNode, TargetProductionTypes.ExactAmount) != null;

                foreach (var inputThroughPut in productionNode.Recipe.InputThroughPuts)
                {
                    var connectionsFiltered = FilterConnections(productionNode.InConnections, inputThroughPut.ProductId).ToList();
                    if (!hasExactTarget)
                    {
                        var newAmount = connectionsFiltered.Sum(v => v.Amount) / inputThroughPut.Amount;
                        if (productionNode.Amount < newAmount) productionNode.Amount = newAmount;
                    }
                    DistributeInputConnections(connectionsFiltered, inputThroughPut.Amount * productionNode.Amount);
                }
                foreach (var outputThroughPut in productionNode.Recipe.OutputThroughPuts)
                {
                    var connectionsFiltered = FilterConnections(productionNode.OutConnections, outputThroughPut.ProductId).ToList();
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
            case ILinkedHasProduct:
                return node.Amount;
            case ILinkedHasRecipe recipeNode:
                var throughPut = GetThroughput(side, recipeNode.Recipe, productId);
                return node.Amount * throughPut.Amount;
            default:
                throw new ArgumentOutOfRangeException(nameof(node));
        }
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

    private static IEnumerable<LinkedConnection> FilterConnections(IEnumerable<LinkedConnection> connections, Guid productId)
    {
        return connections.Where(connection => connection.ProductId.Equals(productId));
    }

    public static bool CompareFloatingPointNumbers(float num1, float num2)
    {
        return Math.Abs(num1 - num2) < 0.1;
    }
    
    private enum Side { In, Out }
}