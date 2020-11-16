using Mvvm.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvm.Framework.UndoRedo
{
    /// <summary>
    /// A strategy to merge the previous command with the currently executed one.
    /// Can be used to reduce the number of undos between operations that are inconvenient to separate.
    /// </summary>
    public interface ICommandMergeStrategy
    {
        /// <summary>
        /// Checks, if two commands can be merged.
        /// </summary>
        /// <param name="first">The chronologically first command.</param>
        /// <param name="second">The chronologically second command.</param>
        /// <returns>True, if the two commands can be merged.</returns>
        public bool CanMerge(IModelChangeCommand first, IModelChangeCommand second);
    }
}
