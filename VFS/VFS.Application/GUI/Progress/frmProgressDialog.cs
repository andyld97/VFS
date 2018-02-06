// ------------------------------------------------------------------------
// frmProgressDialog.cs written by Code A Software (http://www.code-a-software.net)
// All rights reserved
// Created on:      06.01.2017
// Last update on:  06.01.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using VFS.ExtendedVFS;
using V = VFS;

namespace VFS.Application.GUI.Progress
{
    public partial class frmProgressDialog : Form
    {
        private bool initSuccessedAlready = false;
        public VFS CVFS = null;

        public frmProgressDialog()
        {
            InitializeComponent();
           // this.workingInstance = workingInstance;
        }

        private void Progress_OnValueChanged(double value, double step, VFS handle)
        {
            if (!initSuccessedAlready || handle == null || handle != CVFS)
                return;

            if (value >= 0.0 && value <= 1.0 && step >= 0.0 || step <= 1.0)
            {
                this.prgPart.Invoke(new Action(() =>
                {
                    this.prgPart.Value = Convert.ToInt32(value * 100);
                }));
                this.prgMain.Invoke(new Action(() =>
                { 
                    this.prgMain.Value = Convert.ToInt32(step * 100);
                }));

                this.lblElapsedTime.Invoke(new Action(() => {
                    this.lblElapsedTime.Text = handle.VStopWatch.Elapsed.ToString();
                }));

                //if (value == 1 && step == 1)
                  //  Thread.Sleep(1000);
            }
        }

        private void frmProgressDialog_Load(object sender, EventArgs e)
        {
            V.Progress.OnValueChanged += Progress_OnValueChanged;
            V.Progress.LstProgress.Clear();
            this.prgPart.Value = this.prgMain.Value = 0;
            this.initSuccessedAlready = true;
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Tick += delegate
            {
               // if (this.initSuccessedAlready) // ToDo: Translate
                 //   this.lblElapsedTime.Text = "Vergangene Zeit: " + (workingInstance != null ? (workingInstance.CurrentStopWatch != null ? workingInstance.CurrentStopWatch.Elapsed.ToString() : "00:00:00") : "00:00:00");
            };
            tmr.Interval = 1;
            tmr.Start();
           // this.btnCancel.Enabled = (this.workingInstance is VFS.ExtendedVFS.ExtendedVFS && !(this.workingInstance as ExtendedVFS).SaveAfterChange);

            // Calculate center of owner
            int x = this.Owner.Location.X;
            int y = this.Owner.Location.Y;
            int w = this.Owner.Width;
            int h = this.Owner.Height;
            int w1 = this.Width;
            int h1 = this.Height;

            this.Location = new Point(x + (w / 2) - (w1 / 2), y + (h / 2) - (h1 / 2));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.btnCancel.Enabled = false;
            base.OnClosing(e);
            V.Progress.OnValueChanged -= Progress_OnValueChanged;
            this.initSuccessedAlready = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            V.Progress.OnValueChanged -= Progress_OnValueChanged;
            this.initSuccessedAlready = false;
            this.btnCancel.Enabled = false;
            this.Close();
        }
    }
}
