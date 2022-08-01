namespace productionCalculatorLib.components.products;

public class Product
{
    public long Id { get; } = IdGenerators.ProductId;
    public string Name { get; set; }

    public Product(string name)
    {
        Name = name;
    }

    private bool Equals(Product other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Product) obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override string ToString()
    {
        return $"Product:{{Name:{Name}}}";
    }
}