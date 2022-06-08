using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.calculator;

public class CalculatorLimit
{
    private Worksheet _worksheet;
    
    private CalculatorLimit(Worksheet worksheet)
    {
        _worksheet = worksheet;
    }

    public static void ReCalculateAmounts(Worksheet worksheet)
    {
        var w = new CalculatorLimit(worksheet);
        w.Calculate();
    }

    private void Calculate()
    {
        
    }
}