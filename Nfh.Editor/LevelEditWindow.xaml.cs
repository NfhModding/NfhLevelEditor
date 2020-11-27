using Nfh.Domain.Models.InGame;
using Nfh.Editor.Dialogs;
using Nfh.Editor.ViewModels;
using System;
using System.Collections.Generic;
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
