namespace VFS.Application.GUI
{
    partial class VFSOpenFileDialog
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
            this.pnlControls = new System.Windows.Forms.Panel();
            this.pnlBack = new System.Windows.Forms.Panel();
            this.pnlPageForward = new System.Windows.Forms.Panel();
            this.pnlPageBack = new System.Windows.Forms.Panel();
            this.lblPages = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.txtCurrentPath = new System.Windows.Forms.TextBox();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlControls.BackColor = System.Drawing.SystemColors.Control;
            this.pnlControls.Controls.Add(this.pnlBack);
            this.pnlControls.Controls.Add(this.pnlPageForward);
            this.pnlControls.Controls.Add(this.pnlPageBack);
            this.pnlControls.Controls.Add(this.lblPages);
            this.pnlControls.Controls.Add(this.btnOK);
            this.pnlControls.Controls.Add(this.btnCancel);
            this.pnlControls.Location = new System.Drawing.Point(0, 328);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(612, 60);
            this.pnlControls.TabIndex = 0;
            // 
            // pnlBack
            // 
            this.pnlBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlBack.BackColor = System.Drawing.Color.Transparent;
            this.pnlBack.BackgroundImage = global::VFS.Application.Properties.Resources.Back;
            this.pnlBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlBack.Location = new System.Drawing.Point(3, 7);
            this.pnlBack.Name = "pnlBack";
            this.pnlBack.Size = new System.Drawing.Size(49, 50);
            this.pnlBack.TabIndex = 6;
            this.pnlBack.Click += new System.EventHandler(this.pnlBack_Click);
            this.pnlBack.MouseLeave += new System.EventHandler(this.pnlBack_MouseLeave);
            this.pnlBack.MouseHover += new System.EventHandler(this.pnlBack_MouseHover);
            // 
            // pnlPageForward
            // 
            this.pnlPageForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlPageForward.BackgroundImage = global::VFS.Application.Properties.Resources.ForwardBig;
            this.pnlPageForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPageForward.Location = new System.Drawing.Point(96, 20);
            this.pnlPageForward.Name = "pnlPageForward";
            this.pnlPageForward.Size = new System.Drawing.Size(32, 32);
            this.pnlPageForward.TabIndex = 6;
            // 
            // pnlPageBack
            // 
            this.pnlPageBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlPageBack.BackgroundImage = global::VFS.Application.Properties.Resources.BackBig;
            this.pnlPageBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPageBack.Location = new System.Drawing.Point(58, 20);
            this.pnlPageBack.Name = "pnlPageBack";
            this.pnlPageBack.Size = new System.Drawing.Size(32, 32);
            this.pnlPageBack.TabIndex = 5;
            // 
            // lblPages
            // 
            this.lblPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPages.AutoSize = true;
            this.lblPages.Location = new System.Drawing.Point(55, 5);
            this.lblPages.Name = "lblPages";
            this.lblPages.Size = new System.Drawing.Size(73, 15);
            this.lblPages.TabIndex = 4;
            this.lblPages.Text = "Seite 1 von 1";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(445, 20);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(526, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Location = new System.Drawing.Point(0, 30);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(612, 302);
            this.pnlContainer.TabIndex = 1;
            // 
            // txtCurrentPath
            // 
            this.txtCurrentPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentPath.Location = new System.Drawing.Point(3, 3);
            this.txtCurrentPath.Name = "txtCurrentPath";
            this.txtCurrentPath.Size = new System.Drawing.Size(609, 23);
            this.txtCurrentPath.TabIndex = 2;
            this.txtCurrentPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrentPath_KeyDown);
            // 
            // VFSOpenFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(613, 388);
            this.Controls.Add(this.txtCurrentPath);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlControls);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(629, 427);
            this.Name = "VFSOpenFileDialog";
            this.ShowIcon = false;
            this.Text = "Bitte wählen Sie ... aus";
            this.Load += new System.EventHandler(this.VFSOpenFileDialog_Load);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlBack;
        private System.Windows.Forms.Panel pnlPageForward;
        private System.Windows.Forms.Panel pnlPageBack;
        private System.Windows.Forms.Label lblPages;
        private System.Windows.Forms.TextBox txtCurrentPath;
    }
}