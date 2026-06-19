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
            BrightPalette = new Button();
            BleftPalette = new Button();
            Lpalletecount = new Label();
            Lpallete = new Label();
            picPalatte = new PictureBox();
            Lfile = new Label();
            Lfilename = new Label();
            pic = new PictureBox();
            AnimationList = new ListView();
            Animation = new ColumnHeader();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            openColorPaletteToolStripMenuItem = new ToolStripMenuItem();
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
            Lspeed = new Label();
            Tspeed = new TextBox();
            TcurrFrame = new TextBox();
            LcurrFrame = new Label();
            CLoop = new CheckBox();
            Flip = new Button();
            ((System.ComponentModel.ISupportInitialize)picPalatte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // BrightPalette
            // 
            BrightPalette.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BrightPalette.Location = new Point(47, 593);
            BrightPalette.Name = "BrightPalette";
            BrightPalette.Size = new Size(42, 27);
            BrightPalette.TabIndex = 97;
            BrightPalette.Text = ">";
            BrightPalette.UseVisualStyleBackColor = true;
            BrightPalette.Click += BrightPalette_Click;
            // 
            // BleftPalette
            // 
            BleftPalette.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BleftPalette.Location = new Point(2, 593);
            BleftPalette.Name = "BleftPalette";
            BleftPalette.Size = new Size(39, 27);
            BleftPalette.TabIndex = 96;
            BleftPalette.Text = "<";
            BleftPalette.UseVisualStyleBackColor = true;
            BleftPalette.Click += BleftPalette_Click;
            // 
            // Lpalletecount
            // 
            Lpalletecount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Lpalletecount.AutoSize = true;
            Lpalletecount.Location = new Point(120, 615);
            Lpalletecount.Name = "Lpalletecount";
            Lpalletecount.Size = new Size(57, 15);
            Lpalletecount.TabIndex = 95;
            Lpalletecount.Text = "0 out of 0";
            // 
            // Lpallete
            // 
            Lpallete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Lpallete.AutoSize = true;
            Lpallete.Location = new Point(127, 593);
            Lpallete.Name = "Lpallete";
            Lpallete.Size = new Size(45, 15);
            Lpallete.TabIndex = 94;
            Lpallete.Text = "Pallete:";
            // 
            // picPalatte
            // 
            picPalatte.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picPalatte.Location = new Point(193, 581);
            picPalatte.Name = "picPalatte";
            picPalatte.Size = new Size(697, 53);
            picPalatte.TabIndex = 93;
            picPalatte.TabStop = false;
            // 
            // Lfile
            // 
            Lfile.AutoSize = true;
            Lfile.Location = new Point(17, 47);
            Lfile.Name = "Lfile";
            Lfile.Size = new Size(31, 15);
            Lfile.TabIndex = 86;
            Lfile.Text = "File: ";
            // 
            // Lfilename
            // 
            Lfilename.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Lfilename.AutoSize = true;
            Lfilename.Location = new Point(157, 581);
            Lfilename.Name = "Lfilename";
            Lfilename.Size = new Size(0, 15);
            Lfilename.TabIndex = 85;
            // 
            // pic
            // 
            pic.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pic.Location = new Point(156, 75);
            pic.Name = "pic";
            pic.Size = new Size(574, 463);
            pic.TabIndex = 79;
            pic.TabStop = false;
            pic.Click += pic_Click;
            // 
            // AnimationList
            // 
            AnimationList.Alignment = ListViewAlignment.SnapToGrid;
            AnimationList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            AnimationList.Columns.AddRange(new ColumnHeader[] { Animation });
            AnimationList.FullRowSelect = true;
            AnimationList.HeaderStyle = ColumnHeaderStyle.None;
            AnimationList.LabelWrap = false;
            AnimationList.Location = new Point(17, 75);
            AnimationList.MultiSelect = false;
            AnimationList.Name = "AnimationList";
            AnimationList.Size = new Size(133, 492);
            AnimationList.TabIndex = 101;
            AnimationList.UseCompatibleStateImageBehavior = false;
            AnimationList.View = View.Details;
            AnimationList.ItemSelectionChanged += AnimationList_ItemSelectionChanged;
            // 
            // Animation
            // 
            Animation.Width = 108;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, saveToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(89, 24);
            menuStrip1.TabIndex = 102;
            menuStrip1.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, openColorPaletteToolStripMenuItem, saveAllPaletteToolStripMenuItem });
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
            // openColorPaletteToolStripMenuItem
            // 
            openColorPaletteToolStripMenuItem.Name = "openColorPaletteToolStripMenuItem";
            openColorPaletteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+E";
            openColorPaletteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.E;
            openColorPaletteToolStripMenuItem.Size = new Size(228, 22);
            openColorPaletteToolStripMenuItem.Text = "Add color palette";
            openColorPaletteToolStripMenuItem.Click += openColorPaletteToolStripMenuItem_Click;
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
            saveAllPaletteToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+Shit+Alt+S";
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
            documnetedDataToolStripMenuItem.Text = "Documneted data";
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
            spriteSheetToolStripMenuItem1.Text = "SpriteSheet";
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
            fremesRelatedDataToolStripMenuItem.Text = "fremes related data";
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
            // Lspeed
            // 
            Lspeed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Lspeed.AutoSize = true;
            Lspeed.Location = new Point(736, 93);
            Lspeed.Name = "Lspeed";
            Lspeed.Size = new Size(39, 15);
            Lspeed.TabIndex = 103;
            Lspeed.Text = "Speed";
            // 
            // Tspeed
            // 
            Tspeed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Tspeed.Location = new Point(790, 90);
            Tspeed.Name = "Tspeed";
            Tspeed.Size = new Size(100, 23);
            Tspeed.TabIndex = 104;
            // 
            // TcurrFrame
            // 
            TcurrFrame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            TcurrFrame.Enabled = false;
            TcurrFrame.Location = new Point(751, 514);
            TcurrFrame.Name = "TcurrFrame";
            TcurrFrame.Size = new Size(100, 23);
            TcurrFrame.TabIndex = 104;
            // 
            // LcurrFrame
            // 
            LcurrFrame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            LcurrFrame.AutoSize = true;
            LcurrFrame.Location = new Point(757, 489);
            LcurrFrame.Name = "LcurrFrame";
            LcurrFrame.Size = new Size(86, 15);
            LcurrFrame.TabIndex = 103;
            LcurrFrame.Text = "Current Frame:";
            // 
            // CLoop
            // 
            CLoop.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CLoop.AutoSize = true;
            CLoop.Location = new Point(790, 65);
            CLoop.Name = "CLoop";
            CLoop.Size = new Size(53, 19);
            CLoop.TabIndex = 105;
            CLoop.Text = "Loop";
            CLoop.UseVisualStyleBackColor = true;
            // 
            // Flip
            // 
            Flip.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Flip.Location = new Point(778, 134);
            Flip.Name = "Flip";
            Flip.Size = new Size(86, 23);
            Flip.TabIndex = 107;
            Flip.Text = "Flip";
            Flip.UseVisualStyleBackColor = true;
            Flip.Click += Flip_Click;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(902, 646);
            Controls.Add(Flip);
            Controls.Add(CLoop);
            Controls.Add(TcurrFrame);
            Controls.Add(Tspeed);
            Controls.Add(LcurrFrame);
            Controls.Add(Lspeed);
            Controls.Add(AnimationList);
            Controls.Add(BrightPalette);
            Controls.Add(BleftPalette);
            Controls.Add(Lpalletecount);
            Controls.Add(Lpallete);
            Controls.Add(picPalatte);
            Controls.Add(Lfile);
            Controls.Add(Lfilename);
            Controls.Add(pic);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(918, 685);
            Name = "MainPage";
            Text = "MainPage";
            Load += MainPage_Load;
            ((System.ComponentModel.ISupportInitialize)picPalatte).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BrightPalette;
        private Button BleftPalette;
        private Label Lpalletecount;
        private Label Lpallete;
        private PictureBox picPalatte;
        private Label Lcenter;
        private TextBox Tcenter;
        private Label Lfile;
        private Label Lfilename;
        private Label Lframe;
        private TextBox Tframe;
        private PictureBox pic;
        private ListView AnimationList;
        public ColumnHeader Animation;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem openColorPaletteToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem documnetedDataToolStripMenuItem;
        private ToolStripMenuItem spriteSheetToolStripMenuItem;
        private ToolStripMenuItem allToolStripMenuItem;
        private ToolStripMenuItem spriteSheetToolStripMenuItem1;
        private ToolStripMenuItem animationsToolStripMenuItem;
        private ToolStripMenuItem framesToolStripMenuItem;
        private ToolStripMenuItem colorPaletteToolStripMenuItem;
        private ToolStripMenuItem saveAllPaletteToolStripMenuItem;
        private ToolStripMenuItem fileHeaderToolStripMenuItem;
        private Label Lspeed;
        private TextBox Tspeed;
        private Label Lwait;
        private TextBox Twait;
        private ToolStripMenuItem fremesRelatedDataToolStripMenuItem;
        private ToolStripMenuItem saveAllPaletteToolStripMenuItem1;
        private ToolStripMenuItem saveAllAnimationsToolStripMenuItem;
        private ToolStripMenuItem saveAnimationToolStripMenuItem;
        private ToolStripMenuItem savePaletteToolStripMenuItem;
        private TextBox TcurrFrame;
        private Label LcurrFrame;
        private CheckBox CLoop;
        private Button Flip;
    }
}