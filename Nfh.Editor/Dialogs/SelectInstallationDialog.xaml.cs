using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Nfh.Editor.Dialogs
{
    /// <summary>
    /// Interaction logic for SelectInstallationDialog.xaml
    /// </summary>
    public partial class SelectInstallationDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ReadOnlyObservableCollection<string> Installations { get; }
        private string? chosenInstallation;
        public string? ChosenInstallation 
        { 
            get => chosenInstallation; 
            set
            {
                chosenInstallation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChosenInstallation)));
            }
        }

        public SelectInstallationDialog(IEnumerable<string> installations)
        {
            Installations = new ReadOnlyObservableCollection<string>(
                new ObservableCollection<string>(installations));
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
