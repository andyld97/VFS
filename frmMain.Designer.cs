namespace Archiv
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.MainMenue = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateiLadenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateienVerpackenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.httpseitedegdLinkshtmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.Side = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.EndProgramm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Entpack = new System.Windows.Forms.Button();
            this.EntpackSelectedFile = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FilesControl = new System.Windows.Forms.TabControl();
            this.Checker = new System.Windows.Forms.Timer(this.components);
            this.MainMenue.SuspendLayout();
            this.Side.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenue
            // 
            this.MainMenue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
            this.MainMenue.Location = new System.Drawing.Point(0, 0);
            this.MainMenue.Name = "MainMenue";
            this.MainMenue.Size = new System.Drawing.Size(793, 24);
            this.MainMenue.TabIndex = 0;
            this.MainMenue.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiLadenToolStripMenuItem,
            this.dateienVerpackenToolStripMenuItem,
            this.einstellungenToolStripMenuItem,
            this.überToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // dateiLadenToolStripMenuItem
            // 
            this.dateiLadenToolStripMenuItem.Image = global::Archiv.Properties.Resources.Go11;
            this.dateiLadenToolStripMenuItem.Name = "dateiLadenToolStripMenuItem";
            this.dateiLadenToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.dateiLadenToolStripMenuItem.Text = "Datei laden";
            this.dateiLadenToolStripMenuItem.Click += new System.EventHandler(this.dateiLadenToolStripMenuItem_Click);
            // 
            // dateienVerpackenToolStripMenuItem
            // 
            this.dateienVerpackenToolStripMenuItem.Image = global::Archiv.Properties.Resources.Go;
            this.dateienVerpackenToolStripMenuItem.Name = "dateienVerpackenToolStripMenuItem";
            this.dateienVerpackenToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.dateienVerpackenToolStripMenuItem.Text = "Dateien verpacken";
            this.dateienVerpackenToolStripMenuItem.Click += new System.EventHandler(this.dateienVerpackenToolStripMenuItem_Click);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Image = global::Archiv.Properties.Resources.Stift;
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.httpseitedegdLinkshtmToolStripMenuItem});
            this.überToolStripMenuItem.Image = global::Archiv.Properties.Resources.About;
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.überToolStripMenuItem.Text = "Über";
            // 
            // httpseitedegdLinkshtmToolStripMenuItem
            // 
            this.httpseitedegdLinkshtmToolStripMenuItem.Name = "httpseitedegdLinkshtmToolStripMenuItem";
            this.httpseitedegdLinkshtmToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.httpseitedegdLinkshtmToolStripMenuItem.Text = "Homepage";
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Image = global::Archiv.Properties.Resources.Abort;
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Informationen:";
            // 
            // Side
            // 
            this.Side.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Side.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Side.Controls.Add(this.lblName);
            this.Side.Controls.Add(this.lblSize);
            this.Side.Controls.Add(this.lblFile);
            this.Side.Controls.Add(this.EndProgramm);
            this.Side.Controls.Add(this.label1);
            this.Side.Controls.Add(this.progressBar1);
            this.Side.Controls.Add(this.Entpack);
            this.Side.Controls.Add(this.EntpackSelectedFile);
            this.Side.Controls.Add(this.label6);
            this.Side.Controls.Add(this.label5);
            this.Side.Controls.Add(this.label4);
            this.Side.Controls.Add(this.label3);
            this.Side.Controls.Add(this.label2);
            this.Side.Location = new System.Drawing.Point(584, 27);
            this.Side.Name = "Side";
            this.Side.Size = new System.Drawing.Size(209, 530);
            this.Side.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(132, 76);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 15;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(132, 63);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(0, 13);
            this.lblSize.TabIndex = 14;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(132, 50);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(0, 13);
            this.lblFile.TabIndex = 13;
            // 
            // EndProgramm
            // 
            this.EndProgramm.Location = new System.Drawing.Point(28, 265);
            this.EndProgramm.Name = "EndProgramm";
            this.EndProgramm.Size = new System.Drawing.Size(128, 57);
            this.EndProgramm.TabIndex = 12;
            this.EndProgramm.Text = "Beenden";
            this.EndProgramm.UseVisualStyleBackColor = true;
            this.EndProgramm.Click += new System.EventHandler(this.EndProgramm_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 480);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Fortschritt:";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(15, 496);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(173, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // Entpack
            // 
            this.Entpack.Location = new System.Drawing.Point(28, 202);
            this.Entpack.Name = "Entpack";
            this.Entpack.Size = new System.Drawing.Size(128, 57);
            this.Entpack.TabIndex = 10;
            this.Entpack.Text = "Alles entpacken";
            this.Entpack.UseVisualStyleBackColor = true;
            this.Entpack.Click += new System.EventHandler(this.Entpack_Click);
            // 
            // EntpackSelectedFile
            // 
            this.EntpackSelectedFile.Location = new System.Drawing.Point(28, 139);
            this.EntpackSelectedFile.Name = "EntpackSelectedFile";
            this.EntpackSelectedFile.Size = new System.Drawing.Size(128, 57);
            this.EntpackSelectedFile.TabIndex = 9;
            this.EntpackSelectedFile.Text = "Ausgewählte Datei(en) entpacken";
            this.EntpackSelectedFile.UseVisualStyleBackColor = true;
            this.EntpackSelectedFile.Click += new System.EventHandler(this.EntpackSelectedFile_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 30);
            this.label6.TabIndex = 8;
            this.label6.Text = "Aktionen:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Archiv:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Größe des Archivs:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Datei:";
            // 
            // FilesControl
            // 
            this.FilesControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilesControl.Location = new System.Drawing.Point(0, 25);
            this.FilesControl.Name = "FilesControl";
            this.FilesControl.SelectedIndex = 0;
            this.FilesControl.Size = new System.Drawing.Size(587, 532);
            this.FilesControl.TabIndex = 5;
            // 
            // Checker
            // 
            this.Checker.Enabled = true;
            this.Checker.Interval = 1;
            this.Checker.Tick += new System.EventHandler(this.Checker_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(793, 556);
            this.Controls.Add(this.FilesControl);
            this.Controls.Add(this.Side);
            this.Controls.Add(this.MainMenue);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenue;
            this.Name = "frmMain";
            this.Text = "Dateipacker";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MainMenue.ResumeLayout(false);
            this.MainMenue.PerformLayout();
            this.Side.ResumeLayout(false);
            this.Side.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenue;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateienVerpackenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel Side;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button Entpack;
        private System.Windows.Forms.Button EntpackSelectedFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button EndProgramm;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.Label lblSize;
        public System.Windows.Forms.Label lblFile;
        public System.Windows.Forms.TabControl FilesControl;
        private System.Windows.Forms.ToolStripMenuItem dateiLadenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem httpseitedegdLinkshtmToolStripMenuItem;
        private System.Windows.Forms.Timer Checker;

    }
}

