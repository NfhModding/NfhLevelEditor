using System;
using System.Collections.Generic;
using System.Text;

namespace Format.Xml.Helpers
{
    internal static class StringExtensions
    {
        public static char CharAt(this string str, int index, char @default = '\0') =>
            index >= str.Length ? @default : str[index];

        public static bool IsSubstringAt(this string str, int index, string other)
        {
            for (int i = 0; i < other.Length; ++i)
            {
                if (str.CharAt(index + i) != other[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
