using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VFS.Application.GUI.Tab
{
    public class PageController : Design.DesignPanel
    {
        private PageContainer mPageContainer = null;
        private List<Page> pages = new List<Page>();
        private List<PageHeaderItem> pageHeaderItems = new List<PageHeaderItem>();
        private ContextMenu rightClickMenu = new ContextMenu();

        private int selectedIndex => (mPageContainer == null ? -1 : mPageContainer.SelectedIndex);

        public PageController()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // Configure context menu
            rightClickMenu.MenuItems.Add(new MenuItem("Entpacken"));
            rightClickMenu.MenuItems.Add(new MenuItem("Auswerfen"));
            rightClickMenu.MenuItems.Add(new MenuItem("Eigenschaften"));
        }

        public void PassPageContainer(PageContainer container)
        {
            if (mPageContainer != container)
                mPageContainer = container;

            mPageContainer.PageAdded += MPageContainer_PageAdded;
            mPageContainer.PageRemoved += MPageContainer_PageRemoved;
        }

        private void MPageContainer_PageRemoved(Page page)
        {
            pages.Remove(page);
            this.generatePageHeaderItems();
            this.Invalidate();
        }

        private void MPageContainer_PageAdded(Page page)
        {
            pages.Add(page);
            this.generatePageHeaderItems();
            this.Invalidate();
        }

        private void generatePageHeaderItems()
        {
            this.pageHeaderItems.Clear();
            int top = Consts.PageController.START_TOP;

            foreach (Page p in pages)
            {
                Rectangle currentItemRectangle = new Rectangle(0, top, this.Width, Consts.PageController.ITEM_HEIGHT);            
                PageHeaderItem phi = new PageHeaderItem(p, currentItemRectangle);
                pageHeaderItems.Add(phi);

                top += Consts.PageController.ITEM_HEIGHT + Consts.PageController.DISTANCE_BETWEEN_ITEMS;
            }

            // try to select currentPage if length != 0 then there is no page to select.
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Icon Name
            // If current element is selected/hovered, fill background

            foreach (PageHeaderItem currentPHI in pageHeaderItems)
            {
                Rectangle currentItemRectangle = currentPHI.DisplayRectangle;

                if (currentPHI.IsHovered)
                    e.Graphics.FillRectangle(new SolidBrush(Consts.PageController.HoverColor), currentItemRectangle);
                if (currentPHI.IsSelected)
                    e.Graphics.FillRectangle(new SolidBrush(Consts.PageController.SelectColor), currentItemRectangle);

                string shortendText = currentPHI.Name.ShortendString();

                e.Graphics.DrawImage(Properties.Resources.FolderIcon, new Rectangle(currentPHI.DisplayRectangle.X, currentPHI.DisplayRectangle.Y, Consts.PageController.ICON_WIDTH, Consts.PageController.ITEM_HEIGHT));
                e.Graphics.DrawString(shortendText, new Font("Segoe UI", 9F, FontStyle.Regular), new SolidBrush(Color.Black), new Rectangle(Consts.PageController.ICON_WIDTH + 10, currentPHI.DisplayRectangle.Y, this.Width - Consts.PageController.ICON_WIDTH + 10, Consts.PageController.ITEM_HEIGHT), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });                
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool foundOne = false;
            foreach (PageHeaderItem currentPHI in pageHeaderItems)
            {
                if (currentPHI.DisplayRectangle.Contains(e.Location))
                {
                    PageHeaderItem.SetItemState(true, currentPHI, pageHeaderItems);
                    this.Invalidate();
                    foundOne = true;
                    break;
                }
            }

            if (!foundOne)
            {
                PageHeaderItem.SetItemState(true, null, pageHeaderItems);
                this.Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            PageHeaderItem itemAtPosition = searchForItem(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                if (itemAtPosition != null)
                {
                    PageHeaderItem.SetItemState(false, itemAtPosition, pageHeaderItems);
                    this.Invalidate();
                    mPageContainer.SelectedPage = itemAtPosition.AssociatedPage;
                }
                else
                {
                    PageHeaderItem.SetItemState(false, null, pageHeaderItems);
                    this.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (itemAtPosition == null)
                    return;

                rightClickMenu.Show(this, new Point(e.X, e.Y));
            }
        }

        private PageHeaderItem searchForItem(Point location)
        {
            foreach (PageHeaderItem currentPHI in pageHeaderItems)
            {
                if (currentPHI.DisplayRectangle.Contains(location))
                {
                    return currentPHI;
                }
            }
            return null;
        }
    }
}
