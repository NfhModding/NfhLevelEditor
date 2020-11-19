using Nfh.Editor.Adorners;
using Nfh.Editor.Converters;
using Nfh.Editor.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private class RelativePositionConverter : IMultiValueConverter
        {
            private double offset;

            public RelativePositionConverter(double offset)
            {
                this.offset = offset;
            }

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
                {
                    return 0.0;
                }
                double first = (int)values[0];
                double second = (int)values[1];
                return first - second + offset;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
                throw new NotSupportedException();
        }

        private class VisibilityConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (values[0] == null || !(bool)values[1]) return Visibility.Hidden;
                return Visibility.Visible;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
                throw new NotSupportedException();
        }

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
            arrow.IsHitTestVisible = false;
            var size = DesiredSize;
            arrow.X1 = size.Width / 2;
            arrow.Y1 = size.Height / 2;
            {
                var xBinding = new MultiBinding();
                xBinding.Bindings.Add(new Binding("Exit.Position.X"));
                xBinding.Bindings.Add(new Binding("Position.X"));
                xBinding.Converter = new RelativePositionConverter(size.Width / 2);
                arrow.SetBinding(Arrow.X2Property, xBinding);
            }
            {
                var yBinding = new MultiBinding();
                yBinding.Bindings.Add(new Binding("Exit.Position.Y"));
                yBinding.Bindings.Add(new Binding("Position.Y"));
                yBinding.Converter = new RelativePositionConverter(size.Height / 2);
                arrow.SetBinding(Arrow.Y2Property, yBinding);
            }
            {
                var visBinding = new MultiBinding();
                visBinding.Bindings.Add(new Binding("Exit"));
                visBinding.Bindings.Add(new Binding("IsVisible")
                { 
                    Source = this,
                });
                visBinding.Converter = new VisibilityConverter();
                arrow.SetBinding(VisibilityProperty, visBinding);
            }
            adorner.Content = arrow;

            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            adornerLayer.Add(adorner);
        }
    }
}
