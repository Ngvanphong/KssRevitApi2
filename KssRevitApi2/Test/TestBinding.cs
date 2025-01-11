using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
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

            #region comment
            //IEnumerable<ElementId> ids = uiDoc.Selection.GetElementIds();


            //FamilyInstance column = null;
            //try
            //{
            //    Reference pickRef = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, new ColumnFilter(), "Pick a column");
            //    column = doc.GetElement(pickRef) as FamilyInstance;
            //}
            //catch { }

            //BoundingBoxXYZ boundingBox = column.get_BoundingBox(null);

            //List<FamilyInstance> listColumn = new List<FamilyInstance>();
            //var listRefSelected = uiDoc.Selection.PickObjects(ObjectType.Element, new ColumnFilter(), "Pick a columns");

            //var selectdRef = uiDoc.Selection.PickElementsByRectangle(new ColumnFilter(), "Pick a column");

            //var selecREf2 = uiDoc.Selection.PickBox()


            //FilteredElementCollector collection = new FilteredElementCollector(doc, doc.ActiveView.Id);
            //collection = collection.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType();
            //IEnumerable<Wall> walls = collection.ToElements().Cast<Wall>();

            //List<Wall> listWallLevel2 = walls.Where(item =>
            //{
            //    Parameter paramter = item.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE);
            //    ElementId levelId = paramter.AsElementId();
            //    Level level = doc.GetElement(levelId) as Level;
            //    return level.Name == "L2";
            //}).ToList();

            //var level1 = new FilteredElementCollector(doc).OfClass(typeof(Level)).First(x => x.Name == "L1");
            //ElementId idPara = new ElementId(BuiltInParameter.WALL_HEIGHT_TYPE);
            //FilterRule filterRule = ParameterFilterRuleFactory.CreateEqualsRule(idPara, level1.Id);
            //ElementParameterFilter elemenentParaFilter = new ElementParameterFilter(filterRule);
            //var listWall = collection.WherePasses(elemenentParaFilter).ToElements();


            //// collect all type of wall
            //IEnumerable<WallType> listWallType = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls)
            //    .WhereElementIsElementType().Cast<WallType>();

            //Wall wallFirst = listWall.First() as Wall;

            //Parameter parameterLength = wallFirst.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
            //double lengthInch = 2;// don vi inch


            //FormatOptions formatOption= doc.GetUnits().GetFormatOptions(SpecTypeId.Length);
            //double lengthDiplayUnit = UnitUtils.ConvertFromInternalUnits(lengthInch, formatOption.GetUnitTypeId());



            //BoundingBoxXYZ boundingBox = wallFirst.get_BoundingBox(doc.ActiveView);
            //XYZ min = boundingBox.Min;
            //XYZ max = boundingBox.Max;

            //double xMin = Math.Min(min.X, max.X);
            //double yMin = Math.Min(min.Y, max.Y);
            //double zMin = Math.Min(min.Z, max.Z);
            //double xMax = Math.Max(min.X, max.X);
            //double yMax = Math.Max(min.Y, max.Y);
            //double zMax = Math.Max(min.Z, max.Z);


            //Outline outline = new Outline(new XYZ(xMin, yMin, zMin), new XYZ(xMax, yMax, zMax));

            //BoundingBoxIntersectsFilter boundingIntersecFilter = new BoundingBoxIntersectsFilter(outline);
            //ExclusionFilter exculusionFilter = new ExclusionFilter(new List<ElementId> { wallFirst.Id });

            //ElementCategoryFilter beamFilter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            //ElementCategoryFilter colFilter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            //LogicalOrFilter logicalOrFilter = new LogicalOrFilter(new List<ElementFilter> { beamFilter, colFilter });


            //LogicalAndFilter logicalAndFilter = new LogicalAndFilter(new List<ElementFilter> { boundingIntersecFilter, exculusionFilter,
            //    logicalOrFilter });



            //var listIntersect = new FilteredElementCollector(doc, doc.ActiveView.Id).WherePasses(logicalAndFilter)
            //    .ToElements();


            ////IEnumerable<Wall> walls = colleciton.OfClass(typeof(Wall)).Cast<Wall>();


            //FamilyInstance column = doc.GetElement(uiDoc.Selection.GetElementIds().First()) as FamilyInstance;

            //XYZ rightVector = doc.ActiveView.RightDirection;
            //XYZ upVector = doc.ActiveView.UpDirection;
            //XYZ viewVector = doc.ActiveView.ViewDirection;

            //XYZ vectorMove = new XYZ(-1, 0, 0) * 500 / 304.8;
            //using (Transaction t = new Transaction(doc, "MoveElement"))
            //{
            //    t.Start();
            //    ElementTransformUtils.MoveElement(doc, column.Id, vectorMove);
            //    t.Commit();
            //}

            //Location locationCol = column.Location;
            //XYZ locationPoint = (locationCol as LocationPoint).Point;
            //Line lineAxis = Line.CreateUnbound(locationPoint, viewVector);
            //using(Transaction t2= new Transaction(doc, "Rotate"))
            //{
            //    t2.Start();
            //    ElementTransformUtils.RotateElement(doc, column.Id, lineAxis, Math.PI / 6);
            //    t2.Commit();
            //}
            #endregion

            Wall wall = doc.GetElement(uiDoc.Selection.GetElementIds().First()) as Wall;
            LocationCurve locationCurve = wall.Location as LocationCurve;
            Curve curve = locationCurve.Curve;

            //Arc arc = curve as Arc;
            //XYZ pointCenter = arc.Center;

            //XYZ viewVector = doc.ActiveView.ViewDirection.Normalize();
            //Curve curveOfffset=  curve.CreateOffset(500 / 304.8, viewVector);

            //Curve curveReverse = curve.CreateReversed();

            //Transform transformRight1000 = Transform.CreateTranslation(doc.ActiveView.RightDirection.Normalize() * 1000 / 304.8);
            //Curve cureMoveRight1000= curve.CreateTransformed(transformRight1000);


            //Transform transformRotate45 = Transform.CreateRotationAtPoint(XYZ.BasisZ, Math.PI/4, pointCenter);

            // Curve curveRotate = curve.CreateTransformed(transformRotate45);

            //Transform tasnformMove = Transform.CreateTranslation(pointCenter-)


            double startPara = curve.GetEndParameter(0);
            XYZ startPoint = curve.GetEndPoint(0);
            double endPara = curve.GetEndParameter(1);
            XYZ endPoint = curve.GetEndPoint(1);

            double rate = 1000 / 304.8 / curve.Length;
            double startParaAfter = startPara - rate * (endPara - startPara);
            double endParaAfter = endPara + rate * (endPara - startPara);


            curve.MakeBound(startParaAfter, endParaAfter);

            using (Transaction t = new Transaction(doc, "RotateWall"))
            {
                t.Start();
                (wall.Location as LocationCurve).Curve = curve;
                t.Commit();
            }
            //ElementTransformUtils.RotateElement(doc, wall.Id, )
            Curve curve2 = null;
            SetComparisonResult compareResult = curve.Intersect(curve2, out IntersectionResultArray resultArray);
            if (compareResult == SetComparisonResult.Overlap)
            {
                for (int i = 0; i < resultArray.Size; i++)
                {
                    XYZ pontIntersect = resultArray.get_Item(i).XYZPoint;
                }
            }
            XYZ pointAtCenter= curve.Evaluate((startPara+ endPara)/2,false);
            Transform derivative = curve.ComputeDerivatives((startPara + endPara) / 2, false);
            XYZ vectorM=  derivative.BasisX.Normalize() * 1000 / 304.8;
            Transform transformM = Transform.CreateTranslation(vectorM);
            var listCopy= ElementTransformUtils.CopyElement(doc, wall.Id, new XYZ(1000 / 304, 0, 0));
            Curve curve1 = null;
            double offset = 1000 / 304.8;
            double paraStart = curve1.GetEndParameter(0);
            double paraEnd = curve1.GetEndParameter(1);
            double rateLeft = (curve1.Length / 2 - offset) / curve1.Length;
            double paraLeft = paraStart + rateLeft * (paraEnd - paraStart);

            XYZ pointLEft = curve1.Evaluate(paraLeft, false);

            return Result.Succeeded;
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
