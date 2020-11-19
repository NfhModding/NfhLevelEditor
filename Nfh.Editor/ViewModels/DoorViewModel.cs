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
        public ICommand SetExit { get; }
        public ICommand UnsetExit { get; }

        private bool settingExit = false;
        public bool SettingExit 
        { 
            get => settingExit; 
            set
            {
                settingExit = value;
                NotifyPropertyChanged();
            }
        }

        public DoorViewModel? Exit
        { 
            get
            {
                var exitModel = ((Door)Model).Exit;
                return exitModel == null ? null : (DoorViewModel)Level.Objects[exitModel];
            }
            set => ChangeProperty(Model, value?.Model);
        }

        public DoorViewModel(LevelViewModel level, Door door) 
            : base(level, door)
        {
            SetExit = new RelayCommand<object?>(
                _ =>
                { 
                    SettingExit = true;
                    Level.SettingNeighbor = this;
                });
            UnsetExit = new RelayCommand<object?>(
                _ => ChangeProperty(door, (object?)null, nameof(Exit)),
                _ => door.Exit != null);
        }

        internal override void EndClickAction()
        {
            if (Level.SettingNeighbor != null)
            {
                Level.SettingNeighbor.Exit = this;
            }
            base.EndClickAction();
        }
    }
}
