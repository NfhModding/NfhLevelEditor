using Mvvm.Framework.Command;
using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.ViewModels
{
    public class DoorViewModel : LevelObjectViewModel
    {
        public ICommand UnsetExit { get; }

        public DoorViewModel? Exit
        { 
            get
            {
                var exitModel = ((Door)Model).Exit;
                return exitModel == null ? null : (DoorViewModel)Level.Objects[exitModel];
            }
            set => ChangeProperty(Model, value);
        }

        public DoorViewModel(LevelViewModel level, Door door) 
            : base(level, door)
        {
            UnsetExit = new RelayCommand<object?>(
                _ => ChangeProperty(door, (object?)null, nameof(Exit)),
                _ => door.Exit != null);
        }

        internal override void PostInitialize()
        {
            base.PostInitialize();
            NotifyPropertyChanged(nameof(Exit));
        }
    }
}
