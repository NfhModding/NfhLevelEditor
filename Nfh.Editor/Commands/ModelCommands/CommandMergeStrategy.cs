using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.Commands.ModelCommands
{
    public class CommandMergeStrategy : ICommandMergeStrategy
    {
        public bool AllowMerge { get; set; } = false;

        public bool CanMerge(IModelChangeCommand first, IModelChangeCommand second)
        {
            if (!AllowMerge) return false;

            if (first is PropertyChangeCommand c1 && second is PropertyChangeCommand c2)
            {
                if (c1.Target == c2.Target && c1.PropertyInfo == c2.PropertyInfo && c1.Target is LevelObject
                    && c1.PropertyInfo.Name == "Position")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
