// ------------------------------------------------------------------------
// frmMain.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      05.02.2018
// Last update on:  05.02.2018
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using VFS;
using VFS.Net;
using IWshRuntimeLibrary;

namespace VFS.Setup
{
    // IMPORTANT INFORMATION FOR SETUP
    // -------------------------------
    // 1. How to use
    // 1. 1 Copy this project to another place
    // 1. 2 Create an "Archiv-File" and put this file into the ressources of this project (replace the old one) ==> File name is: "File.ap"
    // 1. 3 Set consts: ApplicationName, EXE, CreateFileAssoc, AssocName and AssocExtension
    // Please notice that the content from "Archiv" does have 2 files. _PROGRAMM_.exe and _PROGRAMM_.ico.
    // In case of you don't want the program to create a file association, you just need _PROGRAMM_.exe for the link which will be created on the desktop
    // In case of you want the program to create a file association, you need both files, otherwise it can't work properly. 
    // This project contains a mainfest-file (app.mainfest) which is necessary, because this setup need ADMIN-RIGHTS!
    // Please note also that the messages of this setup will be in german, maybe later I translate them to English
    public partial class frmMain : Form
    {
        private bool ending = false, ready = false;
        private string path;
        public const string EXE = "VFS Application";
        public const string ApplicationName = "VFS";
        public const bool CreateFileAssoc = true;
        public const string AssocName = "VFS";
        public const string AssocExtension = ".vhp";

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtPath.Text = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), ApplicationName);
            this.lblText.Text = "Bitte warten Sie währen Setup " + ApplicationName + " auf Ihrem Computer installiert";
            this.label1.Text = "Willkommen zur Installation von " + ApplicationName;
            this.label2.Text = "Dieses Programm installiert " + ApplicationName + " auf Ihrem Computer";
            this.Text = ApplicationName + " - Setup";        

            Icon current = Properties.Resources.Packer;

            // Convert icon to image
            Bitmap nIcon = new Bitmap(current.Width, current.Height);
            using (Graphics g = Graphics.FromImage(nIcon))
            {
                g.DrawIcon(current, new Rectangle(0, 0, current.Width, current.Height));
            }

            this.picIcon.BackgroundImage = nIcon;
            this.picIcon.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    txtPath.Text = ofd.SelectedPath + @"\" + EXE;
            }
        }

        private void lblHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try {
                System.Diagnostics.Process.Start("http://code-a-software.net");
            }
            catch { }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (ending || ready)
            {
                e.Cancel = false;
                return;
            }
            e.Cancel = true;
            DialogResult msg = MessageBox.Show(this, "Sind Sie sich sicher, dass Sie das Setup wirklich abbrechen wollen?", "Beenden?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == System.Windows.Forms.DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult msg = MessageBox.Show(this, "Sind Sie sich sicher, dass Sie das Setup wirklich abbrechen wollen?", "Beenden?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == System.Windows.Forms.DialogResult.Yes)
            {
                this.ending = true;
                this.Close();
            }
        }

        private async void btnContinue_Click(object sender, EventArgs e)
        {
            // ToDo: Handle fails (use result of read and extract methods of VFS)
            if (ready)
            {
                this.Close();
                return;
            }
            this.pnlStart.Visible = true;
            (sender as Button).Text = "Beenden";
            (sender as Button).Enabled = false;
            lblPath.Visible = false;
            txtPath.Visible = false;
            btnSearch.Visible = false;
            picIcon.Visible = false;

            path = txtPath.Text;
            if (!System.IO.Directory.Exists(path))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                catch { this.addToList("Fehler(0x01): Der Ordner konnte nicht erstellt werden!"); }
            }
            if (System.IO.Directory.Exists(path))
            {
                this.addToList("Kopiere Archiv ...");

                string fPath = System.IO.Path.Combine(new string[] { this.path, "File.ap" });
                Net.FilePath currentFile = new Net.FilePath(fPath);
                System.IO.File.WriteAllBytes(fPath, Properties.Resources.File);
                this.setValue(10);
                this.addToList("Entpacke Archiv ...");


                ExtendedVFS.ExtendedVFS currentVFS = new ExtendedVFS.ExtendedVFS(currentFile, Net.NetStorage.NETStorageProvider);
                await currentVFS.Read(currentFile);
                await currentVFS.Extract(new Net.DirectoryPath(this.path));
                this.setValue(80);
                this.addToList("Lösche Archiv ...");
                System.IO.File.Delete(currentFile.ToFullPath());
                this.setValue(90);

                this.addToList("Erstelle eine Verknüpfung auf dem Desktop ...");
                string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                WshShell shell = new WshShell();
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(System.IO.Path.Combine(new string[] { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), EXE + ".lnk" }));
                string path1 = System.IO.Path.Combine(new string[] { path, EXE + ".exe" });
                link.IconLocation = path1;
                link.TargetPath = path1;
                link.WorkingDirectory = path1;
                link.Save();

                if (CreateFileAssoc)
                {
                    this.addToList("Erstelle Dateiverknüpfug ...");
                    this.addToList((FileAssociation.SetFileAssociation(AssocName, AssocExtension, System.IO.Path.Combine(new string[] { path, EXE + ".ico" }), System.IO.Path.Combine(new string[] { path, EXE + ".exe" }))) ? "Verknüfung wurde erstellt" : "Verknüpfung konnte nicht erstellt werden (0x03)");
                }

                this.setValue(100);
                this.addToList("Fertig");
                this.ready = true;
                btnContinue.Enabled = true;
                this.lblText.Text = "Das Setup ist beendet!";
            }
            else
            {
                this.addToList("Fehler(0x02): Der angegebene Ordner funktioniert nicht!");
                this.addToList("Das Setup wurde beendet!");
                this.lblText.Text = "Das Setup ist leider fehlgeschlagen!";
                btnContinue.Enabled = true;
                ready = true;
            }
        }

        public void addToList(string t)
        {
            this.lstDetails.Invoke(new Action(() => lstDetails.Items.Add(t)));
        }

        public void setValue(int n)
        {
            this.prgProgress.Invoke(new Action(() => this.prgProgress.Value = n));
        }
    }
}
