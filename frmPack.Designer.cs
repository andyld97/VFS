namespace Archiv
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
            this.components = new System.ComponentModel.Container();
            this.FileList = new System.Windows.Forms.ListBox();
            this.xPack = new System.Windows.Forms.Button();
            this.ClearList = new System.Windows.Forms.Button();
            this.DeleteSelectedFile = new System.Windows.Forms.Button();
            this.AddFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Foldername = new System.Windows.Forms.TextBox();
            this.Filename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Research = new System.Windows.Forms.Button();
            this.Checker = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // FileList
            // 
            this.FileList.FormattingEnabled = true;
            this.FileList.Location = new System.Drawing.Point(15, 25);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(589, 238);
            this.FileList.TabIndex = 0;
            // 
            // xPack
            // 
            this.xPack.Location = new System.Drawing.Point(501, 394);
            this.xPack.Name = "xPack";
            this.xPack.Size = new System.Drawing.Size(103, 23);
            this.xPack.TabIndex = 1;
            this.xPack.Text = "Verpacken";
            this.xPack.UseVisualStyleBackColor = true;
            this.xPack.Click += new System.EventHandler(this.xPack_Click);
            // 
            // ClearList
            // 
            this.ClearList.Location = new System.Drawing.Point(15, 274);
            this.ClearList.Name = "ClearList";
            this.ClearList.Size = new System.Drawing.Size(118, 23);
            this.ClearList.TabIndex = 2;
            this.ClearList.Text = "Liste leeren";
            this.ClearList.UseVisualStyleBackColor = true;
            this.ClearList.Click += new System.EventHandler(this.ClearList_Click);
            // 
            // DeleteSelectedFile
            // 
            this.DeleteSelectedFile.Location = new System.Drawing.Point(139, 274);
            this.DeleteSelectedFile.Name = "DeleteSelectedFile";
            this.DeleteSelectedFile.Size = new System.Drawing.Size(322, 23);
            this.DeleteSelectedFile.TabIndex = 3;
            this.DeleteSelectedFile.Text = "Ausgewählte Datei löschen";
            this.DeleteSelectedFile.UseVisualStyleBackColor = true;
            this.DeleteSelectedFile.Click += new System.EventHandler(this.DeleteSelectedFile_Click);
            // 
            // AddFile
            // 
            this.AddFile.Location = new System.Drawing.Point(467, 274);
            this.AddFile.Name = "AddFile";
            this.AddFile.Size = new System.Drawing.Size(137, 23);
            this.AddFile.TabIndex = 4;
            this.AddFile.Text = "Hinzufügen";
            this.AddFile.UseVisualStyleBackColor = true;
            this.AddFile.Click += new System.EventHandler(this.AddFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Dateien:";
            // 
            // Foldername
            // 
            this.Foldername.Location = new System.Drawing.Point(15, 366);
            this.Foldername.Name = "Foldername";
            this.Foldername.Size = new System.Drawing.Size(533, 22);
            this.Foldername.TabIndex = 7;
            // 
            // Filename
            // 
            this.Filename.Location = new System.Drawing.Point(15, 317);
            this.Filename.Name = "Filename";
            this.Filename.Size = new System.Drawing.Size(589, 22);
            this.Filename.TabIndex = 8;
            this.Filename.Text = "File.ap";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Dateiname:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Ordner:";
            // 
            // Research
            // 
            this.Research.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Research.Location = new System.Drawing.Point(554, 366);
            this.Research.Name = "Research";
            this.Research.Size = new System.Drawing.Size(48, 22);
            this.Research.TabIndex = 11;
            this.Research.Text = "...";
            this.Research.UseVisualStyleBackColor = true;
            this.Research.Click += new System.EventHandler(this.Research_Click);
            // 
            // Checker
            // 
            this.Checker.Enabled = true;
            this.Checker.Interval = 1;
            this.Checker.Tick += new System.EventHandler(this.Checker_Tick);
            // 
            // frmPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(616, 422);
            this.Controls.Add(this.Research);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Filename);
            this.Controls.Add(this.Foldername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddFile);
            this.Controls.Add(this.DeleteSelectedFile);
            this.Controls.Add(this.ClearList);
            this.Controls.Add(this.xPack);
            this.Controls.Add(this.FileList);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPack";
            this.ShowIcon = false;
            this.Text = "Dateien einpacken";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox FileList;
        private System.Windows.Forms.Button xPack;
        private System.Windows.Forms.Button ClearList;
        private System.Windows.Forms.Button DeleteSelectedFile;
        private System.Windows.Forms.Button AddFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Foldername;
        private System.Windows.Forms.TextBox Filename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Research;
        private System.Windows.Forms.Timer Checker;
    }
}