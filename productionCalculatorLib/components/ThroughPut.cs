namespace productionCalculatorLib.components;

public class ThroughPut
{
    public Product Product { get; set; }
    public float Amount { get; set; }

    public ThroughPut(Product product, float amount)
    {
        Product = product;
        Amount = amount;
    }

    public override string ToString()
    {
        return $"ThroughPut:{{Product{Product}, Amount:{Amount}}}";
    }
}