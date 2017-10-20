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
    public class GetParas4 : IExternalCommand
    {
        public Result Execute(ExternalCommandData document, ref string message, ElementSet elements)
        {
            UIApplication uiapp = document.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
            FilteredElementCollector wallCollector = new FilteredElementCollector(doc);
            wallCollector.WherePasses(new ElementCategoryFilter(BuiltInCategory.OST_Walls)).WhereElementIsNotElementType();
            string walltest = "";
            string wallstest = "";
            Guid guid = new Guid("cca4f606-7bd5-413f-97f3-68b9689c5e9b");
            foreach (Wall wall in wallCollector)
            {
                Parameter parameter = wall.get_Parameter(guid);
                walltest = parameter.AsString();
                wallstest = wallstest + walltest + "\n";
            }
            MessageBox.Show("所有墙的测试参数值分别为" + wallstest);
            return Result.Succeeded;
        }
    }
}
