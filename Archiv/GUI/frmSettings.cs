// ------------------------------------------------------------------------
// frmSettings.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      02.08.2016
// Last update on:  03.08.2016
// ------------------------------------------------------------------------
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
    public partial class frmSettings : Form
    {
        private string settingsPath = System.IO.Path.Combine(Application.StartupPath, "Settings.xml");
        private Settings settingsInstance = null;
        private Serialization.Serialization<Settings> ser = new Serialization.Serialization<Settings>();

        public frmSettings()
        {
            InitializeComponent();

            this.settingsInstance = new Settings();

            if (System.IO.File.Exists(this.settingsPath))
                this.settingsInstance = ser.Read(settingsPath, Serialization.Serialization<Settings>.Typ.Normal);
            else
                this.ser.Save(this.settingsPath, this.settingsInstance, Serialization.Serialization<Settings>.Typ.Normal);

            this.numMainCounter.Value = this.settingsInstance.MainCounter;
            this.numPackByte.Value = this.settingsInstance.PackByte;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            DialogResult sr = MessageBox.Show(this, "Um die Werte zu speichern, muss das Programm neugestartet werden? Achtung, alle nicht gespeicherten Änderungen gehen verloren. Möchten Sie das Programm neustarten?", "Neustart?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (sr == DialogResult.Yes)
            {
                this.settingsInstance.MainCounter = (int)this.numMainCounter.Value;
                this.settingsInstance.PackByte = (int)this.numPackByte.Value;

                this.ser.Save(this.settingsPath, this.settingsInstance, Serialization.Serialization<Settings>.Typ.Normal);
                Application.Restart();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult sr = MessageBox.Show(this, "Um die Werte zu speichern, muss das Programm neugestartet werden? Achtung, alle nicht gespeicherten Änderungen gehen verloren. Möchten Sie das Programm neustarten?", "Neustart?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (sr == DialogResult.Yes)
            {
                this.settingsInstance.MainCounter = 128;
                this.settingsInstance.PackByte = 45;

                this.numMainCounter.Value = this.settingsInstance.MainCounter;
                this.numPackByte.Value = this.settingsInstance.PackByte;


                this.ser.Save(this.settingsPath, this.settingsInstance, Serialization.Serialization<Settings>.Typ.Normal);
                Application.Restart();
            }
        }
    }
}
