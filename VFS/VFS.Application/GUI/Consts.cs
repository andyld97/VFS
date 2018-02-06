using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VFS.Application.GUI
{
    public static class Consts
    {
        public static readonly string[] IMAGE_EXTENSIONS = new string[] { ".bmp", ".jpg", ".jpeg", ".png" };
        public static readonly string[] MUSIC_EXTENSIONS = new string[] { ".mp3", ".mp4", ".mpg", ".wma", ".avi" };
        public static readonly string[] NO_EXTENSIONS = new string[] { ".iso", ".dll" };

        public static readonly Color SelectionColor = Color.Orange;
        public static readonly Color HoverColor = Color.Blue;

        public static class Breadcrumb
        {
            public const int DISTANCE_BETWEEN_ITEMS = 0;
            public const string SEPERATOR = ">";

            public static readonly Color HoverColor = Color.FromArgb(80, 116, 153, 217);
        }

        public static class ListView
        {
            public const int ITEM_WIDTH = 200;
            public const int ITEM_HEIGHT = 40;
            public const int IMAGE_WIDTH = 40;
            public const int IMAGE_HEIGHT = 40;
            public const int CHAR_LENGTH = 27;

            public static Color HoverColor = Color.FromArgb(155, 0, 166, 255);
            public static Color SelectedColor = Color.FromArgb(155, 116, 217, 129);
        }

        public static class PageController
        {
            public const int DISTANCE_BETWEEN_ITEMS = 10;
            public const int START_TOP = 10;
            public const int ITEM_HEIGHT = 40;
            public const int ICON_WIDTH = 27; // ITEM_HEIGHT * 0.6645569620 for Icon.png (Relation between sides)

            public static readonly Color SelectColor = Color.FromArgb(155, Color.Orange);
            public static readonly Color HoverColor = Color.FromArgb(155, 0, 149, 255);
        }

    }
}
