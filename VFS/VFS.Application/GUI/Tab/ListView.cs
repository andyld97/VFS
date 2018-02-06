// ListView.cs written by Code A Software (http://www.code-a-software.net)
// All rights reserved
// Created on:      13.01.2017
// Last update on:  19.11.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace VFS.Application.GUI.Tab
{
    public sealed class ListView : Design.DesignPanel
    {
        private List<Element> lstItems = new List<Element>();
        private List<Element> lstCurrentItems = new List<Element>();
        private List<Element> nElements = new List<Element>();

        private int currentSite = 1;
        private Point mouseLocation = new Point();

        private bool displayHorizontal = true;

        private Image defaultImageDir = Properties.Resources.Folder;
        private Image selectedImageDir = Properties.Resources.Folder;
        private Image defaultImageFile = Properties.Resources.File;
        private Image selectedImageFile = Properties.Resources.File;

        private ContextMenu rightClickDirectory = null;
        private ContextMenu rightClickFile = null;
        private ContextMenu rightClickDefault = null;
        private ContextMenu rightClickMultipleItems = null;

        private bool isCtrlPressed => (Control.ModifierKeys & Keys.Control) == Keys.Control;
        private bool isShiftPressed => (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

        public delegate void onDoubleClickedElement(Element selectedElement);
        public event onDoubleClickedElement OnDoubleClickedElement;

        public delegate void onSelectedIndexChanged(Element[] currentElement);
        public event onSelectedIndexChanged OnSelectedIndexChanged;

        public delegate void onSizeChanged(Info information);
        public event onSizeChanged OnSizeChanged_;

        public int CurrentSite
        {
            get
            {
                return currentSite;
            }
            set
            {
                currentSite = value;

                if (value > this.CalculateSides())
                    currentSite = 1;
                else if (value <= 0)
                    currentSite = this.CalculateSides();

                this.Refresh();
                this.Invalidate();
            }
        }

        public Element SelectedElement
        {
            get
            {
                foreach (Element currentElement in this.lstCurrentItems)
                    if (currentElement.IsSelected)
                        return currentElement;
                return null;
            }
            set
            {
                foreach (Element currentElement in this.lstCurrentItems)
                {
                    if (currentElement.Name == value.Name && currentElement.IsFile == value.IsFile && currentElement.CurrentFile == value.CurrentFile && currentElement.CurrentDir == value.CurrentDir)
                    {
                        Element.Select(this.lstCurrentItems, currentElement);
                        this.Invalidate();
                        return;
                    }
                }
            }
        }

        public bool DisplayHorizontal
        {
            get
            {
                return this.displayHorizontal;
            }
            set
            {
                this.displayHorizontal = value;
                this.Refresh();
                this.Invalidate();
            }
        }

        public ListView()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // Initialize menus
            rightClickDirectory = new ContextMenu();
            rightClickDirectory.MenuItems.Add(new MenuItem("Kopieren"));
            rightClickDirectory.MenuItems.Add(new MenuItem("Verschieben"));
            rightClickDirectory.MenuItems.Add(new MenuItem("Löschen"));
            rightClickDirectory.MenuItems.Add(new MenuItem("Eigenschaften"));

            rightClickFile = new ContextMenu();
            rightClickFile.MenuItems.Add(new MenuItem("Kopieren"));
            rightClickFile.MenuItems.Add(new MenuItem("Verschieben"));
            rightClickFile.MenuItems.Add(new MenuItem("Löschen"));
            rightClickFile.MenuItems.Add(new MenuItem("Eigenschaften"));

            rightClickDefault = new ContextMenu();
            rightClickDefault.MenuItems.Add(new MenuItem("Neue Datei"));
            rightClickDefault.MenuItems.Add(new MenuItem("Neuer Ordner"));
            rightClickDefault.MenuItems.Add(new MenuItem("Einfügen"));
            rightClickDefault.MenuItems.Add(new MenuItem("Eigenschaften"));

            rightClickMultipleItems = new ContextMenu();
            rightClickMultipleItems.MenuItems.Add(new MenuItem("Archivieren"));
            rightClickMultipleItems.MenuItems.Add(new MenuItem("Kopieren"));
            rightClickMultipleItems.MenuItems.Add(new MenuItem("Verschieben"));
            rightClickMultipleItems.MenuItems.Add(new MenuItem("Löschen"));
            rightClickMultipleItems.MenuItems.Add(new MenuItem("Eigenschaften"));
        }

        public void Add(Element e)
        {
            this.lstItems.Add(e);
            this.Refresh();
        }

        public void Remove(Element e)
        {
            this.lstItems.Remove(e);
            this.Invalidate();
        }

        public void ClearList()
        {
            this.lstItems.Clear();
            this.Refresh();
            this.Invalidate();
        }

        public new void Refresh()
        {
            if (this.CurrentSite > this.CalculateSides())
                this.currentSite = this.CalculateSides();

            int sideCounter = 1, columnCounter = 0, itemCounter = 0;
            int columnMax = this.CalculateColumnLength();
            int itemMax = this.CalculateColumnItemLength();

            nElements.Clear();    
         
            foreach (Element currentElement in this.lstItems)
            {
                if (itemCounter == itemMax)
                {
                    if (columnCounter == columnMax - 1)
                    {
                        itemCounter = 1;
                        columnCounter = 0;
                        sideCounter++;
                        currentElement.Site = sideCounter;
                        currentElement.Column = columnCounter;
                        nElements.Add(currentElement);
                    }
                    else
                    {
                        itemCounter = 1;
                        currentElement.Site = sideCounter;
                        currentElement.Column = ++columnCounter;
                        nElements.Add(currentElement);
                    }
                }
                else
                {
                    currentElement.Site = sideCounter;
                    currentElement.Column = columnCounter;
                    nElements.Add(currentElement);
                    itemCounter++;
                }
            }

            this.lstCurrentItems.Clear();
            itemCounter = 0;
            foreach (Element currentElement in nElements)
            {
                if (currentElement.Site == this.CurrentSite)
                {
                    // Calculate appropriate rectangle
                    if (!displayHorizontal)
                        currentElement.CurrentRectangle = new Rectangle(Consts.ListView.ITEM_WIDTH * currentElement.Column + 10, Consts.ListView.ITEM_HEIGHT * itemCounter++ + 5, Consts.ListView.ITEM_WIDTH, Consts.ListView.ITEM_HEIGHT);
                    else
                        currentElement.CurrentRectangle = new Rectangle(Consts.ListView.ITEM_WIDTH * itemCounter++ + 5, Consts.ListView.ITEM_HEIGHT * currentElement.Column + 10, Consts.ListView.ITEM_WIDTH, Consts.ListView.ITEM_HEIGHT);
                    if (itemCounter == itemMax)
                        itemCounter = 0;
                    this.lstCurrentItems.Add(currentElement);
                }
            }

            this.OnSizeChanged_?.Invoke(new Info(this.CurrentSite, this.CalculateSides()));
        }

        private int devideBackwards(int value, int quotient)
        {
            double result = value / (double)quotient;
            return (int)result;
        }

        private int devideForwards(int value, int quotient)
        {
            if (quotient == 0)
                return 0;

            double result = value / (double)quotient;

            if (value % quotient > 0)
                return Convert.ToInt32(Convert.ToString(result.ToString()[0])) + 1;

            return (int)result;
        }

        public int CalculateColumnItemLength()
        {
            return this.devideBackwards(displayHorizontal ? this.Width : this.Height, displayHorizontal ? Consts.ListView.ITEM_WIDTH : Consts.ListView.ITEM_HEIGHT);
        }

        public int CalculateColumnLength()
        {
            if (lstItems.Count == 0)
                return 0;

            if (displayHorizontal)
                return (this.Height / Consts.ListView.ITEM_HEIGHT);
            else
                return (this.Height / Consts.ListView.ITEM_HEIGHT);
        }

        public int CalculateSides()
        {
            if (CalculateColumnLength() == 0)
                return 1;

            return this.devideForwards(lstItems.Count, (CalculateColumnLength() * CalculateColumnItemLength()));
        }

        private Image readImage(Element e)
        {
            return (e != null ? (e.IsFile ? (e.IsSelected ? selectedImageFile : defaultImageFile) : (e.IsSelected ? selectedImageDir : defaultImageDir)) : null);
        }

        private string getElementName(Element currentElement)
        {
            string currentString = currentElement.IsFile ? currentElement.CurrentFile.GetName() : currentElement.CurrentDir.GetName();
            string result = string.Empty;

            if (currentString.Length > Consts.ListView.CHAR_LENGTH)
            {
                for (int i = 0; i <= Consts.ListView.CHAR_LENGTH - 5; i++)
                    result += currentString[i];
                result += " ...";
            }
            else
                return currentString;

            return result;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.Refresh();

            e.Graphics.FillRectangle(Brushes.White, this.DisplayRectangle);

            foreach (Element currentElement in this.lstCurrentItems)
            {
                if (currentElement.IsHovered)
                    e.Graphics.FillRectangle(new SolidBrush(Consts.ListView.HoverColor), currentElement.CurrentRectangle);
                if (currentElement.IsSelected)
                    e.Graphics.FillRectangle(new SolidBrush(Consts.ListView.SelectedColor), currentElement.CurrentRectangle);

                Image toDraw = this.readImage(currentElement);
                e.Graphics.DrawImage(toDraw, new Rectangle(currentElement.CurrentRectangle.X, currentElement.CurrentRectangle.Y, Consts.ListView.IMAGE_WIDTH, Consts.ListView.IMAGE_HEIGHT));
                e.Graphics.DrawString(this.getElementName(currentElement), new Font("Segoe UI", 9, FontStyle.Regular), Brushes.Black, new Rectangle(currentElement.CurrentRectangle.X + Consts.ListView.IMAGE_WIDTH + 10, currentElement.CurrentRectangle.Y, currentElement.CurrentRectangle.Width - Consts.ListView.ITEM_WIDTH, Consts.ListView.ITEM_HEIGHT), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.mouseLocation = e.Location;

            foreach (Element currentElement in this.lstCurrentItems)
            {
                if (currentElement.CurrentRectangle.Contains(e.Location))
                {
                    if (e.Button != MouseButtons.Left)
                        Element.Hover(this.lstCurrentItems, currentElement);
                    
                    this.Invalidate();
                    break;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                foreach (Element currentElement in this.lstCurrentItems)
                {
                    if (currentElement.CurrentRectangle.Contains(e.Location))
                    {
                        if (!isShiftPressed && !isCtrlPressed)
                        {
                            selectElement(currentElement, false);
                        }
                        else if (isCtrlPressed)
                        {
                            selectElement(currentElement, true);
                        }
                        else if (isShiftPressed)
                        {
                            // Get first selected element
                            int firstIndex = 0;
                            int lastIndex = nElements.IndexOf(currentElement);

                            Element firstSelected = nElements.Where(t => t.IsSelected).FirstOrDefault();
                            if (firstSelected != null)
                                firstIndex = nElements.IndexOf(firstSelected);

                            for (int i = firstIndex; i <= lastIndex; i++)
                                Element.Select(nElements, nElements[i], true, true);

                            // Deselect all elements after current, because range with shift-key is just from first to current.
                            for (int i = lastIndex + 1; i < nElements.Count; i++)
                                Element.Select(nElements, nElements[i], true, false);
                        }
                        this.Invalidate();
                    }
                }
            }
            else
            {
                int countedElements = nElements.Where(ele => ele.IsSelected).Count<Element>();
                if (countedElements > 1)
                {
                    // Show menu for multiple items
                    rightClickMultipleItems.Show(this, e.Location);

                }
                else if (countedElements == 1)
                {
                    // Show menu for a single file/folder
                    // selectedElement cannot be null; because countElements == 1;
                    Element selectedElement = nElements.Where(ele => ele.IsSelected).FirstOrDefault();

                    if (selectedElement.IsFile)
                        rightClickFile.Show(this, e.Location);
                    else
                        rightClickDirectory.Show(this, e.Location);
                }
                else
                {
                    // Show default menu
                    rightClickDefault.Show(this, e.Location);
                }
            }
        }

        private void selectElement(Element current, bool multiple)
        {
            Element.Select(this.nElements, current, multiple, !current.IsSelected);
            this.OnSelectedIndexChanged?.Invoke(nElements.Where(T => T.IsSelected).ToArray<Element>());
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            Element currentElement = Element.GetTab(this.mouseLocation, this.lstCurrentItems);
            if (currentElement != null)
                this.OnDoubleClickedElement?.Invoke(currentElement);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.lstCurrentItems.Count != 0)
            {
                int index = -1;

                if (this.SelectedElement != null)
                    index = this.lstCurrentItems.IndexOf(this.SelectedElement);

                this.Refresh();

                if (index != -1 && index <= this.lstCurrentItems.Count - 1)
                    Element.Select(this.lstCurrentItems, this.lstCurrentItems[index]);
                this.Invalidate();
            }
            this.OnSizeChanged_?.Invoke(new Info(this.CurrentSite, this.CalculateSides()));
        }
    }
}
