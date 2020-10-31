using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvm.Framework.ViewModel
{
    /// <summary>
    /// A command that can be executed to modify the model.
    /// </summary>
    public interface IModelChangeCommand
    {
        /// <summary>
        /// Applies the canges to the model.
        /// </summary>
        public void Execute();
        /// <summary>
        /// Constructs the command that can undo the executed changes on the model.
        /// This must be called before execution so the relevant properties can be copied out.
        /// </summary>
        /// <returns>The <see cref="IModelChangeCommand"/> that can undo this one's changes.</returns>
        public IModelChangeCommand GetUndoCommand();
    }
}
