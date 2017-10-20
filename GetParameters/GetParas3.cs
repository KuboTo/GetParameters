using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GetParameters
{
    [Transaction(TransactionMode.Manual)]
    public class GetParas3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData document, ref string message, ElementSet elements)
        {
            UIApplication uiapp = document.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
            FilteredElementCollector wallCollector = new FilteredElementCollector(doc);
            wallCollector.WherePasses(new ElementCategoryFilter(BuiltInCategory.OST_Walls)).WhereElementIsNotElementType();
            double wallArea = 0;
            double wallsArea = 0;
            string parameterName = "面积";
            foreach (Wall wall in wallCollector)
            {
                Parameter parameter = wall.LookupParameter(parameterName);
                wallArea = parameter.AsDouble();
                wallsArea = wallsArea + wallArea;
            }
            MessageBox.Show("所有墙面积之和为" + wallsArea.ToString() + "。");
            return Result.Succeeded;
        }
    }
}
