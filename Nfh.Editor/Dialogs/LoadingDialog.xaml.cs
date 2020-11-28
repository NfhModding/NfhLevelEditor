using System;
using System.ComponentModel;
using System.Windows;
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
