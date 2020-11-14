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
    public class MetaViewModel
    {
        public ICommand New { get; } = new NewCommand();
        public ICommand Open { get; } = new OpenCommand();
        public ICommand Save { get; } = new SaveCommand();

        public ICommand Undo { get; } = new UndoCommand();
        public ICommand Redo { get; } = new RedoCommand();

        public SeasonPackViewModel SeasonPack { get; }

        public MetaViewModel()
        {
            SeasonPack = new SeasonPackViewModel(Services.Project.LoadSeasonPack(Services.GamePath));
        }
    }
}
