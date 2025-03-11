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

            Element foundation = doc.GetElement(uiDoc.Selection.GetElementIds().First());

            Options option = new Options();
            option.IncludeNonVisibleObjects = false;
            option.DetailLevel = ViewDetailLevel.Medium;
            option.ComputeReferences = true;

            GeometryElement geoElement = foundation.get_Geometry(option);
            List<Solid> listSolid = new List<Solid>();
            foreach (GeometryObject geoObj in geoElement)
            {
                Solid solid = geoObj as Solid;
                if (solid != null && solid.Volume > 0.000001)
                {
                    listSolid.Add(solid);
                }
            }


            PlanarFace bottomFace = null;
            foreach (Solid solid in listSolid)
            {
                foreach (Face face in solid.Faces)
                {
                    if (face is PlanarFace plannarFace)
                    {
                        XYZ normalFace = plannarFace.FaceNormal.Normalize();
                        double dotProduct = normalFace.DotProduct(XYZ.BasisZ);
                        if (Math.Abs(dotProduct + 1) < 0.00001)
                        {
                            bottomFace = plannarFace;
                            break;
                        }
                    }
                }
            }

            CurveLoop curveloopBot = bottomFace.GetEdgesAsCurveLoops().OrderByDescending(x => x.GetExactLength()).First();

            double rebarCover = 50 / 304.8;
            double diameterRebarCover = 13 / 304.8;

            ElementType typeFounation = doc.GetElement(foundation.GetTypeId()) as ElementType;

            double thickness = typeFounation.get_Parameter(BuiltInParameter.FLOOR_ATTR_DEFAULT_THICKNESS_PARAM).AsDouble();

            Transform transformToTop = Transform.CreateTranslation(XYZ.BasisZ * thickness);
            CurveLoop curveloopTop = CurveLoop.CreateViaTransform(curveloopBot, transformToTop);

            CurveLoop curveloopBotRebarOffset = CurveLoop.CreateViaOffset(curveloopBot, diameterRebarCover + rebarCover, -XYZ.BasisZ);
            CurveLoop curveloopTopRebarOffset = CurveLoop.CreateViaOffset(curveloopTop, diameterRebarCover + rebarCover, XYZ.BasisZ);

            XYZ direcitonRebar1 = null;
            Line lineDiretionBot1 = null;
            double spaing1 = 150 / 304.8;

            int indexLineFirst = 0;
            foreach (Curve curve in curveloopBotRebarOffset)
            {
                if (curve is Line line)
                {
                    direcitonRebar1 = line.Direction.Normalize();
                    lineDiretionBot1 = line;
                    break;
                }
                indexLineFirst++;
            }

            Line lineDirectionTop1 = null;
            int index = 0;
            foreach (Curve curve in curveloopTopRebarOffset)
            {

                if (index == indexLineFirst)
                {
                    lineDirectionTop1 = curve as Line;
                    break;
                }
                index++;
            }

            double maxDistance = 0;
            XYZ point = curveloopBot.First().GetEndPoint(0);
            foreach (Curve curve in curveloopBot)
            {
                XYZ p1 = curve.GetEndPoint(0);
                XYZ p2 = curve.GetEndPoint(1);
                double d1 = p1.DistanceTo(point);
                double d2 = p2.DistanceTo(point);
                if (d1 > maxDistance) maxDistance = d1;
                if (d2 > maxDistance) maxDistance = d2;
            }

            double extend = maxDistance / 2;

            double totalOffset = 0;
            XYZ directionSpacing1 = -direcitonRebar1.CrossProduct(-XYZ.BasisZ).Normalize();
            List<List<Curve>> arrayListCurve = new List<List<Curve>>();
            while (true)
            {
                Line lineBotOffset = lineDiretionBot1;
                Line lineTopOffset = lineDirectionTop1;
                if (totalOffset > 0)
                {
                    Transform transform = Transform.CreateTranslation(directionSpacing1 * totalOffset);
                    lineBotOffset = lineDiretionBot1.CreateTransformed(transform) as Line;
                    lineTopOffset = lineDirectionTop1.CreateTransformed(transform) as Line;
                    ExtendLine(ref lineBotOffset, extend);
                    ExtendLine(ref lineTopOffset, extend);

                    XYZ pointInter1 = null;
                    XYZ pointerInter2 = null;
                    foreach (Curve curve in curveloopBotRebarOffset)
                    {
                        lineBotOffset.Intersect(curve, out IntersectionResultArray resultArray);
                        if (resultArray != null && resultArray.Size == 1)
                        {
                            if (pointInter1 == null)
                            {
                                pointInter1 = resultArray.get_Item(0).XYZPoint;
                            }
                            else
                            {
                                pointerInter2 = resultArray.get_Item(0).XYZPoint;
                            }
                        }
                    }

                    XYZ pointTopInter1 = null;
                    XYZ pointerTopInter2 = null;
                    foreach (Curve curve in curveloopTopRebarOffset)
                    {
                        lineBotOffset.Intersect(curve, out IntersectionResultArray resultArray);
                        if (resultArray != null && resultArray.Size == 1)
                        {
                            if (pointTopInter1 == null)
                            {
                                pointTopInter1 = resultArray.get_Item(0).XYZPoint;
                            }
                            else
                            {
                                pointerTopInter2 = resultArray.get_Item(0).XYZPoint;
                            }
                        }
                    }

                    if (pointInter1 == null || pointerInter2 == null) break;

                    Line lineInter1 = Line.CreateBound(pointInter1, pointerInter2);
                    Line lineInter2 = Line.CreateBound(pointTopInter1, pointerTopInter2);
                    if (!lineInter1.Direction.Normalize().IsAlmostEqualTo(lineInter2.Direction.Normalize(), 0.000001))
                    {
                        lineInter2 = lineInter2.CreateReversed() as Line;
                    }

                    Line line1 = Line.CreateBound(lineInter1.GetEndPoint(0), lineInter2.GetEndPoint(0));
                    Line line2 = Line.CreateBound(lineInter1.GetEndPoint(0), lineInter1.GetEndPoint(1));
                    Line line3 = Line.CreateBound(lineInter1.GetEndPoint(1), lineInter1.GetEndPoint(1));
                    arrayListCurve.Add(new List<Curve> { line1, line2, line3 });
                }
                else
                {
                    Line line1 = Line.CreateBound(lineBotOffset.GetEndPoint(0), lineTopOffset.GetEndPoint(0));
                    Line line2 = Line.CreateBound(lineTopOffset.GetEndPoint(0), lineTopOffset.GetEndPoint(1));
                    Line line3 = Line.CreateBound(lineTopOffset.GetEndPoint(1), lineBotOffset.GetEndPoint(1));
                    arrayListCurve.Add(new List<Curve> { line1, line2, line3 });
                }





            }
















            XYZ directionRebar2 = direcitonRebar1.CrossProduct(XYZ.BasisZ).Normalize();








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
