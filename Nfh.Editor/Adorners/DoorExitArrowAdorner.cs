using Nfh.Editor.Shapes;
using Nfh.Editor.ViewModels;
using Nfh.Editor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Nfh.Editor.Adorners
{
    public class DoorExitArrowAdorner : Adorner
    {
        private DoorView doorView;

        public DoorExitArrowAdorner(DoorView doorView) 
            : base(doorView)
        {
            this.doorView = doorView;
            IsHitTestVisible = false;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            // TODO: Factor out constants maybe?

            var doorVm = doorView.DataContext as DoorViewModel;
            if (doorVm == null) return;
            if (doorVm.Exit == null) return;

            var pen = new Pen(Brushes.Red, 2);

            var size = doorView.DesiredSize;
            var p1_ = doorVm.Position;
            var p2_ = doorVm.Exit.Position;

            double headWidth = 5;
            double headHeight = 5;

            double theta = Math.Atan2(headWidth, headHeight);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);

            var p1 = new Point(0, 0);
            var p2 = new Point(p2_.X - p1_.X, p2_.Y - p1_.Y);

            double l1 = Math.Sqrt(p2.X * p2.X + p2.Y * p2.Y);
            double l2 = Math.Sqrt(headWidth * headWidth + headHeight * headHeight);

            var w2 = size.Width / 2;
            var h2 = size.Height / 2;

            var p3 = new Point(
                p2.X + (l2 / l1) * ((p1.X - p2.X) * cost + (p1.Y - p2.Y) * sint) + w2,
                p2.Y + (l2 / l1) * ((p1.Y - p2.Y) * cost - (p1.X - p2.X) * sint) + h2);
            var p4 = new Point(
                p2.X + (l2 / l1) * ((p1.X - p2.X) * cost - (p1.Y - p2.Y) * sint) + w2,
                p2.Y + (l2 / l1) * ((p1.Y - p2.Y) * cost + (p1.X - p2.X) * sint) + h2);

            p1.X += w2;
            p1.Y += h2;
            p2.X += w2;
            p2.Y += h2;

            drawingContext.DrawLine(pen, p1, p2);
            drawingContext.DrawLine(pen, p2, p3);
            drawingContext.DrawLine(pen, p2, p4);
        }
    }
}
