using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface IHasProduct: INode
{
    Product Product { get; set; }
    float Amount { get; set; }
}