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
    }
}
