using Mvvm.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
