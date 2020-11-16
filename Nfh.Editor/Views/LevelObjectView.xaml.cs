using Nfh.Editor.ViewModels;
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
using Xceed.Wpf.AvalonDock.Controls;

namespace Nfh.Editor.Views
{
    /// <summary>
    /// Interaction logic for LevelObjectView.xaml
    /// </summary>
    public partial class LevelObjectView : UserControl
    {
        public LevelObjectView()
        {
            InitializeComponent();
        }

        private void MoveThumb_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e) =>
            SelectProperLayer(sender);

        private void Image_MouseDown(object sender, MouseButtonEventArgs e) =>
            SelectProperLayer(sender);

        private void SelectProperLayer(object sender)
        {
            var element = sender as UIElement;
            if (element == null) return;
            var layerVm = (LevelLayerViewModel)element.FindVisualAncestor<LevelLayerView>().DataContext;
            var levelVm = (LevelViewModel)element.FindVisualAncestor<LevelView>().DataContext;
            levelVm.SelectedLayer = layerVm;
        }
    }
}
