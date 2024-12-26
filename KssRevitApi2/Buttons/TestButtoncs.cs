using Autodesk.Revit.UI;
using adWindow= Autodesk.Windows;
using KssRevitApi2.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UIFramework;
using KssRevitApi2.Properties;
namespace KssRevitApi2.Buttons
{
    internal class TestButtoncs
    {
        public void CreateTestButton(UIControlledApplication app)
        {
            adWindow.RibbonControl ribbonControl = RevitRibbonControl.RibbonControl;
            adWindow.RibbonTab kssTab = null;
            foreach(adWindow.RibbonTab tab in ribbonControl.Tabs)
            {
                if(tab.AutomationName== AppConstants.RibbonTab)
                {
                    kssTab = tab;
                    break;

                }
            }

            if(kssTab==null) app.CreateRibbonTab(AppConstants.RibbonTab);
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
            //pushButtonData.ToolTipImage= new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (2).png", UriKind.RelativeOrAbsolute));


            PushButton pushButton = ribbbonPanel.AddItem(pushButtonData) as PushButton;
            pushButton.Image = new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (2).png", UriKind.RelativeOrAbsolute));
            pushButton.LargeImage= new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (2).png", UriKind.RelativeOrAbsolute));
            pushButton.Enabled = true;


            // set video tooltip

            adWindow.RibbonItem adPushButton = null;

            foreach (adWindow.RibbonTab tab in ribbonControl.Tabs)
            {
                if (tab.AutomationName == AppConstants.RibbonTab)
                {
                    foreach(adWindow.RibbonPanel adPanel in tab.Panels)
                    {
                        foreach(var panel in adPanel.Source.Items)
                        {
                            if (panel.AutomationName == pushButton.ItemText)
                            {
                                adPushButton = panel;
                                break;
                            }
                        }
                        
                    }

                }
            }

            adWindow.RibbonToolTip ribbonTooltip = new adWindow.RibbonToolTip();
            //ribbonTooltip.ExpandedVideo = new Uri("C:/Program Files"+ "/Autodesk/Revit Structure 2013/Program/videos" + "/GUID-053F3A19-7EFF-48D5-A0FA" + "-AF0C2CC4BD9D-low.swf");
            
            ribbonTooltip.ExpandedImage= new BitmapImage(new Uri("/KssRevitApi2;component/Image/icons8-crop-24 (2).png", UriKind.RelativeOrAbsolute));
            
            adPushButton.ToolTip = ribbonTooltip;

        }
    }
}
