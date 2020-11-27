using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Nfh.Editor.Dialogs
{
    /// <summary>
    /// Interaction logic for LoadingDialog.xaml
    /// </summary>
    public partial class LoadingDialog : Window
    {
        private volatile bool isRunning;
        private BackgroundWorker? worker;

        public LoadingDialog()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = isRunning;
        }

        public object? Execute(Func<object?> action)
        {
            isRunning = true;
            object? result = null;

            worker = new BackgroundWorker();
            worker.DoWork += (sender, ev) =>
            {
                ev.Result = action();
            };
            worker.RunWorkerCompleted += (sender, ev) =>
            {
                result = ev.Result;

                Dispatcher.Invoke(() =>
                {
                    isRunning = false;
                    Close();
                });
            };

            worker.RunWorkerAsync();
            ShowDialog();

            return result;
        }
    }
}
