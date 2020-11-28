using Nfh.Editor.ViewModels;
using System;
using System.Windows;

namespace Nfh.Editor
{
    /// <summary>
    /// Interaction logic for MetaWindow.xaml
    /// </summary>
    public partial class MetaWindow : Window
    {
        public MetaWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (metaView.DataContext is MetaViewModel metaVm)
            {
                e.Cancel = metaVm.SaveIfHasChanges() == MessageBoxResult.Cancel;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown(0);
        }
    }
}
