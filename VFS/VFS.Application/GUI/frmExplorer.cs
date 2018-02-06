// ------------------------------------------------------------------------
// frmExplorer.cs written by Code A Software (http://www.code-a-software.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  28.10.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VFS.Interfaces;
using VFS.ExtendedVFS;
using V = VFS;
using System.Threading;
using VFS.Application.GUI.Tab;
using VN = VFS.Net;

namespace VFS.Application.GUI
{
    public partial class frmExplorer : Form
    {
        // private IFile currentFile = null;
        // private Tab.Dialog.AddFolderDirectoryDialog.AddFolderDirectoryDialog dialog = null;
        private frmSearch currentSearchResult = null;        
        private Progress.frmProgressDialog prgDialog;
        private List<string> lstExtensions = new List<string>() { ".jpg", ".jpeg", ".bmp", ".png", ".tiff" };
        public int LastMethod = 0;
        public IFile LastFile = null;
        private Tab.PageContainer pageContainer = null;
        private Tab.PageController pageController = null;
        private VFS currentVFS;

        private Breadcrumb.Breadcrumb mBreadCrumb = null;


        public frmExplorer()
        {
            InitializeComponent();
            this.loadLanguage();
        }

        private void loadLanguage()
        {
            openFileTSMI.Text = Properties.Resources.strOpenFile;
            openFileTSMI.ShortcutKeyDisplayString = Properties.Resources.strOpenFilesKey;
            createFileTSMI.Text = Properties.Resources.strCreateFile;
            createFileTSMI.ShortcutKeyDisplayString = Properties.Resources.strCreateFileKey;
            openSettingsTSMI.Text = Properties.Resources.strSettings;
            openAboutTSMI.Text = Properties.Resources.strAbout;
            closeTabTSMI.Text = Properties.Resources.strCloseTab;
            endTSMI.Text = Properties.Resources.strExit;
            mFileTSMI.Text = Properties.Resources.strFile;

            extractAllTSMI.Text = Properties.Resources.strExtractAll;
            extractAllTSMI.ShortcutKeyDisplayString = Properties.Resources.strExtractAllKey;
            filesTSMI.Text = Properties.Resources.strFiles;
            goBackTSMI.Text = Properties.Resources.strBack;
            pageBackTSMI.Text = Properties.Resources.strPageBack;
            pageForwardTSMI.Text = Properties.Resources.strPageForward;

            directoryTSMI.Text = Properties.Resources.strDirectory;
            fileTSMI.Text = Properties.Resources.strFiles;
            addDirTSMI.Text = Properties.Resources.strAdd;
            dirRenameTSMI.Text = Properties.Resources.strRename;
            dirDeleteTSMI.Text = Properties.Resources.strDelete;
            dirExtractTSMI.Text = Properties.Resources.strExtractCurrentDir;

            fileAddTSMI.Text = Properties.Resources.strAdd;
            fileRenameTSMI.Text = Properties.Resources.strRename;
            fileDeleteTSMI.Text = Properties.Resources.strDelete;
            fileOpenTSMI.Text = Properties.Resources.strOpenFile;
            fileExtractTSMI.Text = Properties.Resources.strExtractCurrentFile;
        }

        private void frmExplorer_Load(object sender, EventArgs e)
        {
            V.Progress.OnValueChanged += Progress_OnValueChanged;
            prgDialog = new GUI.Progress.frmProgressDialog();
            prgDialog.Owner = this;
            prgDialog.Opacity = 0;
            prgDialog.Show();

            // Create PageController / PageContainer
            pageController = new Tab.PageController();
            pnlPageControllerContainer.Controls.Add(pageController);
            pageController.Dock = DockStyle.Fill;

            pageContainer = new Tab.PageContainer(pageController);
            pnlPageContainer.Controls.Add(pageContainer);
            pageContainer.Dock = DockStyle.Fill;

            pageContainer.SelectedPageChanged += delegate (Page page)
            {
                // If the user has selected another page, refresh breadcrumb menu
                mBreadCrumb.ChangeToPage(page);
            };

            mBreadCrumb = new Breadcrumb.Breadcrumb(@"\");
            this.pnlBreadCrumbContainer.Controls.Add(mBreadCrumb);
            mBreadCrumb.Dock = DockStyle.Fill;

            mBreadCrumb.OnCrumbItemClicked += delegate (string nPath)
            {
                if (pageContainer.SelectedPage != null)
                    pageContainer.SelectedPage.Path = nPath;
            };
        }

        private void Progress_OnValueChanged(double value, double step, VFS handle)
        {
            if (handle != this.currentVFS)
                return;

            prgDialog.CVFS = handle;

            if (value == 1.0 && step == 1.0 && prgDialog.IsHandleCreated)
            {
                // Close dialog if open
                prgDialog.Invoke(new Action(() => { prgDialog.Opacity = 0; }));
                return;
            }

            if (prgDialog.IsHandleCreated)
            {
                // Open dialog
                prgDialog.Invoke(new Action(() => { prgDialog.Opacity = 100; }));
            }
        }

        private void openFile(IFile file)
        {
            this.LastFile = file;
            Tab.Page selectedTabPage = this.pageContainer.SelectedPage;
            if (selectedTabPage.IsModified)
            {
                this.LastMethod = 1;
                selectedTabPage.CurrentFileSystem.ReadAllBytes(file.GetPath(), selectedTabPage.CurrentFileSystem.RootDirectory);
                return;
            }

            this.openFile(file.GetBytes().ToArray());          
        }

        private void openFile(byte[] data)
        {
            // Images            
            bool isImage = false;
            foreach (string extension in lstExtensions)
                if (this.LastFile.GetName().ToLower().EndsWith(extension))
                    isImage = true;

            byte[] bytesOfFile = data;

            if (isImage)
            {
                try
                {
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(bytesOfFile));
                }
                catch { }
                return;
            }

            // Text and other files
            string content = System.Text.Encoding.Default.GetString(bytesOfFile);
            if (content == null)
                content = string.Empty;
        }

