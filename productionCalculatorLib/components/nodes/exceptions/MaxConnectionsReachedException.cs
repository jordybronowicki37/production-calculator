namespace productionCalculatorLib.components.nodes.exceptions;

public class MaxConnectionsReachedException: SystemException
{
    public MaxConnectionsReachedException()
    {
    }

    public MaxConnectionsReachedException(string? message) : base(message)
    {
    }
}