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
        public LevelEditWindow(string projectPath, string levelId)
        {
            InitializeComponent();
            var level = Services.Project.LoadLevel(projectPath, levelId);
            levelEditView.DataContext = new LevelEditViewModel(level);
        }
    }
}
