namespace productionCalculatorLib.components.products;

public class ThroughPut
{
    public Product Product { get; set; }
    public float Amount { get; set; }

    public ThroughPut(Product product, float amount)
    {
        Product = product;
        Amount = amount;
    }

    private bool Equals(ThroughPut other)
    {
        return Product.Equals(other.Product);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ThroughPut) obj);
    }

    public override int GetHashCode()
    {
        return Product.GetHashCode();
    }

    public override string ToString()
    {
        return $"ThroughPut:{{Product{Product}, Amount:{Amount}}}";
    }
}