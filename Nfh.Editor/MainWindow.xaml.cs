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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (App.SaveIfHasChanges() == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
