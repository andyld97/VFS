using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VFS.Application.GUI
{
    public partial class frmNotepad : Form
    {
        private string path = string.Empty;
        private VFS currentVFS = null;

        public string Path
        {
            get
            {
                return this.path;
            }
        }

        public frmNotepad()
        {
            InitializeComponent();
        }

        private void frmNotepad_Load(object sender, EventArgs e)
        {

        }

        public void AddText(string text, string path, VFS currentVFS)
        {
            this.txtText.Text = text;
            this.path = path;
            this.currentVFS = currentVFS;

        }

        private void schriftartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                if (fd.ShowDialog(this) == DialogResult.OK)
                {
                    this.txtText.Font = fd.Font;
                }
            }
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (VFSOpenFileDialog ofd = new VFSOpenFileDialog())
            {
                if (ofd.ShowDialog(this.currentVFS.RootDirectory, "Bitte wählen Sie eine Datei aus", true, this) == DialogResult.OK)
                {
                    // ...
                }
            }
        }
    }
}
