namespace productionCalculatorLib.components.nodes.interfaces;

public interface INodeInOut: INodeIn, INodeOut
{
    Recipe Recipe { get; set; }
}