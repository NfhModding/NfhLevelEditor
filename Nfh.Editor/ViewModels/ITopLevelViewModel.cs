using Mvvm.Framework.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    public interface ITopLevelViewModel
    {
        public IUndoRedoStack UndoRedo { get; }

        public void SaveChanges();
    }
}
