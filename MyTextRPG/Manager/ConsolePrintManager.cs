using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal static class ConsolePrintManager
    {
        public static string PadRightToWidth(string str, int totalWidth)
        {
            int width = 0;

            foreach (char c in str)
            {
                // 대부분의 한글은 2칸 차지함
                width += EastAsianWidth.GetWidth(c);
            }

            int padding = Math.Max(0, totalWidth - width);
            return str + new string(' ', padding);
        }

        public static string PadLeftToWidth(string str, int totalWidth)
        {
            int width = 0;

            foreach (char c in str)
            {
                // 대부분의 한글은 2칸 차지함
                width += EastAsianWidth.GetWidth(c);
            }

            int padding = Math.Max(0, totalWidth - width);
            return new string(' ', padding) + str;
        }

        public static class EastAsianWidth
        {
            public static int GetWidth(char c)
            {
                var cat = CharUnicodeInfo.GetUnicodeCategory(c);
                // 한글, 한자, 일본어는 대부분 너비 2
                if (cat == UnicodeCategory.OtherLetter)
                    return 2;

                return 1;
            }
        }
    }
}
