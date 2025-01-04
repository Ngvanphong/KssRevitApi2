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
            double lengthInch = 2;// don vi inch


            FormatOptions formatOption= doc.GetUnits().GetFormatOptions(SpecTypeId.Length);
            double lengthDiplayUnit = UnitUtils.ConvertFromInternalUnits(lengthInch, formatOption.GetUnitTypeId());

            

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


            return Result.Succeeded;
        }
    }

    public class ColumnFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if(elem.Category != null && elem.Category.Id.Value == (long)BuiltInCategory.OST_StructuralColumns)
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
