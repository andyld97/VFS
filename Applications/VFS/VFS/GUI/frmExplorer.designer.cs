namespace VFS.GUI
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
            this.pnlSideBar = new System.Windows.Forms.Panel();
            this.lblSites = new System.Windows.Forms.Label();
            this.lblSizeOfFile = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.lblOrdnerName = new System.Windows.Forms.Label();
            this.tbCtrl = new System.Windows.Forms.TabControl();
            this.tpOpenFiles = new System.Windows.Forms.TabPage();
            this.tbcFiles = new System.Windows.Forms.TabControl();
            this.tpNotepad = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTxt = new System.Windows.Forms.Label();
            this.txtNotepad = new System.Windows.Forms.TextBox();
            this.txtCurrentPath = new System.Windows.Forms.TextBox();
            this.menStrip = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateiÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateiErstellenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSchließenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archivToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allesEntpackenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zurückToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seiteVorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seiteZurückToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hinzufügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.umbenenenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.löschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ausgewähtelnOrdnerEntpackenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hinzufügenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.umbenennenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.löschenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ausgewählteDateiEntpackenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tpImages = new System.Windows.Forms.TabPage();
            this.pnlSideBar.SuspendLayout();
            this.tbCtrl.SuspendLayout();
            this.tpOpenFiles.SuspendLayout();
            this.tpNotepad.SuspendLayout();
            this.menStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSideBar.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSideBar.Controls.Add(this.lblSites);
            this.pnlSideBar.Controls.Add(this.lblSizeOfFile);
            this.pnlSideBar.Controls.Add(this.label2);
            this.pnlSideBar.Controls.Add(this.lblSelectedFile);
            this.pnlSideBar.Controls.Add(this.lblOrdnerName);
            this.pnlSideBar.Location = new System.Drawing.Point(670, 50);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(207, 570);
            this.pnlSideBar.TabIndex = 3;
            // 
            // lblSites
            // 
            this.lblSites.AutoSize = true;
            this.lblSites.Location = new System.Drawing.Point(7, 102);
            this.lblSites.Name = "lblSites";
            this.lblSites.Size = new System.Drawing.Size(73, 15);
            this.lblSites.TabIndex = 0;
            this.lblSites.Text = "Seite 1 von 1";
            // 
            // lblSizeOfFile
            // 
            this.lblSizeOfFile.AutoSize = true;
            this.lblSizeOfFile.Location = new System.Drawing.Point(7, 73);
            this.lblSizeOfFile.Name = "lblSizeOfFile";
            this.lblSizeOfFile.Size = new System.Drawing.Size(92, 15);
            this.lblSizeOfFile.TabIndex = 6;
            this.lblSizeOfFile.Text = "Größe der Datei:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe WP", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 40);
            this.label2.TabIndex = 0;
            this.label2.Text = "Info";
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Location = new System.Drawing.Point(7, 58);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(111, 15);
            this.lblSelectedFile.TabIndex = 5;
            this.lblSelectedFile.Text = "Ausgewählte Datei: ";
            // 
            // lblOrdnerName
            // 
            this.lblOrdnerName.AutoSize = true;
            this.lblOrdnerName.Location = new System.Drawing.Point(7, 43);
            this.lblOrdnerName.Name = "lblOrdnerName";
            this.lblOrdnerName.Size = new System.Drawing.Size(47, 15);
            this.lblOrdnerName.TabIndex = 4;
            this.lblOrdnerName.Text = "Ordner:";
            // 
            // tbCtrl
            // 
            this.tbCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCtrl.Controls.Add(this.tpOpenFiles);
            this.tbCtrl.Controls.Add(this.tpNotepad);
            this.tbCtrl.Controls.Add(this.tpImages);
            this.tbCtrl.Location = new System.Drawing.Point(1, 27);
            this.tbCtrl.Name = "tbCtrl";
            this.tbCtrl.SelectedIndex = 0;
            this.tbCtrl.Size = new System.Drawing.Size(668, 593);
            this.tbCtrl.TabIndex = 4;
            // 
            // tpOpenFiles
            // 
            this.tpOpenFiles.Controls.Add(this.tbcFiles);
            this.tpOpenFiles.Location = new System.Drawing.Point(4, 24);
            this.tpOpenFiles.Name = "tpOpenFiles";
            this.tpOpenFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tpOpenFiles.Size = new System.Drawing.Size(660, 565);
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
            this.tbcFiles.Size = new System.Drawing.Size(654, 559);
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
            this.tpNotepad.Size = new System.Drawing.Size(660, 565);
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
            this.btnSave.Location = new System.Drawing.Point(3, 536);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(651, 23);
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
            this.txtNotepad.Size = new System.Drawing.Size(659, 510);
            this.txtNotepad.TabIndex = 0;
            // 
            // txtCurrentPath
            // 
            this.txtCurrentPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentPath.BackColor = System.Drawing.Color.White;
            this.txtCurrentPath.Location = new System.Drawing.Point(1, 626);
            this.txtCurrentPath.Name = "txtCurrentPath";
            this.txtCurrentPath.ReadOnly = true;
            this.txtCurrentPath.Size = new System.Drawing.Size(876, 23);
            this.txtCurrentPath.TabIndex = 6;
            this.txtCurrentPath.Text = "\\";
            // 
            // menStrip
            // 
            this.menStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.archivToolStripMenuItem});
            this.menStrip.Location = new System.Drawing.Point(0, 0);
            this.menStrip.Name = "menStrip";
            this.menStrip.Size = new System.Drawing.Size(882, 24);
            this.menStrip.TabIndex = 7;
            this.menStrip.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiÖffnenToolStripMenuItem,
            this.dateiErstellenToolStripMenuItem,
            this.einstellungenToolStripMenuItem,
            this.überToolStripMenuItem,
            this.tabSchließenToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // dateiÖffnenToolStripMenuItem
            // 
            this.dateiÖffnenToolStripMenuItem.Image = global::VFS.Properties.Resources.Open;
            this.dateiÖffnenToolStripMenuItem.Name = "dateiÖffnenToolStripMenuItem";
            this.dateiÖffnenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.dateiÖffnenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.dateiÖffnenToolStripMenuItem.Text = "Datei öffnen";
            this.dateiÖffnenToolStripMenuItem.Click += new System.EventHandler(this.dateiÖffnenToolStripMenuItem_Click);
            // 
            // dateiErstellenToolStripMenuItem
            // 
            this.dateiErstellenToolStripMenuItem.Image = global::VFS.Properties.Resources.Add;
            this.dateiErstellenToolStripMenuItem.Name = "dateiErstellenToolStripMenuItem";
            this.dateiErstellenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.dateiErstellenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.dateiErstellenToolStripMenuItem.Text = "Datei erstellen";
            this.dateiErstellenToolStripMenuItem.Click += new System.EventHandler(this.dateiErstellenToolStripMenuItem_Click);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Image = global::VFS.Properties.Resources.Settings;
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItem_Click_1);
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Image = global::VFS.Properties.Resources.About;
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.überToolStripMenuItem.Text = "Über";
            this.überToolStripMenuItem.Click += new System.EventHandler(this.überToolStripMenuItem_Click_1);
            // 
            // tabSchließenToolStripMenuItem
            // 
            this.tabSchließenToolStripMenuItem.Image = global::VFS.Properties.Resources.Abort;
            this.tabSchließenToolStripMenuItem.Name = "tabSchließenToolStripMenuItem";
            this.tabSchließenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.tabSchließenToolStripMenuItem.Text = "Tab schließen";
            this.tabSchließenToolStripMenuItem.Click += new System.EventHandler(this.tabSchließenToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Image = global::VFS.Properties.Resources.CloseIcon;
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // archivToolStripMenuItem
            // 
            this.archivToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allesEntpackenToolStripMenuItem,
            this.navigationToolStripMenuItem,
            this.dateienToolStripMenuItem});
            this.archivToolStripMenuItem.Name = "archivToolStripMenuItem";
            this.archivToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.archivToolStripMenuItem.Text = "Archiv";
            // 
            // allesEntpackenToolStripMenuItem
            // 
            this.allesEntpackenToolStripMenuItem.Image = global::VFS.Properties.Resources.Book_2_;
            this.allesEntpackenToolStripMenuItem.Name = "allesEntpackenToolStripMenuItem";
            this.allesEntpackenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.allesEntpackenToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.allesEntpackenToolStripMenuItem.Text = "Alles entpacken";
            this.allesEntpackenToolStripMenuItem.Click += new System.EventHandler(this.allesEntpackenToolStripMenuItem_Click);
            // 
            // navigationToolStripMenuItem
            // 
            this.navigationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zurückToolStripMenuItem,
            this.seiteVorToolStripMenuItem,
            this.seiteZurückToolStripMenuItem});
            this.navigationToolStripMenuItem.Image = global::VFS.Properties.Resources.Book_3_;
            this.navigationToolStripMenuItem.Name = "navigationToolStripMenuItem";
            this.navigationToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.navigationToolStripMenuItem.Text = "Navigation";
            // 
            // zurückToolStripMenuItem
            // 
            this.zurückToolStripMenuItem.Image = global::VFS.Properties.Resources.Back;
            this.zurückToolStripMenuItem.Name = "zurückToolStripMenuItem";
            this.zurückToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.zurückToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.zurückToolStripMenuItem.Text = "Zurück";
            this.zurückToolStripMenuItem.Click += new System.EventHandler(this.zurückToolStripMenuItem_Click);
            // 
            // seiteVorToolStripMenuItem
            // 
            this.seiteVorToolStripMenuItem.Image = global::VFS.Properties.Resources.RIghtArrow;
            this.seiteVorToolStripMenuItem.Name = "seiteVorToolStripMenuItem";
            this.seiteVorToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.seiteVorToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.seiteVorToolStripMenuItem.Text = "Seite vor";
            this.seiteVorToolStripMenuItem.Click += new System.EventHandler(this.seiteVorToolStripMenuItem_Click);
            // 
            // seiteZurückToolStripMenuItem
            // 
            this.seiteZurückToolStripMenuItem.Image = global::VFS.Properties.Resources.LeftArrow;
            this.seiteZurückToolStripMenuItem.Name = "seiteZurückToolStripMenuItem";
            this.seiteZurückToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.seiteZurückToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.seiteZurückToolStripMenuItem.Text = "Seite zurück";
            this.seiteZurückToolStripMenuItem.Click += new System.EventHandler(this.seiteZurückToolStripMenuItem_Click);
            // 
            // dateienToolStripMenuItem
            // 
            this.dateienToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordnerToolStripMenuItem,
            this.dateiToolStripMenuItem1});
            this.dateienToolStripMenuItem.Image = global::VFS.Properties.Resources.Eigene_Dateien;
            this.dateienToolStripMenuItem.Name = "dateienToolStripMenuItem";
            this.dateienToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.dateienToolStripMenuItem.Text = "Dateien";
            // 
            // ordnerToolStripMenuItem
            // 
            this.ordnerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hinzufügenToolStripMenuItem,
            this.umbenenenToolStripMenuItem,
            this.löschenToolStripMenuItem,
            this.ausgewähtelnOrdnerEntpackenToolStripMenuItem});
            this.ordnerToolStripMenuItem.Image = global::VFS.Properties.Resources.Directory;
            this.ordnerToolStripMenuItem.Name = "ordnerToolStripMenuItem";
            this.ordnerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ordnerToolStripMenuItem.Text = "Ordner";
            // 
            // hinzufügenToolStripMenuItem
            // 
            this.hinzufügenToolStripMenuItem.Image = global::VFS.Properties.Resources.Add;
            this.hinzufügenToolStripMenuItem.Name = "hinzufügenToolStripMenuItem";
            this.hinzufügenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.hinzufügenToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.hinzufügenToolStripMenuItem.Text = "Hinzufügen";
            this.hinzufügenToolStripMenuItem.Click += new System.EventHandler(this.hinzufügenToolStripMenuItem_Click);
            // 
            // umbenenenToolStripMenuItem
            // 
            this.umbenenenToolStripMenuItem.Image = global::VFS.Properties.Resources.Rename;
            this.umbenenenToolStripMenuItem.Name = "umbenenenToolStripMenuItem";
            this.umbenenenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.umbenenenToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.umbenenenToolStripMenuItem.Text = "Umbenenen";
            this.umbenenenToolStripMenuItem.Click += new System.EventHandler(this.umbenenenToolStripMenuItem_Click);
            // 
            // löschenToolStripMenuItem
            // 
            this.löschenToolStripMenuItem.Image = global::VFS.Properties.Resources.Abort;
            this.löschenToolStripMenuItem.Name = "löschenToolStripMenuItem";
            this.löschenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F3)));
            this.löschenToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.löschenToolStripMenuItem.Text = "Löschen";
            this.löschenToolStripMenuItem.Click += new System.EventHandler(this.löschenToolStripMenuItem_Click);
            // 
            // ausgewähtelnOrdnerEntpackenToolStripMenuItem
            // 
            this.ausgewähtelnOrdnerEntpackenToolStripMenuItem.Image = global::VFS.Properties.Resources.checklist_icon;
            this.ausgewähtelnOrdnerEntpackenToolStripMenuItem.Name = "ausgewähtelnOrdnerEntpackenToolStripMenuItem";
            this.ausgewähtelnOrdnerEntpackenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.ausgewähtelnOrdnerEntpackenToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.ausgewähtelnOrdnerEntpackenToolStripMenuItem.Text = "Ausgewählten Ordner entpacken";
            this.ausgewähtelnOrdnerEntpackenToolStripMenuItem.Click += new System.EventHandler(this.ausgewähtelnOrdnerEntpackenToolStripMenuItem_Click);
            // 
            // dateiToolStripMenuItem1
            // 
            this.dateiToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hinzufügenToolStripMenuItem1,
            this.umbenennenToolStripMenuItem,
            this.löschenToolStripMenuItem1,
            this.öffnenToolStripMenuItem,
            this.ausgewählteDateiEntpackenToolStripMenuItem});
            this.dateiToolStripMenuItem1.Image = global::VFS.Properties.Resources.File;
            this.dateiToolStripMenuItem1.Name = "dateiToolStripMenuItem1";
            this.dateiToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.dateiToolStripMenuItem1.Text = "Datei";
            // 
            // hinzufügenToolStripMenuItem1
            // 
            this.hinzufügenToolStripMenuItem1.Image = global::VFS.Properties.Resources.Add;
            this.hinzufügenToolStripMenuItem1.Name = "hinzufügenToolStripMenuItem1";
            this.hinzufügenToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.hinzufügenToolStripMenuItem1.Size = new System.Drawing.Size(249, 22);
            this.hinzufügenToolStripMenuItem1.Text = "Hinzufügen";
            this.hinzufügenToolStripMenuItem1.Click += new System.EventHandler(this.hinzufügenToolStripMenuItem1_Click);
            // 
            // umbenennenToolStripMenuItem
            // 
            this.umbenennenToolStripMenuItem.Image = global::VFS.Properties.Resources.Rename;
            this.umbenennenToolStripMenuItem.Name = "umbenennenToolStripMenuItem";
            this.umbenennenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.umbenennenToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.umbenennenToolStripMenuItem.Text = "Umbenennen";
            this.umbenennenToolStripMenuItem.Click += new System.EventHandler(this.umbenennenToolStripMenuItem_Click);
            // 
            // löschenToolStripMenuItem1
            // 
            this.löschenToolStripMenuItem1.Image = global::VFS.Properties.Resources.Abort;
            this.löschenToolStripMenuItem1.Name = "löschenToolStripMenuItem1";
            this.löschenToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.löschenToolStripMenuItem1.Size = new System.Drawing.Size(249, 22);
            this.löschenToolStripMenuItem1.Text = "Löschen";
            this.löschenToolStripMenuItem1.Click += new System.EventHandler(this.löschenToolStripMenuItem1_Click);
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Image = global::VFS.Properties.Resources.Open1;
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.öffnenToolStripMenuItem.Text = "Öffnen";
            this.öffnenToolStripMenuItem.Click += new System.EventHandler(this.öffnenToolStripMenuItem_Click);
            // 
            // ausgewählteDateiEntpackenToolStripMenuItem
            // 
            this.ausgewählteDateiEntpackenToolStripMenuItem.Image = global::VFS.Properties.Resources.checklist_icon;
            this.ausgewählteDateiEntpackenToolStripMenuItem.Name = "ausgewählteDateiEntpackenToolStripMenuItem";
            this.ausgewählteDateiEntpackenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.ausgewählteDateiEntpackenToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.ausgewählteDateiEntpackenToolStripMenuItem.Text = "Ausgewählte Datei entpacken";
            this.ausgewählteDateiEntpackenToolStripMenuItem.Click += new System.EventHandler(this.ausgewählteDateiEntpackenToolStripMenuItem_Click);
            // 
            // tpImages
            // 
            this.tpImages.Location = new System.Drawing.Point(4, 24);
            this.tpImages.Name = "tpImages";
            this.tpImages.Size = new System.Drawing.Size(660, 565);
            this.tpImages.TabIndex = 2;
            this.tpImages.Text = "Bilder";
            this.tpImages.UseVisualStyleBackColor = true;
            // 
            // frmExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(882, 651);
            this.Controls.Add(this.pnlSideBar);
            this.Controls.Add(this.txtCurrentPath);
            this.Controls.Add(this.tbCtrl);
            this.Controls.Add(this.menStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menStrip;
            this.Name = "frmExplorer";
            this.Text = "Archiv";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlSideBar.ResumeLayout(false);
            this.pnlSideBar.PerformLayout();
            this.tbCtrl.ResumeLayout(false);
            this.tpOpenFiles.ResumeLayout(false);
            this.tpNotepad.ResumeLayout(false);
            this.tpNotepad.PerformLayout();
            this.menStrip.ResumeLayout(false);
            this.menStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrdnerName;
        private System.Windows.Forms.Label lblSizeOfFile;
        private System.Windows.Forms.Label lblSelectedFile;
        private System.Windows.Forms.TabControl tbCtrl;
        private System.Windows.Forms.TabPage tpOpenFiles;
        private System.Windows.Forms.TabControl tbcFiles;
        private System.Windows.Forms.TabPage tpNotepad;
        private System.Windows.Forms.TextBox txtCurrentPath;
        private System.Windows.Forms.Label lblSites;
        private System.Windows.Forms.TextBox txtNotepad;
        private System.Windows.Forms.Label lblTxt;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.MenuStrip menStrip;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archivToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allesEntpackenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem navigationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seiteVorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seiteZurückToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zurückToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hinzufügenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem löschenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ausgewähtelnOrdnerEntpackenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hinzufügenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem löschenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ausgewählteDateiEntpackenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateiÖffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem öffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem umbenenenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem umbenennenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateiErstellenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tabSchließenToolStripMenuItem;
        private System.Windows.Forms.TabPage tpImages;
    }
}

