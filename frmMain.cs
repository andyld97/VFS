using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Archiv.Klassen;
using Microsoft.Win32;

namespace Archiv
{
    public partial class frmMain : Form
    {
        private Archiv.Klassen.Archiv Ar;
        private Archiv.Klassen.Managment Mgm;
        private frmPack PackForm = null;
        public Error err = null;

        public frmMain()
        {
            InitializeComponent();
            Ar = new Klassen.Archiv(this);
            Mgm = new Managment(this.FilesControl, this);
            err = new Error();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            //updateController Objekt initialisieren
            updateSystemDotNet.updateController updController = new updateSystemDotNet.updateController();
            updController.updateUrl = "http://seite.de.gd/Package";
            updController.projectId = "1e16e2f9-ee53-4e72-8428-546160b0e71d";
            updController.publicKey = "<RSAKeyValue><Modulus>mCN32FIWJp87CL0MvltHLPATZU7daH31kmDpXrqESGDKgSb96mzZbr74+0lMWCzvWU09Gh43FCiGpcITvzGuvLLuoBXWtIMI8ayWF7wAyH3t0I5g7WkOf0f+zPeO/jM634zxBa/3oq8KvsN/7iSUl06b/dVJehOxzgbNho9EKKmt8vJ5zb13zF8nM2/5E31AzIfrpUboWZ0Bsk7KKqlDPf14dSlRdkqDpI91rgroQ0Y1hl8OqDlQkRhSwZ4ds+MfxbxGQer3+d5MselfmX8k/PXyrf8i+cLx/hv89pHOspSuCSoE1Ha3GISiPBxHhNjYILAwLdC3XsR4lOLTnWPPlTkJo4lSV2EFMJ66RezRBXnfcMp2IFALoiro8LCVYsf4/U/+Kk0eJFEg9NbW0mP/OEry4eVw9AAs6F4zjuajkwPPHZx+8YepDA2mR+knIqQ6EEm0CqdfjQYxlwAPXKlr0jhV31zFUp68ci303AC6KNkrWcohYcettdpamdT+Zafc55bshMUtEZ43ih0tdAoEnSzdMgq5IRAq4VpXcAr9Bjo/2hLcMoY9BCYmV5ZL9dWN59UUJQ8Awht84VfUJ0qCSpahGmNTh6a5rUPbsMeOuynzTI5bjfcPjCkc8lLJjQ3HrXB70E869RG2mNP6uYjIhizcKvTpmwgJ5W+i4I464z8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            //Releasefilter setzen, per Default wird nur nach finalen Versionen gesucht:
            updController.releaseFilter.checkForFinal = true;
            updController.releaseFilter.checkForBeta = true;
            updController.releaseFilter.checkForAlpha = false;
            updController.restartApplication = true;
            updController.retrieveHostVersion = true; 
            updController.updateInteractive();     

            for (int i = 0; i <= Environment.GetCommandLineArgs().Length - 1; i++)
            {
                if (i != 0)
                {
                    // The first argument is the programm.
                    string Path = Environment.GetCommandLineArgs()[i];
                    Mgm.Add(Path);
                }
            }
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EndProgramm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateienVerpackenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PackForm == null || PackForm.IsDisposed)
            {
                PackForm = new frmPack(this);
                PackForm.Show();
            }
            else
            {
                PackForm.BringToFront();
            }
        }

        private void Entpack_Click(object sender, EventArgs e)
        {
            string Path = Mgm.GetPaths();
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Ar.UnpackFiles(Path, dlg.SelectedPath);
                }
            }
        }

        private void dateiLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true })
            {
                if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (string Item in ofd.FileNames)
                    {
                        Mgm.Add(Item);
                    }
                }
            }
        }

        private void EntpackSelectedFile_Click(object sender, EventArgs e)
        {
            // Extract files to temp folder, then copy selected files from temp to selected folder!
            string FilePath = System.IO.Path.Combine(Application.StartupPath, "Temp");
            string nFilePath = string.Empty;
            if (!System.IO.Directory.Exists(FilePath))
                System.IO.Directory.CreateDirectory(FilePath); // Program cannot run on a read only disk!

            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    nFilePath = dlg.SelectedPath;
                }
            }

            Ar.UnpackFiles(Mgm.GetPaths(), FilePath); // Copy to temp!

            string[] Files = Mgm.SelectedItems();

            // Check whether the file / path name is valid.

            // Create FullFileName.
            List<System.IO.FileInfo> fiList = new List<System.IO.FileInfo>();
            foreach (string es in Files)
            {
                // Check, whether the path is valid.
                try { 
                fiList.Add(new System.IO.FileInfo(System.IO.Path.Combine(FilePath, es)));
                }
                catch (Exception ss)
                {
                    this.err.AddError("The file couldn't read: " + ss.Message);
                }
            }

            foreach (System.IO.FileInfo fi in fiList)
            {
                try
                {
                    fi.CopyTo(System.IO.Path.Combine(nFilePath, fi.Name)); // Copy to real destionation.
                }
                catch (Exception ss)
                {
                    this.err.AddError("The file couldn't be written: " + ss.Message);
                }
            }

            // Delete Tempfolder!
            try
            {
                System.IO.Directory.Delete(FilePath, true);
            }
            catch (Exception ed) {
                err.AddError("The temp folder couldn't delete: " + ed.Message); }
        }

        private void Checker_Tick(object sender, EventArgs e)
        {
            // EntpackSelectedFile
            // EntpackFile

            Entpack.Enabled = FilesControl.SelectedIndex != -1;
            EntpackSelectedFile.Enabled = FilesControl.SelectedIndex != -1 && Mgm.SelectedItems().Length > 0;
        }
    }
}