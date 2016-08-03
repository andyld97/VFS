// ------------------------------------------------------------------------
// frmExplorer.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  03.08.2016
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
        private Tab.Dialog.AddFolderDirectoryDialog.AddFolderDirectoryDialog dialog = null;
        private frmSettings settingsIns = null;

        public frmExplorer()
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tbcFiles.SelectedIndexChanged += TbcFiles_SelectedIndexChanged;

            foreach (string path in Environment.GetCommandLineArgs())
            {
                if (path.EndsWith(".ap"))
                {
                    Tab.vTabPage tbPage = new Tab.vTabPage(path);
                    tbPage.OnSelectedChanged += TbPage_OnSelectedChanged;
                    tbPage.OnSizeChanged_ += TbPage_OnSizeChanged_;
                    tbPage.OnRefresh += TbPage_OnRefresh;
                    tbPage.OnDoubleClickOnFile += TbPage_OnDoubleClickOnFile;
                    this.tbcFiles.TabPages.Add(tbPage);
                }
            }
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

        private void TbPage_OnDoubleClickOnFile(File currentFile)
        {
            this.openFile(currentFile);
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.tbcFiles.TabCount > 0 && this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                selctedTabPage.FireEvent();
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
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dateiÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        Tab.vTabPage tbPage = new Tab.vTabPage(ofd.FileName);
                        tbPage.OnSelectedChanged += TbPage_OnSelectedChanged;
                        tbPage.OnSizeChanged_ += TbPage_OnSizeChanged_;
                        tbPage.OnRefresh += TbPage_OnRefresh;
                        tbPage.OnDoubleClickOnFile += TbPage_OnDoubleClickOnFile;
                        this.tbcFiles.TabPages.Add(tbPage);
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show(this, "Sie haben diese Datei schon geöffnet, Sie können sie nicht nochmal öffnen!", "Schon geöffnet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dateiErstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmPack pack = new frmPack())
            {
                pack.ShowDialog();
            }
        }

        private void einstellungenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.settingsIns == null || this.settingsIns.IsDisposed)
                this.settingsIns = new frmSettings();

            this.settingsIns.Show();
        }

        private void überToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void allesEntpackenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage currentTab = this.tbcFiles.SelectedTab as Tab.vTabPage;
                using (FolderBrowserDialog fld = new FolderBrowserDialog())
                {
                    if (fld.ShowDialog(this) == DialogResult.OK)
                    {
                        currentTab.CurrentFileSystem.ExtractDirectory(currentTab.RootDirectory, fld.SelectedPath);
                    }
                }
            }
            else
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void hinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openDialog(false);
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ausgewähtelnOrdnerEntpackenToolStripMenuItem_Click(object sender, EventArgs e)
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
            else
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void hinzufügenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.openDialog(true);
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
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
                            this.openFile(currentFile);
                        }
                    }
                }
                else
                    MessageBox.Show(this, "Das ist ein Ordner, der kann nicht mit dem Editor geöffnet werden!", "Ordner", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void openFile(File file)
        {
            // Images
            List<string> lstExtensions = new List<string>() { ".jpg", ".jpeg", ".bmp", ".png", ".tiff" };
            bool isImage = false;
            foreach (string extension in lstExtensions)
                if (file.Name.ToLower().EndsWith(extension))
                    isImage = true;

            if (isImage)
            {
                try
                {
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(file.Bytes.ToArray()));
                    this.tpImages.BackgroundImage = x;
                    this.tpImages.BackgroundImageLayout = ImageLayout.Stretch;
                    this.tbCtrl.SelectedTab = this.tpImages;
                }
                catch { }
                return;
            }

            // Text and other files
            string content = System.Text.Encoding.Default.GetString(file.Bytes.ToArray());
            if (content == null)
                content = string.Empty;

            this.txtNotepad.Text = content;
            this.lblTxt.Text = "Datei: " + file.Path;
            this.tbCtrl.SelectedIndex = 1;
            this.btnSave.Enabled = true;
            this.currentFile = file;
        }

        private void ausgewählteDateiEntpackenToolStripMenuItem_Click(object sender, EventArgs e)
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
            else
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void löschenToolStripMenuItem1_Click(object sender, EventArgs e)
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
            else
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void seiteVorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                selctedTabPage.SetIt(true);
            }
        }

        private void seiteZurückToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selctedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                selctedTabPage.SetIt(false);
            }
        }

        private void zurückToolStripMenuItem_Click(object sender, EventArgs e)
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
            else
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void umbenenenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Directory
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selectedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                Tab.Element currentElement = selectedTabPage.Selected;

                if (currentElement == null)
                {
                    MessageBox.Show(this, "Sie haben nichts ausgewählt!", "Keine Auswahl getroffen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!currentElement.IsFile)
                {
                    using (InputDialog ipd = new InputDialog())
                    {
                        if (ipd.ShowDialog(this, "Bitte geben Sie den neuen Namen ein!", "Namen eingeben", currentElement.Name) == DialogResult.OK)
                        {
                            currentElement.Name = ipd.Result;
                            currentElement.CurrentDir.Name = ipd.Result;
                            selectedTabPage.CurrentFileSystem.Save();
                        }
                        else
                            MessageBox.Show(this, "Sie haben eine Datei ausgewählt statt einen Ordner!", "Kein Ordner ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void umbenennenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // File
            if (this.tbcFiles.SelectedTab != null)
            {
                Tab.vTabPage selectedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                Tab.Element currentElement = selectedTabPage.Selected;

                if (currentElement == null)
                {
                    MessageBox.Show(this, "Sie haben nichts ausgewählt!", "Keine Auswahl getroffen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (currentElement.IsFile)
                {
                    using (InputDialog ipd = new InputDialog())
                    {
                        if (ipd.ShowDialog(this, "Bitte geben Sie den neuen Namen ein!", "Namen eingeben", currentElement.Name) == DialogResult.OK)
                        {
                            currentElement.Name = ipd.Result;
                            currentElement.CurrentFile.Name = ipd.Result;
                            selectedTabPage.CurrentFileSystem.Save();
                        }
                    }
                }
                else
                    MessageBox.Show(this, "Sie haben einen Ordner ausgewählt statt einer Datei!", "Keine Datei ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(this, "Sie haben kein Archiv ausgewählt!", "Kein Archiv ausgewählt", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tabSchließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedTab != null)
            {
                DialogResult res = MessageBox.Show(this, "Ungespeicherte Änderungen gehen verloren, möchten Sie jetzt speichern, vor dem Beenden?", "Änderungen speichern", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    Tab.vTabPage selectedTabPage = this.tbcFiles.SelectedTab as Tab.vTabPage;
                    selectedTabPage.CurrentFileSystem.Save();
                    tbcFiles.TabPages.Remove(this.tbcFiles.SelectedTab);
                }
                else if (res == DialogResult.No)
                    tbcFiles.TabPages.Remove(this.tbcFiles.SelectedTab);
            }
            else
                MessageBox.Show(this, "Sie haben kein Archiv geöffnet!", "Nicht notwendig", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
