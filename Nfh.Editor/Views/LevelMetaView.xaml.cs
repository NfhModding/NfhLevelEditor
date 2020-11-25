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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nfh.Editor.Views
{
    /// <summary>
    /// Interaction logic for LevelMetaView.xaml
    /// </summary>
    public partial class LevelMetaView : UserControl
    {
        public LevelMetaView()
        {
            InitializeComponent();
        }

        private void EditThisLevelButton_Click(object sender, RoutedEventArgs e)
        {
            var levelMetaVm = DataContext as LevelMetaViewModel;
            if (levelMetaVm == null) return;

            var window = new LevelEditWindow(levelMetaVm.Name);
            window.Show();
        }
    }
}
