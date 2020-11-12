using Mvvm.Framework.ViewModel;
using Nfh.Editor.ViewModels;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Nfh.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeasonPackViewModel SeasonPack { get; set; }

        public MainWindow()
        {
            SeasonPack = new SeasonPackViewModel(Services.Project.LoadSeasonPack(Services.GamePath));
            InitializeComponent();
        }
    }
}
