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
            Layers = new(new(level.Objects
                .Concat(level.Rooms.SelectMany(room => room.Value.Objects))
                .Select(kv => kv.Value)
                .GroupBy(obj => obj.Layer)
                .Select(g => (LayerIndex: g.Key, Objects: (IEnumerable<LevelObject>)g))
                .OrderBy(t => t.LayerIndex)
                .Select(t => new LevelLayerViewModel(t.Objects))));
            LayersReverse = new(new(Layers.Reverse()));
        }
    }
}
