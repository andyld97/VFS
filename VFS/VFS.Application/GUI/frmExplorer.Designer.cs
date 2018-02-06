namespace VFS.Application.GUI
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mFileTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.createFileTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openSettingsTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openAboutTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTabTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.endTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.vFSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractAllTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.navigationTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.goBackTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.pageForwardTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.pageBackTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.filesTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.directoryTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.addDirTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.dirRenameTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.dirDeleteTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.dirExtractTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.fileTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.fileAddTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.fileRenameTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.fileDeleteTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExtractTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlPageContainer = new Design.DesignPanel();
            this.pnlBreadCrumbContainer = new Design.DesignPanel();
            this.pnlTopBarMain = new Design.DesignPanel();
            this.designButton10 = new Design.DesignButton();
            this.designButton9 = new Design.DesignButton();
            this.designButton8 = new Design.DesignButton();
            this.designButton7 = new Design.DesignButton();
            this.designButton6 = new Design.DesignButton();
            this.designButton5 = new Design.DesignButton();
            this.designButton4 = new Design.DesignButton();
            this.designButton3 = new Design.DesignButton();
            this.designButton2 = new Design.DesignButton();
            this.designButton1 = new Design.DesignButton();
            this.panel3 = new Design.DesignPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlPageControllerContainer = new Design.DesignPanel();
            this.pnlMainNavMount = new Design.DesignPanel();
            this.pnlGoForward = new Design.DesignButton();
            this.pnlGoBack = new Design.DesignButton();
            this.pnlMount = new Design.DesignPanel();
            this.pnlUnmount = new Design.DesignButton();
            this.pnlAddMount = new Design.DesignButton();
            this.pnlBottomBar = new Design.DesignPanel();
            this.chkHorizontal = new System.Windows.Forms.CheckBox();
            this.lblSites = new System.Windows.Forms.Label();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.mainMenu.SuspendLayout();
            this.pnlTopBarMain.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlMainNavMount.SuspendLayout();
            this.pnlMount.SuspendLayout();
            this.pnlBottomBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFileTSMI,
            this.vFSToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(814, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenue";
            this.mainMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mainMenu_ItemClicked);
            // 
            // mFileTSMI
            // 
            this.mFileTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileTSMI,
            this.createFileTSMI,
            this.openSettingsTSMI,
            this.openAboutTSMI,
            this.closeTabTSMI,
            this.endTSMI});
            this.mFileTSMI.Name = "mFileTSMI";
            this.mFileTSMI.Size = new System.Drawing.Size(46, 20);
            this.mFileTSMI.Text = "Datei";
            // 
            // openFileTSMI
            // 
            this.openFileTSMI.Image = global::VFS.Application.Properties.Resources.Open;
            this.openFileTSMI.Name = "openFileTSMI";
            this.openFileTSMI.ShortcutKeyDisplayString = "Strg+O";
            this.openFileTSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileTSMI.Size = new System.Drawing.Size(190, 22);
            this.openFileTSMI.Text = "Datei öffnen";
            this.openFileTSMI.Click += new System.EventHandler(this.openFileTSMI_Click);
            // 
            // createFileTSMI
            // 
            this.createFileTSMI.Image = global::VFS.Application.Properties.Resources.Add;
            this.createFileTSMI.Name = "createFileTSMI";
            this.createFileTSMI.ShortcutKeyDisplayString = "Strg+E";
            this.createFileTSMI.Size = new System.Drawing.Size(190, 22);
            this.createFileTSMI.Text = "Datei erstellen";
            this.createFileTSMI.Click += new System.EventHandler(this.createFileTSMI_Click);
            // 
            // openSettingsTSMI
            // 
            this.openSettingsTSMI.Image = global::VFS.Application.Properties.Resources.Settings;
            this.openSettingsTSMI.Name = "openSettingsTSMI";
            this.openSettingsTSMI.Size = new System.Drawing.Size(190, 22);
            this.openSettingsTSMI.Text = "Einstellungen";
            this.openSettingsTSMI.Click += new System.EventHandler(this.openSettingsTSMI_Click);
            // 
            // openAboutTSMI
            // 
            this.openAboutTSMI.Image = global::VFS.Application.Properties.Resources.About;
            this.openAboutTSMI.Name = "openAboutTSMI";
            this.openAboutTSMI.Size = new System.Drawing.Size(190, 22);
            this.openAboutTSMI.Text = "Über";
            this.openAboutTSMI.Click += new System.EventHandler(this.openAboutTSMI_Click);
            // 
            // closeTabTSMI
            // 
            this.closeTabTSMI.Image = global::VFS.Application.Properties.Resources.Abort;
            this.closeTabTSMI.Name = "closeTabTSMI";
            this.closeTabTSMI.Size = new System.Drawing.Size(190, 22);
            this.closeTabTSMI.Text = "Tab schließen";
            this.closeTabTSMI.Click += new System.EventHandler(this.closeTabTSMI_Click);
            // 
            // endTSMI
            // 
            this.endTSMI.Image = global::VFS.Application.Properties.Resources.CloseIcon;
            this.endTSMI.Name = "endTSMI";
            this.endTSMI.Size = new System.Drawing.Size(190, 22);
            this.endTSMI.Text = "Beenden";
            this.endTSMI.Click += new System.EventHandler(this.endTSMI_Click);
            // 
            // vFSToolStripMenuItem
            // 
            this.vFSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractAllTSMI,
            this.navigationTSMI,
            this.filesTSMI});
            this.vFSToolStripMenuItem.Name = "vFSToolStripMenuItem";
            this.vFSToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.vFSToolStripMenuItem.Text = "VFS";
            // 
            // extractAllTSMI
            // 
            this.extractAllTSMI.Image = global::VFS.Application.Properties.Resources.checklist_icon;
            this.extractAllTSMI.Name = "extractAllTSMI";
            this.extractAllTSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.extractAllTSMI.Size = new System.Drawing.Size(204, 22);
            this.extractAllTSMI.Text = "Alles entpacken";
            this.extractAllTSMI.Click += new System.EventHandler(this.extractAllTSMI_Click);
            // 
            // navigationTSMI
            // 
            this.navigationTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goBackTSMI,
            this.pageForwardTSMI,
            this.pageBackTSMI});
            this.navigationTSMI.Name = "navigationTSMI";
            this.navigationTSMI.Size = new System.Drawing.Size(204, 22);
            this.navigationTSMI.Text = "Navigation";
            // 
            // goBackTSMI
            // 
            this.goBackTSMI.Image = global::VFS.Application.Properties.Resources.BackBig;
            this.goBackTSMI.Name = "goBackTSMI";
            this.goBackTSMI.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.goBackTSMI.Size = new System.Drawing.Size(162, 22);
            this.goBackTSMI.Text = "Zurück";
            // 
            // pageForwardTSMI
            // 
            this.pageForwardTSMI.Image = global::VFS.Application.Properties.Resources.ForwardLittle;
            this.pageForwardTSMI.Name = "pageForwardTSMI";
            this.pageForwardTSMI.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.pageForwardTSMI.Size = new System.Drawing.Size(162, 22);
            this.pageForwardTSMI.Text = "Seite vor";
            // 
            // pageBackTSMI
            // 
            this.pageBackTSMI.Image = global::VFS.Application.Properties.Resources.BackLittle;
            this.pageBackTSMI.Name = "pageBackTSMI";
            this.pageBackTSMI.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.pageBackTSMI.Size = new System.Drawing.Size(162, 22);
            this.pageBackTSMI.Text = "Seite zurück";
            // 
            // filesTSMI
            // 
            this.filesTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.directoryTSMI,
            this.fileTSMI});
            this.filesTSMI.Name = "filesTSMI";
            this.filesTSMI.Size = new System.Drawing.Size(204, 22);
            this.filesTSMI.Text = "Dateien";
            // 
            // directoryTSMI
            // 
            this.directoryTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDirTSMI,
            this.dirRenameTSMI,
            this.dirDeleteTSMI,
            this.dirExtractTSMI});
            this.directoryTSMI.Image = ((System.Drawing.Image)(resources.GetObject("directoryTSMI.Image")));
            this.directoryTSMI.Name = "directoryTSMI";
            this.directoryTSMI.Size = new System.Drawing.Size(111, 22);
            this.directoryTSMI.Text = "Ordner";
            // 
            // addDirTSMI
            // 
            this.addDirTSMI.Image = global::VFS.Application.Properties.Resources.Add;
            this.addDirTSMI.Name = "addDirTSMI";
            this.addDirTSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.addDirTSMI.Size = new System.Drawing.Size(295, 22);
            this.addDirTSMI.Text = "Hinzufügen";
            // 
            // dirRenameTSMI
            // 
            this.dirRenameTSMI.Image = global::VFS.Application.Properties.Resources.Rename;
            this.dirRenameTSMI.Name = "dirRenameTSMI";
            this.dirRenameTSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.dirRenameTSMI.Size = new System.Drawing.Size(295, 22);
            this.dirRenameTSMI.Text = "Umbenennen";
            this.dirRenameTSMI.Click += new System.EventHandler(this.umbenenenToolStripMenuItem_Click);
            // 
            // dirDeleteTSMI
            // 
            this.dirDeleteTSMI.Image = global::VFS.Application.Properties.Resources.CloseIcon;
            this.dirDeleteTSMI.Name = "dirDeleteTSMI";
            this.dirDeleteTSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F3)));
            this.dirDeleteTSMI.Size = new System.Drawing.Size(295, 22);
            this.dirDeleteTSMI.Text = "Löschen";
            // 
            // dirExtractTSMI
            // 
            this.dirExtractTSMI.Image = ((System.Drawing.Image)(resources.GetObject("dirExtractTSMI.Image")));
            this.dirExtractTSMI.Name = "dirExtractTSMI";
            this.dirExtractTSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.dirExtractTSMI.Size = new System.Drawing.Size(295, 22);
            this.dirExtractTSMI.Text = "Ausgewählten Ordner entpacken";
            // 
            // fileTSMI
            // 
            this.fileTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileAddTSMI,
            this.fileRenameTSMI,
            this.fileDeleteTSMI,
            this.fileOpenTSMI,
            this.fileExtractTSMI});
            this.fileTSMI.Name = "fileTSMI";
            this.fileTSMI.Size = new System.Drawing.Size(111, 22);
            this.fileTSMI.Text = "Datei";
            // 
            // fileAddTSMI
            // 
            this.fileAddTSMI.Image = global::VFS.Application.Properties.Resources.Add;
            this.fileAddTSMI.Name = "fileAddTSMI";
            this.fileAddTSMI.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.fileAddTSMI.Size = new System.Drawing.Size(249, 22);
            this.fileAddTSMI.Text = "Hinzufügen";
            // 
            // fileRenameTSMI
            // 
            this.fileRenameTSMI.Image = global::VFS.Application.Properties.Resources.Rename;
            this.fileRenameTSMI.Name = "fileRenameTSMI";
            this.fileRenameTSMI.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.fileRenameTSMI.Size = new System.Drawing.Size(249, 22);
            this.fileRenameTSMI.Text = "Umbenennen";
            // 
            // fileDeleteTSMI
            // 
            this.fileDeleteTSMI.Image = global::VFS.Application.Properties.Resources.CloseIcon;
            this.fileDeleteTSMI.Name = "fileDeleteTSMI";
            this.fileDeleteTSMI.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.fileDeleteTSMI.Size = new System.Drawing.Size(249, 22);
            this.fileDeleteTSMI.Text = "Löschen";
            // 
            // fileOpenTSMI
            // 
            this.fileOpenTSMI.Image = global::VFS.Application.Properties.Resources.Open;
            this.fileOpenTSMI.Name = "fileOpenTSMI";
            this.fileOpenTSMI.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.fileOpenTSMI.Size = new System.Drawing.Size(249, 22);
            this.fileOpenTSMI.Text = "Öffnen";
            // 
            // fileExtractTSMI
            // 
            this.fileExtractTSMI.Name = "fileExtractTSMI";
            this.fileExtractTSMI.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.fileExtractTSMI.Size = new System.Drawing.Size(249, 22);
            this.fileExtractTSMI.Text = "Ausgewählte Datei entpacken";
            // 
            // pnlPageContainer
            // 
            this.pnlPageContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPageContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlPageContainer.BorderThickness = 2;
            this.pnlPageContainer.Location = new System.Drawing.Point(149, 141);
            this.pnlPageContainer.Name = "pnlPageContainer";
            this.pnlPageContainer.Size = new System.Drawing.Size(665, 316);
            this.pnlPageContainer.TabIndex = 11;
            // 
            // pnlBreadCrumbContainer
            // 
            this.pnlBreadCrumbContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBreadCrumbContainer.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBreadCrumbContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlBreadCrumbContainer.BorderThickness = 2;
            this.pnlBreadCrumbContainer.Location = new System.Drawing.Point(149, 83);
            this.pnlBreadCrumbContainer.Name = "pnlBreadCrumbContainer";
            this.pnlBreadCrumbContainer.Size = new System.Drawing.Size(502, 59);
            this.pnlBreadCrumbContainer.TabIndex = 10;
            // 
            // pnlTopBarMain
            // 
            this.pnlTopBarMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTopBarMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTopBarMain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlTopBarMain.BorderThickness = 2;
            this.pnlTopBarMain.Controls.Add(this.designButton10);
            this.pnlTopBarMain.Controls.Add(this.designButton9);
            this.pnlTopBarMain.Controls.Add(this.designButton8);
            this.pnlTopBarMain.Controls.Add(this.designButton7);
            this.pnlTopBarMain.Controls.Add(this.designButton6);
            this.pnlTopBarMain.Controls.Add(this.designButton5);
            this.pnlTopBarMain.Controls.Add(this.designButton4);
            this.pnlTopBarMain.Controls.Add(this.designButton3);
            this.pnlTopBarMain.Controls.Add(this.designButton2);
            this.pnlTopBarMain.Controls.Add(this.designButton1);
            this.pnlTopBarMain.Location = new System.Drawing.Point(149, 24);
            this.pnlTopBarMain.Name = "pnlTopBarMain";
            this.pnlTopBarMain.Size = new System.Drawing.Size(665, 61);
            this.pnlTopBarMain.TabIndex = 8;
            // 
            // designButton10
            // 
            this.designButton10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.designButton10.BackgroundImage = global::VFS.Application.Properties.Resources.AllAps;
            this.designButton10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton10.BorderThickness = 2;
            this.designButton10.Location = new System.Drawing.Point(500, 3);
            this.designButton10.Name = "designButton10";
            this.designButton10.Size = new System.Drawing.Size(50, 50);
            this.designButton10.TabIndex = 20;
            // 
            // designButton9
            // 
            this.designButton9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.designButton9.BackgroundImage = global::VFS.Application.Properties.Resources.Settings;
            this.designButton9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton9.BorderThickness = 2;
            this.designButton9.Location = new System.Drawing.Point(556, 3);
            this.designButton9.Name = "designButton9";
            this.designButton9.Size = new System.Drawing.Size(50, 50);
            this.designButton9.TabIndex = 19;
            // 
            // designButton8
            // 
            this.designButton8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.designButton8.BackgroundImage = global::VFS.Application.Properties.Resources.AboutBig;
            this.designButton8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton8.BorderThickness = 2;
            this.designButton8.Location = new System.Drawing.Point(612, 3);
            this.designButton8.Name = "designButton8";
            this.designButton8.Size = new System.Drawing.Size(50, 50);
            this.designButton8.TabIndex = 18;
            // 
            // designButton7
            // 
            this.designButton7.BackgroundImage = global::VFS.Application.Properties.Resources.Rename;
            this.designButton7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton7.BorderThickness = 2;
            this.designButton7.Location = new System.Drawing.Point(344, 3);
            this.designButton7.Name = "designButton7";
            this.designButton7.Size = new System.Drawing.Size(50, 50);
            this.designButton7.TabIndex = 17;
            // 
            // designButton6
            // 
            this.designButton6.BackgroundImage = global::VFS.Application.Properties.Resources.Paste;
            this.designButton6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton6.BorderThickness = 2;
            this.designButton6.Location = new System.Drawing.Point(288, 3);
            this.designButton6.Name = "designButton6";
            this.designButton6.Size = new System.Drawing.Size(50, 50);
            this.designButton6.TabIndex = 16;
            // 
            // designButton5
            // 
            this.designButton5.BackgroundImage = global::VFS.Application.Properties.Resources.Copy;
            this.designButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton5.BorderThickness = 2;
            this.designButton5.Location = new System.Drawing.Point(232, 3);
            this.designButton5.Name = "designButton5";
            this.designButton5.Size = new System.Drawing.Size(50, 50);
            this.designButton5.TabIndex = 15;
            // 
            // designButton4
            // 
            this.designButton4.BackgroundImage = global::VFS.Application.Properties.Resources.DeleteFolder;
            this.designButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton4.BorderThickness = 2;
            this.designButton4.Location = new System.Drawing.Point(176, 3);
            this.designButton4.Name = "designButton4";
            this.designButton4.Size = new System.Drawing.Size(50, 50);
            this.designButton4.TabIndex = 14;
            // 
            // designButton3
            // 
            this.designButton3.BackgroundImage = global::VFS.Application.Properties.Resources.AddFolder;
            this.designButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton3.BorderThickness = 2;
            this.designButton3.Location = new System.Drawing.Point(120, 3);
            this.designButton3.Name = "designButton3";
            this.designButton3.Size = new System.Drawing.Size(50, 50);
            this.designButton3.TabIndex = 13;
            // 
            // designButton2
            // 
            this.designButton2.BackgroundImage = global::VFS.Application.Properties.Resources.DeleteFile;
            this.designButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton2.BorderThickness = 2;
            this.designButton2.Location = new System.Drawing.Point(64, 3);
            this.designButton2.Name = "designButton2";
            this.designButton2.Size = new System.Drawing.Size(50, 50);
            this.designButton2.TabIndex = 12;
            // 
            // designButton1
            // 
            this.designButton1.BackgroundImage = global::VFS.Application.Properties.Resources.AddFile;
            this.designButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.designButton1.BorderThickness = 2;
            this.designButton1.Location = new System.Drawing.Point(8, 3);
            this.designButton1.Name = "designButton1";
            this.designButton1.Size = new System.Drawing.Size(50, 50);
            this.designButton1.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.panel3.BorderThickness = 2;
            this.panel3.Controls.Add(this.txtSearch);
            this.panel3.Location = new System.Drawing.Point(649, 83);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(165, 59);
            this.panel3.TabIndex = 10;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(8, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(146, 23);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.Text = "Suchen ...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // pnlPageControllerContainer
            // 
            this.pnlPageControllerContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlPageControllerContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlPageControllerContainer.BorderThickness = 2;
            this.pnlPageControllerContainer.Location = new System.Drawing.Point(0, 140);
            this.pnlPageControllerContainer.Name = "pnlPageControllerContainer";
            this.pnlPageControllerContainer.Size = new System.Drawing.Size(151, 382);
            this.pnlPageControllerContainer.TabIndex = 12;
            // 
            // pnlMainNavMount
            // 
            this.pnlMainNavMount.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMainNavMount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlMainNavMount.BorderThickness = 2;
            this.pnlMainNavMount.Controls.Add(this.pnlGoForward);
            this.pnlMainNavMount.Controls.Add(this.pnlGoBack);
            this.pnlMainNavMount.Location = new System.Drawing.Point(1, 83);
            this.pnlMainNavMount.Name = "pnlMainNavMount";
            this.pnlMainNavMount.Size = new System.Drawing.Size(150, 59);
            this.pnlMainNavMount.TabIndex = 9;
            // 
            // pnlGoForward
            // 
            this.pnlGoForward.BackgroundImage = global::VFS.Application.Properties.Resources.ForwardBig;
            this.pnlGoForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGoForward.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlGoForward.BorderThickness = 2;
            this.pnlGoForward.Location = new System.Drawing.Point(75, 14);
            this.pnlGoForward.Name = "pnlGoForward";
            this.pnlGoForward.Size = new System.Drawing.Size(32, 32);
            this.pnlGoForward.TabIndex = 10;
            // 
            // pnlGoBack
            // 
            this.pnlGoBack.BackgroundImage = global::VFS.Application.Properties.Resources.BackBig;
            this.pnlGoBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGoBack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlGoBack.BorderThickness = 2;
            this.pnlGoBack.Location = new System.Drawing.Point(37, 14);
            this.pnlGoBack.Name = "pnlGoBack";
            this.pnlGoBack.Size = new System.Drawing.Size(32, 32);
            this.pnlGoBack.TabIndex = 9;
            // 
            // pnlMount
            // 
            this.pnlMount.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlMount.BorderThickness = 2;
            this.pnlMount.Controls.Add(this.pnlUnmount);
            this.pnlMount.Controls.Add(this.pnlAddMount);
            this.pnlMount.Location = new System.Drawing.Point(1, 24);
            this.pnlMount.Name = "pnlMount";
            this.pnlMount.Size = new System.Drawing.Size(150, 61);
            this.pnlMount.TabIndex = 10;
            // 
            // pnlUnmount
            // 
            this.pnlUnmount.BackgroundImage = global::VFS.Application.Properties.Resources.Unmount;
            this.pnlUnmount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlUnmount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlUnmount.BorderThickness = 2;
            this.pnlUnmount.Location = new System.Drawing.Point(75, 12);
            this.pnlUnmount.Name = "pnlUnmount";
            this.pnlUnmount.Size = new System.Drawing.Size(32, 32);
            this.pnlUnmount.TabIndex = 10;
            // 
            // pnlAddMount
            // 
            this.pnlAddMount.BackgroundImage = global::VFS.Application.Properties.Resources.Hinzufügen;
            this.pnlAddMount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAddMount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlAddMount.BorderThickness = 2;
            this.pnlAddMount.Location = new System.Drawing.Point(37, 12);
            this.pnlAddMount.Name = "pnlAddMount";
            this.pnlAddMount.Size = new System.Drawing.Size(32, 32);
            this.pnlAddMount.TabIndex = 9;
            // 
            // pnlBottomBar
            // 
            this.pnlBottomBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomBar.BackColor = System.Drawing.Color.White;
            this.pnlBottomBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(204)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.pnlBottomBar.BorderThickness = 2;
            this.pnlBottomBar.Controls.Add(this.chkHorizontal);
            this.pnlBottomBar.Controls.Add(this.lblSites);
            this.pnlBottomBar.Controls.Add(this.chkPreview);
            this.pnlBottomBar.Location = new System.Drawing.Point(149, 460);
            this.pnlBottomBar.Name = "pnlBottomBar";
            this.pnlBottomBar.Size = new System.Drawing.Size(665, 65);
            this.pnlBottomBar.TabIndex = 10;
            // 
            // chkHorizontal
            // 
            this.chkHorizontal.AutoSize = true;
            this.chkHorizontal.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkHorizontal.Checked = true;
            this.chkHorizontal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHorizontal.Location = new System.Drawing.Point(88, 3);
            this.chkHorizontal.Name = "chkHorizontal";
            this.chkHorizontal.Size = new System.Drawing.Size(150, 19);
            this.chkHorizontal.TabIndex = 9;
            this.chkHorizontal.Text = "Horizontale Darstellung";
            this.chkHorizontal.UseVisualStyleBackColor = true;
            this.chkHorizontal.CheckedChanged += new System.EventHandler(this.chkHorizontal_CheckedChanged);
            // 
            // lblSites
            // 
            this.lblSites.AutoSize = true;
            this.lblSites.Location = new System.Drawing.Point(589, 38);
            this.lblSites.Name = "lblSites";
            this.lblSites.Size = new System.Drawing.Size(73, 15);
            this.lblSites.TabIndex = 7;
            this.lblSites.Text = "Seite 1 von 1";
            // 
            // chkPreview
            // 
            this.chkPreview.AutoSize = true;
            this.chkPreview.Location = new System.Drawing.Point(8, 3);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(74, 19);
            this.chkPreview.TabIndex = 8;
            this.chkPreview.Text = "Vorschau";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.CheckedChanged += new System.EventHandler(this.chkPreview_CheckedChanged);
            // 
            // frmExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(814, 522);
            this.Controls.Add(this.pnlPageContainer);
            this.Controls.Add(this.pnlBreadCrumbContainer);
            this.Controls.Add(this.pnlTopBarMain);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlPageControllerContainer);
            this.Controls.Add(this.pnlMainNavMount);
            this.Controls.Add(this.pnlMount);
            this.Controls.Add(this.pnlBottomBar);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(830, 561);
            this.Name = "frmExplorer";
            this.Text = "VFS";
            this.Load += new System.EventHandler(this.frmExplorer_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.pnlTopBarMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlMainNavMount.ResumeLayout(false);
            this.pnlMount.ResumeLayout(false);
            this.pnlBottomBar.ResumeLayout(false);
            this.pnlBottomBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mFileTSMI;
        private System.Windows.Forms.ToolStripMenuItem openFileTSMI;
        private System.Windows.Forms.ToolStripMenuItem createFileTSMI;
        private System.Windows.Forms.ToolStripMenuItem openSettingsTSMI;
        private System.Windows.Forms.ToolStripMenuItem openAboutTSMI;
        private System.Windows.Forms.ToolStripMenuItem closeTabTSMI;
        private System.Windows.Forms.ToolStripMenuItem endTSMI;
        private System.Windows.Forms.ToolStripMenuItem vFSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractAllTSMI;
        private System.Windows.Forms.ToolStripMenuItem navigationTSMI;
        private System.Windows.Forms.ToolStripMenuItem goBackTSMI;
        private System.Windows.Forms.ToolStripMenuItem pageForwardTSMI;
        private System.Windows.Forms.ToolStripMenuItem pageBackTSMI;
        private System.Windows.Forms.ToolStripMenuItem filesTSMI;
        private System.Windows.Forms.ToolStripMenuItem directoryTSMI;
        private System.Windows.Forms.ToolStripMenuItem addDirTSMI;
        private System.Windows.Forms.ToolStripMenuItem dirRenameTSMI;
        private System.Windows.Forms.ToolStripMenuItem dirDeleteTSMI;
        private System.Windows.Forms.ToolStripMenuItem dirExtractTSMI;
        private System.Windows.Forms.ToolStripMenuItem fileTSMI;
        private System.Windows.Forms.ToolStripMenuItem fileAddTSMI;
        private System.Windows.Forms.ToolStripMenuItem fileRenameTSMI;
        private System.Windows.Forms.ToolStripMenuItem fileDeleteTSMI;
        private System.Windows.Forms.ToolStripMenuItem fileOpenTSMI;
        private System.Windows.Forms.ToolStripMenuItem fileExtractTSMI;
        private System.Windows.Forms.Label lblSites;
        private System.Windows.Forms.TextBox txtSearch;

        private Design.DesignPanel pnlTopBarMain;
        private Design.DesignPanel pnlMainNavMount;
        private Design.DesignPanel pnlMount;
        private Design.DesignButton pnlAddMount;
        private Design.DesignButton pnlUnmount;        
        private Design.DesignButton pnlGoForward;
        private Design.DesignButton pnlGoBack;

        private Design.DesignPanel pnlBottomBar;
        private Design.DesignPanel pnlBreadCrumbContainer;
        private Design.DesignPanel pnlPageContainer;
        private Design.DesignPanel panel3;
        private Design.DesignPanel pnlPageControllerContainer;
        private System.Windows.Forms.CheckBox chkPreview;
        private Design.DesignButton designButton1;
        private Design.DesignButton designButton2;
        private Design.DesignButton designButton3;
        private Design.DesignButton designButton4;
        private Design.DesignButton designButton5;
        private Design.DesignButton designButton6;
        private Design.DesignButton designButton7;
        private Design.DesignButton designButton8;
        private Design.DesignButton designButton9;
        private Design.DesignButton designButton10;
        private System.Windows.Forms.CheckBox chkHorizontal;
    }
}

