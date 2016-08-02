namespace Archiv.GUI.Tab.Dialog.AddFolderDirectoryDialog
{
    partial class AddFolderDirectoryDialog
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
            this.tbDirFile = new System.Windows.Forms.TabControl();
            this.tbFiles = new System.Windows.Forms.TabPage();
            this.chkOverride = new System.Windows.Forms.CheckBox();
            this.btnDeleteFile = new System.Windows.Forms.Button();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbcFiles = new System.Windows.Forms.TabControl();
            this.lblFilesSelected = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnResearch = new System.Windows.Forms.Button();
            this.tbDirectorys = new System.Windows.Forms.TabPage();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddDirectory = new System.Windows.Forms.Button();
            this.lstDirectories = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDeleteSelctedDirectory = new System.Windows.Forms.Button();
            this.btnClearDirectorys = new System.Windows.Forms.Button();
            this.tbDirFile.SuspendLayout();
            this.tbFiles.SuspendLayout();
            this.tbDirectorys.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbDirFile
            // 
            this.tbDirFile.Controls.Add(this.tbFiles);
            this.tbDirFile.Controls.Add(this.tbDirectorys);
            this.tbDirFile.Location = new System.Drawing.Point(0, 0);
            this.tbDirFile.Name = "tbDirFile";
            this.tbDirFile.SelectedIndex = 0;
            this.tbDirFile.Size = new System.Drawing.Size(654, 384);
            this.tbDirFile.TabIndex = 0;
            // 
            // tbFiles
            // 
            this.tbFiles.Controls.Add(this.chkOverride);
            this.tbFiles.Controls.Add(this.btnDeleteFile);
            this.tbFiles.Controls.Add(this.btnAddFile);
            this.tbFiles.Controls.Add(this.label3);
            this.tbFiles.Controls.Add(this.tbcFiles);
            this.tbFiles.Controls.Add(this.lblFilesSelected);
            this.tbFiles.Controls.Add(this.label1);
            this.tbFiles.Controls.Add(this.btnResearch);
            this.tbFiles.Location = new System.Drawing.Point(4, 24);
            this.tbFiles.Name = "tbFiles";
            this.tbFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tbFiles.Size = new System.Drawing.Size(646, 356);
            this.tbFiles.TabIndex = 0;
            this.tbFiles.Text = "Dateien";
            this.tbFiles.UseVisualStyleBackColor = true;
            // 
            // chkOverride
            // 
            this.chkOverride.AutoSize = true;
            this.chkOverride.Location = new System.Drawing.Point(18, 327);
            this.chkOverride.Name = "chkOverride";
            this.chkOverride.Size = new System.Drawing.Size(210, 19);
            this.chkOverride.TabIndex = 9;
            this.chkOverride.Text = "Vorhandene Dateien überschreiben";
            this.chkOverride.UseVisualStyleBackColor = true;
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Location = new System.Drawing.Point(560, 327);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(80, 23);
            this.btnDeleteFile.TabIndex = 6;
            this.btnDeleteFile.Text = "Löschen";
            this.btnDeleteFile.UseVisualStyleBackColor = true;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(474, 327);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(80, 23);
            this.btnAddFile.TabIndex = 5;
            this.btnAddFile.Text = "Hinzufügen";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Eigene Dateien erstellen:";
            // 
            // tbcFiles
            // 
            this.tbcFiles.Location = new System.Drawing.Point(18, 80);
            this.tbcFiles.Name = "tbcFiles";
            this.tbcFiles.SelectedIndex = 0;
            this.tbcFiles.Size = new System.Drawing.Size(622, 241);
            this.tbcFiles.TabIndex = 3;
            // 
            // lblFilesSelected
            // 
            this.lblFilesSelected.AutoSize = true;
            this.lblFilesSelected.Location = new System.Drawing.Point(99, 34);
            this.lblFilesSelected.Name = "lblFilesSelected";
            this.lblFilesSelected.Size = new System.Drawing.Size(119, 15);
            this.lblFilesSelected.TabIndex = 2;
            this.lblFilesSelected.Text = "0 Dateien ausgewählt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Durchsuchen";
            // 
            // btnResearch
            // 
            this.btnResearch.Location = new System.Drawing.Point(18, 30);
            this.btnResearch.Name = "btnResearch";
            this.btnResearch.Size = new System.Drawing.Size(75, 23);
            this.btnResearch.TabIndex = 0;
            this.btnResearch.Text = "...";
            this.btnResearch.UseVisualStyleBackColor = true;
            this.btnResearch.Click += new System.EventHandler(this.btnResearch_Click);
            // 
            // tbDirectorys
            // 
            this.tbDirectorys.Controls.Add(this.btnClearDirectorys);
            this.tbDirectorys.Controls.Add(this.btnDeleteSelctedDirectory);
            this.tbDirectorys.Controls.Add(this.label4);
            this.tbDirectorys.Controls.Add(this.lstDirectories);
            this.tbDirectorys.Controls.Add(this.btnAddDirectory);
            this.tbDirectorys.Location = new System.Drawing.Point(4, 24);
            this.tbDirectorys.Name = "tbDirectorys";
            this.tbDirectorys.Padding = new System.Windows.Forms.Padding(3);
            this.tbDirectorys.Size = new System.Drawing.Size(646, 356);
            this.tbDirectorys.TabIndex = 1;
            this.tbDirectorys.Text = "Ordner";
            this.tbDirectorys.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(498, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(579, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Abbrechen#";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddDirectory
            // 
            this.btnAddDirectory.Location = new System.Drawing.Point(9, 271);
            this.btnAddDirectory.Name = "btnAddDirectory";
            this.btnAddDirectory.Size = new System.Drawing.Size(83, 23);
            this.btnAddDirectory.TabIndex = 2;
            this.btnAddDirectory.Text = "Hinzufügen";
            this.btnAddDirectory.UseVisualStyleBackColor = true;
            this.btnAddDirectory.Click += new System.EventHandler(this.btnSearchDirecotries_Click);
            // 
            // lstDirectories
            // 
            this.lstDirectories.FormattingEnabled = true;
            this.lstDirectories.ItemHeight = 15;
            this.lstDirectories.Location = new System.Drawing.Point(9, 21);
            this.lstDirectories.Name = "lstDirectories";
            this.lstDirectories.Size = new System.Drawing.Size(631, 244);
            this.lstDirectories.TabIndex = 4;
            this.lstDirectories.SelectedIndexChanged += new System.EventHandler(this.lstDirectories_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ordner:";
            // 
            // btnDeleteSelctedDirectory
            // 
            this.btnDeleteSelctedDirectory.Enabled = false;
            this.btnDeleteSelctedDirectory.Location = new System.Drawing.Point(9, 299);
            this.btnDeleteSelctedDirectory.Name = "btnDeleteSelctedDirectory";
            this.btnDeleteSelctedDirectory.Size = new System.Drawing.Size(83, 23);
            this.btnDeleteSelctedDirectory.TabIndex = 6;
            this.btnDeleteSelctedDirectory.Text = "Löschen";
            this.btnDeleteSelctedDirectory.UseVisualStyleBackColor = true;
            this.btnDeleteSelctedDirectory.Click += new System.EventHandler(this.btnDeleteSelctedDirectory_Click);
            // 
            // btnClearDirectorys
            // 
            this.btnClearDirectorys.Location = new System.Drawing.Point(9, 327);
            this.btnClearDirectorys.Name = "btnClearDirectorys";
            this.btnClearDirectorys.Size = new System.Drawing.Size(83, 23);
            this.btnClearDirectorys.TabIndex = 7;
            this.btnClearDirectorys.Text = "Leeren";
            this.btnClearDirectorys.UseVisualStyleBackColor = true;
            this.btnClearDirectorys.Click += new System.EventHandler(this.btnClearDirectorys_Click);
            // 
            // AddFolderDirectoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(658, 418);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbDirFile);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFolderDirectoryDialog";
            this.Text = "Datei hinzufügen";
            this.Load += new System.EventHandler(this.AddFolderDirectoryDialog_Load);
            this.tbDirFile.ResumeLayout(false);
            this.tbFiles.ResumeLayout(false);
            this.tbFiles.PerformLayout();
            this.tbDirectorys.ResumeLayout(false);
            this.tbDirectorys.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbDirFile;
        private System.Windows.Forms.TabPage tbFiles;
        private System.Windows.Forms.TabPage tbDirectorys;
        private System.Windows.Forms.Label lblFilesSelected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResearch;
        private System.Windows.Forms.Button btnDeleteFile;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tbcFiles;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkOverride;
        private System.Windows.Forms.Button btnAddDirectory;
        private System.Windows.Forms.Button btnClearDirectorys;
        private System.Windows.Forms.Button btnDeleteSelctedDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstDirectories;
    }
}