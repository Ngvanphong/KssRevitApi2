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
    internal class SplitButtons
    {
        public void CreateSplit(UIControlledApplication app)
        {
            try
            {
                app.CreateRibbonTab(AppConstants.RibbonTab);
            }
            catch { }
            RibbonPanel ribbonPanel = null;
            foreach(RibbonPanel item in app.GetRibbonPanels(AppConstants.RibbonTab))
            {
                if (item.Name == AppConstants.TabPanel)
                {
                    ribbonPanel = item;
                    break;
                }
            }
            if(ribbonPanel ==null) ribbonPanel= app.CreateRibbonPanel(AppConstants.RibbonTab, AppConstants.TabPanel);

            PushButtonData pushButton1 = new PushButtonData("SplitButton1", "Split: Button1",
                Assembly.GetExecutingAssembly().Location, typeof(SplitButton1Binding).FullName);
            pushButton1.LongDescription = "Split Button 1";
            pushButton1.Image= new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (3).png", UriKind.RelativeOrAbsolute));
            pushButton1.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (3).png", UriKind.RelativeOrAbsolute));

            PushButtonData pushButton2 = new PushButtonData("SplitButton2", "Split: Button2",
                Assembly.GetExecutingAssembly().Location, typeof(SplitButton2Binding).FullName);
            pushButton2.LongDescription = "Split Button 2";
            pushButton2.Image= new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (2).png", UriKind.RelativeOrAbsolute));
            pushButton2.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (2).png", UriKind.RelativeOrAbsolute));

            SplitButtonData splitButtonData = new SplitButtonData("SplitButton", "Split Button");
            splitButtonData.LongDescription = "Split Btn";
            splitButtonData.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (3).png", UriKind.RelativeOrAbsolute));
            splitButtonData.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (3).png", UriKind.RelativeOrAbsolute));

            SplitButton splitButton = ribbonPanel.AddItem(splitButtonData) as SplitButton;
            splitButton.AddPushButton(pushButton1); splitButton.AddPushButton(pushButton2);
            splitButton.IsSynchronizedWithCurrentItem = false;


        }
    }
}
