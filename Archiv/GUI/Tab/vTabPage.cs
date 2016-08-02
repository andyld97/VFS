// ------------------------------------------------------------------------
// vTabPage.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  02.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using VFS;

namespace Archiv.GUI.Tab
{
    public class vTabPage : TabPage
    {
        private string path = string.Empty;
        private VFS.VFS currentFileSystem = null;
        private Directory currentDirectory = null;
        private const int distance = 32;
        private Point mouseLocation = new Point();

        public delegate void onSelectedChanged(Element currentElement);
        public event onSelectedChanged OnSelectedChanged;

        public delegate void onSizeChanged(Info information);
        public event onSizeChanged OnSizeChanged_;

        public delegate void onRefresh();
        public event onRefresh OnRefresh;

        public delegate void onDoubleClickOnFile(File currentFile);
        public event onDoubleClickOnFile OnDoubleClickOnFile;

        public delegate void onSavedRedirectron();
        public event onSavedRedirectron OnSavedRedirection;

        #region Calculate Items
        public int CurrentSite = 1;
        private List<Element> lstElements = new List<Element>();

        public List<Element> GetList()
        {
            int tabs = calculateTabPerSize();
            int c = 0;
            int s = 1;
            List<Element> sList = new List<Element>();

            for (int i = 0; i <= lstElements.Count - 1; i++)
            {
                if (c == tabs)
                {
                    c = 0;
                    s++;
                }
                lstElements[i].Site = s;
                c++;
            }

            if (this.lstElements.Count < tabs)
                return this.lstElements;

            if (Element.GetTabsNumbers(lstElements, CurrentSite) != null)
                return Element.GetTabsNumbers(lstElements, CurrentSite);
            return sList;
        }

        private int calculateTabPerSize()
        {
            int g = this.Height == 0 ? 1 : this.Height;
            int a1 = distance / 2;
            int a2 = distance / 2;
            int t = this.lstElements.Count;

            //  ts = g / (a1 + a2)
            double result = g / (a1 + a2);
            return Convert.ToInt32(Math.Ceiling(result));
        }

        public int calculateSites()
        {
            int g = this.Height == 0 ? 1 : this.Height;
            int a1 = distance / 2;
            int a2 = distance / 2;
            int t = this.lstElements.Count;

            // s(g) = (t * (a1 + a2)) / g
            double result = (t * (a1 + a2)) / g;
            return Convert.ToInt32(Math.Ceiling(result)) + 1;
        }

        public void SetIt(bool dir)
        {
            int sites = this.calculateSites();
            if (!dir)
            {
                if (this.CurrentSite + 1 <= sites)
                    this.CurrentSite++;
                else
                    this.CurrentSite = 1;
            }
            else
            {
                if (this.CurrentSite - 1 > 0)
                    this.CurrentSite--;
                else
                    this.CurrentSite = this.calculateSites();
            }

            // To refresh the label, firing the event!
            this.FireEvent();

            this.Refresh();
        }

        public void FireEvent()
        {
            if (this.OnSizeChanged_ != null)
                this.OnSizeChanged_(new Info(this.CurrentSite, this.calculateSites()));
        }

        #endregion

        public string Path
        {
            get
            {
                return this.path;
            }
        }

        public Directory CurrentDirectory
        {
            get
            {
                return this.currentDirectory;
            }
            set
            {
                if (value != null)
                {
                    this.currentDirectory = value;
                    this.Refresh();
                }
            }
        }

        public Directory RootDirectory
        {
            get
            {
                return this.currentFileSystem.RootDirectory;
            }
        }

        public VFS.VFS CurrentFileSystem
        {
            get
            {
                return this.currentFileSystem;
            }
        }

        public Element Selected
        {
            get
            {
                foreach (Element selElement in this.GetList())
                {
                    if (selElement.IsSelected)
                        return selElement;
                }
                return null;
            }
        }

