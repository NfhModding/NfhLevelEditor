using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvm.Framework
{
    /// <summary>
    /// Default implementation of <see cref="IUndoRedoStack"/> that follows the standard single timeline.
    /// </summary>
    public class UndoRedoStack : IUndoRedoStack
    {
        public bool HasUnsavedChanges => saveIndex != undoStack.Count;
        public bool CanUndo => undoStack.Count > 0;
        public bool CanRedo => redoStack.Count > 0;

        private Stack<IModelChangeCommand> undoStack = new();
        private Stack<IModelChangeCommand> redoStack = new();
        private int saveIndex;

        public void Execute(IModelChangeCommand command)
        {
            // We need to throw away redos
            redoStack.Clear();
            // Make this command undo-able
            var undoCommand = command.GetUndoCommand();
            undoStack.Push(undoCommand);
            // Finally execute the command
            command.Execute();
            // Make sure to invalidate save if needed
            if (saveIndex >= undoStack.Count) saveIndex = -1;
        }

        public void Undo()
        {
            if (!CanUndo) return;
            var undoCommand = undoStack.Pop();
            // Make it redoable
            var redoCommand = undoCommand.GetUndoCommand();
            redoStack.Push(redoCommand);
            // Execute undo
            undoCommand.Execute();
        }

        public void Redo()
        {
            if (!CanRedo) return;
            var redoCommand = redoStack.Pop();
            // Make it undoable
            var undoCommand = redoCommand.GetUndoCommand();
            undoStack.Push(undoCommand);
            // Execute redo
            redoCommand.Execute();
        }

        public void Save() => saveIndex = undoStack.Count;
    }
}
