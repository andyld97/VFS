// ------------------------------------------------------------------------
// frmAbout.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      02.08.2016
// Last update on:  02.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Archiv.GUI
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            this.lblVersion.Text = "Version: " + Application.ProductVersion.ToString();
        }

        private void lnkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/andy123456789088/Archiv");
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Die Seite https://github.com/andy123456789088/Archiv konnte nicht aufgerufen werden!", "Seite konnte nicht geöffnet werden!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://seite.bplaced.net");
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Die Seite http://seite.bplaced.net konnte nicht aufgerufen werden!", "Seite konnte nicht geöffnet werden!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
