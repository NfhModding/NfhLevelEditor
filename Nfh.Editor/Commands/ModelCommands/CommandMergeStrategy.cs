using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.Commands.ModelCommands
{
    public class CommandMergeStrategy : ICommandMergeStrategy
    {
        public bool AllowMerge { get; set; } = false;

        public bool CanMerge(IModelChangeCommand first, IModelChangeCommand second) => AllowMerge;
    }
}
