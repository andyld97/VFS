using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Archiv
{
    public partial class frmPack : Form
    {
        private List<System.IO.FileInfo> flr = new List<System.IO.FileInfo>();
        private frmMain Instance = null;

        public frmPack(frmMain Instance)
        {
            InitializeComponent();
            this.Instance = Instance;
        }

        private void ClearList_Click(object sender, EventArgs e)
        {
            flr.Clear();
            Refresh();
        }

        new private void Refresh()
        {
            FileList.DataSource = flr.ToArray();
            FileList.DisplayMember = "FileName";
        }

        private void AddFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true })
            {
                if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (string s in ofd.FileNames)
                    {
                        flr.Add(new System.IO.FileInfo(s));
                    }
                }
            }
            Refresh();
        }

        private void DeleteSelectedFile_Click(object sender, EventArgs e)
        {
            if (FileList.SelectedIndex != -1)
            {
                // Delete Index.
                flr.RemoveAt(FileList.SelectedIndex);
                Refresh();
            }
        }

        private void Research_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fld = new FolderBrowserDialog())
            {
                if (fld.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    Foldername.Text = fld.SelectedPath;
                }
            }
        }

        private void Checker_Tick(object sender, EventArgs e)
        {
            xPack.Enabled = (FileList.Items.Count != 0 && Foldername.Text != string.Empty && System.IO.Directory.Exists(Foldername.Text) && Filename.Text != string.Empty);
        }

        private void xPack_Click(object sender, EventArgs e)
        {
            Archiv.Klassen.Archiv d = new Klassen.Archiv(this.Instance);
            d.PackEasy((from n in flr select n.FullName).ToArray<string>(), System.IO.Path.Combine(Foldername.Text, Filename.Text));
            this.Close();
        }
    }
}
