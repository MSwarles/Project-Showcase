namespace MapEditor
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripMapDimensions = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripMapLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripDebug = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayTilesetGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.highlightSelectedLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.layerPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlTimer = new System.Windows.Forms.Timer(this.components);
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.newMapToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.openMapToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.saveMapToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.displayGridToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cbZoom = new System.Windows.Forms.ToolStripComboBox();
            this.zoomOutToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.zoomInToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.drawToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.eraseToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.lblBrushSize = new System.Windows.Forms.ToolStripLabel();
            this.decreaseBrushSizeToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.cbBrushSize = new System.Windows.Forms.ToolStripComboBox();
            this.increaseBrushSizeToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.mapDisplay = new MapEditor.MapDisplay();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.gbLayerPanel = new System.Windows.Forms.GroupBox();
            this.clbLayers = new System.Windows.Forms.CheckedListBox();
            this.layerPanelToolStrip = new System.Windows.Forms.ToolStrip();
            this.newLayerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.removeLayerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.layerMoveUpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.layerMoveDownToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.layerPropertiesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.gbTilesetPanel = new System.Windows.Forms.GroupBox();
            this.tileSetSplitContainer = new System.Windows.Forms.SplitContainer();
            this.nudCollisionType = new System.Windows.Forms.NumericUpDown();
            this.btnSetDefaultCollision = new System.Windows.Forms.Button();
            this.cbCollisionType = new System.Windows.Forms.ComboBox();
            this.panelTileset = new System.Windows.Forms.Panel();
            this.pbTilesetPreview = new System.Windows.Forms.PictureBox();
            this.pbTilePreview = new System.Windows.Forms.PictureBox();
            this.nudCurrentTile = new System.Windows.Forms.NumericUpDown();
            this.lbTileset = new System.Windows.Forms.ListBox();
            this.tileSetPanelToolStrip = new System.Windows.Forms.ToolStrip();
            this.newTilesetToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openTilesetToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveTilesetToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.removeTilesetToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.displayTilesetGridToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomOutTilesetToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.zoomInTilesetToolStripMenuButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.renameTilesetToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.lblTile = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.btnSetCurrentCollision = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.Panel2.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            this.gbLayerPanel.SuspendLayout();
            this.layerPanelToolStrip.SuspendLayout();
            this.gbTilesetPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileSetSplitContainer)).BeginInit();
            this.tileSetSplitContainer.Panel1.SuspendLayout();
            this.tileSetSplitContainer.Panel2.SuspendLayout();
            this.tileSetSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCollisionType)).BeginInit();
            this.panelTileset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTilesetPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTilePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurrentTile)).BeginInit();
            this.tileSetPanelToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripMapDimensions,
            this.statusStripMapLocation,
            this.statusStripDebug});
            this.statusStrip.Location = new System.Drawing.Point(0, 801);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip.Size = new System.Drawing.Size(1572, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusStripMapDimensions
            // 
            this.statusStripMapDimensions.AutoSize = false;
            this.statusStripMapDimensions.AutoToolTip = true;
            this.statusStripMapDimensions.Image = global::MapEditor.Properties.Resources.mapSizeSmall;
            this.statusStripMapDimensions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripMapDimensions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.statusStripMapDimensions.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.statusStripMapDimensions.Name = "statusStripMapDimensions";
            this.statusStripMapDimensions.Size = new System.Drawing.Size(80, 17);
            this.statusStripMapDimensions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripMapDimensions.ToolTipText = "Map Dimensions";
            // 
            // statusStripMapLocation
            // 
            this.statusStripMapLocation.AutoSize = false;
            this.statusStripMapLocation.AutoToolTip = true;
            this.statusStripMapLocation.Image = global::MapEditor.Properties.Resources.locationSmall;
            this.statusStripMapLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripMapLocation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.statusStripMapLocation.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.statusStripMapLocation.Name = "statusStripMapLocation";
            this.statusStripMapLocation.Size = new System.Drawing.Size(200, 17);
            this.statusStripMapLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripMapLocation.ToolTipText = "Current Tile";
            // 
            // statusStripDebug
            // 
            this.statusStripDebug.Margin = new System.Windows.Forms.Padding(50, 3, 0, 2);
            this.statusStripDebug.Name = "statusStripDebug";
            this.statusStripDebug.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.statusStripDebug.Size = new System.Drawing.Size(50, 17);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.layerToolStripMenuItem,
            this.tilesetToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1572, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.openMapToolStripMenuItem,
            this.toolStripSeparator11,
            this.saveMapToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.fileMenuSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newMapToolStripMenuItem
            // 
            this.newMapToolStripMenuItem.Image = global::MapEditor.Properties.Resources.newFile;
            this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
            this.newMapToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + N";
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.newMapToolStripMenuItem.Text = "&New...";
            // 
            // openMapToolStripMenuItem
            // 
            this.openMapToolStripMenuItem.Image = global::MapEditor.Properties.Resources.openFile;
            this.openMapToolStripMenuItem.Name = "openMapToolStripMenuItem";
            this.openMapToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + O";
            this.openMapToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.openMapToolStripMenuItem.Text = "&Open...";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(204, 6);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Enabled = false;
            this.saveMapToolStripMenuItem.Image = global::MapEditor.Properties.Resources.saveFile;
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + S";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.saveMapToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Image = global::MapEditor.Properties.Resources.saveFile;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + Shift + S";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // fileMenuSeparator
            // 
            this.fileMenuSeparator.Name = "fileMenuSeparator";
            this.fileMenuSeparator.Size = new System.Drawing.Size(204, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Image = global::MapEditor.Properties.Resources.settings;
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences...";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayGridToolStripMenuItem,
            this.displayTilesetGridToolStripMenuItem,
            this.toolStripSeparator10,
            this.highlightSelectedLayerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // displayGridToolStripMenuItem
            // 
            this.displayGridToolStripMenuItem.Checked = true;
            this.displayGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayGridToolStripMenuItem.Image = global::MapEditor.Properties.Resources.viewGrid;
            this.displayGridToolStripMenuItem.Name = "displayGridToolStripMenuItem";
            this.displayGridToolStripMenuItem.ShortcutKeyDisplayString = "G";
            this.displayGridToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.displayGridToolStripMenuItem.Text = "Map &Grid";
            // 
            // displayTilesetGridToolStripMenuItem
            // 
            this.displayTilesetGridToolStripMenuItem.Checked = true;
            this.displayTilesetGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayTilesetGridToolStripMenuItem.Image = global::MapEditor.Properties.Resources.viewGrid;
            this.displayTilesetGridToolStripMenuItem.Name = "displayTilesetGridToolStripMenuItem";
            this.displayTilesetGridToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + Shift + G";
            this.displayTilesetGridToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.displayTilesetGridToolStripMenuItem.Text = "Tileset Grid";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(216, 6);
            // 
            // highlightSelectedLayerToolStripMenuItem
            // 
            this.highlightSelectedLayerToolStripMenuItem.Name = "highlightSelectedLayerToolStripMenuItem";
            this.highlightSelectedLayerToolStripMenuItem.ShortcutKeyDisplayString = "H";
            this.highlightSelectedLayerToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.highlightSelectedLayerToolStripMenuItem.Text = "&Highlight Selected Layer";
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapPropertiesToolStripMenuItem});
            this.mapToolStripMenuItem.Enabled = false;
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "&Map";
            // 
            // mapPropertiesToolStripMenuItem
            // 
            this.mapPropertiesToolStripMenuItem.Image = global::MapEditor.Properties.Resources.properties;
            this.mapPropertiesToolStripMenuItem.Name = "mapPropertiesToolStripMenuItem";
            this.mapPropertiesToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.mapPropertiesToolStripMenuItem.Text = "Map &Properties...";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLayerToolStripMenuItem,
            this.removeLayerToolStripMenuItem,
            this.toolStripSeparator4,
            this.layerPropertiesToolStripMenuItem,
            this.toolStripMenuItem1});
            this.layerToolStripMenuItem.Enabled = false;
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.layerToolStripMenuItem.Text = "&Layer";
            // 
            // newLayerToolStripMenuItem
            // 
            this.newLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.newLayer;
            this.newLayerToolStripMenuItem.Name = "newLayerToolStripMenuItem";
            this.newLayerToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + Shift + N";
            this.newLayerToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.newLayerToolStripMenuItem.Text = "Add New Layer";
            // 
            // removeLayerToolStripMenuItem
            // 
            this.removeLayerToolStripMenuItem.Enabled = false;
            this.removeLayerToolStripMenuItem.Image = global::MapEditor.Properties.Resources.remove;
            this.removeLayerToolStripMenuItem.Name = "removeLayerToolStripMenuItem";
            this.removeLayerToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + Shift + Del";
            this.removeLayerToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.removeLayerToolStripMenuItem.Text = "&Remove Layer";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(240, 6);
            // 
            // layerPropertiesToolStripMenuItem
            // 
            this.layerPropertiesToolStripMenuItem.Enabled = false;
            this.layerPropertiesToolStripMenuItem.Image = global::MapEditor.Properties.Resources.properties;
            this.layerPropertiesToolStripMenuItem.Name = "layerPropertiesToolStripMenuItem";
            this.layerPropertiesToolStripMenuItem.ShortcutKeyDisplayString = "F4";
            this.layerPropertiesToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.layerPropertiesToolStripMenuItem.Text = "Layer Properties...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(243, 22);
            this.toolStripMenuItem1.Text = "Add Collision Layer";
            // 
            // tilesetToolStripMenuItem
            // 
            this.tilesetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTilesetToolStripMenuItem,
            this.openTilesetToolStripMenuItem,
            this.saveTilesetToolStripMenuItem,
            this.removeTilesetToolStripMenuItem});
            this.tilesetToolStripMenuItem.Enabled = false;
            this.tilesetToolStripMenuItem.Name = "tilesetToolStripMenuItem";
            this.tilesetToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.tilesetToolStripMenuItem.Text = "&Tileset";
            // 
            // newTilesetToolStripMenuItem
            // 
            this.newTilesetToolStripMenuItem.Image = global::MapEditor.Properties.Resources.newFile;
            this.newTilesetToolStripMenuItem.Name = "newTilesetToolStripMenuItem";
            this.newTilesetToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.newTilesetToolStripMenuItem.Text = "Add &New Tileset...";
            // 
            // openTilesetToolStripMenuItem
            // 
            this.openTilesetToolStripMenuItem.Image = global::MapEditor.Properties.Resources.openFile;
            this.openTilesetToolStripMenuItem.Name = "openTilesetToolStripMenuItem";
            this.openTilesetToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.openTilesetToolStripMenuItem.Text = "&Open Tileset...";
            // 
            // saveTilesetToolStripMenuItem
            // 
            this.saveTilesetToolStripMenuItem.Enabled = false;
            this.saveTilesetToolStripMenuItem.Image = global::MapEditor.Properties.Resources.saveFile;
            this.saveTilesetToolStripMenuItem.Name = "saveTilesetToolStripMenuItem";
            this.saveTilesetToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveTilesetToolStripMenuItem.Text = "&Save Tileset...";
            // 
            // removeTilesetToolStripMenuItem
            // 
            this.removeTilesetToolStripMenuItem.Enabled = false;
            this.removeTilesetToolStripMenuItem.Image = global::MapEditor.Properties.Resources.remove;
            this.removeTilesetToolStripMenuItem.Name = "removeTilesetToolStripMenuItem";
            this.removeTilesetToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.removeTilesetToolStripMenuItem.Text = "&Remove Tileset";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.mainToolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuButton,
            this.openMapToolStripMenuButton,
            this.saveMapToolStripMenuButton,
            this.toolStripSeparator1,
            this.displayGridToolStripButton,
            this.toolStripSeparator2,
            this.cbZoom,
            this.zoomOutToolStripMenuButton,
            this.zoomInToolStripMenuButton,
            this.toolStripSeparator3,
            this.drawToolStripMenuButton,
            this.eraseToolStripMenuButton,
            this.toolStripSeparator9,
            this.lblBrushSize,
            this.decreaseBrushSizeToolStripMenuButton,
            this.cbBrushSize,
            this.increaseBrushSizeToolStripMenuButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.mainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mainToolStrip.Size = new System.Drawing.Size(1572, 39);
            this.mainToolStrip.TabIndex = 3;
            this.mainToolStrip.Text = "mainToolStrip";
            // 
            // newMapToolStripMenuButton
            // 
            this.newMapToolStripMenuButton.AutoSize = false;
            this.newMapToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newMapToolStripMenuButton.Image = global::MapEditor.Properties.Resources.newFile;
            this.newMapToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newMapToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newMapToolStripMenuButton.Margin = new System.Windows.Forms.Padding(1);
            this.newMapToolStripMenuButton.Name = "newMapToolStripMenuButton";
            this.newMapToolStripMenuButton.Size = new System.Drawing.Size(27, 31);
            this.newMapToolStripMenuButton.Text = "New Map (Ctrl + N)";
            // 
            // openMapToolStripMenuButton
            // 
            this.openMapToolStripMenuButton.AutoSize = false;
            this.openMapToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openMapToolStripMenuButton.Image = global::MapEditor.Properties.Resources.openFile;
            this.openMapToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openMapToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMapToolStripMenuButton.Margin = new System.Windows.Forms.Padding(1);
            this.openMapToolStripMenuButton.Name = "openMapToolStripMenuButton";
            this.openMapToolStripMenuButton.Size = new System.Drawing.Size(27, 27);
            this.openMapToolStripMenuButton.Text = "Open Map (Ctrl + O)";
            // 
            // saveMapToolStripMenuButton
            // 
            this.saveMapToolStripMenuButton.AutoSize = false;
            this.saveMapToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveMapToolStripMenuButton.Enabled = false;
            this.saveMapToolStripMenuButton.Image = global::MapEditor.Properties.Resources.saveFile;
            this.saveMapToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveMapToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveMapToolStripMenuButton.Margin = new System.Windows.Forms.Padding(1);
            this.saveMapToolStripMenuButton.Name = "saveMapToolStripMenuButton";
            this.saveMapToolStripMenuButton.Size = new System.Drawing.Size(27, 27);
            this.saveMapToolStripMenuButton.Text = "Save Map (Ctrl + S)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // displayGridToolStripButton
            // 
            this.displayGridToolStripButton.AutoSize = false;
            this.displayGridToolStripButton.Checked = true;
            this.displayGridToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayGridToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.displayGridToolStripButton.Image = global::MapEditor.Properties.Resources.viewGrid;
            this.displayGridToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.displayGridToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.displayGridToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.displayGridToolStripButton.Name = "displayGridToolStripButton";
            this.displayGridToolStripButton.Size = new System.Drawing.Size(27, 27);
            this.displayGridToolStripButton.Text = "Toggle Map Grid (Ctrl + G)";
            this.displayGridToolStripButton.ToolTipText = "Toggle Map Grid (Ctrl + G)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(5, 0, 8, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // cbZoom
            // 
            this.cbZoom.AutoSize = false;
            this.cbZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZoom.Enabled = false;
            this.cbZoom.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbZoom.Items.AddRange(new object[] {
            "25 %",
            "50 %",
            "75 %",
            "100 %",
            "150 %",
            "200 %",
            "250 %",
            "300 %",
            "350%",
            "400 %"});
            this.cbZoom.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.cbZoom.Name = "cbZoom";
            this.cbZoom.Size = new System.Drawing.Size(55, 23);
            this.cbZoom.ToolTipText = "Zoom Percentage";
            // 
            // zoomOutToolStripMenuButton
            // 
            this.zoomOutToolStripMenuButton.AutoSize = false;
            this.zoomOutToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutToolStripMenuButton.Image = global::MapEditor.Properties.Resources.zoomOut;
            this.zoomOutToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomOutToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutToolStripMenuButton.Margin = new System.Windows.Forms.Padding(1);
            this.zoomOutToolStripMenuButton.Name = "zoomOutToolStripMenuButton";
            this.zoomOutToolStripMenuButton.Size = new System.Drawing.Size(27, 27);
            this.zoomOutToolStripMenuButton.Text = "Zoom Out (&X)";
            // 
            // zoomInToolStripMenuButton
            // 
            this.zoomInToolStripMenuButton.AutoSize = false;
            this.zoomInToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInToolStripMenuButton.Image = global::MapEditor.Properties.Resources.zoomIn;
            this.zoomInToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomInToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInToolStripMenuButton.Margin = new System.Windows.Forms.Padding(1);
            this.zoomInToolStripMenuButton.Name = "zoomInToolStripMenuButton";
            this.zoomInToolStripMenuButton.Size = new System.Drawing.Size(27, 27);
            this.zoomInToolStripMenuButton.Text = "Zoom In (&Z)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // drawToolStripMenuButton
            // 
            this.drawToolStripMenuButton.AutoSize = false;
            this.drawToolStripMenuButton.Checked = true;
            this.drawToolStripMenuButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawToolStripMenuButton.Image = global::MapEditor.Properties.Resources.draw;
            this.drawToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.drawToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawToolStripMenuButton.Margin = new System.Windows.Forms.Padding(1);
            this.drawToolStripMenuButton.Name = "drawToolStripMenuButton";
            this.drawToolStripMenuButton.Size = new System.Drawing.Size(27, 27);
            this.drawToolStripMenuButton.Text = "Draw (D)";
            // 
            // eraseToolStripMenuButton
            // 
            this.eraseToolStripMenuButton.AutoSize = false;
            this.eraseToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eraseToolStripMenuButton.Image = global::MapEditor.Properties.Resources.erase;
            this.eraseToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eraseToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eraseToolStripMenuButton.Margin = new System.Windows.Forms.Padding(1);
            this.eraseToolStripMenuButton.Name = "eraseToolStripMenuButton";
            this.eraseToolStripMenuButton.Size = new System.Drawing.Size(27, 27);
            this.eraseToolStripMenuButton.Text = "Erase (E)";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 33);
            // 
            // lblBrushSize
            // 
            this.lblBrushSize.Name = "lblBrushSize";
            this.lblBrushSize.Size = new System.Drawing.Size(63, 30);
            this.lblBrushSize.Text = "Brush Size:";
            // 
            // decreaseBrushSizeToolStripMenuButton
            // 
            this.decreaseBrushSizeToolStripMenuButton.AutoSize = false;
            this.decreaseBrushSizeToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.decreaseBrushSizeToolStripMenuButton.Image = global::MapEditor.Properties.Resources.minus;
            this.decreaseBrushSizeToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.decreaseBrushSizeToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.decreaseBrushSizeToolStripMenuButton.Name = "decreaseBrushSizeToolStripMenuButton";
            this.decreaseBrushSizeToolStripMenuButton.Size = new System.Drawing.Size(28, 30);
            this.decreaseBrushSizeToolStripMenuButton.Text = "Decrease Brush Size ( [ )";
            // 
            // cbBrushSize
            // 
            this.cbBrushSize.AutoSize = false;
            this.cbBrushSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBrushSize.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbBrushSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbBrushSize.Name = "cbBrushSize";
            this.cbBrushSize.Size = new System.Drawing.Size(40, 23);
            this.cbBrushSize.ToolTipText = "Brush Size";
            // 
            // increaseBrushSizeToolStripMenuButton
            // 
            this.increaseBrushSizeToolStripMenuButton.AutoSize = false;
            this.increaseBrushSizeToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.increaseBrushSizeToolStripMenuButton.Image = global::MapEditor.Properties.Resources.plus;
            this.increaseBrushSizeToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.increaseBrushSizeToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.increaseBrushSizeToolStripMenuButton.Name = "increaseBrushSizeToolStripMenuButton";
            this.increaseBrushSizeToolStripMenuButton.Size = new System.Drawing.Size(28, 30);
            this.increaseBrushSizeToolStripMenuButton.Text = "Increase Brush Size ( ] )";
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 63);
            this.mainSplitContainer.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.mainSplitContainer.Panel1.Controls.Add(this.vScrollBar);
            this.mainSplitContainer.Panel1.Controls.Add(this.mapDisplay);
            this.mainSplitContainer.Panel1.Controls.Add(this.hScrollBar);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mainSplitContainer.Panel2.Controls.Add(this.rightSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(1572, 738);
            this.mainSplitContainer.SplitterDistance = 1298;
            this.mainSplitContainer.TabIndex = 2;
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar.Enabled = false;
            this.vScrollBar.LargeChange = 60;
            this.vScrollBar.Location = new System.Drawing.Point(1281, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 720);
            this.vScrollBar.TabIndex = 3;
            // 
            // mapDisplay
            // 
            this.mapDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapDisplay.Location = new System.Drawing.Point(0, 0);
            this.mapDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.mapDisplay.Name = "mapDisplay";
            this.mapDisplay.Size = new System.Drawing.Size(1280, 720);
            this.mapDisplay.TabIndex = 2;
            this.mapDisplay.Text = "mapDisplay";
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.Enabled = false;
            this.hScrollBar.LargeChange = 40;
            this.hScrollBar.Location = new System.Drawing.Point(0, 721);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(1280, 17);
            this.hScrollBar.TabIndex = 1;
            // 
            // rightSplitContainer
            // 
            this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplitContainer.IsSplitterFixed = true;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.rightSplitContainer.Name = "rightSplitContainer";
            this.rightSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightSplitContainer.Panel1
            // 
            this.rightSplitContainer.Panel1.Controls.Add(this.gbLayerPanel);
            // 
            // rightSplitContainer.Panel2
            // 
            this.rightSplitContainer.Panel2.Controls.Add(this.gbTilesetPanel);
            this.rightSplitContainer.Panel2.Controls.Add(this.lblTile);
            this.rightSplitContainer.Size = new System.Drawing.Size(270, 738);
            this.rightSplitContainer.SplitterDistance = 215;
            this.rightSplitContainer.SplitterWidth = 5;
            this.rightSplitContainer.TabIndex = 0;
            // 
            // gbLayerPanel
            // 
            this.gbLayerPanel.Controls.Add(this.clbLayers);
            this.gbLayerPanel.Controls.Add(this.layerPanelToolStrip);
            this.gbLayerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLayerPanel.Location = new System.Drawing.Point(0, 0);
            this.gbLayerPanel.Name = "gbLayerPanel";
            this.gbLayerPanel.Size = new System.Drawing.Size(270, 215);
            this.gbLayerPanel.TabIndex = 22;
            this.gbLayerPanel.TabStop = false;
            this.gbLayerPanel.Text = "Layers";
            // 
            // clbLayers
            // 
            this.clbLayers.BackColor = System.Drawing.SystemColors.Window;
            this.clbLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbLayers.FormattingEnabled = true;
            this.clbLayers.Location = new System.Drawing.Point(3, 16);
            this.clbLayers.MaximumSize = new System.Drawing.Size(274, 248);
            this.clbLayers.Name = "clbLayers";
            this.clbLayers.ScrollAlwaysVisible = true;
            this.clbLayers.Size = new System.Drawing.Size(264, 166);
            this.clbLayers.TabIndex = 23;
            this.clbLayers.ThreeDCheckBoxes = true;
            // 
            // layerPanelToolStrip
            // 
            this.layerPanelToolStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.layerPanelToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layerPanelToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.layerPanelToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLayerToolStripButton,
            this.removeLayerToolStripButton,
            this.toolStripSeparator5,
            this.layerMoveUpToolStripButton,
            this.layerMoveDownToolStripButton,
            this.toolStripSeparator6,
            this.layerPropertiesToolStripButton});
            this.layerPanelToolStrip.Location = new System.Drawing.Point(3, 182);
            this.layerPanelToolStrip.Name = "layerPanelToolStrip";
            this.layerPanelToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.layerPanelToolStrip.Size = new System.Drawing.Size(264, 30);
            this.layerPanelToolStrip.TabIndex = 24;
            this.layerPanelToolStrip.Text = "layerPanelToolStrip";
            // 
            // newLayerToolStripButton
            // 
            this.newLayerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newLayerToolStripButton.Enabled = false;
            this.newLayerToolStripButton.Image = global::MapEditor.Properties.Resources.newLayer;
            this.newLayerToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newLayerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newLayerToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.newLayerToolStripButton.Name = "newLayerToolStripButton";
            this.newLayerToolStripButton.Size = new System.Drawing.Size(27, 28);
            this.newLayerToolStripButton.Text = "Add New Layer (Ctrl + Shift + N)";
            // 
            // removeLayerToolStripButton
            // 
            this.removeLayerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeLayerToolStripButton.Enabled = false;
            this.removeLayerToolStripButton.Image = global::MapEditor.Properties.Resources.remove;
            this.removeLayerToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.removeLayerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeLayerToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.removeLayerToolStripButton.Name = "removeLayerToolStripButton";
            this.removeLayerToolStripButton.Size = new System.Drawing.Size(27, 28);
            this.removeLayerToolStripButton.Text = "Remove Layer (Ctrl + Shift + Del)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 30);
            // 
            // layerMoveUpToolStripButton
            // 
            this.layerMoveUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.layerMoveUpToolStripButton.Enabled = false;
            this.layerMoveUpToolStripButton.Image = global::MapEditor.Properties.Resources.upArrow;
            this.layerMoveUpToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.layerMoveUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.layerMoveUpToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.layerMoveUpToolStripButton.Name = "layerMoveUpToolStripButton";
            this.layerMoveUpToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.layerMoveUpToolStripButton.Text = "Move Layer Up";
            // 
            // layerMoveDownToolStripButton
            // 
            this.layerMoveDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.layerMoveDownToolStripButton.Enabled = false;
            this.layerMoveDownToolStripButton.Image = global::MapEditor.Properties.Resources.downArrow;
            this.layerMoveDownToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.layerMoveDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.layerMoveDownToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.layerMoveDownToolStripButton.Name = "layerMoveDownToolStripButton";
            this.layerMoveDownToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.layerMoveDownToolStripButton.Text = "Move Layer Down";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 30);
            // 
            // layerPropertiesToolStripButton
            // 
            this.layerPropertiesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.layerPropertiesToolStripButton.Enabled = false;
            this.layerPropertiesToolStripButton.Image = global::MapEditor.Properties.Resources.properties;
            this.layerPropertiesToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.layerPropertiesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.layerPropertiesToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.layerPropertiesToolStripButton.Name = "layerPropertiesToolStripButton";
            this.layerPropertiesToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.layerPropertiesToolStripButton.Text = "Layer Properties (F4)";
            // 
            // gbTilesetPanel
            // 
            this.gbTilesetPanel.Controls.Add(this.tileSetSplitContainer);
            this.gbTilesetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTilesetPanel.Location = new System.Drawing.Point(0, 0);
            this.gbTilesetPanel.Name = "gbTilesetPanel";
            this.gbTilesetPanel.Size = new System.Drawing.Size(270, 518);
            this.gbTilesetPanel.TabIndex = 15;
            this.gbTilesetPanel.TabStop = false;
            this.gbTilesetPanel.Text = "Tile Sets";
            // 
            // tileSetSplitContainer
            // 
            this.tileSetSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileSetSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.tileSetSplitContainer.IsSplitterFixed = true;
            this.tileSetSplitContainer.Location = new System.Drawing.Point(3, 16);
            this.tileSetSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tileSetSplitContainer.Name = "tileSetSplitContainer";
            this.tileSetSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // tileSetSplitContainer.Panel1
            // 
            this.tileSetSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tileSetSplitContainer.Panel1.Controls.Add(this.btnSetCurrentCollision);
            this.tileSetSplitContainer.Panel1.Controls.Add(this.nudCollisionType);
            this.tileSetSplitContainer.Panel1.Controls.Add(this.btnSetDefaultCollision);
            this.tileSetSplitContainer.Panel1.Controls.Add(this.cbCollisionType);
            this.tileSetSplitContainer.Panel1.Controls.Add(this.panelTileset);
            this.tileSetSplitContainer.Panel1.Controls.Add(this.pbTilePreview);
            this.tileSetSplitContainer.Panel1.Controls.Add(this.nudCurrentTile);
            // 
            // tileSetSplitContainer.Panel2
            // 
            this.tileSetSplitContainer.Panel2.Controls.Add(this.lbTileset);
            this.tileSetSplitContainer.Panel2.Controls.Add(this.tileSetPanelToolStrip);
            this.tileSetSplitContainer.Size = new System.Drawing.Size(264, 499);
            this.tileSetSplitContainer.SplitterDistance = 357;
            this.tileSetSplitContainer.TabIndex = 0;
            // 
            // nudCollisionType
            // 
            this.nudCollisionType.Location = new System.Drawing.Point(177, 31);
            this.nudCollisionType.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudCollisionType.Name = "nudCollisionType";
            this.nudCollisionType.Size = new System.Drawing.Size(83, 20);
            this.nudCollisionType.TabIndex = 33;
            this.nudCollisionType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSetDefaultCollision
            // 
            this.btnSetDefaultCollision.Location = new System.Drawing.Point(177, 65);
            this.btnSetDefaultCollision.Name = "btnSetDefaultCollision";
            this.btnSetDefaultCollision.Size = new System.Drawing.Size(84, 23);
            this.btnSetDefaultCollision.TabIndex = 32;
            this.btnSetDefaultCollision.Text = "Set As Default";
            this.btnSetDefaultCollision.UseVisualStyleBackColor = true;
            // 
            // cbCollisionType
            // 
            this.cbCollisionType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCollisionType.FormattingEnabled = true;
            this.cbCollisionType.Location = new System.Drawing.Point(177, 3);
            this.cbCollisionType.Name = "cbCollisionType";
            this.cbCollisionType.Size = new System.Drawing.Size(84, 21);
            this.cbCollisionType.TabIndex = 31;
            // 
            // panelTileset
            // 
            this.panelTileset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTileset.AutoScroll = true;
            this.panelTileset.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelTileset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTileset.Controls.Add(this.pbTilesetPreview);
            this.panelTileset.Location = new System.Drawing.Point(2, 96);
            this.panelTileset.MaximumSize = new System.Drawing.Size(261, 261);
            this.panelTileset.Name = "panelTileset";
            this.panelTileset.Size = new System.Drawing.Size(261, 261);
            this.panelTileset.TabIndex = 30;
            // 
            // pbTilesetPreview
            // 
            this.pbTilesetPreview.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbTilesetPreview.Location = new System.Drawing.Point(0, 0);
            this.pbTilesetPreview.Name = "pbTilesetPreview";
            this.pbTilesetPreview.Size = new System.Drawing.Size(256, 256);
            this.pbTilesetPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbTilesetPreview.TabIndex = 5;
            this.pbTilesetPreview.TabStop = false;
            // 
            // pbTilePreview
            // 
            this.pbTilePreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTilePreview.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbTilePreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbTilePreview.Location = new System.Drawing.Point(3, 3);
            this.pbTilePreview.Name = "pbTilePreview";
            this.pbTilePreview.Size = new System.Drawing.Size(66, 64);
            this.pbTilePreview.TabIndex = 27;
            this.pbTilePreview.TabStop = false;
            // 
            // nudCurrentTile
            // 
            this.nudCurrentTile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudCurrentTile.Location = new System.Drawing.Point(3, 68);
            this.nudCurrentTile.Name = "nudCurrentTile";
            this.nudCurrentTile.Size = new System.Drawing.Size(65, 20);
            this.nudCurrentTile.TabIndex = 28;
            this.nudCurrentTile.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbTileset
            // 
            this.lbTileset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTileset.BackColor = System.Drawing.SystemColors.Window;
            this.lbTileset.FormattingEnabled = true;
            this.lbTileset.Location = new System.Drawing.Point(0, 0);
            this.lbTileset.Name = "lbTileset";
            this.lbTileset.ScrollAlwaysVisible = true;
            this.lbTileset.Size = new System.Drawing.Size(264, 108);
            this.lbTileset.TabIndex = 30;
            // 
            // tileSetPanelToolStrip
            // 
            this.tileSetPanelToolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tileSetPanelToolStrip.AutoSize = false;
            this.tileSetPanelToolStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.tileSetPanelToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.tileSetPanelToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tileSetPanelToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTilesetToolStripButton,
            this.openTilesetToolStripButton,
            this.saveTilesetToolStripButton,
            this.removeTilesetToolStripButton,
            this.toolStripSeparator7,
            this.displayTilesetGridToolStripButton,
            this.toolStripSeparator12,
            this.zoomOutTilesetToolStripMenuButton,
            this.zoomInTilesetToolStripMenuButton,
            this.toolStripSeparator8,
            this.renameTilesetToolStripButton});
            this.tileSetPanelToolStrip.Location = new System.Drawing.Point(0, 108);
            this.tileSetPanelToolStrip.Name = "tileSetPanelToolStrip";
            this.tileSetPanelToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tileSetPanelToolStrip.Size = new System.Drawing.Size(267, 30);
            this.tileSetPanelToolStrip.TabIndex = 31;
            // 
            // newTilesetToolStripButton
            // 
            this.newTilesetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newTilesetToolStripButton.Enabled = false;
            this.newTilesetToolStripButton.Image = global::MapEditor.Properties.Resources.newFile;
            this.newTilesetToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newTilesetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newTilesetToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.newTilesetToolStripButton.Name = "newTilesetToolStripButton";
            this.newTilesetToolStripButton.Size = new System.Drawing.Size(27, 28);
            this.newTilesetToolStripButton.Text = "Add New Tile Set";
            // 
            // openTilesetToolStripButton
            // 
            this.openTilesetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openTilesetToolStripButton.Enabled = false;
            this.openTilesetToolStripButton.Image = global::MapEditor.Properties.Resources.openFile;
            this.openTilesetToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openTilesetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTilesetToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.openTilesetToolStripButton.Name = "openTilesetToolStripButton";
            this.openTilesetToolStripButton.Size = new System.Drawing.Size(27, 28);
            this.openTilesetToolStripButton.Text = "Open Tile Set";
            // 
            // saveTilesetToolStripButton
            // 
            this.saveTilesetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveTilesetToolStripButton.Enabled = false;
            this.saveTilesetToolStripButton.Image = global::MapEditor.Properties.Resources.saveFile;
            this.saveTilesetToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveTilesetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveTilesetToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.saveTilesetToolStripButton.Name = "saveTilesetToolStripButton";
            this.saveTilesetToolStripButton.Size = new System.Drawing.Size(27, 28);
            this.saveTilesetToolStripButton.Text = "Save Tile Set";
            // 
            // removeTilesetToolStripButton
            // 
            this.removeTilesetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeTilesetToolStripButton.Enabled = false;
            this.removeTilesetToolStripButton.Image = global::MapEditor.Properties.Resources.remove;
            this.removeTilesetToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.removeTilesetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeTilesetToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.removeTilesetToolStripButton.Name = "removeTilesetToolStripButton";
            this.removeTilesetToolStripButton.Size = new System.Drawing.Size(27, 28);
            this.removeTilesetToolStripButton.Text = "Remove Tile Set";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 30);
            // 
            // displayTilesetGridToolStripButton
            // 
            this.displayTilesetGridToolStripButton.Checked = true;
            this.displayTilesetGridToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayTilesetGridToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.displayTilesetGridToolStripButton.Image = global::MapEditor.Properties.Resources.viewGrid;
            this.displayTilesetGridToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.displayTilesetGridToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.displayTilesetGridToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.displayTilesetGridToolStripButton.Name = "displayTilesetGridToolStripButton";
            this.displayTilesetGridToolStripButton.Size = new System.Drawing.Size(27, 28);
            this.displayTilesetGridToolStripButton.Text = "Toggle Tile Set Grid";
            this.displayTilesetGridToolStripButton.ToolTipText = "Toggle Tile Set Grid";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 30);
            // 
            // zoomOutTilesetToolStripMenuButton
            // 
            this.zoomOutTilesetToolStripMenuButton.AutoSize = false;
            this.zoomOutTilesetToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutTilesetToolStripMenuButton.Image = global::MapEditor.Properties.Resources.zoomOut;
            this.zoomOutTilesetToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomOutTilesetToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutTilesetToolStripMenuButton.Margin = new System.Windows.Forms.Padding(1);
            this.zoomOutTilesetToolStripMenuButton.Name = "zoomOutTilesetToolStripMenuButton";
            this.zoomOutTilesetToolStripMenuButton.Size = new System.Drawing.Size(23, 27);
            this.zoomOutTilesetToolStripMenuButton.ToolTipText = "Zoom Out";
            // 
            // zoomInTilesetToolStripMenuButton
            // 
            this.zoomInTilesetToolStripMenuButton.AutoSize = false;
            this.zoomInTilesetToolStripMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInTilesetToolStripMenuButton.Image = global::MapEditor.Properties.Resources.zoomIn;
            this.zoomInTilesetToolStripMenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomInTilesetToolStripMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInTilesetToolStripMenuButton.Margin = new System.Windows.Forms.Padding(1);
            this.zoomInTilesetToolStripMenuButton.Name = "zoomInTilesetToolStripMenuButton";
            this.zoomInTilesetToolStripMenuButton.Size = new System.Drawing.Size(23, 27);
            this.zoomInTilesetToolStripMenuButton.ToolTipText = "Zoom In";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 30);
            // 
            // renameTilesetToolStripButton
            // 
            this.renameTilesetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renameTilesetToolStripButton.Enabled = false;
            this.renameTilesetToolStripButton.Image = global::MapEditor.Properties.Resources.properties;
            this.renameTilesetToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.renameTilesetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renameTilesetToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.renameTilesetToolStripButton.Name = "renameTilesetToolStripButton";
            this.renameTilesetToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.renameTilesetToolStripButton.Text = "Rename Tile Set";
            // 
            // lblTile
            // 
            this.lblTile.Location = new System.Drawing.Point(23, -52);
            this.lblTile.Name = "lblTile";
            this.lblTile.Size = new System.Drawing.Size(75, 14);
            this.lblTile.TabIndex = 14;
            this.lblTile.Text = "Selected Tile";
            this.lblTile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSetCurrentCollision
            // 
            this.btnSetCurrentCollision.Location = new System.Drawing.Point(123, 65);
            this.btnSetCurrentCollision.Name = "btnSetCurrentCollision";
            this.btnSetCurrentCollision.Size = new System.Drawing.Size(48, 23);
            this.btnSetCurrentCollision.TabIndex = 34;
            this.btnSetCurrentCollision.Text = "Set";
            this.btnSetCurrentCollision.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1572, 823);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Designer";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            this.rightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            this.gbLayerPanel.ResumeLayout(false);
            this.gbLayerPanel.PerformLayout();
            this.layerPanelToolStrip.ResumeLayout(false);
            this.layerPanelToolStrip.PerformLayout();
            this.gbTilesetPanel.ResumeLayout(false);
            this.tileSetSplitContainer.Panel1.ResumeLayout(false);
            this.tileSetSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tileSetSplitContainer)).EndInit();
            this.tileSetSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCollisionType)).EndInit();
            this.panelTileset.ResumeLayout(false);
            this.panelTileset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTilesetPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTilePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurrentTile)).EndInit();
            this.tileSetPanelToolStrip.ResumeLayout(false);
            this.tileSetPanelToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeTilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLayerToolStripMenuItem;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.Timer controlTimer;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusStripMapLocation;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton newMapToolStripMenuButton;
        private System.Windows.Forms.ToolStripButton openMapToolStripMenuButton;
        private System.Windows.Forms.ToolStripButton saveMapToolStripMenuButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton displayGridToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox cbZoom;
        private System.Windows.Forms.ToolStripButton zoomInToolStripMenuButton;
        private System.Windows.Forms.ToolStripButton zoomOutToolStripMenuButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton drawToolStripMenuButton;
        private System.Windows.Forms.ToolStripButton eraseToolStripMenuButton;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem layerPropertiesToolStripMenuItem;
        private System.Windows.Forms.SplitContainer rightSplitContainer;
        private System.Windows.Forms.Label lblTile;
        private System.Windows.Forms.GroupBox gbLayerPanel;
        private System.Windows.Forms.ToolStrip layerPanelToolStrip;
        private System.Windows.Forms.ToolStripButton newLayerToolStripButton;
        private System.Windows.Forms.ToolStripButton removeLayerToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton layerMoveUpToolStripButton;
        private System.Windows.Forms.ToolStripButton layerMoveDownToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton layerPropertiesToolStripButton;
        private System.Windows.Forms.CheckedListBox clbLayers;
        private System.Windows.Forms.GroupBox gbTilesetPanel;
        private System.Windows.Forms.SplitContainer tileSetSplitContainer;
        private System.Windows.Forms.Panel panelTileset;
        private System.Windows.Forms.PictureBox pbTilesetPreview;
        private System.Windows.Forms.PictureBox pbTilePreview;
        private System.Windows.Forms.NumericUpDown nudCurrentTile;
        private System.Windows.Forms.ToolStrip tileSetPanelToolStrip;
        private System.Windows.Forms.ToolStripButton newTilesetToolStripButton;
        private System.Windows.Forms.ToolStripButton openTilesetToolStripButton;
        private System.Windows.Forms.ToolStripButton saveTilesetToolStripButton;
        private System.Windows.Forms.ToolStripButton removeTilesetToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton displayTilesetGridToolStripButton;
        private System.Windows.Forms.ListBox lbTileset;
        private System.Windows.Forms.ToolStripMenuItem displayTilesetGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusStripDebug;
        private System.Windows.Forms.ToolStripButton renameTilesetToolStripButton;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private MapDisplay mapDisplay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton decreaseBrushSizeToolStripMenuButton;
        private System.Windows.Forms.ToolStripComboBox cbBrushSize;
        private System.Windows.Forms.ToolStripButton increaseBrushSizeToolStripMenuButton;
        private System.Windows.Forms.ToolStripLabel lblBrushSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem highlightSelectedLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton zoomOutTilesetToolStripMenuButton;
        private System.Windows.Forms.ToolStripButton zoomInTilesetToolStripMenuButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel statusStripMapDimensions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button btnSetDefaultCollision;
        private System.Windows.Forms.ComboBox cbCollisionType;
        private System.Windows.Forms.NumericUpDown nudCollisionType;
        private System.Windows.Forms.Button btnSetCurrentCollision;
    }
}