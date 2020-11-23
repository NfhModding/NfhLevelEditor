using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Nfh.Editor.ViewModels
{
    public class LevelObjectViewModel : EditorViewModelBase
    {
        public Point Position
        {
            get
            {
                var result = Model.Position;
                if (room != null)
                {
                    result.X += room.Offset.X;
                    result.Y += room.Offset.Y;
                }
                if (Model.Visuals != null && Model.Visuals.Regions.Count > 0)
                {
                    var firstRegion = Model.Visuals.Regions.First();
                    result.X += firstRegion.Bounds.Left;
                    result.Y += firstRegion.Bounds.Top;
                }
                return result;
            }
            set
            {
                if (room != null)
                {
                    value.X -= room.Offset.X;
                    value.Y -= room.Offset.Y;
                }
                if (Model.Visuals != null && Model.Visuals.Regions.Count > 0)
                {
                    var firstRegion = Model.Visuals.Regions.First();
                    value.X -= firstRegion.Bounds.Left;
                    value.Y -= firstRegion.Bounds.Top;
                }
                ChangeProperty(Model, value);
            }
        }
        public BitmapImage? Image { get; protected set; }
        
        // Needed for backreferencing
        public LevelObject Model { get; }
        public LevelViewModel Level { get; }

        private Room? room;

        public LevelObjectViewModel(LevelViewModel level, Room? room, LevelObject levelObject)
            : base(levelObject)
        {
            Level = level;
            this.room = room;
            Model = levelObject;

            DetermineVisuals();
        }

        internal virtual void EndClickAction()
        {
            if (Level.SettingNeighbor != null)
            {
                // TODO: Maybe it's enough for the level to know that a neighbor is being set?
                // DoorVM could get rid of it's 'SettingExit'
                Level.SettingNeighbor.SettingExit = false;
                Level.SettingNeighbor = null;
            }
        }

        private void DetermineVisuals()
        {
            if (Model.Visuals == null) return;
            if (!Model.Visuals.Animations.TryGetValue("ms", out var ms)) return;
            var firstNonNullPath = ms.Frames
                    .Select(frame => frame.ImagePath)
                    .FirstOrDefault(path => path != null);
            if (firstNonNullPath == null) return;

            var image = Services.Image.LoadAnimationFrame(Model.Visuals.Id, firstNonNullPath, Services.GamePath);
            Image = BitmapToImageSource(image);
        }
    }
}
