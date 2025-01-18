using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.ComponentModel;


namespace KssRevitApi2.Test
{
    [Transaction(TransactionMode.Manual)]
    public class TestBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            XYZ firstPoint = uiDoc.Selection.PickPoint("Pick start point");
            XYZ endPoint = uiDoc.Selection.PickPoint("Pick end point");

            XYZ pointOfDim = uiDoc.Selection.PickPoint("Choose a point to put dim");


            double xMin = Math.Min(firstPoint.X, endPoint.X);
            double yMin = Math.Min(firstPoint.Y, endPoint.Y);
            double zMin = Math.Min(firstPoint.Z, endPoint.Z);

            double xMax = Math.Max(firstPoint.X, endPoint.X);
            double yMax = Math.Max(firstPoint.Y, endPoint.Y);
            double zMax = Math.Max(firstPoint.Z, endPoint.Z);

            double extend = 10 / 304.8;
            XYZ min = new XYZ(xMin - extend, yMin - extend, zMin - extend);
            XYZ max = new XYZ(xMax + extend, yMax + extend, zMax + extend);
            Outline outline = new Outline(min, max);


            BoundingBoxIntersectsFilter boundingFilter = new BoundingBoxIntersectsFilter(outline);
            ElementCategoryFilter categoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            LogicalAndFilter andFitler = new LogicalAndFilter(new List<ElementFilter> { boundingFilter, categoryFilter });
            IEnumerable<Wall> walls = new FilteredElementCollector(doc, doc.ActiveView.Id).WherePasses(andFitler)
                .OfClass(typeof(Wall)).Cast<Wall>();


            ReferenceArray referenceArray = new ReferenceArray();
            foreach(Wall wall in walls)
            {
                var innerFaces = HostObjectUtils.GetSideFaces(wall, ShellLayerType.Interior);
                if(innerFaces.Any()) 
                    foreach (var item in innerFaces)
                    {
                        referenceArray.Append(item);
                    }

                var exteriorFaces = HostObjectUtils.GetSideFaces(wall, ShellLayerType.Exterior);
                if (exteriorFaces.Any())
                    foreach (var item in exteriorFaces)
                    {
                        referenceArray.Append(item);
                    }

            }

            XYZ directionWall = null;
            foreach(Wall wall in walls)
            {
                Location location = wall.Location;
                if (location!=null  && location is LocationCurve locaitonCurve)
                {
                    if(locaitonCurve.Curve is Line lineItem)
                    {
                        directionWall = lineItem.Direction.Normalize();
                        break;
                    }
                }

            }

            XYZ direcitonDim = directionWall.CrossProduct(doc.ActiveView.ViewDirection).Normalize();

            Line line = Line.CreateUnbound(pointOfDim, direcitonDim);

            Dimension dimension = null;
            using(Transaction t= new Transaction(doc, "CreateDim"))
            {
                t.Start();
                dimension= doc.Create.NewDimension(doc.ActiveView, line, referenceArray);
                t.Commit();
            }

            DimensionSegmentArray segementArray = dimension.Segments;
            if (segementArray == null)
            {
                ReferenceArray refereenceArray1 = dimension.References;
                double value = dimension.Value.Value;
                string valueString = dimension.ValueString;
            }
            else
            {
                ReferenceArray referenceSegement = dimension.References;
                var iteratorReference = referenceSegement.GetEnumerator();
                iteratorReference.Reset();

                var iterator = segementArray.GetEnumerator();

                iteratorReference.MoveNext();

                iterator.Reset();
                while (iterator.MoveNext())
                {
                    DimensionSegment segment = iterator.Current as DimensionSegment;
                    double value = segment.Value.Value;
                    string valueString = segment.ValueString;

                    Reference startRef = iteratorReference.Current as Reference;
                    iteratorReference.MoveNext();
                    Reference endRef = iteratorReference.Current as Reference;

                    ReferenceArray refArrayItem = new ReferenceArray();
                    refArrayItem.Append(startRef); refArrayItem.Append(endRef);
                   
                    using(Transaction t= new Transaction(doc, "DivideDim"))
                    {
                        t.Start();
                        doc.Create.NewDimension(doc.ActiveView, line, refArrayItem);
                        t.Commit();
                    }


                }

            }

            
            return Result.Succeeded;
        }


        public static  void  GetGeomtryInstance(GeometryInstance geoInst,List<Solid> listSolidResut)
        {
            GeometryElement geoElement = geoInst.GetInstanceGeometry();
            foreach(GeometryObject geoObj  in geoElement)
            {
                if(geoObj is Solid solid && solid.Volume > 0.0001)
                {
                    listSolidResut.Add(solid);
                }
                else
                {
                    GeometryInstance geoItemInst = geoObj as GeometryInstance;
                    if (geoItemInst != null) GetGeomtryInstance(geoItemInst, listSolidResut);
                }
            }
        }
        
    }

    public class ColumnFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem.Category != null && elem.Category.Id.Value == (long)BuiltInCategory.OST_StructuralColumns)
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
