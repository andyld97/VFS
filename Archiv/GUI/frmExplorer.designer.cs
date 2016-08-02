namespace Archiv.GUI
{
    partial class frmExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExplorer));
            this.btnAddDirectory = new System.Windows.Forms.Button();
            this.pnlSideBar = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnExtractFiles = new System.Windows.Forms.Button();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnDeleteFile = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExtractDirs = new System.Windows.Forms.Button();
            this.btnDeleteDirectory = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.picGoForward = new System.Windows.Forms.PictureBox();
            this.picGoBack = new System.Windows.Forms.PictureBox();
            this.lblSites = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExtractAll = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSizeOfFile = new System.Windows.Forms.Label();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.lblOrdnerName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCtrl = new System.Windows.Forms.TabControl();
            this.tpOpenFiles = new System.Windows.Forms.TabPage();
            this.tbcFiles = new System.Windows.Forms.TabControl();
            this.tpNotepad = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTxt = new System.Windows.Forms.Label();
            this.txtNotepad = new System.Windows.Forms.TextBox();
            this.mnStrip = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateiLadenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateiVerpackenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCurrentPath = new System.Windows.Forms.TextBox();
            this.pnlSideBar.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGoForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGoBack)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tbCtrl.SuspendLayout();
            this.tpOpenFiles.SuspendLayout();
            this.tpNotepad.SuspendLayout();
            this.mnStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddDirectory
            // 
            this.btnAddDirectory.Location = new System.Drawing.Point(6, 22);
            this.btnAddDirectory.Name = "btnAddDirectory";
            this.btnAddDirectory.Size = new System.Drawing.Size(89, 24);
            this.btnAddDirectory.TabIndex = 2;
            this.btnAddDirectory.Text = "Hinzufügen";
            this.btnAddDirectory.UseVisualStyleBackColor = true;
            this.btnAddDirectory.Click += new System.EventHandler(this.btnCreateDir_Click);
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSideBar.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSideBar.Controls.Add(this.groupBox5);
            this.pnlSideBar.Controls.Add(this.groupBox4);
            this.pnlSideBar.Controls.Add(this.groupBox3);
            this.pnlSideBar.Controls.Add(this.groupBox1);
            this.pnlSideBar.Controls.Add(this.label6);
            this.pnlSideBar.Controls.Add(this.lblSizeOfFile);
            this.pnlSideBar.Controls.Add(this.lblSelectedFile);
            this.pnlSideBar.Controls.Add(this.lblOrdnerName);
            this.pnlSideBar.Controls.Add(this.label2);
            this.pnlSideBar.Location = new System.Drawing.Point(633, 25);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(207, 505);
            this.pnlSideBar.TabIndex = 3;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnExtractFiles);
            this.groupBox5.Controls.Add(this.btnAddFiles);
            this.groupBox5.Controls.Add(this.btnDeleteFile);
            this.groupBox5.Controls.Add(this.btnOpenFile);
            this.groupBox5.Location = new System.Drawing.Point(4, 254);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(191, 107);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Dateien";
            // 
            // btnExtractFiles
            // 
            this.btnExtractFiles.Location = new System.Drawing.Point(6, 82);
            this.btnExtractFiles.Name = "btnExtractFiles";
            this.btnExtractFiles.Size = new System.Drawing.Size(179, 24);
            this.btnExtractFiles.TabIndex = 10;
            this.btnExtractFiles.Text = "Ausgewählte Datei entpacken";
            this.btnExtractFiles.UseVisualStyleBackColor = true;
            this.btnExtractFiles.Click += new System.EventHandler(this.btnExtractFiles_Click);
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Location = new System.Drawing.Point(6, 22);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(179, 24);
            this.btnAddFiles.TabIndex = 9;
            this.btnAddFiles.Text = "Hinzufügen";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Location = new System.Drawing.Point(101, 52);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(84, 24);
            this.btnDeleteFile.TabIndex = 8;
            this.btnDeleteFile.Text = "Löschen";
            this.btnDeleteFile.UseVisualStyleBackColor = true;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(6, 52);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(89, 24);
            this.btnOpenFile.TabIndex = 7;
            this.btnOpenFile.Text = "Öffnen";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExtractDirs);
            this.groupBox4.Controls.Add(this.btnDeleteDirectory);
            this.groupBox4.Controls.Add(this.btnAddDirectory);
            this.groupBox4.Location = new System.Drawing.Point(4, 157);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(191, 97);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ordner";
            // 
            // btnExtractDirs
            // 
            this.btnExtractDirs.Location = new System.Drawing.Point(6, 52);
            this.btnExtractDirs.Name = "btnExtractDirs";
            this.btnExtractDirs.Size = new System.Drawing.Size(177, 39);
            this.btnExtractDirs.TabIndex = 10;
            this.btnExtractDirs.Text = "Ausgewählten Ordner entpacken";
            this.btnExtractDirs.UseVisualStyleBackColor = true;
            this.btnExtractDirs.Click += new System.EventHandler(this.btnExtractDirs_Click);
            // 
            // btnDeleteDirectory
            // 
            this.btnDeleteDirectory.Location = new System.Drawing.Point(101, 22);
            this.btnDeleteDirectory.Name = "btnDeleteDirectory";
            this.btnDeleteDirectory.Size = new System.Drawing.Size(82, 24);
            this.btnDeleteDirectory.TabIndex = 10;
            this.btnDeleteDirectory.Text = "Löschen";
            this.btnDeleteDirectory.UseVisualStyleBackColor = true;
            this.btnDeleteDirectory.Click += new System.EventHandler(this.btnDeleteDirectory_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGoBack);
            this.groupBox3.Controls.Add(this.picGoForward);
            this.groupBox3.Controls.Add(this.picGoBack);
            this.groupBox3.Controls.Add(this.lblSites);
            this.groupBox3.Location = new System.Drawing.Point(4, 367);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 117);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Navigation";
            // 
            // btnGoBack
            // 
            this.btnGoBack.Location = new System.Drawing.Point(7, 84);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(176, 23);
            this.btnGoBack.TabIndex = 3;
            this.btnGoBack.Text = "Zurück";
            this.btnGoBack.UseVisualStyleBackColor = true;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // picGoForward
            // 
            this.picGoForward.BackgroundImage = global::Archiv.Properties.Resources.next;
            this.picGoForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picGoForward.Location = new System.Drawing.Point(47, 22);
            this.picGoForward.Name = "picGoForward";
            this.picGoForward.Size = new System.Drawing.Size(32, 32);
            this.picGoForward.TabIndex = 2;
            this.picGoForward.TabStop = false;
            // 
            // picGoBack
            // 
            this.picGoBack.BackgroundImage = global::Archiv.Properties.Resources.LeftBlue;
            this.picGoBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picGoBack.Location = new System.Drawing.Point(9, 22);
            this.picGoBack.Name = "picGoBack";
            this.picGoBack.Size = new System.Drawing.Size(32, 32);
            this.picGoBack.TabIndex = 1;
            this.picGoBack.TabStop = false;
            // 
            // lblSites
            // 
            this.lblSites.AutoSize = true;
            this.lblSites.Location = new System.Drawing.Point(6, 57);
            this.lblSites.Name = "lblSites";
            this.lblSites.Size = new System.Drawing.Size(73, 15);
            this.lblSites.TabIndex = 0;
            this.lblSites.Text = "Seite 1 von 1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExtractAll);
            this.groupBox1.Location = new System.Drawing.Point(4, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 59);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Archiv";
            // 
            // btnExtractAll
            // 
            this.btnExtractAll.Location = new System.Drawing.Point(9, 22);
            this.btnExtractAll.Name = "btnExtractAll";
            this.btnExtractAll.Size = new System.Drawing.Size(176, 25);
            this.btnExtractAll.TabIndex = 9;
            this.btnExtractAll.Text = "Alles entpacken";
            this.btnExtractAll.UseVisualStyleBackColor = true;
            this.btnExtractAll.Click += new System.EventHandler(this.btnExtractAll_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(117, 484);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Version: 1.0.0.1";
            // 
            // lblSizeOfFile
            // 
            this.lblSizeOfFile.AutoSize = true;
            this.lblSizeOfFile.Location = new System.Drawing.Point(7, 74);
            this.lblSizeOfFile.Name = "lblSizeOfFile";
            this.lblSizeOfFile.Size = new System.Drawing.Size(92, 15);
            this.lblSizeOfFile.TabIndex = 6;
            this.lblSizeOfFile.Text = "Größe der Datei:";
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Location = new System.Drawing.Point(7, 59);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(111, 15);
            this.lblSelectedFile.TabIndex = 5;
            this.lblSelectedFile.Text = "Ausgewählte Datei: ";
            // 
            // lblOrdnerName
            // 
            this.lblOrdnerName.AutoSize = true;
            this.lblOrdnerName.Location = new System.Drawing.Point(7, 44);
            this.lblOrdnerName.Name = "lblOrdnerName";
            this.lblOrdnerName.Size = new System.Drawing.Size(47, 15);
            this.lblOrdnerName.TabIndex = 4;
            this.lblOrdnerName.Text = "Ordner:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe WP", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 40);
            this.label2.TabIndex = 0;
            this.label2.Text = "Info";
            // 
            // tbCtrl
            // 
            this.tbCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCtrl.Controls.Add(this.tpOpenFiles);
            this.tbCtrl.Controls.Add(this.tpNotepad);
            this.tbCtrl.Location = new System.Drawing.Point(1, 25);
            this.tbCtrl.Name = "tbCtrl";
            this.tbCtrl.SelectedIndex = 0;
            this.tbCtrl.Size = new System.Drawing.Size(626, 475);
            this.tbCtrl.TabIndex = 4;
            // 
            // tpOpenFiles
            // 
            this.tpOpenFiles.Controls.Add(this.tbcFiles);
            this.tpOpenFiles.Location = new System.Drawing.Point(4, 24);
            this.tpOpenFiles.Name = "tpOpenFiles";
            this.tpOpenFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tpOpenFiles.Size = new System.Drawing.Size(618, 447);
            this.tpOpenFiles.TabIndex = 0;
            this.tpOpenFiles.Text = "Geöffnete Dateien";
            this.tpOpenFiles.UseVisualStyleBackColor = true;
            // 
            // tbcFiles
            // 
            this.tbcFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcFiles.Location = new System.Drawing.Point(3, 3);
            this.tbcFiles.Name = "tbcFiles";
            this.tbcFiles.SelectedIndex = 0;
            this.tbcFiles.Size = new System.Drawing.Size(612, 441);
            this.tbcFiles.TabIndex = 5;
            // 
            // tpNotepad
            // 
            this.tpNotepad.Controls.Add(this.btnSave);
            this.tpNotepad.Controls.Add(this.lblTxt);
            this.tpNotepad.Controls.Add(this.txtNotepad);
            this.tpNotepad.Location = new System.Drawing.Point(4, 24);
            this.tpNotepad.Name = "tpNotepad";
            this.tpNotepad.Padding = new System.Windows.Forms.Padding(3);
            this.tpNotepad.Size = new System.Drawing.Size(618, 447);
            this.tpNotepad.TabIndex = 1;
            this.tpNotepad.Text = "Editor";
            this.tpNotepad.UseVisualStyleBackColor = true;
            this.tpNotepad.Click += new System.EventHandler(this.tpNotepad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(3, 413);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(615, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTxt
            // 
            this.lblTxt.AutoSize = true;
            this.lblTxt.Location = new System.Drawing.Point(6, 2);
            this.lblTxt.Name = "lblTxt";
            this.lblTxt.Size = new System.Drawing.Size(37, 15);
            this.lblTxt.TabIndex = 1;
            this.lblTxt.Text = "Datei:";
            // 
            // txtNotepad
            // 
            this.txtNotepad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotepad.Location = new System.Drawing.Point(0, 20);
            this.txtNotepad.Multiline = true;
            this.txtNotepad.Name = "txtNotepad";
            this.txtNotepad.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNotepad.Size = new System.Drawing.Size(618, 387);
            this.txtNotepad.TabIndex = 0;
            this.txtNotepad.TextChanged += new System.EventHandler(this.txtNotepad_TextChanged);
            // 
            // mnStrip
            // 
            this.mnStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
            this.mnStrip.Location = new System.Drawing.Point(0, 0);
            this.mnStrip.Name = "mnStrip";
            this.mnStrip.Size = new System.Drawing.Size(840, 24);
            this.mnStrip.TabIndex = 5;
            this.mnStrip.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiLadenToolStripMenuItem,
            this.dateiVerpackenToolStripMenuItem,
            this.überToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // dateiLadenToolStripMenuItem
            // 
            this.dateiLadenToolStripMenuItem.Image = global::Archiv.Properties.Resources.DurchsuchenIcon;
            this.dateiLadenToolStripMenuItem.Name = "dateiLadenToolStripMenuItem";
            this.dateiLadenToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.dateiLadenToolStripMenuItem.Text = "Datei laden";
            this.dateiLadenToolStripMenuItem.Click += new System.EventHandler(this.dateiLadenToolStripMenuItem_Click);
            // 
            // dateiVerpackenToolStripMenuItem
            // 
            this.dateiVerpackenToolStripMenuItem.Image = global::Archiv.Properties.Resources.Add;
            this.dateiVerpackenToolStripMenuItem.Name = "dateiVerpackenToolStripMenuItem";
            this.dateiVerpackenToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.dateiVerpackenToolStripMenuItem.Text = "Datei verpacken";
            this.dateiVerpackenToolStripMenuItem.Click += new System.EventHandler(this.dateiVerpackenToolStripMenuItem_Click);
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Image = global::Archiv.Properties.Resources.About;
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.überToolStripMenuItem.Text = "Über";
            this.überToolStripMenuItem.Click += new System.EventHandler(this.überToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Image = global::Archiv.Properties.Resources.CloseIcon;
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // txtCurrentPath
            // 
            this.txtCurrentPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentPath.BackColor = System.Drawing.Color.White;
            this.txtCurrentPath.Location = new System.Drawing.Point(1, 506);
            this.txtCurrentPath.Name = "txtCurrentPath";
            this.txtCurrentPath.ReadOnly = true;
            this.txtCurrentPath.Size = new System.Drawing.Size(622, 23);
            this.txtCurrentPath.TabIndex = 6;
            this.txtCurrentPath.Text = "\\";
            // 
            // frmExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(840, 531);
            this.Controls.Add(this.pnlSideBar);
            this.Controls.Add(this.txtCurrentPath);
            this.Controls.Add(this.tbCtrl);
            this.Controls.Add(this.mnStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnStrip;
            this.Name = "frmExplorer";
            this.Text = "Archiv";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlSideBar.ResumeLayout(false);
            this.pnlSideBar.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGoForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGoBack)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tbCtrl.ResumeLayout(false);
            this.tpOpenFiles.ResumeLayout(false);
            this.tpNotepad.ResumeLayout(false);
            this.tpNotepad.PerformLayout();
            this.mnStrip.ResumeLayout(false);
            this.mnStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddDirectory;
        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrdnerName;
        private System.Windows.Forms.Label lblSizeOfFile;
        private System.Windows.Forms.Label lblSelectedFile;
        private System.Windows.Forms.Button btnExtractAll;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TabControl tbCtrl;
        private System.Windows.Forms.TabPage tpOpenFiles;
        private System.Windows.Forms.TabControl tbcFiles;
        private System.Windows.Forms.TabPage tpNotepad;
        private System.Windows.Forms.MenuStrip mnStrip;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateiVerpackenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateiLadenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCurrentPath;
        private System.Windows.Forms.Label lblSites;
        private System.Windows.Forms.PictureBox picGoBack;
        private System.Windows.Forms.PictureBox picGoForward;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.TextBox txtNotepad;
        private System.Windows.Forms.Label lblTxt;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnDeleteFile;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnDeleteDirectory;
        private System.Windows.Forms.Button btnExtractFiles;
        private System.Windows.Forms.Button btnExtractDirs;
    }
}

