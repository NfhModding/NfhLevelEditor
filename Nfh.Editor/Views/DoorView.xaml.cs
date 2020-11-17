using Nfh.Editor.Adorners;
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
        private Adorner doorExit;

        public DoorView()
        {
            doorExit = new DoorExitArrowAdorner(this);
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            adornerLayer.Add(doorExit);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            adornerLayer.Remove(doorExit);
        }
    }
}
