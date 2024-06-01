using productionCalculatorLib.components.calculator.linkedDomain;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.calculator;

internal static class CalculatorNodeChecker
{
    internal static void CheckNodeConnectionForAlerts(ICollection<INode> nodes, ICollection<WorksheetAlert> alerts)
    {
        foreach (var node in nodes)
        {
            if (node is ILinkedNodeOut nodeOut) CheckNodeForAlerts(nodeOut, alerts);
            if (node is ILinkedNodeIn nodeIn) CheckNodeForAlerts(nodeIn, alerts);
        }
    }

    private static void CheckNodeForAlerts(ILinkedNodeOut nodeOut, ICollection<WorksheetAlert> alerts)
    {
        var productIds = nodeOut switch
        {
            ILinkedHasProduct productNode => new List<Guid> { productNode.ProductId },
            ILinkedHasRecipe recipeNode => recipeNode.Recipe.OutputThroughPuts.Select(t => t.ProductId).ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(nodeOut))
        };

        foreach (var productId in productIds)
        {
            if (nodeOut.OutConnections.Any(c => c.ProductId.Equals(productId))) continue;
            alerts.Add(new WorksheetAlert(WorksheetAlertType.NodeMissingOutput)
            {
                NodeId = nodeOut.Id,
                ProductId = productId
            });
        }
        
        foreach (var connection in nodeOut.OutConnections)
        {
            if (productIds.Contains(connection.ProductId)) continue;
            alerts.Add(new WorksheetAlert(WorksheetAlertType.ConnectionInvalidOutput)
            {
                NodeId = nodeOut.Id,
                ConnectionId = connection.Id,
                ProductId = connection.ProductId
            });
        }
    }

    private static void CheckNodeForAlerts(ILinkedNodeIn nodeIn, ICollection<WorksheetAlert> alerts)
    {
        var productIds = nodeIn switch
        {
            ILinkedHasProduct productNode => new List<Guid> { productNode.ProductId },
            ILinkedHasRecipe recipeNode => recipeNode.Recipe.InputThroughPuts.Select(t => t.ProductId).ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(nodeIn))
        };

        foreach (var productId in productIds)
        {
            if (nodeIn.InConnections.Any(c => c.ProductId.Equals(productId))) continue;
            alerts.Add(new WorksheetAlert(WorksheetAlertType.NodeMissingInput)
            {
                NodeId = nodeIn.Id,
                ProductId = productId
            });
        }
        
        foreach (var connection in nodeIn.InConnections)
        {
            if (productIds.Contains(connection.ProductId)) continue;
            alerts.Add(new WorksheetAlert(WorksheetAlertType.ConnectionInvalidInput)
            {
                NodeId = nodeIn.Id,
                ConnectionId = connection.Id,
                ProductId = connection.ProductId
            });
        }
    }
}