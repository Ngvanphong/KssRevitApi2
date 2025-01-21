using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssRevitApi2.CreateColumns
{
    public static class CreateColumnAppShow
    {
        public static CreateColumnWpf FormCreateColumn { get; set; }

        public static void ShowForm()
        {
            try { FormCreateColumn.Close(); }catch { }

            PickPointHandler pickPointHanlder= new PickPointHandler();
            ExternalEvent pickPointEvent= ExternalEvent.Create(pickPointHanlder);

            FormCreateColumn = new CreateColumnWpf(pickPointEvent);
            FormCreateColumn.Show();
        }
    }
}
