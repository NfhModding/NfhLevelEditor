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
            get => Model.Position;
            set => ChangeProperty(Model, value);
        }
        public BitmapImage? Image { get; protected set; }
        
        // Needed for backreferencing
        public LevelObject Model { get; }
        public LevelViewModel Level { get; }

        public LevelObjectViewModel(LevelViewModel level, LevelObject levelObject)
            : base(levelObject)
        {
            Level = level;
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
