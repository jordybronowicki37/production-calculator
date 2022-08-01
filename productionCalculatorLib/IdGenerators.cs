namespace productionCalculatorLib;

public static class IdGenerators
{
    private static long _nodeId;
    public static long NodeId => _nodeId++;
    
    private static long _worksheetId;
    public static long WorksheetId => _worksheetId++;
    
    private static long _connectionId;
    public static long ConnectionId => _connectionId++;
    
    private static long _productId;
    public static long ProductId => _productId++;
    
    private static long _recipeId;
    public static long RecipeId => _recipeId++;
}