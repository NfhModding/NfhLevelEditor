using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.InGame;
using Nfh.Editor.Commands.ModelCommands;
using Nfh.Editor.Commands.UiCommands;
using System.Windows;
using System.Windows.Input;

namespace Nfh.Editor.ViewModels
{
    public class LevelEditViewModel : ViewModelBase, ITopLevelViewModel
    {
        // Current instance
        public static LevelEditViewModel? Current { get; private set; }

        // Undo-redo stack for level editor
        public IUndoRedoStack UndoRedo { get; } = new UndoRedoStack
        {
            MergeStrategy = new CommandMergeStrategy(),
        };

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
            : base(MetaViewModel.Current.ModelChangeNotifier)
        {
            Current = this;
            Level = new LevelViewModel(level);
        }

        public MessageBoxResult SaveIfHasChanges()
        {
            if (!UndoRedo.HasUnsavedChanges) return MessageBoxResult.Yes;
            // Prompt if we want to save
            var result = MessageBox.Show(
                "Would you like to save the current changes?",
                "Save",
                MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes) SaveChanges();
            return result;
        }

        public void SaveChanges()
        {
            if (Level == null) return;
            Services.Project.SaveLevel(Level.Level, MetaViewModel.Current.ProjectPath);
            UndoRedo.Save();
        }
    }
}
