using Nfh.Editor.Adorners;
using Nfh.Editor.Shapes;
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

namespace Nfh.Editor.Views
{
    /// <summary>
    /// Interaction logic for DoorView.xaml
    /// </summary>
    public partial class DoorView : UserControl
    {
        private ContentPresenterAdorner adorner;

        public DoorView()
        {
            InitializeComponent();
            adorner = new ContentPresenterAdorner(this);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var arrow = new Arrow();
            arrow.DataContext = DataContext;
            arrow.X1 = 10;
            arrow.Y1 = 10;
            arrow.SetBinding(Arrow.X2Property, new Binding("Position.X"));
            arrow.SetBinding(Arrow.Y2Property, new Binding("Position.Y"));
            adorner.Content = arrow;

            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            adornerLayer.Add(adorner);
        }
    }
}
