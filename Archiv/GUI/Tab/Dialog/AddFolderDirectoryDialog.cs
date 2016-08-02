// ------------------------------------------------------------------------
// AddFolderDirectoryDialog.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  02.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VFS;
using Archiv.GUI.Tab.Dialog.AddFolderDirectoryDialog;

namespace Archiv.GUI.Tab.Dialog.AddFolderDirectoryDialog
{
    public partial class AddFolderDirectoryDialog : Form
    {
        private int index = -1;
        private string[] fileNames = null;
        private Directory currentDir = null;
        private VFS.VFS currentFS = null;
        private List<string> dirPathes = new List<string>();

        public int Index
        {
            get
            {
                return index;
            }
            private set
            {
                index = value;
                this.tbDirFile.SelectedIndex = value;
            }
        }

        public AddFolderDirectoryDialog(bool isFile, Directory currentDir, VFS.VFS currentFS)
        {
            InitializeComponent();
            this.Index = Convert.ToInt32(!isFile);
            this.Text = isFile ? "Dateien auswählen" : "Ordner auswählen";
            this.currentDir = currentDir;
            this.currentFS = currentFS;
            this.tbDirFile.SelectedIndexChanged += TbDirFile_SelectedIndexChanged;
        }

        private void TbDirFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.index != (sender as TabControl).SelectedIndex)
                (sender as TabControl).SelectedIndex = this.index;
        }

        private void AddFolderDirectoryDialog_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
                bool gCondition = this.fileNames != null && this.currentDir != null && this.currentFS != null;
                // Check files in tbcFiles
                bool sCondition = false;
                foreach (TabPage tb in this.tbcFiles.TabPages)
                {
                    FilePage currentPage = tb as FilePage;
                    if (string.IsNullOrEmpty(currentPage.FileContent) || string.IsNullOrEmpty(currentPage.FileName))
                    {
                        sCondition = false;
                        break;
                    }
                    else
                        sCondition = true;
                }

                if (gCondition || (sCondition && this.currentDir != null && this.currentFS != null))
                {
                    if (gCondition)
                    {
                        // Add files from this.fileNames
                        foreach (string currentFile in this.fileNames)
                        {
                            string[] segments = currentFile.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                            if (segments.Length == 0)
                                continue;
                            if (!this.chkOverride.Checked && this.currentFS.FileExists(segments[segments.Length - 1], this.currentDir))
                                MessageBox.Show(this, "Die Datei existiert schon und Sie haben den Haken zum Überschreiben nicht gesetzt => Nichts passiert!", "Nichts ist passiert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                try
                                {
                                    this.currentFS.WriteAllBytes(System.IO.File.ReadAllBytes(currentFile), segments[segments.Length - 1], this.currentDir, true);
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                    }
                    if (sCondition)
                    {
                        // Add own generated files from tbcFiles
                        foreach (TabPage tb in this.tbcFiles.TabPages)
                        {
                            FilePage currentPage = tb as FilePage;
                            // Check if file exists
                            if (this.currentFS.FileExists(this.currentDir.ToFullPath() + @"\" + currentPage.FileName, this.currentFS.RootDirectory))
                            {
                                if (MessageBox.Show(this, "Die Datei existiert schon. Möchten Sie sie überschreiben?", "Datei existiert schon", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    this.currentFS.WriteAllText(currentPage.FileContent, currentPage.FileName, this.currentDir, true);
                                else
                                    return;
                            }
                            else
                                this.currentFS.WriteAllText(currentPage.FileContent, currentPage.FileName, this.currentDir, true);
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(this, "Entweder sind einige Ihrer selbst erstellen Dateien leer (Dateiname oder Inhalt) oder Sie haben keine Dateien angegeben", "Ungültige Eingabe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (this.index == 1)
            {
                this.btnAddDirectory.Enabled = this.btnDeleteSelctedDirectory.Enabled = this.btnClearDirectorys.Enabled = false;
                (sender as Button).Enabled = false;
                btnCancel.Enabled = false;

                // Add all dirs to currentDir
                // ToDo: Add try-catch {} block
                if (currentDir != null)
                {
                    foreach (string path in this.dirPathes)
                    {
                        foreach (System.IO.DirectoryInfo cDir in new System.IO.DirectoryInfo(path).GetDirectories("*.*", System.IO.SearchOption.AllDirectories))
                        {
                            string temp = cDir.FullName.Replace(path, string.Empty);
                            string pathFromDir = string.Empty;
                            for (int i = 1; i <= temp.Length - 1; i++)
                                pathFromDir += temp[i].ToString(); 
                            currentDir.AddPathes(new string[] { pathFromDir });

                            // Add files to this folders
                            foreach (System.IO.FileInfo fInfo in cDir.GetFiles())
                            {
                                string t = fInfo.FullName.Replace(path, string.Empty);
                                string pathFromFile = string.Empty;
                                for (int i = 1; i <= t.Length - 1; i++)
                                    pathFromFile += t[i].ToString();

                                this.currentDir.AddFile(pathFromFile);
                                this.currentFS.WriteAllBytes(System.IO.File.ReadAllBytes(fInfo.FullName), pathFromFile, true);
                            }
                        }
                    }
                    this.currentFS.Save();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            this.tbcFiles.TabPages.Add(new FilePage(string.Empty));
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (this.tbcFiles.SelectedIndex != -1)
            {
                if (MessageBox.Show(this, "Sind Sie sich sicher, dass Sie diese Datei löschen möchten, d.h. sie wird dann nicht erstellt?", "Sicher?", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    this.tbcFiles.TabPages.RemoveAt(this.tbcFiles.SelectedIndex);
            }
            else
                MessageBox.Show(this, "Sie haben keine Datei erstellt, die gelöscht werden könnte!", "Keine Datei", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnResearch_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true })
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    this.fileNames = ofd.FileNames;
                    this.lblFilesSelected.Text = ofd.FileNames.Length + " Dateien ausgewählt";
                }
            }
        }

        private void btnSearchDirecotries_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fld = new FolderBrowserDialog())
            {
                if (fld.ShowDialog(this) == DialogResult.OK)
                {
                    if (!dirPathes.Contains(fld.SelectedPath))
                        dirPathes.Add(fld.SelectedPath);
                    else
                        MessageBox.Show(this, "Der aktuelle Pfad befindet sich schon in der Liste, er wird nicht doppelt hinzugefügt!", "Aktueller Pfad existiert schon!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.lstDirectories.DataSource = dirPathes.ToArray();
                }
            }
        }

        private void lstDirectories_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDeleteSelctedDirectory.Enabled = ((sender as ListBox).SelectedIndex != -1);
        }

        private void btnDeleteSelctedDirectory_Click(object sender, EventArgs e)
        {
            this.dirPathes.RemoveAt(lstDirectories.SelectedIndex);
            this.lstDirectories.DataSource = dirPathes.ToArray();
        }

        private void btnClearDirectorys_Click(object sender, EventArgs e)
        {
            this.dirPathes.Clear();
            this.lstDirectories.DataSource = dirPathes.ToArray();
        }
    }
}
