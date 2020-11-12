﻿using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.Meta;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    internal class SeasonPackViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<SeasonViewModel> Seasons { get; set; }
        public SeasonViewModel? SelectedSeason { get; set; }

        public SeasonPackViewModel(SeasonPack seasonPack) 
            : base(Services.ModelChangeNotifier, seasonPack)
        {
            Seasons = new ReadOnlyObservableCollection<SeasonViewModel>(
                new ObservableCollection<SeasonViewModel>(
                    seasonPack.Seasons
                    .OrderBy(s => s.Value.Index)
                    .Select(s => new SeasonViewModel(s.Value.Season))));
        }
    }
}