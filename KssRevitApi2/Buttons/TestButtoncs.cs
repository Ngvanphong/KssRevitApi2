using Autodesk.Revit.UI;
using KssRevitApi2.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
namespace KssRevitApi2.Buttons
{
    internal class TestButtoncs
    {
        public void CreateTestButton(UIControlledApplication app)
        {
            try
            {
                app.CreateRibbonTab(AppConstants.RibbonTab);
            }
            catch { }
            RibbonPanel ribbbonPanel = null;
            foreach(var panel in app.GetRibbonPanels())
            {
                if(panel.Name== AppConstants.TabPanel)
                {
                    ribbbonPanel = panel;
                    break;
                }
            }
            if(ribbbonPanel == null)
            {
                ribbbonPanel= app.CreateRibbonPanel(AppConstants.RibbonTab,AppConstants.TabPanel);
            }

            PushButtonData pushButtonData = new PushButtonData("TestKss", "Test \n Revit Api",
                Assembly.GetExecutingAssembly().Location, typeof(TestBinding).FullName);
            pushButtonData.LongDescription = "Long Description";

            PushButton pushButton = ribbbonPanel.AddItem(pushButtonData) as PushButton;
            pushButton.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (2).png", UriKind.RelativeOrAbsolute));
            pushButton.LargeImage= new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (2).png", UriKind.RelativeOrAbsolute));
            pushButton.Enabled = true;

        }
    }
}
