using Mvvm.Framework.ViewModel;

namespace Mvvm.Framework.UndoRedo
{
    /// <summary>
    /// A safe default for <see cref="ICommandMergeStrategy"/> that simply doesn't allow merging anything.
    /// </summary>
    public class NullCommandMergeStrategy : ICommandMergeStrategy
    {
        public bool CanMerge(IModelChangeCommand first, IModelChangeCommand second) => false;
    }
}
