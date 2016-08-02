// ------------------------------------------------------------------------
// frmExplorer.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  02.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VFS;

namespace Archiv.GUI
{
    public partial class frmExplorer : Form
    {
        private File currentFile = null;

        public frmExplorer()
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.picGoBack.Click += PicGoBack_Click;
            this.picGoForward.Click += PicGoForward_Click;
            this.tbcFiles.SelectedIndexChanged += TbcFiles_SelectedIndexChanged;
        }

        private void TbcFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                selctedTabPage.Refresh();
                selctedTabPage.Select();
            }
        }

        private void PicGoForward_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                selctedTabPage.SetIt(false);
            }
        }

        private void PicGoBack_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                selctedTabPage.SetIt(true);
            }
        }

        private void dateiLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    Tab.vTabPage tbPage = new Tab.vTabPage(ofd.FileName);
                    tbPage.OnSelectedChanged += TbPage_OnSelectedChanged;
                    tbPage.OnSizeChanged_ += TbPage_OnSizeChanged_;
                    tbPage.OnRefresh += TbPage_OnRefresh;
                    tbPage.OnDoubleClickOnFile += TbPage_OnDoubleClickOnFile;
                    this.tbcFiles.TabPages.Add(tbPage);
                }
            }
        }

        private void TbPage_OnDoubleClickOnFile(File currentFile)
        {
            this.currentFile = currentFile;
            this.txtNotepad.Text = System.Text.Encoding.Default.GetString(currentFile.Bytes.ToArray());
            this.lblTxt.Text = "Datei: " + currentFile.Path;
            this.tbCtrl.SelectedIndex = 1;
            this.btnSave.Enabled = true;
        }

        private void TbPage_OnRefresh()
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                string fPath = selctedTabPage.CurrentDirectory.ToFullPath();
                if (string.IsNullOrEmpty(fPath))
                    this.txtCurrentPath.Text = @"\";
                else
                    this.txtCurrentPath.Text = fPath;
            }
        }

        private void TbPage_OnSizeChanged_(Tab.Info information)
        {
            this.lblSites.Text = "Seite " + information.CurrentSite + " von " + information.MaxSite;
        }

        private void TbPage_OnSelectedChanged(Tab.Element currentElement)
        {
            if (currentElement.IsFile)
            {
                this.lblOrdnerName.Text = "Ordner: " + (string.IsNullOrEmpty(currentElement.CurrentFile.Parent.Name) ? "root" : currentElement.CurrentFile.Parent.Name);
                this.lblSelectedFile.Text = "Ausgewählte Datei: " + currentElement.Name;
                this.lblSizeOfFile.Text = "Größe der Datei: " + currentElement.CurrentFile.CalculateLength().ToString();
            }
            else
            {
                if (currentElement.Type == Tab.Element.Type_.Default)
                {
                    this.lblOrdnerName.Text = "Ordner: -";
                    this.lblSelectedFile.Text = "Ausgewählte Datei: -";
                    this.lblSizeOfFile.Text = "Größer der Datei: -";
                    return;
                }
                this.lblOrdnerName.Text = "Ordner: " + (string.IsNullOrEmpty(currentElement.CurrentDir.Name) ? "root" : currentElement.CurrentDir.Name);
                this.lblSelectedFile.Text = "Ausgewählte Datei: -";
                this.lblSizeOfFile.Text = "Größer der Datei: -";
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            // Get current Tabpage
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                if (string.IsNullOrEmpty(selctedTabPage.CurrentDirectory.Name) && string.IsNullOrEmpty(selctedTabPage.CurrentDirectory.ToFullPath()))
                    MessageBox.Show(this, "Sie befinden sich derzeit imt Hauptverzeichnis, es gibt hier kein Zurück!", "Root-Verzeichnis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (selctedTabPage.CurrentDirectory.Parent != null)
                    selctedTabPage.CurrentDirectory = selctedTabPage.CurrentDirectory.Parent;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.tbcFiles.TabCount > 0 && this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                selctedTabPage.FireEvent();
            }
        }

        private void btnExtractSelectedFile_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                if (selctedTabPage.Selected.IsFile)
                {
                    File currentFile = selctedTabPage.Selected.CurrentFile;
                    if (currentFile != null)
                    {
                        if (currentFile.Bytes.Count() != 0)
                        {
                            string content = selctedTabPage.CurrentFileSystem.ReadAllText(currentFile.Path, selctedTabPage.RootDirectory);
                            if (content == null)
                                content = string.Empty;

                            this.txtNotepad.Text = content;
                            this.lblTxt.Text = "Datei: " + currentFile.Path;
                            this.tbCtrl.SelectedIndex = 1;
                            this.btnSave.Enabled = true;
                            this.currentFile = currentFile;
                        }
                    }
                }
                else
                    MessageBox.Show(this, "Das ist ein Ordner, der kann nicht mit dem Editor geöffnet werden!", "Ordner", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.currentFile != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                selctedTabPage.CurrentFileSystem.WriteAllText(this.txtNotepad.Text, this.currentFile.Name, selctedTabPage.CurrentDirectory, true);
                selctedTabPage.CurrentFileSystem.Save();
            }
        }

        private void tpNotepad_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {

        }

        private Tab.Dialog.AddFolderDirectoryDialog.AddFolderDirectoryDialog dialog = null;

        private void openDialog(bool state)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;

                dialog = new Tab.Dialog.AddFolderDirectoryDialog.AddFolderDirectoryDialog(state, selctedTabPage.CurrentDirectory, selctedTabPage.CurrentFileSystem);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    selctedTabPage.CurrentFileSystem.Save();
                    if (state)
                        selctedTabPage.Refresh();
                }
            }
            else
                MessageBox.Show(this, "Bitte öffnen Sie erst ein neues Archiv!", "Neues Archiv öffnen!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void btnCreateDir_Click(object sender, EventArgs e)
        {
            this.openDialog(false);
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            this.openDialog(true);
        }

        private void txtNotepad_TextChanged(object sender, EventArgs e)
        {

        }

        // ToDo: Test deleting a directory
        private void btnDeleteDirectory_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage currentTab = this.tbcFiles.SelectedTab as Tab.vTabPage;
                if (currentTab.Selected != null)
                {
                    if (!currentTab.Selected.IsFile)
                    {
                        if (MessageBox.Show(this, "Sind Sie sich wirklich sicher, dass Sie den Ordner löschen möchten?", "Sicher?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (currentTab.Selected.CurrentDir.Parent == null)
                                currentTab.CurrentFileSystem.RootDirectory.Parent.Remove(currentTab.Selected.CurrentDir);
                            else
                                currentTab.Selected.CurrentDir.Parent.Remove(currentTab.Selected.CurrentDir);
                            currentTab.CurrentFileSystem.Save();
                        }
                    }
                    else
                        MessageBox.Show(this, "Das ausgewählte Objekt ist eine Datei, kein Ordner! Bitte schauen Sie unter der Kategorie Datei, um eine Datei zu löschen!", "Falsche Auswahl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show(this, "Sie haben keine Datei ausgewählt!", "Keine Auswahl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(this, "Sie haben keinen Ordner ausgewählt!", "Kein Ordner ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnExtractDirs_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage currentTab = this.tbcFiles.SelectedTab as Tab.vTabPage;
                if (currentTab.Selected != null && !currentTab.Selected.IsFile)
                {
                    using (FolderBrowserDialog fld = new FolderBrowserDialog())
                    {
                        if (fld.ShowDialog(this) == DialogResult.OK)
                            currentTab.CurrentFileSystem.ExtractDirectory(currentTab.Selected.CurrentDir, fld.SelectedPath);
                    }
                }
                else
                {
                    // Null  or File
                }
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage currentTab = this.tbcFiles.SelectedTab as Tab.vTabPage;
                if (currentTab.Selected != null && currentTab.Selected.IsFile)
                {
                    currentTab.Selected.CurrentFile.Parent.Files.Remove(currentTab.Selected.CurrentFile);
                    currentTab.CurrentFileSystem.Save();
                }
                else
                {
                    // Null or Directory
                }
            }
        }

        private void btnExtractFiles_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage currentTab = this.tbcFiles.SelectedTab as Tab.vTabPage;
                if (currentTab.Selected != null && currentTab.Selected.IsFile)
                {
                    using (FolderBrowserDialog fld = new FolderBrowserDialog())
                    {
                        if (fld.ShowDialog(this) == DialogResult.OK)
                        {
                            try
                            {
                                System.IO.File.WriteAllBytes(System.IO.Path.Combine(fld.SelectedPath, currentTab.Selected.CurrentFile.Name), currentTab.Selected.CurrentFile.Bytes.ToArray());
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        private void btnExtractAll_Click(object sender, EventArgs e)
        {

        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmAbout about = new frmAbout())
            {
                about.ShowDialog();
            }
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateiVerpackenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
