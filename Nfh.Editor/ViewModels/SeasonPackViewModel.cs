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
    public class SeasonPackViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<SeasonViewModel> Seasons { get; }
        private SeasonViewModel? selectedSeason;
        public SeasonViewModel? SelectedSeason
        { 
            get => selectedSeason; 
            set { selectedSeason = value; NotifyPropertyChanged(); }
        }

        public SeasonPackViewModel(SeasonPack seasonPack) 
            : base(Services.ModelChangeNotifier, seasonPack)
        {
            Seasons = new ReadOnlyObservableCollection<SeasonViewModel>(
                new ObservableCollection<SeasonViewModel>(
                    seasonPack.Seasons
                    .OrderBy(s => s.Value.Index)
                    .Select(s => new SeasonViewModel(this, s.Value.Season))));
        }
    }
}
