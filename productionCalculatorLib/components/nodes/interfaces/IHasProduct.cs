namespace productionCalculatorLib.components.nodes.interfaces;

public interface IHasProduct
{
    Product Product { get; set; }
    int Amount { get; set; }
}