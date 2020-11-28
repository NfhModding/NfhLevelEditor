using Mvvm.Framework.UndoRedo;

namespace Nfh.Editor.ViewModels
{
    public interface ITopLevelViewModel
    {
        public IUndoRedoStack UndoRedo { get; }

        public void SaveChanges();
    }
}
