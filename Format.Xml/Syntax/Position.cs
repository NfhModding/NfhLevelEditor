namespace Format.Xml.Syntax
{
    /// <summary>
    /// Describes user-friendly position in a text file.
    /// </summary>
    public struct Position
    {
        /// <summary>
        /// The zero-based line (or row) index.
        /// </summary>
        public readonly int Line;
        /// <summary>
        /// The zedo-based column (or character) index.
        /// </summary>
        public readonly int Column;

        /// <summary>
        /// The starting position of every text file: First line, first character.
        /// </summary>
        internal static readonly Position Zero = new Position(line: 0, column: 0);

        /// <summary>
        /// Initializes a new <see cref="Position"/>.
        /// </summary>
        /// <param name="line">The zero-based line index.</param>
        /// <param name="column">The zero-based column index.</param>
        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        internal Position Advance(int n = 1) => new Position(line: Line, column: Column + n);
        internal Position Newline() => new Position(line: Line + 1, column: 0);
    }
}
