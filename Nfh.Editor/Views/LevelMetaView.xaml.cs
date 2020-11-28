using Nfh.Editor.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
