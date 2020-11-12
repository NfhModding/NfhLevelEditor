using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    internal class SeasonViewModel : ViewModelBase
    {
        public string Name => season.Id;

        private Season season;

        public SeasonViewModel(Season season) 
            : base(Services.ModelChangeNotifier, season)
        {
            this.season = season;
        }
    }
}
