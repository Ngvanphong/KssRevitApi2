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
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = commandData.Application.ActiveUIDocument.Document;

            List<Curve> listCurveEdge = new List<Curve>();
            while (true)
            {
                try
                {
                    Reference pickRef = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Edge, "Pick edge of host");
                    Edge edge = doc.GetElement(pickRef).GetGeometryObjectFromReference(pickRef) as Edge;
                    if (edge != null && edge.AsCurve() != null) listCurveEdge.Add(edge.AsCurve());
                }
                catch { break; }
            }
            List<Curve> listCurveOrder = new List<Curve>();

            XYZ startPointCurvelop = null;
            foreach (Curve curve in listCurveEdge)
            {
                List<XYZ> listPointCurve = new List<XYZ> { curve.GetEndPoint(0), curve.GetEndPoint(1) };
                foreach (XYZ point in listPointCurve)
                {
                    bool hasStart = true;
                    foreach (Curve curveCheck in listCurveEdge)
                    {
                        if (curveCheck == curve) continue;
                        List<XYZ> listPointCheck = new List<XYZ> { curveCheck.GetEndPoint(0), curveCheck.GetEndPoint(1) };
                        foreach (XYZ pointCheck in listPointCheck)
                        {
                            if (point.IsAlmostEqualTo(pointCheck,0.0001))
                            {
                                hasStart = false;
                                break;
                            }
                        }
                        if (!hasStart) break;
                    }
                    if (hasStart)
                    {
                        startPointCurvelop = point;
                        break;
                    }

                }
                if (startPointCurvelop != null) break;

            }

            CurveLoop curveloop = new CurveLoop();
            XYZ pointNext = startPointCurvelop;
            foreach(Curve  curve in listCurveEdge)
            {
                XYZ sp = curve.GetEndPoint(0);
                XYZ ep= curve.GetEndPoint(1);
                if(sp.IsAlmostEqualTo(pointNext, 0.0001))
                {
                    curveloop.Append(curve);
                }
                if(ep.IsAlmostEqualTo(pointNext, 0.0001))
                {
                    Curve curveRevert = curve.CreateReversed();
                }
            }




            return Result.Succeeded;
        }
    }
}
