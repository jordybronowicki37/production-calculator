namespace productionCalculatorLib.components.nodes.interfaces;

public interface INodeOut: INode
{
    IList<INode> OutputNodes { get; }
    void AddOutputNode(INodeIn node);
}