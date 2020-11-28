using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nfh.Editor.Views
{
    /// <summary>
    /// Interaction logic for MetaView.xaml
    /// </summary>
    public partial class MetaView : UserControl
    {
        public MetaView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) =>
            Keyboard.Focus(this);
    }
}
