using Nfh.Editor.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Nfh.Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new SelectInstallationDialog(new string[] 
            { 
                "C:/AAAAAAAAAA",
                "C:/BBBBBBBBBB",
            }).ShowDialog();
        }
    }
}
