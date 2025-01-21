using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssRevitApi2.CreateColumns
{
    [Transaction(TransactionMode.Manual)]
    public class CreateColumnBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            CreateColumnAppShow.ShowForm();

            List<FamilySymbol> listTypeCol = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralColumns)
                .WhereElementIsElementType().OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>().ToList();

            CreateColumnAppShow.FormCreateColumn.DataContext = new CreateColumnData();
            CreateColumnAppShow.FormCreateColumn.comboboxTypeColumn.ItemsSource= listTypeCol;
               
            return Result.Succeeded;
        }
    }
}
