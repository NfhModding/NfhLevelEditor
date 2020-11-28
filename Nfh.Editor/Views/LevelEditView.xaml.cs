using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nfh.Editor.Views
{
    /// <summary>
    /// Interaction logic for LevelEditView.xaml
    /// </summary>
    public partial class LevelEditView : UserControl
    {
        public LevelEditView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) =>
            Keyboard.Focus(this);
    }
}
