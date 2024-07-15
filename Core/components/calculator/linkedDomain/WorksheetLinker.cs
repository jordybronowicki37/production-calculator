using ProductionCalculator.Core.components.entityContainer;
using ProductionCalculator.Core.components.nodes.interfaces;
using ProductionCalculator.Core.components.nodes.nodeTypes;
using ProductionCalculator.Core.components.worksheet;

namespace ProductionCalculator.Core.components.calculator.linkedDomain;

public static class WorksheetLinker
{
    public static LinkedWorksheet LinkWorksheet(Worksheet worksheet, EntityContainer entityContainer)
    {
        var linkedNodes = new List<INode>();
        var linkedConnections = new List<LinkedConnection>();
        
        foreach (var node in worksheet.Nodes)
        {
            switch (node)
            {
                case SpawnNode spawnNode:
                    linkedNodes.Add(new LinkedSpawnNode(entityContainer, spawnNode));
                    break;
                case ProductionNode productionNode:
                    linkedNodes.Add(new LinkedProductionNode(entityContainer, productionNode));
                    break;
                case EndNode endNode:
                    linkedNodes.Add(new LinkedEndNode(entityContainer, endNode));
                    break;
            }
        }

        foreach (var connection in worksheet.Connections)
        {
            var nodeOut = (ILinkedNodeOut) linkedNodes.First(n => n.Id == connection.NodeInId);
            var nodeIn = (ILinkedNodeIn) linkedNodes.First(n => n.Id == connection.NodeOutId);
            var linkedConnection = new LinkedConnection(entityContainer, connection, nodeOut, nodeIn);
            linkedConnections.Add(linkedConnection);
            nodeOut.OutConnections.Add(linkedConnection);
            nodeIn.InConnections.Add(linkedConnection);
        }
        
        return new LinkedWorksheet(linkedNodes, linkedConnections);
    }
    
    public record LinkedWorksheet(ICollection<INode> Nodes, ICollection<LinkedConnection> Connections);
}