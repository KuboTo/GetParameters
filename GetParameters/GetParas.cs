using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GetParameters
{
    [Transaction(TransactionMode.Manual)]
    public class GetParas : IExternalCommand
    {
        public Result Execute(ExternalCommandData document, ref string message, ElementSet elements)
        {
            UIApplication uiapp = document.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            FilteredElementCollector wallCollector = new FilteredElementCollector(doc);
            wallCollector.WherePasses(new ElementCategoryFilter(BuiltInCategory.OST_Walls)).WhereElementIsNotElementType();
            double wallArea = 0;
            double wallsArea = 0;
            foreach (Wall wall in wallCollector)
            {
                ParameterSet parameters = wall.Parameters;
                foreach (Parameter parameter in parameters)
                {
                    if (parameter.Definition.Name == "面积")
                    {
                        wallArea = parameter.AsDouble();
                        wallsArea = wallsArea + wallArea;
                    }
                }
            }
            MessageBox.Show("所有墙面积之和为" + wallsArea.ToString() + "。");
            return Result.Succeeded;
        }

       
    }
}