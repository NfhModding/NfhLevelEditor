using Nfh.Editor.ViewModels;
using Nfh.Editor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Nfh.Editor.Adorners
{
    public class DoorExitArrowAdorner : Adorner
    {
        public static readonly double StrokeThickness = 2;
        public static readonly Pen Pen = new Pen(Brushes.Red, StrokeThickness);
        public static readonly double HeadWidth = 5;
        public static readonly double HeadHeight = 5;

        private DoorView doorView;

        public DoorExitArrowAdorner(DoorView doorView)
            : base(doorView)
        {
            this.doorView = doorView;
            IsHitTestVisible = false;

            Loaded += DoorExitArrowAdorner_Loaded;
        }

        private void DoorExitArrowAdorner_Loaded(object sender, RoutedEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent(doorView);
            while (!(parent is Window))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            Mouse.AddMouseMoveHandler(parent, (_, _) => InvalidateVisual());
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var doorVm = doorView.DataContext as DoorViewModel;
            if (doorVm == null || !doorVm.SettingExit) return;

            var size = doorView.DesiredSize;
            //var p1_ = doorVm.Position;
            var p2_ = Mouse.GetPosition(doorView);

            double theta = Math.Atan2(HeadWidth, HeadHeight);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);

            var w2 = size.Width / 2;
            var h2 = size.Height / 2;

            var p1 = new Point(w2, h2);
            var p2 = new Point(p2_.X, p2_.Y);

            double l1 = Math.Sqrt(p2.X * p2.X + p2.Y * p2.Y);
            double l2 = Math.Sqrt(HeadWidth * HeadWidth + HeadHeight * HeadHeight);

            var p3 = new Point(
                p2.X + (l2 / l1) * ((p1.X - p2.X) * cost + (p1.Y - p2.Y) * sint),
                p2.Y + (l2 / l1) * ((p1.Y - p2.Y) * cost - (p1.X - p2.X) * sint));
            var p4 = new Point(
                p2.X + (l2 / l1) * ((p1.X - p2.X) * cost - (p1.Y - p2.Y) * sint),
                p2.Y + (l2 / l1) * ((p1.Y - p2.Y) * cost + (p1.X - p2.X) * sint));

            drawingContext.DrawLine(Pen, p1, p2);
            drawingContext.DrawLine(Pen, p2, p3);
            drawingContext.DrawLine(Pen, p2, p4);
        }
    }
}
