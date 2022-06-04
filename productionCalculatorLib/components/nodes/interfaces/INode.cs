namespace productionCalculatorLib.components.nodes.interfaces;

public interface INode
{
    int Id { get; }
    void RemoveConnectedNode(INode node);
}