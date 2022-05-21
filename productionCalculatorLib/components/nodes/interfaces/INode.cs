using productionCalculatorLib.components.nodes.enums;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface INode
{
    NodeTypes NodeType { get; }

    void RemoveConnectedNode(INode node);
}