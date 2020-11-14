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

namespace Nfh.Editor.Dialogs
{
    /// <summary>
    /// Interaction logic for LoadingDialog.xaml
    /// </summary>
    public partial class LoadingDialog : Window
    {
        public object? Result { get; set; }

        private Func<object?> process;
        private BackgroundWorker worker;

        public LoadingDialog(Func<object?> process)
        {
            this.process = process;
            worker = new BackgroundWorker();

            InitializeComponent();

            worker.DoWork += RunWork;
            worker.RunWorkerCompleted += RunWorkCompleted;

            worker.RunWorkerAsync();
        }

        private void RunWork(object sender, DoWorkEventArgs args) => Result = process();

        private void RunWorkCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            DialogResult = true;
            Close();
        }
    }
}
