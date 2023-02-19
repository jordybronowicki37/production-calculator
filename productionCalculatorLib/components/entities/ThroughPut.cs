namespace productionCalculatorLib.components.entities;

public class ThroughPut
{
    public Guid ProductId { get; init; }
    public float Amount { get; set; }
    
    public ThroughPut() {}

    public ThroughPut(Product product, float amount)
    {
        ProductId = product.Id;
        Amount = amount;
    }

    private bool Equals(ThroughPut other)
    {
        return ProductId.Equals(other.ProductId);
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
        return ProductId.GetHashCode();
    }

    public override string ToString()
    {
        return $"ThroughPut:{{ProductId{ProductId}, Amount:{Amount}}}";
    }
}