        private void TbPage_OnDoubleClickOnFile(IFile currentFile)
        {
            this.openFile(currentFile);
        }

        private void PageRefreshEvent()
        {
            if (this.pageContainer.SelectedPage != null)
            {
                Tab.Page selctedTabPage = this.pageContainer.SelectedPage;
                string fPath = selctedTabPage.CurrentDirectory.ToFullPath();
                if (string.IsNullOrEmpty(fPath))
                    mBreadCrumb.Path = @"\";
                else
                    mBreadCrumb.Path = fPath;
            }
        }

        private void umbenenenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void openFileTSMI_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    //try
                    //{
                        Tab.Page page = new Tab.Page(ofd.FileName, System.IO.Path.GetFileNameWithoutExtension(ofd.FileName));
                        await page.Load();
                        page.LoadAfterLoad();
                        page.OnSideChanged += TbPage_OnSizeChanged_;
                        page.OnRefresh += PageRefreshEvent;
                        page.OnDoubleClickOnFile += TbPage_OnDoubleClickOnFile;
                        page.OnPathChanged += delegate (string path)
                        {
                            this.mBreadCrumb.Path = path;
                        };

                        this.pageContainer.AddPage(page);

                        this.currentVFS = page.CurrentFileSystem;
                        this.mBreadCrumb.VFSName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                    //}
                  //  catch (Exception f)
                   // {
                   //     MessageBox.Show(f.Message);
                      //  MessageBox.Show(this, "Sie haben diese Datei schon geöffnet, Sie können sie nicht nochmal öffnen!", "Schon geöffnet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            }
        }


        private void TbPage_OnSizeChanged_(Tab.Info information)
        {
            this.lblSites.Text = "Seite " + information.CurrentSite + " von " + information.MaxSite;
        }


        private void createFileTSMI_Click(object sender, EventArgs e)
        {

        }

        private void openSettingsTSMI_Click(object sender, EventArgs e)
        {

        }

        private void openAboutTSMI_Click(object sender, EventArgs e)
        {

        }

        private void closeTabTSMI_Click(object sender, EventArgs e)
        {

        }

        private void endTSMI_Click(object sender, EventArgs e)
        {

        }

        private void extractAllTSMI_Click(object sender, EventArgs e)
        {
            if (this.pageContainer.SelectedPage != null)
            {
                Tab.Page currentTab = this.pageContainer.SelectedPage;
                using (FolderBrowserDialog fld = new FolderBrowserDialog())
                {
                    if (fld.ShowDialog(this) == DialogResult.OK)
                    {
                        currentTab.CurrentFileSystem.ExtractDirectory(currentTab.RootDirectory, new VN.DirectoryPath(fld.SelectedPath));
                    }
                }
            }
            else // ToDo: Translation
                MessageBox.Show(this, "Sie haben kein VFS ausgewählt!", "Kein VFS ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Suchen ...")
                txtSearch.Text = string.Empty;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                txtSearch.Text = "Suchen ...";
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                // Do the search
                Tab.Page selectedTabPage = this.pageContainer.SelectedPage;
                if (selectedTabPage != null)
                {
                    // ToDo: Possiblity to choose where to search RootDir or currentDir or just currentDir (no recurse)
                    SearchResult result = selectedTabPage.CurrentFileSystem.Search(this.txtSearch.Text, selectedTabPage.CurrentDirectory, true);
                    // Open new form with searchResult
                    if (this.currentSearchResult == null || this.currentSearchResult.IsDisposed)
                    {
                        this.currentSearchResult = new frmSearch();
                        this.currentSearchResult.AddSearchResult(result, this.txtSearch.Text, selectedTabPage);
                        this.currentSearchResult.Show();
                    }
                    else
                    {
                        this.currentSearchResult.AddSearchResult(result, this.txtSearch.Text, selectedTabPage);
                        this.currentSearchResult.BringToFront();
                    }
                }
            }
        }

        private void mainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tbcFiles_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Tab.Page selctedTabPage = this.pageContainer.SelectedPage;

            if (selctedTabPage != null)
                currentVFS = selctedTabPage.CurrentFileSystem;
        }

        private void pnlLeftSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            this.pageContainer.DisplayPreview = chkPreview.Checked;
        }

        private void chkHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            this.pageContainer.SelectedPage.DisplayHorizontal = chkPreview.Checked;
        }
    }
}
