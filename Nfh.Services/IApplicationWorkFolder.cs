using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services
{
    internal interface IWorkFolder
    {
        public DirectoryInfo Info { get; }
    }
}
