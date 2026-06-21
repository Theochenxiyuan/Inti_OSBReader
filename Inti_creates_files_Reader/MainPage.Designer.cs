namespace Inti_creates_files_Reader
{
    partial class MainPage
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
            menuStrip1 = new MenuStrip();
            toolTip1 = new ToolTip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            openFolderToolStripMenuItem = new ToolStripMenuItem();
            openColorPaletteToolStripMenuItem = new ToolStripMenuItem();
            recentFilesToolStripMenuItem = new ToolStripMenuItem();
            recentFoldersToolStripMenuItem = new ToolStripMenuItem();
            saveAllPaletteToolStripMenuItem = new ToolStripMenuItem();
            saveAnimationToolStripMenuItem = new ToolStripMenuItem();
            saveAllAnimationsToolStripMenuItem = new ToolStripMenuItem();
            savePaletteToolStripMenuItem = new ToolStripMenuItem();
            saveAllPaletteToolStripMenuItem1 = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            documnetedDataToolStripMenuItem = new ToolStripMenuItem();
            allToolStripMenuItem = new ToolStripMenuItem();
            fileHeaderToolStripMenuItem = new ToolStripMenuItem();
            spriteSheetToolStripMenuItem1 = new ToolStripMenuItem();
            animationsToolStripMenuItem = new ToolStripMenuItem();
            framesToolStripMenuItem = new ToolStripMenuItem();
            colorPaletteToolStripMenuItem = new ToolStripMenuItem();
            fremesRelatedDataToolStripMenuItem = new ToolStripMenuItem();
            spriteSheetToolStripMenuItem = new ToolStripMenuItem();
            mainSplitContainer = new SplitContainer();
            FolderTree = new TreeView();
            contentSplitContainer = new SplitContainer();
            AnimationList = new ListView();
            Animation = new ColumnHeader();
            rightPanel = new Panel();
            contentAreaPanel = new Panel();
            pic = new PictureBox();
            controlsPanel = new Panel();
            CLoop = new CheckBox();
            Lspeed = new Label();
            Tspeed = new TextBox();
            Flip = new Button();
            BplayPause = new Button();
            BsaveGif = new Button();
            bottomTable = new TableLayoutPanel();
            paletteControlsPanel = new Panel();
            Lpallete = new Label();
            BleftPalette = new Button();
            BrightPalette = new Button();
            Lpalletecount = new Label();
            picPalatte = new PictureBox();
            infoPanel = new Panel();
            LcurrFrame = new Label();
            TcurrFrame = new TextBox();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)contentSplitContainer).BeginInit();
            contentSplitContainer.Panel1.SuspendLayout();
            contentSplitContainer.Panel2.SuspendLayout();
            contentSplitContainer.SuspendLayout();
            rightPanel.SuspendLayout();
            contentAreaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            controlsPanel.SuspendLayout();
            bottomTable.SuspendLayout();
            paletteControlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPalatte).BeginInit();
            infoPanel.SuspendLayout();
            SuspendLayout();
            //
            // menuStrip1
            //
            menuStrip1.Dock = DockStyle.Top;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, saveToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1082, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip";
            //
            // fileToolStripMenuItem
            //
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, openFolderToolStripMenuItem, openColorPaletteToolStripMenuItem, recentFilesToolStripMenuItem, recentFoldersToolStripMenuItem, saveAllPaletteToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            //
            // openToolStripMenuItem
            //
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+E";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            openToolStripMenuItem.Size = new Size(228, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += Bopen_Click;
            //
            // openFolderToolStripMenuItem
            //
            openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            openFolderToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+E";
            openFolderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.E;
            openFolderToolStripMenuItem.Size = new Size(228, 22);
            openFolderToolStripMenuItem.Text = "Open Folder";
            openFolderToolStripMenuItem.Click += openFolderToolStripMenuItem_Click;
            //
            // openColorPaletteToolStripMenuItem
            //
            openColorPaletteToolStripMenuItem.Name = "openColorPaletteToolStripMenuItem";
            openColorPaletteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+E";
            openColorPaletteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.E;
            openColorPaletteToolStripMenuItem.Size = new Size(228, 22);
            openColorPaletteToolStripMenuItem.Text = "Add color palette";
            openColorPaletteToolStripMenuItem.Click += openColorPaletteToolStripMenuItem_Click;
            // 
            // recentFilesToolStripMenuItem
            // 
            recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            recentFilesToolStripMenuItem.Size = new Size(228, 22);
            recentFilesToolStripMenuItem.Text = "Recent Files";
            // 
            // recentFoldersToolStripMenuItem
            // 
            recentFoldersToolStripMenuItem.Name = "recentFoldersToolStripMenuItem";
            recentFoldersToolStripMenuItem.Size = new Size(228, 22);
            recentFoldersToolStripMenuItem.Text = "Recent Folders";
            // 
            // saveAllPaletteToolStripMenuItem

            //
            saveAllPaletteToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveAnimationToolStripMenuItem, saveAllAnimationsToolStripMenuItem, savePaletteToolStripMenuItem, saveAllPaletteToolStripMenuItem1 });
            saveAllPaletteToolStripMenuItem.Name = "saveAllPaletteToolStripMenuItem";
            saveAllPaletteToolStripMenuItem.Size = new Size(228, 22);
            saveAllPaletteToolStripMenuItem.Text = "Save";
            //
            // saveAnimationToolStripMenuItem
            //
            saveAnimationToolStripMenuItem.Name = "saveAnimationToolStripMenuItem";
            saveAnimationToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            saveAnimationToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveAnimationToolStripMenuItem.Size = new Size(255, 22);
            saveAnimationToolStripMenuItem.Text = "Animation";
            saveAnimationToolStripMenuItem.Click += saveAnimationToolStripMenuItem_Click;
            //
            // saveAllAnimationsToolStripMenuItem
            //
            saveAllAnimationsToolStripMenuItem.Name = "saveAllAnimationsToolStripMenuItem";
            saveAllAnimationsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+S";
            saveAllAnimationsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAllAnimationsToolStripMenuItem.Size = new Size(255, 22);
            saveAllAnimationsToolStripMenuItem.Text = "All Animations";
            saveAllAnimationsToolStripMenuItem.Click += saveAllAnimationsToolStripMenuItem_Click;
            //
            // savePaletteToolStripMenuItem
            //
            savePaletteToolStripMenuItem.Name = "savePaletteToolStripMenuItem";
            savePaletteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+S";
            savePaletteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.S;
            savePaletteToolStripMenuItem.Size = new Size(255, 22);
            savePaletteToolStripMenuItem.Text = "Color Palette";
            savePaletteToolStripMenuItem.Click += savePaletteToolStripMenuItem_Click;
            //
            // saveAllPaletteToolStripMenuItem1
            //
            saveAllPaletteToolStripMenuItem1.Name = "saveAllPaletteToolStripMenuItem1";
            saveAllPaletteToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+Shift+Alt+S";
            saveAllPaletteToolStripMenuItem1.ShortcutKeys = Keys.Control | Keys.Alt | Keys.Shift | Keys.S;
            saveAllPaletteToolStripMenuItem1.Size = new Size(255, 22);
            saveAllPaletteToolStripMenuItem1.Text = "All Color Palettes";
            saveAllPaletteToolStripMenuItem1.Click += saveAllPaletteToolStripMenuItem1_Click;
            //
            // saveToolStripMenuItem
            //
            saveToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { documnetedDataToolStripMenuItem, spriteSheetToolStripMenuItem });
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(44, 20);
            saveToolStripMenuItem.Text = "View";
            //
            // documnetedDataToolStripMenuItem
            //
            documnetedDataToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { allToolStripMenuItem, fileHeaderToolStripMenuItem, spriteSheetToolStripMenuItem1, animationsToolStripMenuItem, framesToolStripMenuItem, colorPaletteToolStripMenuItem, fremesRelatedDataToolStripMenuItem });
            documnetedDataToolStripMenuItem.Name = "documnetedDataToolStripMenuItem";
            documnetedDataToolStripMenuItem.Size = new Size(179, 22);
            documnetedDataToolStripMenuItem.Text = "Documented data";
            //
            // allToolStripMenuItem
            //
            allToolStripMenuItem.Name = "allToolStripMenuItem";
            allToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+D";
            allToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D;
            allToolStripMenuItem.Size = new Size(175, 22);
            allToolStripMenuItem.Text = "All";
            allToolStripMenuItem.Click += allToolStripMenuItem_Click;
            //
            // fileHeaderToolStripMenuItem
            //
            fileHeaderToolStripMenuItem.Name = "fileHeaderToolStripMenuItem";
            fileHeaderToolStripMenuItem.Size = new Size(175, 22);
            fileHeaderToolStripMenuItem.Text = "File Header";
            fileHeaderToolStripMenuItem.Click += fileHeaderToolStripMenuItem_Click;
            //
            // spriteSheetToolStripMenuItem1
            //
            spriteSheetToolStripMenuItem1.Name = "spriteSheetToolStripMenuItem1";
            spriteSheetToolStripMenuItem1.Size = new Size(175, 22);
            spriteSheetToolStripMenuItem1.Text = "Sprite Sheet";
            spriteSheetToolStripMenuItem1.Click += spriteSheetToolStripMenuItem1_Click;
            //
            // animationsToolStripMenuItem
            //
            animationsToolStripMenuItem.Name = "animationsToolStripMenuItem";
            animationsToolStripMenuItem.Size = new Size(175, 22);
            animationsToolStripMenuItem.Text = "Animations";
            animationsToolStripMenuItem.Click += animationsToolStripMenuItem_Click;
            //
            // framesToolStripMenuItem
            //
            framesToolStripMenuItem.Name = "framesToolStripMenuItem";
            framesToolStripMenuItem.Size = new Size(175, 22);
            framesToolStripMenuItem.Text = "Frames";
            framesToolStripMenuItem.Click += framesToolStripMenuItem_Click;
            //
            // colorPaletteToolStripMenuItem
            //
            colorPaletteToolStripMenuItem.Name = "colorPaletteToolStripMenuItem";
            colorPaletteToolStripMenuItem.Size = new Size(175, 22);
            colorPaletteToolStripMenuItem.Text = "Color palette";
            colorPaletteToolStripMenuItem.Click += colorPaletteToolStripMenuItem_Click;
            //
            // fremesRelatedDataToolStripMenuItem
            //
            fremesRelatedDataToolStripMenuItem.Name = "fremesRelatedDataToolStripMenuItem";
            fremesRelatedDataToolStripMenuItem.Size = new Size(175, 22);
            fremesRelatedDataToolStripMenuItem.Text = "Frames related data";
            fremesRelatedDataToolStripMenuItem.Click += fremesRelatedDataToolStripMenuItem_Click;
            //
            // spriteSheetToolStripMenuItem
            //
            spriteSheetToolStripMenuItem.Name = "spriteSheetToolStripMenuItem";
            spriteSheetToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Q";
            spriteSheetToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            spriteSheetToolStripMenuItem.Size = new Size(179, 22);
            spriteSheetToolStripMenuItem.Text = "Sprite Sheet";
            spriteSheetToolStripMenuItem.Click += spriteSheetToolStripMenuItem_Click;
            //
            // mainSplitContainer
            //
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.FixedPanel = FixedPanel.Panel1;
            mainSplitContainer.Location = new Point(0, 24);
            mainSplitContainer.Name = "mainSplitContainer";
            //
            // mainSplitContainer.Panel1
            //
            mainSplitContainer.Panel1.Controls.Add(FolderTree);
            //
            // mainSplitContainer.Panel2
            //
            mainSplitContainer.Panel2.Controls.Add(contentSplitContainer);
            mainSplitContainer.Size = new Size(1082, 622);
            mainSplitContainer.SplitterDistance = 174;
            mainSplitContainer.TabIndex = 1;
            //
            // FolderTree
            //
            FolderTree.Dock = DockStyle.Fill;
            FolderTree.HideSelection = false;
            FolderTree.Location = new Point(0, 0);
            FolderTree.Name = "FolderTree";
            FolderTree.Size = new Size(174, 622);
            FolderTree.TabIndex = 0;
            FolderTree.AfterSelect += FolderTree_AfterSelect;
            FolderTree.KeyDown += FolderTree_KeyDown;
            //
            // contentSplitContainer
            //
            contentSplitContainer.Dock = DockStyle.Fill;
            contentSplitContainer.FixedPanel = FixedPanel.Panel1;
            contentSplitContainer.Location = new Point(0, 0);
            contentSplitContainer.Name = "contentSplitContainer";
            //
            // contentSplitContainer.Panel1
            //
            contentSplitContainer.Panel1.Controls.Add(AnimationList);
            //
            // contentSplitContainer.Panel2
            //
            contentSplitContainer.Panel2.Controls.Add(rightPanel);
            contentSplitContainer.Size = new Size(904, 622);
            contentSplitContainer.SplitterDistance = 133;
            contentSplitContainer.TabIndex = 0;
            //
            // AnimationList
            //
            AnimationList.Alignment = ListViewAlignment.SnapToGrid;
            AnimationList.Columns.AddRange(new ColumnHeader[] { Animation });
            AnimationList.Dock = DockStyle.Fill;
            AnimationList.FullRowSelect = true;
            AnimationList.HeaderStyle = ColumnHeaderStyle.None;
            AnimationList.LabelWrap = false;
            AnimationList.Location = new Point(0, 0);
            AnimationList.MultiSelect = false;
            AnimationList.Name = "AnimationList";
            AnimationList.Size = new Size(133, 622);
            AnimationList.TabIndex = 0;
            AnimationList.UseCompatibleStateImageBehavior = false;
            AnimationList.View = View.Details;
            AnimationList.ItemSelectionChanged += AnimationList_ItemSelectionChanged;
            AnimationList.SizeChanged += AnimationList_SizeChanged;
            //
            // Animation
            //
            Animation.Width = 129;
            //
            // rightPanel
            //
            rightPanel.Controls.Add(contentAreaPanel);
            rightPanel.Controls.Add(controlsPanel);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(0, 0);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(767, 622);
            rightPanel.TabIndex = 0;
            //
            // contentAreaPanel
            //
            contentAreaPanel.Controls.Add(pic);
            contentAreaPanel.Controls.Add(bottomTable);
            contentAreaPanel.Controls.Add(infoPanel);
            contentAreaPanel.Dock = DockStyle.Fill;
            contentAreaPanel.Location = new Point(0, 0);
            contentAreaPanel.Name = "contentAreaPanel";
            contentAreaPanel.Padding = new Padding(6);
            contentAreaPanel.Size = new Size(695, 622);
            contentAreaPanel.TabIndex = 0;
            //
            // pic
            //
            pic.BackColor = SystemColors.ControlDarkDark;
            pic.Dock = DockStyle.Fill;
            pic.Location = new Point(6, 48);
            pic.Margin = new Padding(0);
            pic.Name = "pic";
            pic.Size = new Size(683, 492);
            pic.TabIndex = 0;
            pic.TabStop = false;
            //
            // controlsPanel
            //
            controlsPanel.Dock = DockStyle.Right;
            controlsPanel.Controls.Add(CLoop);
            controlsPanel.Controls.Add(Lspeed);
            controlsPanel.Controls.Add(Tspeed);
            controlsPanel.Controls.Add(Flip);
            controlsPanel.Controls.Add(BplayPause);
            controlsPanel.Controls.Add(BsaveGif);
            controlsPanel.Location = new Point(695, 0);
            controlsPanel.Name = "controlsPanel";
            controlsPanel.Padding = new Padding(3, 6, 3, 3);
            controlsPanel.Size = new Size(72, 622);
            controlsPanel.TabIndex = 1;
            //
            // CLoop
            //
            CLoop.AutoSize = true;
            CLoop.Location = new Point(3, 3);
            CLoop.Name = "CLoop";
            CLoop.Size = new Size(53, 19);
            CLoop.TabIndex = 0;
            CLoop.Text = "Loop";
            CLoop.UseVisualStyleBackColor = true;
            CLoop.CheckedChanged += CLoop_CheckedChanged;
            //
            // Lspeed
            //
            Lspeed.AutoSize = true;
            Lspeed.Location = new Point(3, 28);
            Lspeed.Name = "Lspeed";
            Lspeed.Size = new Size(39, 15);
            Lspeed.TabIndex = 1;
            Lspeed.Text = "Speed";
            //
            // Tspeed
            //
            Tspeed.Location = new Point(3, 46);
            Tspeed.Name = "Tspeed";
            Tspeed.Size = new Size(60, 23);
            Tspeed.TabIndex = 2;
            Tspeed.Leave += Tspeed_Leave;
            //
            // Flip
            //
            Flip.Location = new Point(3, 75);
            Flip.Name = "Flip";
            Flip.Size = new Size(60, 23);
            Flip.TabIndex = 3;
            Flip.Text = "Flip";
            Flip.UseVisualStyleBackColor = true;
            Flip.Click += Flip_Click;
            //
            // BplayPause
            //
            BplayPause.Location = new Point(3, 104);
            BplayPause.Name = "BplayPause";
            BplayPause.Size = new Size(60, 23);
            BplayPause.TabIndex = 4;
            BplayPause.Text = "Pause";
            BplayPause.UseVisualStyleBackColor = true;
            BplayPause.Click += BplayPause_Click;
            //
            // BsaveGif
            //
            BsaveGif.Location = new Point(3, 133);
            BsaveGif.Name = "BsaveGif";
            BsaveGif.Size = new Size(60, 23);
            BsaveGif.TabIndex = 5;
            BsaveGif.Text = "GIF";
            BsaveGif.UseVisualStyleBackColor = true;
            BsaveGif.Click += saveAnimationToolStripMenuItem_Click;
            toolTip1.SetToolTip(BleftPalette, "Previous palette");
            toolTip1.SetToolTip(BrightPalette, "Next palette");
            toolTip1.SetToolTip(Flip, "Flip horizontally");
            toolTip1.SetToolTip(BplayPause, "Pause/Play (Space)");
            toolTip1.SetToolTip(BsaveGif, "Export current animation as GIF");
            //
            // bottomTable
            //
            bottomTable.ColumnCount = 2;
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F));
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            bottomTable.Controls.Add(paletteControlsPanel, 0, 0);
            bottomTable.Controls.Add(picPalatte, 1, 0);
            bottomTable.Dock = DockStyle.Bottom;
            bottomTable.Location = new Point(6, 540);
            bottomTable.Name = "bottomTable";
            bottomTable.RowCount = 1;
            bottomTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            bottomTable.Size = new Size(683, 76);
            bottomTable.TabIndex = 2;
            //
            // paletteControlsPanel
            //
            paletteControlsPanel.Controls.Add(Lpallete);
            paletteControlsPanel.Controls.Add(BleftPalette);
            paletteControlsPanel.Controls.Add(BrightPalette);
            paletteControlsPanel.Controls.Add(Lpalletecount);
            paletteControlsPanel.Dock = DockStyle.Fill;
            paletteControlsPanel.Location = new Point(0, 0);
            paletteControlsPanel.Margin = new Padding(0);
            paletteControlsPanel.Name = "paletteControlsPanel";
            paletteControlsPanel.Size = new Size(240, 76);
            paletteControlsPanel.TabIndex = 0;
            //
            // Lpallete
            //
            Lpallete.AutoSize = true;
            Lpallete.Location = new Point(3, 7);
            Lpallete.Name = "Lpallete";
            Lpallete.Size = new Size(45, 15);
            Lpallete.TabIndex = 0;
            Lpallete.Text = "Palette:";
            //
            // BleftPalette
            //
            BleftPalette.Location = new Point(3, 25);
            BleftPalette.Name = "BleftPalette";
            BleftPalette.Size = new Size(39, 27);
            BleftPalette.TabIndex = 1;
            BleftPalette.Text = "<";
            BleftPalette.UseVisualStyleBackColor = true;
            BleftPalette.Click += BleftPalette_Click;
            //
            // BrightPalette
            //
            BrightPalette.Location = new Point(48, 25);
            BrightPalette.Name = "BrightPalette";
            BrightPalette.Size = new Size(42, 27);
            BrightPalette.TabIndex = 2;
            BrightPalette.Text = ">";
            BrightPalette.UseVisualStyleBackColor = true;
            BrightPalette.Click += BrightPalette_Click;
            //
            // Lpalletecount
            //
            Lpalletecount.AutoSize = true;
            Lpalletecount.Location = new Point(3, 55);
            Lpalletecount.Name = "Lpalletecount";
            Lpalletecount.Size = new Size(57, 15);
            Lpalletecount.TabIndex = 3;
            Lpalletecount.Text = "—";
            //
            // picPalatte
            //
            picPalatte.BackColor = SystemColors.ControlDarkDark;
            picPalatte.Dock = DockStyle.Fill;
            picPalatte.Location = new Point(240, 0);
            picPalatte.Margin = new Padding(0);
            picPalatte.Name = "picPalatte";
            picPalatte.Size = new Size(518, 76);
            picPalatte.TabIndex = 1;
            picPalatte.TabStop = false;
            //
            // infoPanel
            //
            infoPanel.Controls.Add(LcurrFrame);
            infoPanel.Controls.Add(TcurrFrame);
            infoPanel.Dock = DockStyle.Top;
            infoPanel.Location = new Point(6, 6);
            infoPanel.Name = "infoPanel";
            infoPanel.Size = new Size(683, 42);
            infoPanel.TabIndex = 3;
            //
            // LcurrFrame
            //
            LcurrFrame.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LcurrFrame.AutoSize = true;
            LcurrFrame.Location = new Point(491, 3);
            LcurrFrame.Name = "LcurrFrame";
            LcurrFrame.Size = new Size(86, 15);
            LcurrFrame.TabIndex = 2;
            LcurrFrame.Text = "Current Frame:";
            //
            // TcurrFrame
            //
            TcurrFrame.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TcurrFrame.Enabled = false;
            TcurrFrame.Location = new Point(583, 0);
            TcurrFrame.Name = "TcurrFrame";
            TcurrFrame.Size = new Size(100, 23);
            TcurrFrame.TabIndex = 3;
            //
            // statusStrip1
            //
            statusStrip1.Dock = DockStyle.Bottom;
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip1.Location = new Point(0, 624);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1082, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            //
            // statusLabel
            //
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 17);
            statusLabel.Text = "Ready";
            //
            // MainPage
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 646);
            Controls.Add(mainSplitContainer);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(900, 500);
            Name = "MainPage";
            Text = "Inti OSB Reader";
            Load += MainPage_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            contentSplitContainer.Panel1.ResumeLayout(false);
            contentSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)contentSplitContainer).EndInit();
            contentSplitContainer.ResumeLayout(false);
            rightPanel.ResumeLayout(false);
            contentAreaPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            controlsPanel.ResumeLayout(false);
            controlsPanel.PerformLayout();
            bottomTable.ResumeLayout(false);
            paletteControlsPanel.ResumeLayout(false);
            paletteControlsPanel.PerformLayout();
            infoPanel.ResumeLayout(false);
            infoPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem openFolderToolStripMenuItem;
        private ToolStripMenuItem openColorPaletteToolStripMenuItem;
        private ToolStripMenuItem recentFilesToolStripMenuItem;
        private ToolStripMenuItem recentFoldersToolStripMenuItem;
        private ToolStripMenuItem saveAllPaletteToolStripMenuItem;
        private ToolStripMenuItem saveAnimationToolStripMenuItem;
        private ToolStripMenuItem saveAllAnimationsToolStripMenuItem;
        private ToolStripMenuItem savePaletteToolStripMenuItem;
        private ToolStripMenuItem saveAllPaletteToolStripMenuItem1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem documnetedDataToolStripMenuItem;
        private ToolStripMenuItem allToolStripMenuItem;
        private ToolStripMenuItem fileHeaderToolStripMenuItem;
        private ToolStripMenuItem spriteSheetToolStripMenuItem1;
        private ToolStripMenuItem animationsToolStripMenuItem;
        private ToolStripMenuItem framesToolStripMenuItem;
        private ToolStripMenuItem colorPaletteToolStripMenuItem;
        private ToolStripMenuItem fremesRelatedDataToolStripMenuItem;
        private ToolStripMenuItem spriteSheetToolStripMenuItem;
        private SplitContainer mainSplitContainer;
        private TreeView FolderTree;
        private SplitContainer contentSplitContainer;
        private ListView AnimationList;
        public ColumnHeader Animation;
        private Panel rightPanel;
        private Panel contentAreaPanel;
        private PictureBox pic;
        private Panel controlsPanel;
        private CheckBox CLoop;
        private Label Lspeed;
        private TextBox Tspeed;
        private Button Flip;
        private Button BplayPause;
        private Button BsaveGif;
        private TableLayoutPanel bottomTable;
        private Panel paletteControlsPanel;
        private Label Lpallete;
        private Button BleftPalette;
        private Button BrightPalette;
        private Label Lpalletecount;
        private PictureBox picPalatte;
        private Panel infoPanel;
        private Label LcurrFrame;
        private TextBox TcurrFrame;
        private ToolTip toolTip1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
    }
}
