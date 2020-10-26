using System;

namespace Format.Xml.Syntax
{
    internal static class XmlParser
    {
        public static bool MatchXmlHeader(ref ParseState state) =>
            MatchAll(ref state, "<?xml ", "version", "=", "\"1.0\"", "?>");

        public static bool MatchOpenTagBegin(ref ParseState state, string tag)
        {
            var s = state;
            if (!MatchOpenTagBegin(ref s, out var tag2))
            {
                return false;
            }
            if (tag != tag2)
            {
                return false;
            }
            state = s;
            return true;
        }

        public static bool MatchOpenTagBegin(ref ParseState state, out string tag)
        {
            var s = state;
            if (!MatchAll(ref s, "<"))
            {
                tag = null;
                return false;
            }
            if (MatchAll(ref s, "/"))
            {
                // It's an end tag
                tag = null;
                return false;
            }
            if (!MatchIdent(ref s, out tag))
            {
                // TODO: Syntax error
                throw new NotImplementedException();
            }
            state = s;
            return true;
        }

        public static bool MatchAttribute(ref ParseState state, out string key, out string value)
        {
            if (!MatchIdent(ref state, out key))
            {
                key = null;
                value = null;
                return false;
            }
            if (!MatchAll(ref state, "="))
            {
                // TODO: Syntax error
                throw new NotImplementedException();
            }
            if (!MatchString(ref state, out value))
            {
                // TODO: Syntax error
                throw new NotImplementedException();
            }
            return true;
        }

        public static void MatchTagContent(ref ParseState state, out string content)
        {
            content = string.Empty;
            while (!state.Matches("<") && !state.IsEnd)
            {
                content += state.Peek();
                state = state.Advance();
            }
            // Unescape it
            content = XmlString.Unescape(content);
        }

        public static bool MatchOpenTagEnd(ref ParseState state) =>
            MatchAll(ref state, ">");

        public static bool MatchOpenTagCompoundEnd(ref ParseState state) =>
            MatchAll(ref state, "/", ">");

        public static bool MatchCloseTag(ref ParseState state, string tag) =>
            MatchAll(ref state, "<", "/", tag, ">");

        public static bool MatchString(ref ParseState state, out string str)
        {
            var s = state;
            Blank(ref s);
            str = string.Empty;
            char openChar = s.Peek();
            if (openChar != '\'' && openChar != '"')
            {
                return false;
            }
            s = s.Advance();
            while (s.Peek(0, openChar) != openChar)
            {
                str += s.Peek();
                s = s.Advance();
            }
            if (!s.IsEnd)
            {
                s = s.Advance();
            }
            state = s;
            // Unescape
            str = XmlString.Unescape(str);
            return true;
        }

        /*
         * Internal logic.
         */

        private static bool MatchIdent(ref ParseState state, out string ident)
        {
            var s = state;
            Blank(ref s);
            ident = string.Empty;
            for (; IsIdent(s.Peek()); ident += s.Peek(), s = s.Advance()) ;
            if (ident.Length == 0)
            {
                ident = null;
                return false;
            }
            state = s;
            return true;
        }

        private static bool IsIdent(char ch) =>
            char.IsLetterOrDigit(ch) || ch == '_' || ch == '-';

        private static bool MatchAll(ref ParseState state, params string[] strs)
        {
            var s = state;
            foreach (var str in strs)
            {
                Blank(ref s);
                if (!s.Matches(str))
                {
                    return false;
                }
                s = s.Advance(str.Length);
            }
            state = s;
            return true;
        }

        private static void Blank(ref ParseState state)
        {
            while (true)
            {
                int idx = state.Index;
                WhiteSpace(ref state);
                Comment(ref state);
                if (idx == state.Index)
                {
                    return;
                }
            }
        }

        private static void Comment(ref ParseState state)
        {
            if (state.Matches("<!--"))
            {
                state = state.Advance(4);
                while (!state.Matches("-->") && !state.IsEnd)
                {
                    state = state.Advance();
                }
                if (!state.IsEnd)
                {
                    state = state.Advance(3);
                }
            }
        }

        private static void WhiteSpace(ref ParseState state)
        {
            for (; char.IsWhiteSpace(state.Peek()); state = state.Advance()) ;
        }
    }
}
