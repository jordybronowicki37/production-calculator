namespace productionCalculatorLib.components.nodes.interfaces;

public interface IHasProduct: INode
{
    Guid ProductId { get; set; }
    float Amount { get; set; }
}