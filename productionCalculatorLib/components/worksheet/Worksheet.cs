using System.Collections.ObjectModel;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes;
using productionCalculatorLib.components.nodes.abstractions;

namespace productionCalculatorLib.components.worksheet;

public class Worksheet
{
    public Worksheet() {}

    public long Id { get; set; }
    public string Name { get; set; } = "";
    public EntityContainer EntityContainer { get; } = new();

    public bool CalculationSucceeded { get; set; } = true;
    public string CalculationError { get; set; } = "";

    public IList<ANode> Nodes { get; } = new List<ANode>();
    public void AddNode(ANode node)
    {
        if (!Nodes.Contains(node))
        {
            Nodes.Add(node);
        }
    }
    public void RemoveNode(ANode node)
    {
        Nodes.Remove(node);
        node.ClearConnections();
    }

    public NodeBuilder<TNodeType> GetNodeBuilder<TNodeType>() where TNodeType : ANode, new()
    {
        return new NodeBuilder<TNodeType>(this);
    }
}