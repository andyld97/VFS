using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Archiv.GUI
{
    public partial class InputDialog : Form
    {
        public string Result
        {
            get
            {
                return this.txtInput.Text;
            }
        }
        public InputDialog()
        {
            InitializeComponent();
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {

        }

        public DialogResult ShowDialog(IWin32Window owner, string text, string title, string defaultText = "")
        {
            this.lblText.Text = text;
            this.Text = title;
            this.txtInput.Text = defaultText;

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

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
