using Nfh.Domain.Models.InGame;
using Nfh.Editor.ViewModels;
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
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragDelta += MoveThumb_DragDelta;
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var element = DataContext as LevelObjectViewModel;
            if (element == null) return;

            double newX = element.Position.X + e.HorizontalChange;
            double newY = element.Position.Y + e.VerticalChange;
            
            if (SnapsToDevicePixels)
            {
                newX = Math.Round(newX);
                newY = Math.Round(newY);
            }

            element.Position = new System.Drawing.Point((int)newX, (int)newY);
        }
    }
}
