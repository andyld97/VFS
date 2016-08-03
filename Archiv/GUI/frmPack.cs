// ------------------------------------------------------------------------
// frmSettings.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      03.08.2016
// Last update on:  03.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Archiv.GUI
{
    public partial class frmPack : Form
    {
        private List<string> lstFiles_ = new List<string>();
        private List<string> lstDirectories_ = new List<string>();
        private bool containsData = false,
                     containsPath = false,
                     isReady = false;

        public frmPack()
        {
            InitializeComponent();
        }

        private void refreshData()
        {
            int index = this.lstFiles.SelectedIndex;            
            this.lstFiles.DataSource = this.lstFiles_.ToArray();
            if (index != -1 && this.lstFiles_.Count > 0)
            {
                if (index >= this.lstFiles.Items.Count - 1)
                    this.lstFiles.SelectedIndex = this.lstFiles.Items.Count - 1;
                else
                    this.lstFiles.SelectedIndex = index;
            }

            index = this.lstDirectories.SelectedIndex;
            this.lstDirectories.DataSource = this.lstDirectories_.ToArray();
            if (index != -1 && this.lstDirectories_.Count > 0)
            {
                if (index >= this.lstDirectories.Items.Count - 1)
                    this.lstDirectories.SelectedIndex = this.lstDirectories.Items.Count - 1;
                else
                    this.lstDirectories.SelectedIndex = index;               
            }

            this.btnFilesRemove.Enabled = (this.lstFiles.SelectedIndex != -1 && this.lstFiles_.Count != 0);
            this.btnDirRemove.Enabled = (this.lstDirectories.SelectedIndex != -1 && this.lstDirectories_.Count != 0);
            this.containsData = (this.lstFiles_.Count != 0 || this.lstDirectories_.Count != 0);
            this.btnOk.Enabled = this.containsPath && this.containsData;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.pnlProgress.Visible && isReady)
                this.DialogResult = DialogResult.OK;

            this.pnlProgress.Visible = true;
            this.pnlButtons.Enabled = false;

            // Definie data for thread
            string path = this.txtPath.Text;

            Settings currentSettings = Settings.Read();
            VFS.VFS currentSystem = new VFS.VFS(string.Empty, path, currentSettings.MainCounter, currentSettings.PackByte);

            Thread workingThread = null;
            workingThread = new Thread(new ThreadStart(() => {
                // Add files at first
                foreach (string file in this.lstFiles_)
                {
                    string[] segements = file.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    if (segements.Length > 0)
                    {
                        try
                        {
                            VFS.File currentFile = new VFS.File(segements[segements.Length - 1], currentSystem.RootDirectory);
                            currentFile.Bytes = System.IO.File.ReadAllBytes(file).ToList();
                            currentSystem.RootDirectory.Files.Add(currentFile);

                            this.addItemToStateBox(file + " wurde kopiert ...");
                        }
                        catch (Exception)
                        {
                            this.addItemToStateBox(file + " konnte nicht kopiert werden");
                        }
                    }
                }

                foreach (string dir in this.lstDirectories_)
                {
                    System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(dir);
                    VFS.Directory vDir = new VFS.Directory(info.Name);

                    Action<string> recurseDirs = null;
                    recurseDirs = new Action<string>((string lastDir) => {
                        System.IO.DirectoryInfo data = new System.IO.DirectoryInfo(lastDir);
                        string[] segements = data.FullName.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                        string nPath = string.Empty;
                        bool doAdding = false;
                        for (int i = 0; i <= segements.Length - 1; i++)
                        {
                            if (segements[i] == vDir.Name)
                            {
                                doAdding = true;
                                continue;
                            }

                            if (doAdding)
                                nPath += segements[i] + @"\";
                        }

                        vDir.AddPathes(new string[] { nPath  });
                        this.addItemToStateBox(nPath + " wurde erstellt ...");

                        VFS.Directory lastDirectory = VFS.Directory.CalculateLastNode(vDir, nPath);
                        foreach (var fi in data.GetFiles())
                        {
                            VFS.File currentFile = new VFS.File(fi.Name, lastDirectory);
                            try
                            {
                                currentFile.Bytes = System.IO.File.ReadAllBytes(fi.FullName).ToList();
                                this.addItemToStateBox(currentFile.Path + " wurde kopiert ...");
                            }
                            catch (Exception)
                            {
                                this.addItemToStateBox(currentFile.Path + "konnte nicht kopiert werden ...");
                            }
                            lastDirectory.Files.Add(currentFile);
                        }

                        foreach (var d in data.GetDirectories())
                            recurseDirs(d.FullName);
                    });

                    recurseDirs(dir);
                    currentSystem.RootDirectory.SubDirs.Add(vDir);                
                }
                currentSystem.Save();
                this.addItemToStateBox("Die Datei wurde gespeichert. Fertig!");
                if (this.pnlButtons.InvokeRequired)
                    this.pnlButtons.Invoke(new Action(() => { this.pnlButtons.Enabled = true; }));
                else
                    this.pnlButtons.Enabled = true;
                this.isReady = true;
                workingThread.Abort();
            }));
            workingThread.Start();
        }

        private void addItemToStateBox(string item)
        {
            try
            {
                if (this.lstState.InvokeRequired)
                    this.lstState.Invoke(new Action(() => { this.lstState.Items.Add(item); this.lstState.SelectedIndex = this.lstState.Items.Count - 1; }));
                else
                {
                    this.lstState.SelectedIndex = this.lstState.Items.Count - 1;
                    this.lstState.Items.Add(item);
                }
            }
            catch { }
        }

        private void btnFilesAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true })
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    // Check for file names that existing more than once
                    foreach (string fileName in ofd.FileNames)
                    {
                        if (!this.lstFiles_.Contains(fileName))
                            this.lstFiles_.Add(fileName);
                        else
                            MessageBox.Show(this, "Die Datei wurde schon hinzugefügt!", "Datei existiert bereits", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.refreshData();
                }
            }
        }

        private void btnFilesClear_Click(object sender, EventArgs e)
        {
            this.lstFiles_.Clear();
            this.refreshData();
        }

        private void btnFilesRemove_Click(object sender, EventArgs e)
        {
            if (this.lstFiles.SelectedIndex != -1)
            {
                this.lstFiles_.RemoveAt(this.lstFiles.SelectedIndex);
                this.refreshData();
            }
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnFilesRemove.Enabled = (this.lstFiles.SelectedIndex != -1 && this.lstFiles_.Count != 0);
        }

        private void btnDirAdd_Click(object sender, EventArgs e)
        {
            // txtDirPath
            string content = this.txtDirPath.Text;
            if (!string.IsNullOrEmpty(content) && System.IO.Directory.Exists(content))
            {
                if (!this.lstDirectories_.Contains(content))
                {
                    this.lstDirectories_.Add(content);
                    this.refreshData();
                }
                else
                    MessageBox.Show(this, "Der Ordner wurde schon hinzugefügt!", "Ordner existiert bereits", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (FolderBrowserDialog fld = new FolderBrowserDialog())
                {
                    if (fld.ShowDialog(this) == DialogResult.OK)
                    {
                        if (!this.lstDirectories_.Contains(fld.SelectedPath))
                        {
                            this.lstDirectories_.Add(fld.SelectedPath);
                            this.refreshData();
                        }
                        else
                            MessageBox.Show(this, "Der Ordner wurde schon hinzugefügt!", "Ordner existiert bereits", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            this.txtDirPath.Text = string.Empty;
        }

        private void lstDirectories_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDirRemove.Enabled = (this.lstDirectories.SelectedIndex != -1 && this.lstDirectories_.Count != 0);
        }

        private void btnDirClear_Click(object sender, EventArgs e)
        {
            this.lstDirectories_.Clear();
            this.refreshData();
        }

        private void btnDirRemove_Click(object sender, EventArgs e)
        {
            if (this.lstDirectories.SelectedIndex != -1)
            {
                this.lstDirectories_.RemoveAt(this.lstDirectories.SelectedIndex);
                this.refreshData();
            }
        }

        private void btnResearch_Click(object sender, EventArgs e)
        {
            // txtPath
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    if (!sfd.FileName.EndsWith(".ap"))
                        this.txtPath.Text = sfd.FileName + ".ap";
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            this.containsPath = !string.IsNullOrEmpty(this.txtPath.Text);

            this.btnOk.Enabled = this.containsPath && this.containsData;
        }
    }
}
