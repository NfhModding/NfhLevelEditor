using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal interface IConverter
    {
        public TTo Convert<TFrom, TTo>(TFrom model)
            where TFrom : notnull
            where TTo : notnull;
    }
}
