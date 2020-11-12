using Mvvm.Framework.ViewModel;
using Nfh.Domain.Interfaces;
using Nfh.Domain.Models.InGame;
using Nfh.Domain.Models.Meta;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Nfh.Editor.ViewModels
{
    public class LevelMetaViewModel : ViewModelBase
    {
        public string Name => levelMeta.Id;
        public string Title 
        { 
            get => levelMeta.Description.Title; 
            set 
            { 
                levelMeta.Description.Title = value;
                NotifyAllModelChanges(); 
            }
        }
        public BitmapImage Thumbnail { get; set; }
        public string ThumbnailDescription
        {
            get => levelMeta.Description.ThumbnailDescription;
            set
            {
                levelMeta.Description.ThumbnailDescription = value;
                NotifyAllModelChanges();
            }
        }
        public string Description
        {
            get => levelMeta.Description.Description;
            set
            {
                levelMeta.Description.Description = value;
                NotifyAllModelChanges();
            }
        }
        public string Hint
        {
            get => levelMeta.Description.Hint;
            set
            {
                levelMeta.Description.Hint = value;
                NotifyAllModelChanges();
            }
        }
        public bool Unlocked 
        { 
            get => levelMeta.Unlocked;
            set
            {
                levelMeta.Unlocked = value;
                NotifyAllModelChanges();
            }
        }
        public int TrickCount => levelMeta.TrickCount;
        public int MinPercent
        {
            get => levelMeta.MinPercent;
            set
            {
                levelMeta.MinPercent = value;
                NotifyAllModelChanges();
            }
        }
        public TimeSpan? TimeLimit
        {
            get => levelMeta.TimeLimit;
            set
            {
                levelMeta.TimeLimit = value;
                NotifyAllModelChanges();
            }
        }

        private LevelMeta levelMeta;

        public LevelMetaViewModel(LevelMeta levelMeta) 
            : base(Services.ModelChangeNotifier, levelMeta)
        {
            this.levelMeta = levelMeta;
            Thumbnail = BitmapToImageSource(Services.Image.LoadLevelThumbnail(levelMeta.Id, Services.GamePath));
        }

        // Source: https://stackoverflow.com/questions/22499407/how-to-display-a-bitmap-in-a-wpf-image
        private static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }
    }
}
