using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Nfh.Editor.Thumbs
{
    internal class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragStarted += MoveThumb_DragStarted;
            DragDelta += MoveThumb_DragDelta;
        }

        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            var element = DataContext as UIElement;
            if (element == null) return;

            double left = Canvas.GetLeft(element);
            double top = Canvas.GetTop(element);

            if (double.IsNaN(left)) Canvas.SetLeft(element, 0.0);
            if (double.IsNaN(top)) Canvas.SetTop(element, 0.0);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var element = DataContext as UIElement;
            if (element == null) return;

            double left = Canvas.GetLeft(element);
            double top = Canvas.GetTop(element);

            double newLeft = left + e.HorizontalChange;
            double newTop = top + e.VerticalChange;
            
            if (SnapsToDevicePixels)
            {
                newLeft = Math.Round(newLeft);
                newTop = Math.Round(newTop);
            }

            Canvas.SetLeft(element, newLeft);
            Canvas.SetTop(element, newTop);
        }
    }
}
