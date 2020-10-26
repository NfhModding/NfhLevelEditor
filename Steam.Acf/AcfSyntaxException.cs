using System;

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
