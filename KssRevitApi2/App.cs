using Autodesk.Revit.UI;
using KssRevitApi2.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssRevitApi2
{
    public class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {

            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            new TestButtoncs().CreateTestButton(application);
            new SplitButtons().CreateSplit(application);
            new SmallButton().CreateSmallButton(application);
            new CombineButtons().CreateCombine(application);
            return Result.Succeeded;
        }
    }
}
