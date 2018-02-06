using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VFS.Application.GUI.Tab
{
    public class PageHeaderItem
    {
        public readonly string Name;
        public readonly Rectangle DisplayRectangle;
        public readonly Page AssociatedPage;

        private bool isHovered = false;
        private bool isSelected = false;

        public bool IsHovered => this.isHovered;

        public bool IsSelected => this.isSelected;

        public PageHeaderItem(Page asscoiatedPage, Rectangle displayRectangle)
        {
            this.Name = asscoiatedPage.Name;
            this.AssociatedPage = asscoiatedPage;
            this.DisplayRectangle = displayRectangle;
        }
       
        public static void SetItemState(bool hover, PageHeaderItem phi, List<PageHeaderItem> items)
        {
            foreach (PageHeaderItem currentPHI in items)
            {
                if (hover)
                    currentPHI.isHovered = false;
                else
                    currentPHI.isSelected = false;
            }

            if (phi != null)
            {
                if (hover)
                    phi.isHovered = true;
                else
                    phi.isSelected = true;
            }
        }

    }
}
