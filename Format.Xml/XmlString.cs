using Format.Xml.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Format.Xml
{
    internal static class XmlString
    {
        public static string Escape(string str)
        {
            var result = new StringBuilder();
            result.EnsureCapacity(str.Length);
            foreach (var ch in str)
            {
                if (char.IsControl(ch))
                {
                    // Encode as hex string
                    result.Append($"&#x{(int)ch:X};");
                }
                else
                {
                    switch (ch)
                    {
                    case '<': result.Append("&lt;"); break;
                    case '>': result.Append("&gt;"); break;
                    case '&': result.Append("&amp;"); break;
                    case '\'': result.Append("&apos;"); break;
                    case '"': result.Append("&quot;"); break;
                    default: result.Append(ch); break;
                    }
                }
            }
            return result.ToString();
        }

        public static string Unescape(string str)
        {
            var result = new StringBuilder();
            result.EnsureCapacity(str.Length);
            for (int i = 0; i < str.Length;)
            {
                if (str.CharAt(i) == '&')
                {
                    ++i;
                    int afterAmp = i;
                    // Escape sequence
                    if (str.CharAt(i) == '#')
                    {
                        ++i;
                        bool hex = false;
                        if (str.CharAt(i) == 'x')
                        {
                            hex = true;
                            ++i;
                        }
                        // Either decimal, or hex number until ';'
                        string codeStr = string.Empty;
                        while (str.CharAt(i, ';') != ';')
                        {
                            codeStr += str[i];
                            ++i;
                        }
                        // Expect ';'
                        if (str.CharAt(i) != ';')
                        {
                            i = afterAmp;
                            continue;
                        }
                        ++i;
                        var charCode = int.Parse(codeStr, hex ? NumberStyles.HexNumber : NumberStyles.Integer);
                        result.Append((char)charCode);
                    }
                    else
                    {
                        if (str.IsSubstringAt(i, "lt;"))
                        {
                            result.Append('<');
                            i += 3;
                        }
                        else if (str.IsSubstringAt(i, "gt;"))
                        {
                            result.Append('>');
                            i += 3;
                        }
                        else if (str.IsSubstringAt(i, "amp;"))
                        {
                            result.Append('&');
                            i += 4;
                        }
                        else if (str.IsSubstringAt(i, "apos;"))
                        {
                            result.Append('\'');
                            i += 5;
                        }
                        else if (str.IsSubstringAt(i, "quot;"))
                        {
                            result.Append('"');
                            i += 5;
                        }
                        else
                        {
                            i = afterAmp;
                            continue;
                        }
                    }
                }
                else
                {
                    // Non-escape character
                    result.Append(str[i]);
                    ++i;
                }
            }
            return result.ToString();
        }
    }
}
