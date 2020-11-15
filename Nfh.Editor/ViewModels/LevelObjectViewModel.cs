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
        public int X
        {
            get => levelObject.Position.X;
            set => ChangeProperty(levelObject, value, "Position.X");
        }
        public int Y
        {
            get => levelObject.Position.Y;
            set => ChangeProperty(levelObject, value, "Position.Y");
        }

        private LevelObject levelObject;

        public LevelObjectViewModel(LevelObject levelObject)
            : base(levelObject)
        {
            this.levelObject = levelObject;
        }
    }
}
