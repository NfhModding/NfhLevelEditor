using Nfh.Domain.Models.Meta;
using Nfh.Editor.Dialogs;
using System;
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
            : base(MetaViewModel.Current.UndoRedo, levelMeta)
        {
            this.levelMeta = levelMeta;
            var thumbnail = new LoadingDialog().Execute(() => 
                BitmapToImageSource(Services.Image.LoadLevelThumbnail(levelMeta.Id, MetaViewModel.Current.GamePath)));
            Thumbnail = (BitmapImage)thumbnail;
        }
    }
}
