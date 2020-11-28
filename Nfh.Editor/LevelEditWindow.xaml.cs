using Nfh.Domain.Models.InGame;
using Nfh.Editor.Dialogs;
using Nfh.Editor.ViewModels;
using System.Windows;

namespace Nfh.Editor
{
    /// <summary>
    /// Interaction logic for LevelEditWindow.xaml
    /// </summary>
    public partial class LevelEditWindow : Window
    {
        public LevelEditWindow(string levelId)
        {
            InitializeComponent();
            var level = new LoadingDialog().Execute(() =>
                Services.Project.LoadLevel(MetaViewModel.Current.ProjectPath, levelId));
            levelEditView.DataContext = new LevelEditViewModel((Level)level);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (levelEditView.DataContext is LevelEditViewModel levelVm)
            {
                e.Cancel = levelVm.SaveIfHasChanges() == MessageBoxResult.Cancel;
            }
        }
    }
}
