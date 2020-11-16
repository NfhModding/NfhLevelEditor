using Mvvm.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvm.Framework.UndoRedo
{
    /// <summary>
    /// A container to handle the undo and redo of <see cref="IModelChangeCommand"/>s.
    /// </summary>
    public interface IUndoRedoStack
    {
        /// <summary>
        /// The <see cref="ICommandMergeStrategy"/> to use.
        /// </summary>
        public ICommandMergeStrategy MergeStrategy { get; set; }

        /// <summary>
        /// True, if there are changes that hasn't been saved.
        /// </summary>
        public bool HasUnsavedChanges { get; }
        /// <summary>
        /// True, if there are things that can be undone.
        /// </summary>
        public bool CanUndo { get; }
        /// <summary>
        /// True, if there are things that can be redone.
        /// </summary>
        public bool CanRedo { get; }

        /// <summary>
        /// Executes the given command. Clears any redo commands after. Adds it as an undoable command.
        /// </summary>
        /// <param name="command">The <see cref="IModelChangeCommand"/> to execute.</param>
        public void Execute(IModelChangeCommand command);
        /// <summary>
        /// Undoes the last command, if there was any.
        /// </summary>
        public void Undo();
        /// <summary>
        /// Redoes the last undone command, if any.
        /// </summary>
        public void Redo();
        /// <summary>
        /// Marks the current state as a save point.
        /// </summary>
        public void Save();
        /// <summary>
        /// Resets this stack so that there os nothing to undo, redo or save.
        /// </summary>
        public void Reset();
    }
}
