namespace ProductionCalculator.Core.components.nodes.interfaces;

public interface IHasProduct: INode
{
    Guid ProductId { get; set; }
}