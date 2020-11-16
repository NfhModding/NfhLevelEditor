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
    public class LevelMetaViewModel : EditorViewModelBase
    {
        public string Name => levelMeta.Id;
        public string Title 
        { 
            get => levelMeta.Description.Title;
            set => ChangeProperty(levelMeta, value, "Description.Title");
        }
        public BitmapImage Thumbnail { get; set; }
        public string ThumbnailDescription
        {
            get => levelMeta.Description.ThumbnailDescription;
            set => ChangeProperty(levelMeta, value, "Description.ThumbnailDescription");
        }
        public string Description
        {
            get => levelMeta.Description.Description;
            set => ChangeProperty(levelMeta, value, "Description.Description");
        }
        public string Hint
        {
            get => levelMeta.Description.Hint;
            set => ChangeProperty(levelMeta, value, "Description.Hint");
        }
        public bool Unlocked 
        { 
            get => levelMeta.Unlocked;
            set => ChangeProperty(levelMeta, value);
        }
        public int TrickCount => levelMeta.TrickCount;
        public int MinPercent
        {
            get => levelMeta.MinPercent;
            set => ChangeProperty(levelMeta, value);
        }
        public TimeSpan? TimeLimit
        {
            get => levelMeta.TimeLimit;
            set => ChangeProperty(levelMeta, value);
        }

        private LevelMeta levelMeta;

        public LevelMetaViewModel(LevelMeta levelMeta) 
            : base(Services.ModelChangeNotifier, levelMeta)
        {
            this.levelMeta = levelMeta;
            Thumbnail = BitmapToImageSource(Services.Image.LoadLevelThumbnail(levelMeta.Id, Services.GamePath));
        }
    }
}
