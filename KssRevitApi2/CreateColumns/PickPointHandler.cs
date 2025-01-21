using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssRevitApi2.CreateColumns
{
    public class PickPointHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uiDoc = app.ActiveUIDocument;
            Document doc = uiDoc.Document;
            XYZ point = null;
           

        }

        public string GetName()
        {
            return "PickPointHandler";
        }
    }
}
