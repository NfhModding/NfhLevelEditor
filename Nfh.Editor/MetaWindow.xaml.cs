using Nfh.Domain.Interfaces;
using Nfh.Editor.Dialogs;
using Nfh.Editor.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
