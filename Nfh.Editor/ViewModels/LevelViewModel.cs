using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    public class LevelViewModel : EditorViewModelBase
    {
        public DoorViewModel? SettingNeighbor { get; set; }

        public ReadOnlyDictionary<LevelObject, LevelObjectViewModel> Objects { get; }
        public ReadOnlyObservableCollection<LevelLayerViewModel> Layers { get; }
        public ReadOnlyObservableCollection<LevelLayerViewModel> LayersReverse { get; }

        private LevelLayerViewModel? selectedLayer;
        public LevelLayerViewModel? SelectedLayer 
        { 
            get => selectedLayer; 
            set
            {
                selectedLayer = value;
                NotifyPropertyChanged();
            }
        }

        public LevelViewModel(Level level)
        {
            Objects = new(level.Objects
                .Concat(level.Rooms.SelectMany(room => room.Value.Objects))
                .Select(kv => kv.Value)
                .Select(ToViewModel)
                .ToDictionary(obj => obj.Model));
            Layers = new(new(Objects.Values
                .GroupBy(obj => obj.Model.Layer)
                .Select(g => (LayerIndex: g.Key, Objects: (IEnumerable<LevelObjectViewModel>)g))
                .OrderBy(t => t.LayerIndex)
                .Select(t => new LevelLayerViewModel(t.Objects))));
            LayersReverse = new(new(Layers.Reverse()));

            foreach (var obj in Objects.Values) obj.PostInitialize();
        }

        private LevelObjectViewModel ToViewModel(LevelObject lo) => lo switch
        {
            Door door => new DoorViewModel(this, door),
            Actor actor => new ActorViewModel(this, actor),
            _ => new LevelObjectViewModel(this, lo),
        };
    }
}
