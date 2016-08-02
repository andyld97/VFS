// ------------------------------------------------------------------------
// FilePage.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  02.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Archiv.GUI.Tab.Dialog.AddFolderDirectoryDialog
{
    public class FilePage : TabPage
    {
        private TextBox txtHeader = null;
        private TextBox txtBody = null;
        private Label lblHeader = null;
        private Label lblBody = null;
        private const int difference = 0;

        public string FileName
        {
            get
            {
                return this.txtHeader.Text;
            }
        }

        public string FileContent
        {
            get
            {
                return this.txtBody.Text;
            }
        }

        public FilePage(string name) : base (name)
        {
            
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            this.InitalizeComponent();
        }

        public void InitalizeComponent()
        {
            this.txtHeader = new TextBox();
            this.txtBody = new TextBox();
            this.lblHeader = new Label();
            this.lblBody = new Label();

            //
            // lblHeader
            //
            this.lblHeader.Location = new Point(0, 0);
            this.lblHeader.AutoSize = true;
            this.lblHeader.Text = "Dateiname mit Endung (Beispiel.txt):";
            //
            // txtHeader
            //
            this.txtHeader.Location = new Point(3, 16 + difference);
            this.txtHeader.Width = this.Width - 3;
            this.txtHeader.TextChanged += TxtHeader_TextChanged;
            //
            // lblBody
            //
            this.lblBody.Location = new Point(0, this.txtHeader.Bottom + 5 + difference);
            this.lblBody.AutoSize = true;
            this.lblBody.Text = "Inhalt der Datei:";
            //
            // txtBody
            //
            this.txtBody.Location = new Point(3, this.txtHeader.Bottom + 20 + difference);
            this.txtBody.Multiline = true;
            this.txtBody.Width = this.Width - 3;
            this.txtBody.Height = 150;
            this.txtBody.ScrollBars = ScrollBars.Both;

            this.Controls.AddRange(new Control[] { this.txtHeader, this.txtBody, this.lblBody, this.lblHeader });
        }

        private void TxtHeader_TextChanged(object sender, EventArgs e)
        {
            this.Text = (sender as TextBox).Text;
        }
    }
}
