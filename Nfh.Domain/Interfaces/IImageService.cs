using System.Drawing;

namespace Nfh.Domain.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// Loads the thumbnail image for a level.
        /// </summary>
        public Bitmap LoadLevelThumbnail(string levelId, string gamePath);
        /// <summary>
        /// Loads the animation frame for a <see cref="Models.InGame.LevelObject"/>.
        /// </summary>
        public Bitmap LoadAnimationFrame(string objectId, string frameName, string gamePath);
    }
}
