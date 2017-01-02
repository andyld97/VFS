namespace VFS.GUI
{ 
    partial class frmPack
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPack));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnResearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.lstDirectories = new System.Windows.Forms.ListBox();
            this.btnFilesAdd = new System.Windows.Forms.Button();
            this.btnFilesRemove = new System.Windows.Forms.Button();
            this.btnFilesClear = new System.Windows.Forms.Button();
            this.btnDirClear = new System.Windows.Forms.Button();
            this.btnDirRemove = new System.Windows.Forms.Button();
            this.btnDirAdd = new System.Windows.Forms.Button();
            this.txtDirPath = new System.Windows.Forms.TextBox();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lstState = new System.Windows.Forms.ListBox();
            this.pnlButtons.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButtons.BackColor = System.Drawing.SystemColors.Control;
            this.pnlButtons.Controls.Add(this.btnResearch);
            this.pnlButtons.Controls.Add(this.txtPath);
            this.pnlButtons.Controls.Add(this.label1);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Location = new System.Drawing.Point(1, 324);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(622, 52);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(445, 16);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(526, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pfad:";
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(11, 18);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(258, 23);
            this.txtPath.TabIndex = 1;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // btnResearch
            // 
            this.btnResearch.Location = new System.Drawing.Point(275, 18);
            this.btnResearch.Name = "btnResearch";
            this.btnResearch.Size = new System.Drawing.Size(38, 23);
            this.btnResearch.TabIndex = 4;
            this.btnResearch.Text = "...";
            this.btnResearch.UseVisualStyleBackColor = true;
            this.btnResearch.Click += new System.EventHandler(this.btnResearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dateien:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ordner:";
            // 
            // lstFiles
            // 
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.ItemHeight = 15;
            this.lstFiles.Location = new System.Drawing.Point(12, 27);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(302, 229);
            this.lstFiles.TabIndex = 3;
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
            // 
            // lstDirectories
            // 
            this.lstDirectories.FormattingEnabled = true;
            this.lstDirectories.ItemHeight = 15;
            this.lstDirectories.Location = new System.Drawing.Point(324, 27);
            this.lstDirectories.Name = "lstDirectories";
            this.lstDirectories.Size = new System.Drawing.Size(288, 229);
            this.lstDirectories.TabIndex = 4;
            this.lstDirectories.SelectedIndexChanged += new System.EventHandler(this.lstDirectories_SelectedIndexChanged);
            // 
            // btnFilesAdd
            // 
            this.btnFilesAdd.Location = new System.Drawing.Point(12, 262);
            this.btnFilesAdd.Name = "btnFilesAdd";
            this.btnFilesAdd.Size = new System.Drawing.Size(139, 23);
            this.btnFilesAdd.TabIndex = 5;
            this.btnFilesAdd.Text = "Hinzufügen";
            this.btnFilesAdd.UseVisualStyleBackColor = true;
            this.btnFilesAdd.Click += new System.EventHandler(this.btnFilesAdd_Click);
            // 
            // btnFilesRemove
            // 
            this.btnFilesRemove.Enabled = false;
            this.btnFilesRemove.Location = new System.Drawing.Point(12, 291);
            this.btnFilesRemove.Name = "btnFilesRemove";
            this.btnFilesRemove.Size = new System.Drawing.Size(302, 23);
            this.btnFilesRemove.TabIndex = 6;
            this.btnFilesRemove.Text = "Ausgewähltes Element löschen";
            this.btnFilesRemove.UseVisualStyleBackColor = true;
            this.btnFilesRemove.Click += new System.EventHandler(this.btnFilesRemove_Click);
            // 
            // btnFilesClear
            // 
            this.btnFilesClear.Location = new System.Drawing.Point(157, 262);
            this.btnFilesClear.Name = "btnFilesClear";
            this.btnFilesClear.Size = new System.Drawing.Size(157, 23);
            this.btnFilesClear.TabIndex = 7;
            this.btnFilesClear.Text = "Leeren";
            this.btnFilesClear.UseVisualStyleBackColor = true;
            this.btnFilesClear.Click += new System.EventHandler(this.btnFilesClear_Click);
            // 
            // btnDirClear
            // 
            this.btnDirClear.Location = new System.Drawing.Point(324, 291);
            this.btnDirClear.Name = "btnDirClear";
            this.btnDirClear.Size = new System.Drawing.Size(90, 23);
            this.btnDirClear.TabIndex = 10;
            this.btnDirClear.Text = "Leeren";
            this.btnDirClear.UseVisualStyleBackColor = true;
            this.btnDirClear.Click += new System.EventHandler(this.btnDirClear_Click);
            // 
            // btnDirRemove
            // 
            this.btnDirRemove.Enabled = false;
            this.btnDirRemove.Location = new System.Drawing.Point(420, 291);
            this.btnDirRemove.Name = "btnDirRemove";
            this.btnDirRemove.Size = new System.Drawing.Size(192, 23);
            this.btnDirRemove.TabIndex = 9;
            this.btnDirRemove.Text = "Ausgewähltes Element löschen";
            this.btnDirRemove.UseVisualStyleBackColor = true;
            this.btnDirRemove.Click += new System.EventHandler(this.btnDirRemove_Click);
            // 
            // btnDirAdd
            // 
            this.btnDirAdd.Location = new System.Drawing.Point(527, 262);
            this.btnDirAdd.Name = "btnDirAdd";
            this.btnDirAdd.Size = new System.Drawing.Size(85, 23);
            this.btnDirAdd.TabIndex = 8;
            this.btnDirAdd.Text = "Hinzufügen";
            this.btnDirAdd.UseVisualStyleBackColor = true;
            this.btnDirAdd.Click += new System.EventHandler(this.btnDirAdd_Click);
            // 
            // txtDirPath
            // 
            this.txtDirPath.Location = new System.Drawing.Point(324, 262);
            this.txtDirPath.Name = "txtDirPath";
            this.txtDirPath.Size = new System.Drawing.Size(197, 23);
            this.txtDirPath.TabIndex = 5;
            // 
            // pnlProgress
            // 
            this.pnlProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProgress.Controls.Add(this.lstState);
            this.pnlProgress.Controls.Add(this.label4);
            this.pnlProgress.Location = new System.Drawing.Point(1, 0);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(622, 323);
            this.pnlProgress.TabIndex = 11;
            this.pnlProgress.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Fortschritt:";
            // 
            // lstState
            // 
            this.lstState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstState.FormattingEnabled = true;
            this.lstState.HorizontalScrollbar = true;
            this.lstState.ItemHeight = 15;
            this.lstState.Location = new System.Drawing.Point(14, 27);
            this.lstState.Name = "lstState";
            this.lstState.ScrollAlwaysVisible = true;
            this.lstState.Size = new System.Drawing.Size(596, 274);
            this.lstState.TabIndex = 1;
            // 
            // frmPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(623, 375);
            this.Controls.Add(this.pnlProgress);
            this.Controls.Add(this.txtDirPath);
            this.Controls.Add(this.btnDirClear);
            this.Controls.Add(this.btnDirRemove);
            this.Controls.Add(this.btnDirAdd);
            this.Controls.Add(this.btnFilesClear);
            this.Controls.Add(this.btnFilesRemove);
            this.Controls.Add(this.btnFilesAdd);
            this.Controls.Add(this.lstDirectories);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPack";
            this.Text = "Archiv erstellen";
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnResearch;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.ListBox lstDirectories;
        private System.Windows.Forms.Button btnFilesAdd;
        private System.Windows.Forms.Button btnFilesRemove;
        private System.Windows.Forms.Button btnFilesClear;
        private System.Windows.Forms.Button btnDirClear;
        private System.Windows.Forms.Button btnDirRemove;
        private System.Windows.Forms.Button btnDirAdd;
        private System.Windows.Forms.TextBox txtDirPath;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstState;
    }
}