using productionCalculatorLib.components.worksheet;

namespace SiteReact.Controllers.dto.worksheets;

public class DtoWorksheetSmall
{
    public string Name { get; }
    
    public DtoWorksheetSmall(Worksheet worksheet)
    {
        Name = worksheet.Name;
    }
}