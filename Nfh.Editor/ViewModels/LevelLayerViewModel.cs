﻿using Nfh.Domain.Models.InGame;
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
        public ObservableCollection<LevelObjectViewModel> Objects { get; }

        public LevelLayerViewModel(IEnumerable<LevelObject> levelObjects)
        {
            Objects = new ObservableCollection<LevelObjectViewModel>(levelObjects.Select(ToViewModel));
        }

        private static LevelObjectViewModel ToViewModel(LevelObject levelObject) => levelObject switch
        {
            Actor actor => new ActorViewModel(actor),
            Door door => new DoorViewModel(door),
            _ => new LevelObjectViewModel(levelObject),
        };
    }
}