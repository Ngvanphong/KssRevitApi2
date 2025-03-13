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

            IEnumerable<ElementId> idsSelected = uiDoc.Selection.GetElementIds();
            List<FamilyInstance> listBeam= new List<FamilyInstance>();
            foreach(ElementId id in idsSelected)
            {
                FamilyInstance item = doc.GetElement(id) as FamilyInstance;
                if(item != null && item.Category != null && item.Category.Id.Value == (long)BuiltInCategory.OST_StructuralFraming)
                {
                    if(item.Location != null && item.Location is LocationCurve) listBeam.Add(item);
                }
            }

            List<XYZ> listPoint = new List<XYZ>();
            foreach(FamilyInstance beam in listBeam)
            {
                Curve curveBeam = (beam.Location as LocationCurve).Curve;
                List<XYZ> listPointBeam = new List<XYZ>
                {
                    curveBeam.GetEndPoint(0), curveBeam.GetEndPoint(1)
                };
                
                foreach(XYZ point in listPointBeam)
                {
                    if(!listPoint.Exists(x=>x.IsAlmostEqualTo(point,0.001))) listPoint.Add(point);
                }
            }

            // arrange points
            List<List<XYZ>> listPointSort= new List<List<XYZ>>();
            foreach(XYZ point in listPoint)
            {
                bool isExist = false;
                foreach(List<XYZ> listCols in listPointSort)
                {
                    if (Math.Abs(listCols[0].X - point.X)<0.001)
                    {
                        listCols.Add(point);
                        isExist = true;
                        break;
                    }
                }
                if(!isExist)
                {
                    listPointSort.Add(new List<XYZ> { point });
                }
            }

            listPointSort = listPointSort.OrderBy(listCol => listCol[0].X).ToList();

            List<List<XYZ>> listPointSortFinish = new List<List<XYZ>>();
            foreach (List<XYZ> listCol in listPointSort)
            {
                var newlistCol = listCol.OrderBy(item => item.Y).ToList();
                listPointSortFinish.Add(newlistCol);
            }

            List<Line> listLine = new List<Line>();
            for(int col= 0; col< listPointSortFinish.Count; col += 2)
            {
                for(int row= 0; row< listPointSortFinish[col].Count; row += 2)
                {
                    XYZ pointOrigin = listPointSortFinish[col][row];
                    XYZ left = null;
                    if(col-1>=0 && row+1< listPointSortFinish[col].Count)
                    {
                        left= listPointSortFinish[col - 1][row + 1];
                    }

                }

            }








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
