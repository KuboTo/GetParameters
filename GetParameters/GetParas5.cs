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
    public class Test5 : IExternalCommand
    {
        public Result Execute(ExternalCommandData document, ref string message, ElementSet elements)
        {
            UIApplication uiapp = document.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
            FilteredElementCollector wallCollector = new FilteredElementCollector(doc);
            wallCollector.WherePasses(new ElementCategoryFilter(BuiltInCategory.OST_Walls)).WhereElementIsNotElementType();
            string walltest = "";
            string wallstest = "";
            Autodesk.Revit.ApplicationServices.Application app = document.Application.Application;
            app.SharedParametersFilename = @"C:\Users\xincubus\Desktop\test.txt";
            DefinitionFile definitionFile = app.OpenSharedParameterFile();
            DefinitionGroup group = definitionFile.Groups.get_Item("wall");
            Definition definition = group.Definitions.get_Item("测试");
            foreach (Wall wall in wallCollector)
            {
                Parameter parameter = wall.get_Parameter(definition);
                walltest = parameter.AsString();
                wallstest = wallstest + walltest + "\n";
            }
            MessageBox.Show("所有墙的测试参数值分别为" + wallstest);
            return Result.Succeeded;
        }
    }
}
