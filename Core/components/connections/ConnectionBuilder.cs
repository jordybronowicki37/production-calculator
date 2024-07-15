using ProductionCalculator.Core.components.entities;
using ProductionCalculator.Core.components.nodes.interfaces;
using ProductionCalculator.Core.components.worksheet;

namespace ProductionCalculator.Core.components.connections;

public class ConnectionBuilder
{
    private Worksheet _worksheet;
    private INodeOut _nodeOut;
    private INodeIn _nodeIn;
    private Product _product;

    public ConnectionBuilder(Worksheet worksheet, INodeOut nodeOut, INodeIn nodeIn, Product product)
    {
        _worksheet = worksheet;
        _nodeOut = nodeOut;
        _nodeIn = nodeIn;
        _product = product;
    }

    public Connection Build()
    {
        var connection = new Connection(_nodeOut, _nodeIn, _product);
        _worksheet.AddConnection(connection);
        return connection;
    }
}