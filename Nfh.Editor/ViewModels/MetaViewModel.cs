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
        public ICommand New { get; } = ApplicationCommands.New;
        public ICommand Open { get; } = ApplicationCommands.Open;
        public ICommand Save { get; } = ApplicationCommands.Save;

        public ICommand Undo { get; } = ApplicationCommands.Undo;
        public ICommand Redo { get; } = ApplicationCommands.Redo;

        public SeasonPackViewModel SeasonPack { get; }

        public MetaViewModel()
        {
            SeasonPack = new SeasonPackViewModel(Services.Project.LoadSeasonPack(Services.GamePath));
        }
    }
}
