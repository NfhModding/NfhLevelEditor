using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
