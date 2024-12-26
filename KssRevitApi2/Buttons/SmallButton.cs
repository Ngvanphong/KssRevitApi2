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
    internal class SmallButton
    {
        public void CreateSmallButton(UIControlledApplication app)
        {
            try
            {
                app.CreateRibbonTab(AppConstants.RibbonTab);
            }
            catch { }
            RibbonPanel ribbonPanel = null;
            foreach (RibbonPanel item in app.GetRibbonPanels(AppConstants.RibbonTab))
            {
                if (item.Name == AppConstants.TabPanel)
                {
                    ribbonPanel = item;
                    break;
                }
            }
            if (ribbonPanel == null) ribbonPanel = app.CreateRibbonPanel(AppConstants.RibbonTab, AppConstants.TabPanel);

            PushButtonData pushButton1 = new PushButtonData("StackButton1", "Split: Button1",
               Assembly.GetExecutingAssembly().Location, typeof(SplitButton1Binding).FullName);
            pushButton1.LongDescription = "Split Button 1";
            pushButton1.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));
            pushButton1.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));

            PushButtonData pushButton2 = new PushButtonData("StackButton2", "Split: Button2",
                Assembly.GetExecutingAssembly().Location, typeof(SplitButton2Binding).FullName);
            pushButton2.LongDescription = "Split Button 2";
            pushButton2.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));
            pushButton2.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));
            
            PushButtonData pushButton3 = new PushButtonData("StackButton3", "Split: Button3",
                Assembly.GetExecutingAssembly().Location, typeof(SplitButton3Binding).FullName);
            pushButton3.LongDescription = "Split Button 2";
            pushButton3.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));
            pushButton3.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));

            ribbonPanel.AddStackedItems(pushButton1, pushButton2, pushButton3);
            
        }
    }
}