        public vTabPage(string path) : base(System.IO.Path.GetFileNameWithoutExtension(path))
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            this.path = path;
            this.currentFileSystem = new VFS.VFS(System.IO.Path.Combine(Application.StartupPath, "Log.log"), path, 128, 45);
            this.currentFileSystem.Read(path);
            this.currentFileSystem.OnReady += CurrentFileSystem_OnReady;
            this.currentFileSystem.RootDirectory.OnChanged += RootDirectory_OnChanged;
            this.currentFileSystem.OnSaved += CurrentFileSystem_OnSaved;
            this.currentDirectory = this.currentFileSystem.RootDirectory;
            this.Refresh();
        }

        private void RootDirectory_OnChanged(Directory.Type action)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.Refresh()));
            else
                this.Refresh();
        }

        private void CurrentFileSystem_OnReady()
        {
            this.currentDirectory = this.currentFileSystem.RootDirectory;
            this.Invoke(new Action(() => this.Refresh()));
        }

        private void CurrentFileSystem_OnSaved()
        {
            this.Invoke(new Action(() => this.Refresh()));
        }

        new public void Select()
        {
            if (this.Selected != null)
                this.OnSelectedChanged(this.Selected);
            else
                this.OnSelectedChanged(new Element(string.Empty, Element.Type_.Default, null, null));
        }

        new public void Refresh()
        {
            this.lstElements = new List<Element>();
            // Add all elements.
            foreach (Directory currentDir in this.currentDirectory.SubDirs)
                this.lstElements.Add(new Element(currentDir.Name, Element.Type_.Directory, currentDir));

            foreach (File currentFile in this.currentDirectory.Files)
                this.lstElements.Add(new Element(currentFile.Name, Element.Type_.File, null, currentFile));

            // Add all new events
            foreach (Element el in this.lstElements)
                el.OnSelected += El_OnSelected;

            this.Invalidate();

            // Throw refresh
            if (this.OnRefresh != null)
                this.OnRefresh();
        }

        private void El_OnSelected(bool state, Element currentElement)
        {
            if (state && this.OnSelectedChanged != null)
                this.OnSelectedChanged(currentElement);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw all elemens right now - but not all, if we don't have the space.
            int index = 0;
            foreach (Element currentElement in this.GetList())
            {
                Rectangle currentRectangle = new Rectangle(new Point(0, index * distance), new Size(this.Width, distance));
                currentElement.CurrentRectangle = currentRectangle;
                e.Graphics.FillRectangle(currentElement.IsSelected ? Brushes.Blue : currentElement.IsHovered ? Brushes.Orange : Brushes.White, currentRectangle);
                e.Graphics.DrawImage(currentElement.IsFile ? Archiv.Properties.Resources.File : Archiv.Properties.Resources.Directory, new Rectangle(0, index * distance, 32, 32));
                e.Graphics.DrawString(currentElement.Name, new Font("Segoe UI", 9F, FontStyle.Regular), new SolidBrush(Color.Black), new Rectangle(32, currentRectangle.Y, currentRectangle.Width, currentRectangle.Height), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
                index++;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Element toSelectElement = Element.GetTab(e.Location, this.GetList());
            if (toSelectElement != null)
                Element.Hover(this.GetList(), toSelectElement);
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            // Save mouse point for OnDoubleClick
            this.mouseLocation = e.Location;

            // Select element
            if (e.Button == MouseButtons.Left)
            {
                Element toSelectElement = Element.GetTab(e.Location, this.GetList());
                if (toSelectElement != null)
                    Element.Select(this.GetList(), toSelectElement);
                this.Invalidate();
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            Element toSelectElement = Element.GetTab(this.mouseLocation, this.GetList());
            if (toSelectElement != null)
            {
                if (toSelectElement.CurrentDir != null)
                {
                    this.currentDirectory = toSelectElement.CurrentDir;
                    this.Refresh();
                }
                else
                {
                    // Doing something with a file.
                    if (this.OnDoubleClickOnFile != null)
                        this.OnDoubleClickOnFile(toSelectElement.CurrentFile);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.FireEvent();
        }

    }
}
