using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.Meta;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                seasonPackVm.SelectedSeason = this;
                selectedLevel = value; 
                NotifyPropertyChanged(); 
            }
        }

        private SeasonPackViewModel seasonPackVm;
        private Season season;

        public SeasonViewModel(SeasonPackViewModel seasonPackVm, Season season) 
            : base(Services.ModelChangeNotifier, season)
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
