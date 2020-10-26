using System;
using System.Collections.Generic;
using System.Text;

namespace Steam.Acf
{
    /// <summary>
    /// Utility to read and write Steam ACF manifest format.
    /// </summary>
    public static class AcfFile
    {
        /// <summary>
        /// Parses an ACF source into an <see cref="AcfObject"/>.
        /// </summary>
        /// <param name="source">The source string to parse.</param>
        /// <returns>The parsed <see cref="AcfObject"/>.</returns>
        public static AcfObject Parse(string source)
        {
            int offs = 0;
            var key = ParseString(source, ref offs);
            if (key == null)
            {
                throw new AcfSyntaxException(offs, "Expected root element.");
            }

            if (MatchChar(source, ref offs, '{'))
            {
                return ParseObject(source, ref offs, key);
            }
            else
            {
                throw new AcfSyntaxException(offs, "Expected '{' after root element name.");
            }
        }

        // After '{'
        private static AcfObject ParseObject(string source, ref int offset, string objectName)
        {
            int offs = offset;
            var entries = new Dictionary<string, AcfEntry>();
            var result = new AcfObject(objectName, entries);
            while (!MatchChar(source, ref offs, '}'))
            {
                var key = ParseString(source, ref offs);

                if (MatchChar(source, ref offs, '{'))
                {
                    entries[key] = ParseObject(source, ref offs, key);
                }
                else
                {
                    var value = ParseString(source, ref offs);
                    if (value == null)
                    {
                        throw new AcfSyntaxException(offs, "Expected '{' or string value after string key.");
                    }
                    entries[key] = new AcfString(value);
                }
            }
            offset = offs;
            return result;
        }

        private static string ParseString(string source, ref int offset)
        {
            int offs = offset;

            if (!MatchChar(source, ref offs, '"'))
            {
                return null;
            }
            string result = "";
            for (; ; ++offs)
            {
                if (offs == source.Length)
                {
                    return null;
                }
                if (source[offs] == '"')
                {
                    ++offs;
                    break;
                }
                if (char.IsControl(source[offs]))
                {
                    return null;
                }
                result += source[offs];
            }
            offset = offs;
            return result;
        }

        private static bool MatchChar(string source, ref int offset, char ch)
        {
            int offs = offset;
            SkipWhitespace(source, ref offs);
            if (offs == source.Length)
            {
                return false;
            }
            if (source[offs] == ch)
            {
                ++offs;
                offset = offs;
                return true;
            }
            return false;
        }

        private static void SkipWhitespace(string source, ref int offset)
        {
            for (; offset < source.Length && char.IsWhiteSpace(source[offset]); ++offset) ;
        }

        internal static string ToString(AcfObject obj)
        {
            var sb = new StringBuilder();
            WriteObject(obj, sb, 0);
            return sb.ToString();
        }

        private static void WriteObject(AcfObject obj, StringBuilder sb, int indent)
        {
            // "name"
            WriteIndent(sb, indent);
            WriteString(obj.Name, sb);
            sb.AppendLine();

            // {
            WriteIndent(sb, indent);
            sb.Append('{');
            sb.AppendLine();

            foreach (var entry in obj)
            {
                if (entry.Value is AcfObject subobj)
                {
                    WriteObject(subobj, sb, indent + 1);
                }
                else
                {
                    // "key" "value"
                    var str = entry.Value as AcfString;
                    WriteIndent(sb, indent + 1);
                    WriteString(entry.Key, sb);
                    sb.Append("\t\t");
                    WriteString(str.Value, sb);
                    sb.AppendLine();
                }
            }

            // }
            WriteIndent(sb, indent);
            sb.Append('}');
            sb.AppendLine();
        }

        private static void WriteString(string str, StringBuilder sb)
        {
            sb.Append('"');
            sb.Append(str);
            sb.Append('"');
        }

        private static void WriteIndent(StringBuilder sb, int indent)
        {
            sb.Append('\t', indent);
        }
    }
}
