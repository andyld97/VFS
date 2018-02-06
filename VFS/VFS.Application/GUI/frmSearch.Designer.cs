namespace VFS.Application.GUI
{
    partial class frmSearch
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
            this.pnlSearchContainer = new System.Windows.Forms.Panel();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblSides = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlSearchContainer
            // 
            this.pnlSearchContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearchContainer.BackColor = System.Drawing.Color.White;
            this.pnlSearchContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchContainer.Name = "pnlSearchContainer";
            this.pnlSearchContainer.Size = new System.Drawing.Size(640, 277);
            this.pnlSearchContainer.TabIndex = 0;
            // 
            // lblPath
            // 
            this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(12, 280);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(134, 15);
            this.lblPath.TabIndex = 1;
            this.lblPath.Text = "Pfad: Nichts ausgewählt";
            // 
            // lblSides
            // 
            this.lblSides.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSides.AutoSize = true;
            this.lblSides.Location = new System.Drawing.Point(12, 295);
            this.lblSides.Name = "lblSides";
            this.lblSides.Size = new System.Drawing.Size(73, 15);
            this.lblSides.TabIndex = 2;
            this.lblSides.Text = "Seite 1 von 1";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlLeft.BackgroundImage = global::VFS.Application.Properties.Resources.BackBig;
            this.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLeft.Location = new System.Drawing.Point(15, 313);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(48, 48);
            this.pnlLeft.TabIndex = 3;
            this.pnlLeft.Click += new System.EventHandler(this.pnlLeft_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlRight.BackgroundImage = global::VFS.Application.Properties.Resources.ForwardBig;
            this.pnlRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRight.Location = new System.Drawing.Point(69, 313);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(48, 48);
            this.pnlRight.TabIndex = 4;
            this.pnlRight.Click += new System.EventHandler(this.pnlRight_Click);
            // 
            // frmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(641, 371);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.lblSides);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.pnlSearchContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(657, 410);
            this.Name = "frmSearch";
            this.ShowIcon = false;
            this.Text = "Suchergebnis: ";
            this.Load += new System.EventHandler(this.frmSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSearchContainer;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblSides;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
    }
}