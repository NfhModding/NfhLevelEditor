using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.InGame;
using Nfh.Editor.Commands.UiCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.ViewModels
{
    public class LevelEditViewModel : ViewModelBase
    {
        public ICommand Save { get; } = new SaveCommand();
        public ICommand Exit { get; } = new ExitCommand();

        public ICommand Undo { get; } = new UndoCommand();
        public ICommand Redo { get; } = new RedoCommand();

        private LevelViewModel? level;
        public LevelViewModel? Level
        {
            get => level;
            set
            {
                level = value;
                NotifyPropertyChanged();
            }
        }

        public LevelEditViewModel(Level level) 
            : base(Services.ModelChangeNotifier)
        {
            Level = new LevelViewModel(level);
        }
    }
}
