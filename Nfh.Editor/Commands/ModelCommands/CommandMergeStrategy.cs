using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;

namespace Nfh.Editor.Commands.ModelCommands
{
    public class CommandMergeStrategy : ICommandMergeStrategy
    {
        public bool AllowMerge { get; set; } = false;

        public bool CanMerge(IModelChangeCommand first, IModelChangeCommand second) => AllowMerge;
    }
}
