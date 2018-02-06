using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using VFS.Application.GUI.Tab;

namespace VFS.Application.GUI.Breadcrumb
{
    public class Breadcrumb : Design.DesignPanel 
    {
        private string path = string.Empty;
        private string vfsName = string.Empty;
        private List<BreadcrumbItem> BreadcrumbItems = new List<BreadcrumbItem>();

        public delegate void onCrumbItemClicked(string nPath);
        public event onCrumbItemClicked OnCrumbItemClicked;

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
                // / > Dir1 > Dir2 > Dir3 > Dir4
                generateBreadCrumbItems();
                this.Invalidate();
            }
        }

        public string VFSName
        {
            get
            {
                return this.vfsName;
            }
            set
            {
                this.vfsName = value;
                // / > Dir1 > Dir2 > Dir3 > Dir4
                generateBreadCrumbItems();
                this.Invalidate();
            }
        }

        public Breadcrumb(string path)
        {
            this.path = path;
        }

        private void generateBreadCrumbItems()
        {
            this.BreadcrumbItems.Clear();

            string[] oldSegments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            int left = 0;

            string[] segments = new string[oldSegments.Length + 1];
            segments[0] = this.vfsName;
            for (int i = 0; i <= oldSegments.Length - 1; i++)
                segments[i + 1] = oldSegments[i];

            int seperatorWidth = Helper.MeasureText(Consts.Breadcrumb.SEPERATOR).Item1;

            for (int i = 0; i <= segments.Length - 1; i++)
            {
                if (string.IsNullOrEmpty(segments[i]))
                    continue;

                string segement = segments[i].ShortendString();

                // Generate segement
                int currentWidth = Helper.MeasureText(segement).Item1;
                Rectangle currentRectangle = new Rectangle(left, 5, currentWidth, this.Height - 10);
                BreadcrumbItem currentBCI = new BreadcrumbItem(segement, segments[i], BreadcrumbItem.Type.Path, currentRectangle);
                BreadcrumbItems.Add(currentBCI);
                left += currentWidth + Consts.Breadcrumb.DISTANCE_BETWEEN_ITEMS;

                if (i != segments.Length - 1 || path == @"\")
                {
                    // Generate >                    
                    Rectangle seperatorRectangle = new Rectangle(left, 5, seperatorWidth, this.Height - 10);
                    BreadcrumbItem seperatorBCI = new BreadcrumbItem(seperatorRectangle);
                    BreadcrumbItems.Add(seperatorBCI);
                    left += seperatorWidth + Consts.Breadcrumb.DISTANCE_BETWEEN_ITEMS;
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            BreadcrumbItem.HoverItem(BreadcrumbItems, null);
            this.Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Don't draw nothing or >
            if (BreadcrumbItems.Count == 0 || BreadcrumbItems.Count == 1)
                return;

            foreach (BreadcrumbItem currentBCI in BreadcrumbItems)
            {
                Font normalFont = new Font("Segoe UI", 12F, FontStyle.Regular);
                Font seperatorFont = new Font("Segoe UI", 15F, FontStyle.Bold);

                if (currentBCI.IsHovered)
                    e.Graphics.FillRectangle(new SolidBrush(Consts.Breadcrumb.HoverColor), currentBCI.DisplayRectangle);

                e.Graphics.DrawString(currentBCI.Text,  currentBCI.Kind == BreadcrumbItem.Type.Path ? normalFont : seperatorFont , new SolidBrush(Color.Black), currentBCI.DisplayRectangle, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool foundItem = false;

            foreach (BreadcrumbItem currentBCI in BreadcrumbItems)
            {
                if (currentBCI.DisplayRectangle.Contains(e.Location) && currentBCI.Kind != BreadcrumbItem.Type.Seperator)
                {
                    foundItem = true;
                    BreadcrumbItem.HoverItem(BreadcrumbItems, currentBCI);
                    this.Invalidate();
                    break;
                }
            }

            if (!foundItem)
            {
                BreadcrumbItem.HoverItem(BreadcrumbItems, null);
                this.Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                // Check if item is hovered and run item
                BreadcrumbItem currentItem = BreadcrumbItems.Where(t => t.IsHovered && t.Kind == BreadcrumbItem.Type.Path).FirstOrDefault();

                if (currentItem != null)
                {
                    // Generate path from bci to 0 
                    string nPath = this.ToString(currentItem, BreadcrumbItems);
                    this.OnCrumbItemClicked?.Invoke(nPath);
                }
            }
        }

        public void ChangeToPage(Page p)
        {
            this.vfsName = p.Name;
            this.Path = p.Path;
        }

        public string ToString(BreadcrumbItem to, List<BreadcrumbItem> items)
        {
            string path = string.Empty;

            if (items.IndexOf(to) == 0)
                return @"\";

            int index = items.IndexOf(to);
            if (index != -1)
            {
                for (int i = 1; i <= index; i++)
                {
                    BreadcrumbItem currentItem = items[i];
                    path += currentItem.FullText.Replace(">", @"\");
                }
                return path;
            }
            else
                return string.Empty;
        }
                 
    }
}
