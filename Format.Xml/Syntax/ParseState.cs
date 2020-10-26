using Format.Xml.Helpers;

namespace Format.Xml.Syntax
{
    internal struct ParseState
    {
        public readonly Position Position;
        public readonly int Index;

        private readonly string source;

        public bool IsEnd => Index >= source.Length;

        public ParseState(string source, int index, Position position)
        {
            this.source = source;
            Index = index;
            Position = position;
        }

        public ParseState(string source)
            : this(source, 0, Position.Zero)
        {
        }

        public char Peek(int forward = 0, char @default = '\0') =>
            source.CharAt(Index + forward, @default);

        public bool Matches(string str) =>
            source.IsSubstringAt(Index, str);

        public ParseState Advance(int amount = 1)
        {
            var pos = Position;
            var idx = Index;
            for (int i = 0; i < amount;)
            {
                var ch = Peek(i);
                ++i;
                ++idx;
                if (ch == '\n')
                {
                    // Unix-style newline
                    pos = pos.Newline();
                }
                else if (ch == '\r')
                {
                    if (Peek(i) == '\n')
                    {
                        // Windows-style newline
                        ++i;
                        ++idx;
                    }
                    // OS-X 9 style otherwise
                    pos = pos.Newline();
                }
                else
                {
                    pos = pos.Advance();
                }
            }
            return new ParseState(source, idx, pos);
        }
    }
}
