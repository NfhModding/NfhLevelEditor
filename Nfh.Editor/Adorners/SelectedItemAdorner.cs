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
    public class SelectedItemAdorner : Adorner
    {
        private Pen renderPen = new Pen(new SolidColorBrush(Colors.Red), 1.0);

        public SelectedItemAdorner(UIElement adornedElement) 
            : base(adornedElement)
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect rect = new Rect(AdornedElement.DesiredSize);

            drawingContext.DrawLine(renderPen, rect.TopLeft, rect.TopRight);
            drawingContext.DrawLine(renderPen, rect.TopRight, rect.BottomRight);
            drawingContext.DrawLine(renderPen, rect.BottomRight, rect.BottomLeft);
            drawingContext.DrawLine(renderPen, rect.BottomLeft, rect.TopLeft);
        }
    }
}
