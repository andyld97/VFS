using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VFS;
using VFS.Interfaces;
using VFS.Application.GUI.Tab;

namespace VFS.Application.GUI
{
    public partial class frmSearch : Form
    {
        private SearchResult currentResult = new SearchResult();
        private string value = string.Empty;
        private GUI.Tab.ListView listView;
        private Page currentTabPage;

        public frmSearch()
        {
            InitializeComponent();
            listView = new GUI.Tab.ListView();
            this.pnlSearchContainer.Controls.Add(listView);
            listView.Dock = DockStyle.Fill;

            //listView.OnSelectedIndexChanged += Ls_OnSelectedIndexChanged;
            listView.OnSizeChanged_ += Ls_OnSizeChanged_;
            listView.OnDoubleClickedElement += ListView_OnDoubleClickedElement;
        }

        public void AddSearchResult(SearchResult rs, string value, Page currentTabPage)
        {
            this.currentResult = rs;
            this.value = value;
            this.currentTabPage = currentTabPage;

            this.Text = "Suchergebnis: " + value;
            listView.ClearList();

            foreach (IDirectory currentDir in rs.Directories)
                listView.Add(new Element(currentDir.GetName(), Element.Type_.Directory, currentDir, null));

            foreach (IFile currentFile in rs.Files)
                listView.Add(new Element(currentFile.GetName(), Element.Type_.File, null, currentFile));
        }

        private void ListView_OnDoubleClickedElement(Element selectedElement)
        {
            // Show element in tab
            if (currentTabPage != null)
                currentTabPage.OpenFile(selectedElement);
        }

        private void Ls_OnSizeChanged_(Info information)
        {
            this.lblSides.Text = "Seite " + information.CurrentSite + " von " + information.MaxSite;
        }

        private void Ls_OnSelectedIndexChanged(bool state, Element currentElement)
        {
            if (state && currentElement != null)
                this.lblPath.Text = "Pfad: " + (currentElement.IsFile ? currentElement.CurrentFile.GetPath() : currentElement.CurrentDir.ToFullPath());
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {

        }

        private void pnlLeft_Click(object sender, EventArgs e)
        {
            listView.CurrentSite--;
        }

        private void pnlRight_Click(object sender, EventArgs e)
        {
            listView.CurrentSite++;
        }
    }
}
