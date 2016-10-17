using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Setup
{
    // IMPORTANT INFORMATION FOR SETUP
    // -------------------------------
    // 1. How to use
    // 1. 1 Copy this project to another place
    // 1. 2 Create an "Archiv-File" and put this file into the ressources of this project (replace the old one) ==> File name is: "File.ap"
    // 1. 3 Set consts: EXE, CreateFileAssoc, AssocName and AssocExtension
    // Please notice that the content from "Archiv" does have 2 files. _PROGRAMM_.exe and _PROGRAMM_.ico.
    // In case of you don't want the program to create a file association, you just need _PROGRAMM_.exe for the link which will be created on the desktop
    // In case of you want the program to create a file association, you need both files, otherwise it can't work properly. 
    // This project contains a mainfest-file (app.mainfest) which is necessary, because this setup need ADMIN-RIGHTS!
    // Please note also that the messages of this setup will be in german, maybe later I translate them to English
    public partial class frmMain : Form
    {
        private bool ending = false, ready = false;
        private string path;
        public const string EXE = "Archiv";
        public const bool CreateFileAssoc = true;
        public const string AssocName = "Archiv";
        public const string AssocExtension = ".ap";

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtPath.Text = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), EXE);
            this.lblText.Text = "Bitte warten Sie währen Setup " + EXE + " auf Ihrem Computer installiert";
            this.label1.Text = "Willkommen zur Installation von " + EXE;
            this.label2.Text = "Dieses Programm installiert " + EXE + " auf Ihrem Computer";
            this.Text = EXE + " - Setup";        

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

        private void btnContinue_Click(object sender, EventArgs e)
        {
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

            System.ComponentModel.BackgroundWorker doWork = new BackgroundWorker();
            doWork.DoWork += doWork_DoWork;
            doWork.RunWorkerCompleted += doWork_RunWorkerCompleted;
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
                doWork.RunWorkerAsync();
            else
            {
                this.addToList("Fehler(0x02): Der angegebene Ordner funktioniert nicht!");
                this.addToList("Das Setup wurde beendet!");
                this.lblText.Text = "Das Setup ist leider fehlgeschlagen!";
                btnContinue.Enabled = true;
                ready = true;
            }
        }

        private void doWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnContinue.Enabled = true;
            this.lblText.Text = "Das Setup ist beendet!";
        }

        public void addToList(string t)
        {
            this.lstDetails.Invoke(new Action(() => lstDetails.Items.Add(t)));
        }

        public void setValue(int n)
        {
            this.prgProgress.Invoke(new Action(() => this.prgProgress.Value = n));
        }

        private void doWork_DoWork(object sender, DoWorkEventArgs e)
        {
            this.addToList("Kopiere Archiv ...");
            string curPath = System.IO.Path.Combine(new string[] { this.path, "File.ap" });
            System.IO.File.WriteAllBytes(curPath, Properties.Resources.File);
            this.setValue(10);
            this.addToList("Entpacke Archiv ...");

            ExtendendVFS currentVFS = new ExtendendVFS(curPath);
            currentVFS.OnReady += delegate {
                currentVFS.Extract(this.path);
                this.setValue(80);
                this.addToList("Lösche Archiv ...");
                System.IO.File.Delete(curPath);
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
            };
            currentVFS.Read(curPath);
            currentVFS.RecieveMessage += delegate (string message)
            {
                this.addToList(message);
            };

            while (!ready)
            { }
            
        }
    }
}
