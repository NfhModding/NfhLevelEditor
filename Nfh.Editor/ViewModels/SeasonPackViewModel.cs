using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.Meta;
using Nfh.Editor.Commands.ModelCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nfh.Editor.ViewModels
{
    public class SeasonPackViewModel : ViewModelBase
    {
        public SeasonPack SeasonPack { get; }

        public ReadOnlyObservableCollection<SeasonViewModel> Seasons { get; }
        private SeasonViewModel? selectedSeason;
        public SeasonViewModel? SelectedSeason
        { 
            get => selectedSeason; 
            set { selectedSeason = value; NotifyPropertyChanged(); }
        }

        public SeasonPackViewModel(SeasonPack seasonPack)
           : base(MetaViewModel.Current.ModelChangeNotifier, seasonPack)
        {
            SeasonPack = seasonPack;
            Seasons = new ReadOnlyObservableCollection<SeasonViewModel>(
                new ObservableCollection<SeasonViewModel>(
                    seasonPack.Seasons
                    .OrderBy(s => s.Value.Index)
                    .Select(s => new SeasonViewModel(this, s.Value.Season))));
        }
    }
}
