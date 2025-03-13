using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
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
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = commandData.Application.ActiveUIDocument.Document;







            return Result.Succeeded;
        }

        public static void ExtendLine(ref Line curve, double extend)
        {
            double sp = curve.GetEndParameter(0); double ep = curve.GetEndParameter(1);
            double rate = extend / curve.Length;
            curve.MakeBound(sp - rate * (ep - sp), ep + rate * (ep - sp));
        }
    }
}
