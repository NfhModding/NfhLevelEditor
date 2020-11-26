using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    public class ActorViewModel : LevelObjectViewModel
    {
        public override Point Position 
        {
            get
            {
                var actor = (Actor)Model;
                var pos = base.Position;
                pos.X -= actor.Hotspot.X;
                pos.Y -= actor.Hotspot.Y;
                return pos;
            }
            set
            {
                var actor = (Actor)Model;
                value.X += actor.Hotspot.X;
                value.Y += actor.Hotspot.Y;
                base.Position = value;
            }
        }

        public ActorViewModel(LevelViewModel level, Room? room, Actor actor)
            : base(level, room, actor)
        {
        }
    }
}
