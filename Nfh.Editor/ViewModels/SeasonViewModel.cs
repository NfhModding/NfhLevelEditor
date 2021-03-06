﻿using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.Meta;
using System.Collections.ObjectModel;
using System.Linq;

namespace Nfh.Editor.ViewModels
{
    public class SeasonViewModel : ViewModelBase
    {
        public string Name => season.Id;
        public ReadOnlyObservableCollection<LevelMetaViewModel> Levels { get; }
        private LevelMetaViewModel? selectedLevel;
        public LevelMetaViewModel? SelectedLevel 
        { 
            get => selectedLevel; 
            set 
            {
                if (seasonPackVm.SelectedSeason != null && seasonPackVm.SelectedSeason != this)
                {
                    seasonPackVm.SelectedSeason.SelectedLevel = null;
                }
                seasonPackVm.SelectedSeason = this;
                selectedLevel = value; 
                NotifyPropertyChanged(); 
            }
        }

        private SeasonPackViewModel seasonPackVm;
        private Season season;

        public SeasonViewModel(SeasonPackViewModel seasonPackVm, Season season) 
            : base(MetaViewModel.Current.ModelChangeNotifier, season)
        {
            this.seasonPackVm = seasonPackVm;
            this.season = season;
            Levels = new ReadOnlyObservableCollection<LevelMetaViewModel>(
                new ObservableCollection<LevelMetaViewModel>(
                    season.Levels
                    .OrderBy(s => s.Value.Index)
                    .Select(s => new LevelMetaViewModel(s.Value.Level))));
        }
    }
}
