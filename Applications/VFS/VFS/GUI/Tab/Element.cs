// ------------------------------------------------------------------------
// Element.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  02.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using VFS;
using VFS.Interfaces;

namespace VFS.GUI.Tab
{
    public class Element
    {
        public string Name;
        public Type_ Type = Type_.Default;
        public int Site = 0;
        public Rectangle CurrentRectangle = new Rectangle();
        private IDirectory currentDir = null;
        private IFile currentFile = null;
        private bool hovered = false;
        private bool selected = false;

        public delegate void onSelected(bool state, Element currentElement);
        public event onSelected OnSelected;

        public bool IsHovered
        {
            get
            {
                return this.hovered;
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.selected;
            }
            private set
            {
                this.selected = value;
                // Fire event!
                if (this.OnSelected != null)
                    this.OnSelected(value, this);
            }
        }

        public IDirectory CurrentDir
        {
            get
            {
                return this.currentDir;
            }
        }

        public IFile CurrentFile
        {
            get
            {
                return this.currentFile;
            }
        }

        public bool IsFile
        {
            get
            {
                return this.Type == Type_.File;
            }
        }

        public enum Type_
        {
            File,
            Directory,
            Default
        }

        public Element(string Name, Type_ Type, IDirectory currentDir = null, IFile currentFile = null)
        {
            this.Name = Name;
            this.Type = Type;

            // currentDir and currentFile != null at the same time is not accepted!
            if (currentDir != null)
                this.currentDir = currentDir;
            else if (currentFile != null)
                this.currentFile = currentFile;
        }

        public Element()
        {

        }

        public static Element GetTab(Point loc, List<Element> lstTab)
        {
            if (lstTab == null)
                return null;
            for (int i = 0; i <= lstTab.Count - 1; i++)
            {
                if (lstTab[i].CurrentRectangle.Contains(loc))
                {
                    return lstTab[i];
                }
            }
            return null;
        }

        public static List<Element> GetTabsNumbers(List<Element> lst, int number)
        {
            List<Element> current = new List<Element>();
            foreach (Element l in lst)
            {
                if (l.Site == number)
                    current.Add(l);
            }
            return current;
        }

        public static void Hover(List<Element> lst, Element cElement)
        {
            foreach (Element cur in lst)
                cur.hovered = false;
            cElement.hovered = true;
        }

        public static void Select(List<Element> lst, Element cElement)
        {
            foreach (Element cur in lst)
                cur.IsSelected = false;
            cElement.IsSelected = true;
            cElement.hovered = false;
        }
    }
}
