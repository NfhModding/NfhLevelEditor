using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    public class DoorViewModel : LevelObjectViewModel
    {
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
        }

        internal override void PostInitialize()
        {
            base.PostInitialize();
            NotifyPropertyChanged(nameof(Exit));
        }
    }
}
