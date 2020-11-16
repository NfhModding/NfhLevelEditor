using Mvvm.Framework.ViewModel;
using Nfh.Editor.Commands.UiCommands;
using Nfh.Editor.ViewModels;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nfh.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ICommand Undo { get; set; } = new UndoCommand();
        public ICommand Redo { get; set; } = new RedoCommand();

        public LevelLayerViewModel LevelLayer { get; set; }

        public MainWindow()
        {
            // TODO: Temp
            var levelObjs = Services.Project.LoadLevel("", "").Object.Values;
            LevelLayer = new LevelLayerViewModel(levelObjs);
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus((Grid)sender);
        }
    }
}
