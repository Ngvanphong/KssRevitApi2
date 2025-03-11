using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View = Autodesk.Revit.DB.View;

namespace KssRevitApi2
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc= uiDoc.Document;
            List<ElementId> ids = uiDoc.Selection.GetElementIds().ToList();
            List<Floor> floors= new List<Floor>();
            foreach (var id in ids)
            {
                Floor floor = doc.GetElement(id) as Floor;
                if(floor != null) floors.Add(floor);
            }

            ReferencePlane referencePlane = null;
            try
            {
                Reference pickObject = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element,
                   new ReferencePlaneFilter(), "Pick a reference plane");
                referencePlane = doc.GetElement(pickObject) as ReferencePlane;
            }
            catch { }


            
            foreach(Floor floor in floors)
            {
                CurveLoop curveloopBounadry = new CurveLoop();
                var iterator = floor.GetSlabShapeEditor().SlabShapeCreases.GetEnumerator();
                iterator.Reset();
                while( iterator.MoveNext())
                {
                    SlabShapeCrease createShape= iterator.Current as SlabShapeCrease;
                    if(createShape != null  && createShape.CreaseType == SlabShapeCreaseType.Boundary)
                    {
                        curveloopBounadry.Append(createShape.Curve);
                    }
                }

                double thick = 5 / 304.8;
                Solid solid = GeometryCreationUtilities.CreateExtrusionGeometry(new List<CurveLoop> { curveloopBounadry }, -XYZ.BasisZ, thick);


                Plane plane = referencePlane.GetPlane();
                var solidLet= BooleanOperationsUtils.CutWithHalfSpace(solid,plane);

                Plane plane2= Plane.CreateByNormalAndOrigin(-plane.Normal, plane.Origin);
                var solidRight = BooleanOperationsUtils.CutWithHalfSpace(solid, plane2);
                
                Solid solidReuslt= solidLet.Volume > solidRight.Volume ? solidLet : solidRight;

                foreach(Face face in solidReuslt.Faces)
                {
                    PlanarFace plannarFace= face as PlanarFace;
                    if(plannarFace != null)
                    {
                        XYZ normal = plannarFace.FaceNormal.Normalize();
                        if (normal.IsAlmostEqualTo(XYZ.BasisZ,0.0001))
                        {
                            foreach(CurveLoop curveloop in plannarFace.GetEdgesAsCurveLoops())
                            {

                            }
                        }
                    }
                }


            }



            return Result.Succeeded;
        }
    }

    public class ReferencePlaneFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is ReferencePlane) return true;
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
