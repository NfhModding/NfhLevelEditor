using Nfh.Editor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class PatchCommand : UiCommand
    {
        public override string? Name => "Patch Game";
        public override InputGesture? Gesture => null;

        public PatchCommand() 
            : base(p => Patch(p), p => CanPatch(p))
        {
        }

        private static void Patch(object parameter)
        {
            if (!(parameter is MetaViewModel vm))
            {
                throw new ArgumentException("The parameter of a patch command must the Meta-VM!", nameof(parameter));
            }
            vm.PatchGame();
        }

        private static bool CanPatch(object parameter)
        {
            if (parameter == null) return false;
            if (!(parameter is MetaViewModel vm))
            {
                throw new ArgumentException("The parameter of a patch command must the Meta-VM!", nameof(parameter));
            }
            return vm.SeasonPack != null;
        }
    }
}
