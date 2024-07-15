namespace ProductionCalculator.Core.components.nodes.exceptions;

public class MaxConnectionsReachedException: SystemException
{
    public MaxConnectionsReachedException()
    {
    }

    public MaxConnectionsReachedException(string? message) : base(message)
    {
    }
}