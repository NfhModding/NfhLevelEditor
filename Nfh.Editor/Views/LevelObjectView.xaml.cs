using Nfh.Editor.Adorners;
using Nfh.Editor.ViewModels;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Controls;

namespace Nfh.Editor.Views
{
    /// <summary>
    /// Interaction logic for LevelObjectView.xaml
    /// </summary>
    public partial class LevelObjectView : UserControl
    {
        private class SelectedAdornerVisibilityConverter : IValueConverter
        {
            private LevelObjectViewModel levelObject;

            public SelectedAdornerVisibilityConverter(LevelObjectViewModel levelObject)
            {
                this.levelObject = levelObject;
            }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value == null) return Visibility.Hidden;
                if (!(value is LevelLayerViewModel levelLayerVm)) throw new ArgumentException();

                return levelLayerVm.Objects.Contains(levelObject) ? Visibility.Visible : Visibility.Hidden;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
                throw new NotSupportedException();
        }

        public LevelObjectView()
        {
            InitializeComponent();
        }

        private void MoveThumb_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            SelectProperLayer(sender);
            EndClickAction();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectProperLayer(sender);
            EndClickAction();
        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectProperLayer(sender);
            EndClickAction();
        }

        private void SelectProperLayer(object sender)
        {
            var element = sender as UIElement;
            if (element == null) return;
            var layerVm = (LevelLayerViewModel)element.FindVisualAncestor<LevelLayerView>().DataContext;
            var levelVm = (LevelViewModel)element.FindVisualAncestor<LevelView>().DataContext;
            levelVm.SelectedLayer = layerVm;
        }

        private void EndClickAction()
        {
            var vm = DataContext as LevelObjectViewModel;
            if (vm != null) vm.EndClickAction();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            var selectedItemAdorner = new SelectedItemAdorner(this);
            adornerLayer.Add(selectedItemAdorner);

            selectedItemAdorner.SetBinding(
                VisibilityProperty, 
                new Binding()
                { 
                    Source = DataContext,
                    Path = new PropertyPath("Level.SelectedLayer"),
                    Converter = new SelectedAdornerVisibilityConverter((LevelObjectViewModel)DataContext),
                });
        }
    }
}
