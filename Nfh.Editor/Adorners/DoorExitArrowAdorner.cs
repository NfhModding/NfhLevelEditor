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
            var doorVm = doorView.DataContext as DoorViewModel;
            if (doorVm == null) return;
            if (doorVm.Exit == null) return;

            var pen = new Pen(Brushes.Red, 2);

            var size = doorView.DesiredSize;
            var p1 = doorVm.Position;
            var p2 = doorVm.Exit.Position;

            drawingContext.DrawLine(
                pen, 
                new Point(size.Width / 2, size.Height / 2), 
                new Point(p2.X - p1.X + size.Width / 2, p2.Y - p1.Y + size.Height / 2));
        }
    }
}
