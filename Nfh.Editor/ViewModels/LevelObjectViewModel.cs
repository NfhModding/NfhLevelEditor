using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    public class LevelObjectViewModel : EditorViewModelBase
    {
        public Point Position
        {
            get => levelObject.Position;
            set => ChangeProperty(levelObject, value);
        }

        private LevelObject levelObject;

        public LevelObjectViewModel(LevelObject levelObject)
            : base(levelObject)
        {
            this.levelObject = levelObject;
        }
    }
}
