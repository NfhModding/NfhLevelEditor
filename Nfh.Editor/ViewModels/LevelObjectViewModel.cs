using Nfh.Domain.Models.InGame;
using Nfh.Editor.Dialogs;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Nfh.Editor.ViewModels
{
    public class LevelObjectViewModel : EditorViewModelBase
    {
        public virtual Point Position
        {
            get
            {
                var result = Model.Position;
                if (room != null)
                {
                    result.X += room.Offset.X;
                    result.Y += room.Offset.Y;
                }
                /*
                NOTE: This is probably for user intercation hitbox
                if (Model.Visuals != null && Model.Visuals.Regions.Count > 0)
                {
                    var firstRegion = Model.Visuals.Regions.First();
                    result.X += firstRegion.Bounds.Left;
                    result.Y += firstRegion.Bounds.Top;
                }*/
                result.X += frameOffset.X;
                result.Y += frameOffset.Y;
                return result;
            }
            set
            {
                if (room != null)
                {
                    value.X -= room.Offset.X;
                    value.Y -= room.Offset.Y;
                }
                /*
                NOTE: This is probably for user intercation hitbox
                if (Model.Visuals != null && Model.Visuals.Regions.Count > 0)
                {
                    var firstRegion = Model.Visuals.Regions.First();
                    value.X -= firstRegion.Bounds.Left;
                    value.Y -= firstRegion.Bounds.Top;
                }
                */
                value.X -= frameOffset.X;
                value.Y -= frameOffset.Y;
                ChangeProperty(Model, value);
            }
        }
        public BitmapImage? Image { get; protected set; }

        // Needed for backreferencing
        public LevelObject Model { get; }
        public LevelViewModel Level { get; }

        private Room? room;
        private Point frameOffset = new Point();

        public LevelObjectViewModel(LevelViewModel level, Room? room, LevelObject levelObject)
            : base(LevelEditViewModel.Current.UndoRedo, levelObject)
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
            var ms = GetAnimation(Model.Visuals);
            if (ms == null) return;
            var firstVisibleFrame = ms.Frames
                    .FirstOrDefault(frame => frame.ImagePath != null);
            if (firstVisibleFrame == null) return;

            frameOffset = firstVisibleFrame.ImageOffset;
            var image = new LoadingDialog().Execute(() => 
                BitmapToImageSource(Services.Image.LoadAnimationFrame(
                    Model.Visuals.Id, firstVisibleFrame.ImagePath, MetaViewModel.Current.GamePath)));
            Image = (BitmapImage)image;
        }

        private static Animation? GetAnimation(Visuals visuals)
        {
            if (visuals.Animations.TryGetValue("ms", out var ms)) return ms;
            if (visuals.Animations.TryGetValue("ms2", out var ms2)) return ms2;
            return null;
        }
    }
}
