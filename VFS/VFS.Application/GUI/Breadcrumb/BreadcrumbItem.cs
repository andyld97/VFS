using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VFS.Application.GUI.Breadcrumb
{
    public class BreadcrumbItem
    {
        public readonly string Text;
        public readonly string FullText = string.Empty;
        public readonly Type Kind = Type.Path;
        public readonly Rectangle DisplayRectangle;

        private bool isHovered = false;
        public bool IsHovered => this.isHovered;

        public BreadcrumbItem(string text, string fullText, Type kind, Rectangle displayRectangle)
        {
            this.Text = text;
            this.Kind = kind;
            this.FullText = fullText;
            this.DisplayRectangle = displayRectangle;
        }

        public BreadcrumbItem(Rectangle displayRectangle)
        {
            this.Text = ">";
            this.FullText = this.Text;
            this.Kind = Type.Seperator;
            this.DisplayRectangle = displayRectangle;
        }

        public enum Type
        {
            Path,
            Seperator
        }

        public static void HoverItem(List<BreadcrumbItem> items, BreadcrumbItem toHover)
        {            
            foreach (BreadcrumbItem bi in items)
                bi.isHovered = false;

            if (toHover != null)
                toHover.isHovered = true;
        }
    }
}
