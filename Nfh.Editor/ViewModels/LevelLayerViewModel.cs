using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    public class LevelLayerViewModel : EditorViewModelBase
    {
        private bool visible = true;
        public bool Visible 
        { 
            get => visible; 
            set
            {
                visible = value;
                NotifyPropertyChanged();
            }
        }
        private bool editable = true;
        public bool Editable
        {
            get => editable;
            set
            {
                editable = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<LevelObjectViewModel> Objects { get; }

        public LevelLayerViewModel(IEnumerable<LevelObjectViewModel> levelObjects)
            : base(LevelEditViewModel.Current.UndoRedo, levelObjects.Select(wm => wm.Model).ToArray())
        {
            Objects = new ObservableCollection<LevelObjectViewModel>(levelObjects);
        }
    }
}
