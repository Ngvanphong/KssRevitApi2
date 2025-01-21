using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KssRevitApi2.CreateColumns
{
    /// <summary>
    /// Interaction logic for CreateColumnWpf.xaml
    /// </summary>
    public partial class CreateColumnWpf : Window
    {
        private ExternalEvent _pickPointEvent;
        public CreateColumnWpf(ExternalEvent pickPointEvent)
        {
            InitializeComponent();
            _pickPointEvent = pickPointEvent;
        }

       

        private void btnCreateColumn(object sender, RoutedEventArgs e)
        {
            _pickPointEvent.Raise();
        }
    }
}
