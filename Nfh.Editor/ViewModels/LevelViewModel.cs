using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public Level Level { get; }

        public LevelViewModel(Level level)
            : base(LevelEditViewModel.Current.UndoRedo, level)
        {
            Level = level;
            Objects = new(level.Objects.Values
                .Select(obj => (Object: obj, Room: (Room?)null))
                .Concat(level.Rooms.Values.SelectMany(room => room.Objects.Values.Select(obj => (Object: obj, Room: room))))
                .Select(pair => ToViewModel(pair.Room, pair.Object))
                .ToDictionary(obj => obj.Model));
            Layers = new(new(Objects.Values
                .GroupBy(obj => obj.Model.Layer)
                .Select(g => (LayerIndex: g.Key, Objects: (IEnumerable<LevelObjectViewModel>)g))
                .OrderBy(t => t.LayerIndex)
                .Select(t => new LevelLayerViewModel(t.Objects))));
            if (Layers.Count > 0)
            {
                Layers[0].Editable = false;
            }
            LayersReverse = new(new(Layers.Reverse()));
        }

        private LevelObjectViewModel ToViewModel(Room? room, LevelObject lo) => lo switch
        {
            Door door => new DoorViewModel(this, room, door),
            Actor actor => new ActorViewModel(this, room, actor),
            _ => new LevelObjectViewModel(this, room, lo),
        };
    }
}
