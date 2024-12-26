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
    internal class CombineButtons
    {
        public void CreateCombine(UIControlledApplication app)
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

            PushButtonData pushButton1 = new PushButtonData("CombineButton1", "Split: Button1",
               Assembly.GetExecutingAssembly().Location, typeof(SplitButton1Binding).FullName);
            pushButton1.LongDescription = "Split Button 1";
            pushButton1.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));
            pushButton1.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));

            PushButtonData pushButton2 = new PushButtonData("CombineButton2", "Split: Button2",
                Assembly.GetExecutingAssembly().Location, typeof(SplitButton2Binding).FullName);
            pushButton2.LongDescription = "Split Button 2";
            pushButton2.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));
            pushButton2.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));


            PushButtonData pushButtonSplit1 = new PushButtonData("CombineSplitButton3", "Split: Button3",
                Assembly.GetExecutingAssembly().Location, typeof(SplitButton3Binding).FullName);
            pushButtonSplit1.LongDescription = "Split Button 1";
            pushButtonSplit1.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));
            pushButtonSplit1.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));

            PushButtonData pushButtonSplit2 = new PushButtonData("CombineSplitButton4", "Split: Button3",
                Assembly.GetExecutingAssembly().Location, typeof(SplitButton3Binding).FullName);
            pushButtonSplit2.LongDescription = "Split Button 1";
            pushButtonSplit2.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));
            pushButtonSplit2.LargeImage = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-16 (1).png", UriKind.RelativeOrAbsolute));

            SplitButtonData splitData = new SplitButtonData("SplitCombine", "Split Button");
            splitData.LongDescription = "Split Button";

            IList<RibbonItem> ribbonItems = ribbonPanel.AddStackedItems(pushButton1, splitData, pushButton2);
            foreach (RibbonItem item in ribbonItems)
            {
                if (item.Name == "SplitCombine")
                {
                    SplitButton splitButton = item as SplitButton;
                    splitButton.AddPushButton(pushButtonSplit1);
                    splitButton.AddPushButton(pushButtonSplit2);
                    break;
                }
            }

        }
    }
}
