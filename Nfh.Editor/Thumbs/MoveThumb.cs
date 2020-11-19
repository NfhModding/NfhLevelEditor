using Nfh.Domain.Models.InGame;
using Nfh.Editor.Commands.ModelCommands;
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
            DragStarted += MoveThumb_DragStarted;
            DragDelta += MoveThumb_DragDelta;
            DragCompleted += MoveThumb_DragCompleted;
        }

        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            var element = DataContext as LevelObjectViewModel;
            if (element == null) return;
            if (element.Level.SettingNeighbor != null)
            {
                CancelDrag();
                return;
            }

            // First we force a new command on the stack
            element.Position = element.Position;
            // Then allow merging to this command
            var ms = (CommandMergeStrategy)Services.UndoRedo.MergeStrategy;
            ms.AllowMerge = true;
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

        private void MoveThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            var ms = (CommandMergeStrategy)Services.UndoRedo.MergeStrategy;
            ms.AllowMerge = false;
        }
    }
}
