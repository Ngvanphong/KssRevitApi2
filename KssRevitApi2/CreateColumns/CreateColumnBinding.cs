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
        public static CreateDimensionListening _updater = null;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = commandData.Application.ActiveUIDocument.Document;

            //using(Transaction t= new Transaction(doc, "Create"))
            //{
            //    t.Start();

            //    FailureHandlingOptions familyHandlerOption = t.GetFailureHandlingOptions();
            //    familyHandlerOption.SetFailuresPreprocessor(new WarningHandle());
            //    t.SetFailureHandlingOptions(familyHandlerOption);


            //    Wall.Create(doc, new List<Curve>(), true);


            //    t.Commit(familyHandlerOption);
            //}

            // listening

            UIApplication uiapp = commandData.Application;
            var app = uiapp.Application;
            if (_updater == null)
            {
                _updater = new CreateDimensionListening(app.ActiveAddInId);
                UpdaterRegistry.RegisterUpdater(_updater);

                ElementCategoryFilter f = new ElementCategoryFilter(BuiltInCategory.OST_Dimensions);
                UpdaterRegistry.AddTrigger(_updater.GetUpdaterId(), f, Element.GetChangeTypeElementAddition());
            }
            else
            {
                try
                {
                    UpdaterRegistry.UnregisterUpdater(_updater.GetUpdaterId());
                    _updater = null;
                }
                catch { }

            }


            doc.LoadFamilySymbol(string.Empty, "NameType", new LoadFamilyOpiton(), out FamilySymbol symbol);

            FamilySymbol familySymbol = null;
            if (!familySymbol.IsActive) familySymbol.Activate();

            Family family = null;
            Document familyDoc= doc.EditFamily(family);
            familyDoc.Close();


            //familyDoc.FamilyCreate.NewExtrusion()

  


            return Result.Succeeded;
        }

       
    }
    public class LoadFamilyOpiton : IFamilyLoadOptions
    {
        public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
        {
            overwriteParameterValues = true;
            return true;
        }

        public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
        {
            overwriteParameterValues = true;
            source = FamilySource.Family;
            return true;
        }
    }
    public class WarningHandle : IFailuresPreprocessor
    {
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            foreach(FailureMessageAccessor warningErr in failuresAccessor.GetFailureMessages())
            {
                if(warningErr.GetSeverity() == FailureSeverity.Warning)
                {
                    failuresAccessor.DeleteWarning(warningErr);
                    FailureDefinitionId idOfWaring= warningErr.GetFailureDefinitionId();
                    if(idOfWaring == BuiltInFailures.WallFailures.WallNegativeHeight)
                    {

                    }
                    return FailureProcessingResult.Continue;
                }
                else 
                {
                    failuresAccessor.DeleteWarning(warningErr);
                    return FailureProcessingResult.ProceedWithRollBack;
                }
            }
            return FailureProcessingResult.Continue;
        }
    }
}
