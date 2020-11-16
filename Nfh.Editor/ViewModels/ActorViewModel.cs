﻿using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    public class ActorViewModel : LevelObjectViewModel
    {
        public ActorViewModel(Actor actor)
            : base(actor)
        {
        }
    }
}