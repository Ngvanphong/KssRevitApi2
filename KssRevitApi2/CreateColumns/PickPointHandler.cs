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
    public class PickPointHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uiDoc = app.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Floor floor = null;
            Reference refTopFace = HostObjectUtils.GetTopFaces(floor).First();
            Face topFace = doc.GetElement(refTopFace).GetGeometryObjectFromReference(refTopFace) as Face;
            Line line = null;
            AreaReinforcementType.CreateDefaultAreaReinforcementType(doc);


            CurveLoop curveloop = null;
            Curve curve1 = null;
            Curve curve2 = null;
            int index = 0;
            foreach(Curve curveIem in curveloop)
            {
                if (index == 0) curve1 = curveIem;
                else if (index == 1) curve2 = curveIem;
            }

            CurveLoop newCurveLoop1 = new CurveLoop();
            newCurveLoop1.Append(curve1); newCurveLoop1.Append(curve2);

            CurveLoop newCurveloop2 = CurveLoop.CreateViaOffset(newCurveLoop1, 1000 / 304.8, XYZ.BasisZ);

            Curve curve11 = null;
            Curve curve22 = null;

            Curve curve4 = curve22.CreateReversed();

            Curve curve5 = curve11.CreateReversed();

            Curve curve3 = Line.CreateBound(curve2.GetEndPoint(1), curve4.GetEndPoint(0));

       




        }

        public string GetName()
        {
            return "PickPointHandler";
        }
    }
}
