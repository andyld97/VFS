using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VFS.Application.GUI.Tab
{
    public class PageContainer : UserControl 
    {
        private List<Page> pages = new List<Page>();
        private Page currentPage = null;
        private PageController pageController = null;

        public delegate void pageAdded(Page page);
        public event pageAdded PageAdded;

        public delegate void pageRemoved(Page page);
        public event pageRemoved PageRemoved;

        public delegate void selectedPageChanged(Page page);
        public event selectedPageChanged SelectedPageChanged;

        private bool displayPreview = false;
        private Preview previewControl = null;

        public Page SelectedPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                var val = value;

                if (currentPage != null)
                    currentPage.OnSelectedChanged -= CurrentPage_OnSelectedChanged;

                if (this.pages.Contains(val))
                {
                    currentPage = val;

                    this.Controls.Clear();

                    if (this.DisplayPreview)
                    {
                        currentPage.Dock = DockStyle.None;
                        currentPage.Location = new Point(0, 0);
                        currentPage.Size = new Size(this.Width / 2, this.Height);

                        currentPage.OnSelectedChanged += CurrentPage_OnSelectedChanged;

                        previewControl.Location = new Point(this.Width / 2, 0);
                        previewControl.Size = new Size(this.Width / 2, this.Height);

                        this.Controls.AddRange(new Control[] { currentPage, previewControl });
                    }
                    else
                    {
                        this.Controls.Add(currentPage);
                        currentPage.Dock = DockStyle.Fill;
                    }

                    // Refresh breadcrumb
                    this.SelectedPageChanged?.Invoke(val);
                }
            }
        }

        private void CurrentPage_OnSelectedChanged(Element[] elements)
        {
            if (DisplayPreview)
                previewControl.SelectionChanged(elements);
        }

        public int SelectedIndex
        {
            get
            {
                if (this.SelectedPage != null)
                    return pages.IndexOf(this.SelectedPage);
                else
                    return -1;
            }
            set
            {
                int nIndex = value;
                if (nIndex != -1 && nIndex >= 0 && nIndex <= pages.Count - 1)
                    SelectedPage = pages[nIndex];
            }
        }

        public bool DisplayPreview
        {
            get
            {
                return displayPreview;
            }
            set
            {
                displayPreview = value;
                if (SelectedPage != null)
                    this.SelectedPage = SelectedPage;
            }
        }

        public int TabCount => this.pages.Count;

        public PageContainer(PageController pc)
        {
            this.pageController = pc;
            pc.PassPageContainer(this);

            this.previewControl = new Preview(this);
            // ToDo: Link preview with select event
        }

        public void AddPage(Page page, bool selectPageAfterAdd = true)
        {
            pages.Add(page);
            if (selectPageAfterAdd)
                this.SelectedPage = page;

            this.PageAdded?.Invoke(page);
        }

        public void RemovePage(Page page)
        {
            if (SelectedPage == page)
            {
                pages.Remove(page);
                this.Controls.Clear();

                // Select next or last page.
                if (pages.Count != 0)
                {
                    if (SelectedIndex + 1 <= pages.Count - 1)
                        SelectedIndex++;
                    else if (SelectedIndex - 1 >= 0)
                        SelectedIndex--;
                }
            }
            else
                pages.Remove(page);

            this.PageRemoved?.Invoke(page);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DisplayPreview)
            {
                currentPage.Location = new Point(0, 0);
                currentPage.Size = new Size(this.Width / 2, this.Height);

                previewControl.Location = new Point(this.Width / 2, 0);
                previewControl.Size = new Size(this.Width / 2, this.Height);
            }
        }
    }
}
