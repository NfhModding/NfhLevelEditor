using Format.Xml.Syntax;
using System;

namespace Format.Xml
{
    /// <summary>
    /// An <see cref="Exception"/> that happens during parsing, if the document contains some invalid sequence.
    /// </summary>
    public class XmlSyntaxException : Exception
    {
        /// <summary>
        /// The <see cref="Position"/> the syntax error happened at.
        /// </summary>
        public readonly Position Position;

        public XmlSyntaxException(Position position, string message)
            : base(message)
        {
            Position = position;
        }
    }
}
