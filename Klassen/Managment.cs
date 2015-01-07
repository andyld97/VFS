using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Archiv.Klassen
{
    public class Managment
    {
        private TabControl tr = null;
        private frmMain Instance = null;
        private List<string> sr = new List<string>();

        public Managment(TabControl tr, frmMain Instance)
        {
            this.tr = tr;
            this.Instance = Instance;
            this.Instance.FilesControl.SelectedIndexChanged += FilesControl_SelectedIndexChanged;
        }

        public void Add(string Path)
        {
            Archiv a = new Archiv(this.Instance);
            var Items = a.GetFileNames(Path);
            
            //
            // Controls!
            //
            TabPage btr = new TabPage(a.GetFileName(Path));
            ListBox lst = new ListBox();
            sr.Add(Path);
            lst.DataSource = Items;
            lst.SelectionMode = SelectionMode.MultiExtended;
            lst.SelectedIndexChanged += lst_SelectedIndexChanged;
            btr.Controls.Add(lst);
            lst.Dock = DockStyle.Fill;
            this.tr.TabPages.Add(btr);
        }

        public string GetPaths()
        {
            return sr[this.tr.SelectedIndex];
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetItems();
            this.Instance.lblFile.Text = Convert.ToString(((ListBox)sender).SelectedItem);
        }

        private void FilesControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetItems();
        }

        private void SetItems()
        {
            string CurrentPath = sr[this.Instance.FilesControl.SelectedIndex];
            this.Instance.lblName.Text = new Archiv(this.Instance).GetFileName(CurrentPath).Replace(".ap", String.Empty);
            long Length = new System.IO.FileInfo(CurrentPath).Length;
            this.Instance.lblSize.Text = Calculate(Length).ToString() + " " + GetEinheit(Length).ToString();
        }

        public string[] SelectedItems()
        {
            foreach (Control s in tr.TabPages[tr.SelectedIndex].Controls)
            {
                if (s.GetType() == typeof(ListBox))
                {
                    List<string> f = new List<string>();
                    var Arr = ((ListBox)s).SelectedItems;
                    foreach (string d in Arr)
                    {
                        f.Add(d);
                    }
                    return f.ToArray();
                }
            }
            return null;
        }

        enum Byte
        {
            B,
            KB,
            MB,
            GB,
            TB
        }

        private Byte GetEinheit(long Digit)
        {
            if (Digit <= 1024)
                return Byte.B;
            else if (Digit <= Math.Pow(1024, 2))
                return Byte.KB;
            else if (Digit <= Math.Pow(1024, 3))
                return Byte.MB;
            else if (Digit <= Math.Pow(1024, 4))
                return Byte.GB;
            else if (Digit <= Math.Pow(1024, 5))
                return Byte.TB;
            else
                return Byte.B;
        }

        private double Calculate(long Digit)
        {
            int s = Array.IndexOf(Enum.GetValues(typeof(Byte)), GetEinheit(Digit));
            return Math.Round(Digit / Math.Pow(1024, s), 2);
        }
    }    
}
