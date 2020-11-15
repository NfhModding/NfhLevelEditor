using Mvvm.Framework.ViewModel;
using Nfh.Editor.Commands;
using Nfh.Editor.Commands.UiCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.ViewModels
{
    public class MetaViewModel : ViewModelBase
    {
        public static MetaViewModel? Current { get; private set; }

        public ICommand Exit { get; } = new ExitCommand();

        public ICommand New { get; } = new NewCommand();
        public ICommand Open { get; } = new OpenCommand();
        public ICommand Save { get; } = new SaveCommand();

        public ICommand Undo { get; } = new UndoCommand();
        public ICommand Redo { get; } = new RedoCommand();

        public ICommand Patch { get; } = new PatchCommand();

        private SeasonPackViewModel? seasonPack;
        public SeasonPackViewModel? SeasonPack 
        { 
            get => seasonPack; 
            set
            {
                seasonPack = value;
                NotifyPropertyChanged();
            }
        }

        public MetaViewModel() 
            : base(Services.ModelChangeNotifier)
        {
            Current = this;
        }
    }
}
