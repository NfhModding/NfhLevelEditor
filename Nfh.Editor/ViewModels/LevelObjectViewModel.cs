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
            get => levelObject.Position;
            set => ChangeProperty(levelObject, value);
        }
        public BitmapImage? Image { get; protected set; }
        
        private LevelObject levelObject;

        public LevelObjectViewModel(LevelObject levelObject)
            : base(levelObject)
        {
            this.levelObject = levelObject;
            if (   levelObject.Visuals != null 
                && levelObject.Visuals.Animations.TryGetValue("ms", out var ms))
            {
                var firstNonNullPath = ms.Frames
                    .Select(frame => frame.ImagePath)
                    .FirstOrDefault(path => path != null);
                if (firstNonNullPath != null)
                {
                    var image = Services.Image.LoadAnimationFrame(levelObject.Id, firstNonNullPath, Services.GamePath);
                    Image = BitmapToImageSource(image);
                }
            }
        }
    }
}
