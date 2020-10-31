using Nfh.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services
{
    internal class ApplicationWorkFolder : IApplicationWorkFolder
    {
        private DirectoryInfo? info;
        public DirectoryInfo Info
        {
            get
            {
                if (info == null)
                {
                    var appDataFolder = new DirectoryInfo(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

                    var workFolder = new DirectoryInfo(
                        Path.Combine(appDataFolder.FullName, ApplicationInformation.ApplicationName));

                    workFolder.Create();                    

                    info = workFolder;
                }

                return info;
            }
        }
    }
}
