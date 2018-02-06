using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VFS.Application.GUI
{
    public static class Helper
    {
        public static (int, bool) MeasureText(string text)
        {
            if (!string.IsNullOrEmpty(text))
                return (TextRenderer.MeasureText(text, new Font("Segoe UI", 12F, FontStyle.Regular)).Width + (text == ">" ? 0 : 10), true);
            return (0, false);
        }

        public static string ShortendString(this string data, int chars = 15, string shortText = "...")
        {
            if (data.Length <= chars)
                return data;

            int finalLength = chars - shortText.Length;
            string nData = string.Empty;

            for (int i = 0; i <= finalLength - 1; i++)
                nData += data[i];

            return nData + shortText;
        }
    }
}
