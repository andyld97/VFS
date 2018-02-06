using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VFS.Interfaces;
using VFS.Application.GUI.Tab;

namespace VFS.Application.GUI
{
    public partial class VFSOpenFileDialog : Form
    {
        private IDirectory currentDir = null;
        private string title = string.Empty;
        private bool isFile = false;
        private GUI.Tab.ListView currentListView = null;

        public IDirectory CurrentDirectory
        {
            get
            {
                return this.currentDir;
            }
            private set
            {
                this.currentListView.ClearList();

                this.currentDir = value;

                // Add all elements of currentDir
                foreach (IDirectory currentSubDir in this.currentDir.GetSubDirectories())
                    this.currentListView.Add(new Element(currentSubDir.GetName(), Element.Type_.Directory, currentSubDir, null));

                foreach (IFile currentSubFile in this.currentDir.GetFiles())
                    this.currentListView.Add(new Element(currentSubFile.GetName(), Element.Type_.File, null, currentSubFile));

                this.txtCurrentPath.Text = this.currentDir.ToFullPath();
            }
        }

        public VFSOpenFileDialog()
        {
            InitializeComponent();
        }

        private void VFSOpenFileDialog_Load(object sender, EventArgs e)
        {

        }

        public DialogResult ShowDialog(IDirectory currentDir, string title, bool isFile, IWin32Window owner)
        {
            this.currentDir = currentDir;
            this.title = title;
            this.isFile = isFile;
            this.Text = title;

            this.txtCurrentPath.Text = this.currentDir.ToFullPath();
            this.currentListView = new Tab.ListView();

            this.CurrentDirectory = currentDir;

            this.pnlContainer.Controls.Add(this.currentListView);
            this.currentListView.Dock = DockStyle.Fill;
            this.currentListView.OnDoubleClickedElement += delegate (Element currentElement)
            {
                if (!currentElement.IsFile)
                    this.CurrentDirectory = currentElement.CurrentDir;
            };

            //this.currentListView.Refresh();

            return base.ShowDialog(owner);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void pnlBack_MouseHover(object sender, EventArgs e)
        {
            (sender as Control).Location = new Point((sender as Control).Location.X + 1, (sender as Control).Location.Y + 1);
            (sender as Control).BackColor = Color.Orange;
        }

        private void pnlBack_MouseLeave(object sender, EventArgs e)
        {
            (sender as Control).Location = new Point((sender as Control).Location.X - 1, (sender as Control).Location.Y - 1);
            (sender as Control).BackColor = Color.Transparent;
        }

        private void txtCurrentPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                // Try to navigate to the path.
                string path = this.currentDir.ToFullPath();
                IDirectory rootDir = this.currentDir;
                IDirectory currentNode = null;
                while (rootDir.GetParent() != null)
                    rootDir = rootDir.GetParent();

                currentNode = rootDir;

                string[] segments = this.txtCurrentPath.Text.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i <= segments.Length - 1; i++)
                {
                    if (currentNode.Contains(segments[i]))
                        currentNode = currentNode.GetSubDirectories()[currentNode.IndexOf(segments[i])];
                    else
                        return;
                }

                // Navigate to currentNode
                this.CurrentDirectory = currentNode;                                 
            }
        }

        private void pnlBack_Click(object sender, EventArgs e)
        {
            string[] segements = this.txtCurrentPath.Text.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

            if (segements.Length > 0)
            {
                string nPath = string.Empty;
                for (int i = 0; i <= segements.Length - 2; i++)
                    nPath += segements[i] + @"\";

                this.txtCurrentPath.Text = nPath;
                this.txtCurrentPath_KeyDown(null, new KeyEventArgs(Keys.Enter));               
            }
        }
    }
}
