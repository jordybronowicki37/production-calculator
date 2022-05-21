namespace productionCalculatorLib.components.nodes.interfaces;

public interface INodeIn: INode
{
    IList<INode> InputNodes { get; }
    void AddInputNode(INodeOut node);
}