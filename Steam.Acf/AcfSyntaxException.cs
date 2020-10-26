using System;
using System.Collections.Generic;
using System.Text;

namespace Steam.Acf
{
    public class AcfSyntaxException : Exception
    {
        public int Index { get; }

        public AcfSyntaxException(int idx, string message)
            : base(message)
        {
            Index = idx;
        }
    }
}
