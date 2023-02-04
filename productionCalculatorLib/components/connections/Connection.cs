﻿using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.connections;

public class Connection
{
    public long Id { get; set; }
    public long NodeInId { get; init; }
    public long NodeOutId { get; init; }
    public virtual Product Product { get; set; }
    public float Amount { get; set; }
    public virtual ICollection<TargetConnection> Targets { get; private set; } = new List<TargetConnection>();
    
    public Connection() {}

    public Connection(INodeOut nodeIn, INodeIn nodeOut, Product product)
    {
        NodeInId = nodeIn.Id;
        NodeOutId = nodeOut.Id;
        Product = product;
    }

    public void AddConnectionTarget(TargetConnection target)
    {
        if (!Targets.Contains(target)) Targets.Add(target);
    }
    
    public void RemoveConnectionTarget(TargetConnection target)
    {
        Targets.Remove(target);
    }

    protected bool Equals(Connection other)
    {
        return NodeInId.Equals(other.NodeInId) && NodeOutId.Equals(other.NodeOutId) && Product.Equals(other.Product);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Connection) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(NodeInId, NodeOutId);
    }

    public static bool operator ==(Connection? left, Connection? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Connection? left, Connection? right)
    {
        return !Equals(left, right);
    }
}