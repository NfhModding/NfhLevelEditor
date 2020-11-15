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
            : base(p => App.PatchGame(), p => (p as MetaViewModel)?.SeasonPack != null)
        {
        }
    }
}
