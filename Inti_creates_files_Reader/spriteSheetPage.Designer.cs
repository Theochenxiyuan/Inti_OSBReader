using Inti_creates_files_Reader.Properties;

namespace Inti_creates_files_Reader
{
    partial class spriteSheetPage
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
            pic = new PictureBox();
            BrightPalette = new Button();
            BleftPalette = new Button();
            Lpalletecount = new Label();
            Lpallete = new Label();
            picPalatte = new PictureBox();
            Lfile = new Label();
            Bbackground = new Button();
            button1 = new Button();
            button2 = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            spriteSheetToolStripMenuItem = new ToolStripMenuItem();
            saveEachLayerSeToolStripMenuItem = new ToolStripMenuItem();
            colorPaletteToolStripMenuItem = new ToolStripMenuItem();
            allColorPalettesToolStripMenuItem = new ToolStripMenuItem();
            backgroundColor = new PictureBox();
            centerColor = new PictureBox();
            button3 = new Button();
            button4 = new Button();
            BsavePng = new Button();
            Lspacing = new Label();
            Nspacing = new NumericUpDown();
            LviewMode = new Label();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPalatte).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)backgroundColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)centerColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Nspacing).BeginInit();
            SuspendLayout();
            // 
            // pic
            // 
            pic.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pic.Location = new Point(12, 57);
            pic.Name = "pic";
            pic.Size = new Size(969, 560);
            pic.TabIndex = 0;
            pic.TabStop = false;
            // 
            // BrightPalette
            // 
            BrightPalette.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BrightPalette.Location = new Point(56, 624);
            BrightPalette.Name = "BrightPalette";
            BrightPalette.Size = new Size(42, 27);
            BrightPalette.TabIndex = 104;
            BrightPalette.Text = ">";
            BrightPalette.UseVisualStyleBackColor = true;
            BrightPalette.Click += BrightPalette_Click;
            // 
            // BleftPalette
            // 
            BleftPalette.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BleftPalette.Location = new Point(11, 624);
            BleftPalette.Name = "BleftPalette";
            BleftPalette.Size = new Size(39, 27);
            BleftPalette.TabIndex = 103;
            BleftPalette.Text = "<";
            BleftPalette.UseVisualStyleBackColor = true;
            BleftPalette.Click += BleftPalette_Click;
            // 
            // Lpalletecount
            // 
            Lpalletecount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Lpalletecount.AutoSize = true;
            Lpalletecount.Location = new Point(117, 657);
            Lpalletecount.Name = "Lpalletecount";
            Lpalletecount.Size = new Size(57, 15);
            Lpalletecount.TabIndex = 102;
            Lpalletecount.Text = "1 out of 1";
            // 
            // Lpallete
            // 
            Lpallete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Lpallete.AutoSize = true;
            Lpallete.Location = new Point(124, 635);
            Lpallete.Name = "Lpallete";
            Lpallete.Size = new Size(45, 15);
            Lpallete.TabIndex = 101;
            Lpallete.Text = "Palette:";
            // 
            // picPalatte
            // 
            picPalatte.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picPalatte.Location = new Point(192, 623);
            picPalatte.Name = "picPalatte";
            picPalatte.Size = new Size(942, 55);
            picPalatte.TabIndex = 100;
            picPalatte.TabStop = false;
            // 
            // Lfile
            // 
            Lfile.AutoSize = true;
            Lfile.Location = new Point(12, 39);
            Lfile.Name = "Lfile";
            Lfile.Size = new Size(31, 15);
            Lfile.TabIndex = 99;
            Lfile.Text = "File: ";
            // 
            // Bbackground
            // 
            Bbackground.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Bbackground.Location = new Point(999, 57);
            Bbackground.Name = "Bbackground";
            Bbackground.Size = new Size(84, 23);
            Bbackground.TabIndex = 106;
            Bbackground.Text = "Background";
            Bbackground.UseVisualStyleBackColor = true;
            Bbackground.Click += Bbackground_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(999, 95);
            button1.Name = "button1";
            button1.Size = new Size(135, 23);
            button1.TabIndex = 107;
            button1.Text = "Show Original Atlas";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Sprite_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new Point(999, 135);
            button2.Name = "button2";
            button2.Size = new Size(84, 23);
            button2.TabIndex = 107;
            button2.Text = "Center";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Center_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1146, 24);
            menuStrip1.TabIndex = 108;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { spriteSheetToolStripMenuItem, saveEachLayerSeToolStripMenuItem, colorPaletteToolStripMenuItem, allColorPalettesToolStripMenuItem });
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(98, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // spriteSheetToolStripMenuItem
            // 
            spriteSheetToolStripMenuItem.Name = "spriteSheetToolStripMenuItem";
            spriteSheetToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            spriteSheetToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            spriteSheetToolStripMenuItem.Size = new Size(282, 22);
            spriteSheetToolStripMenuItem.Text = "SpriteSheet";
            spriteSheetToolStripMenuItem.Click += Save_Click;
            // 
            // saveEachLayerSeToolStripMenuItem
            // 
            saveEachLayerSeToolStripMenuItem.Name = "saveEachLayerSeToolStripMenuItem";
            saveEachLayerSeToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+S";
            saveEachLayerSeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveEachLayerSeToolStripMenuItem.Size = new Size(282, 22);
            saveEachLayerSeToolStripMenuItem.Text = "Save each layer seperately";
            saveEachLayerSeToolStripMenuItem.Click += SaveAll_Click;
            // 
            // colorPaletteToolStripMenuItem
            // 
            colorPaletteToolStripMenuItem.Name = "colorPaletteToolStripMenuItem";
            colorPaletteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+S";
            colorPaletteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.S;
            colorPaletteToolStripMenuItem.Size = new Size(282, 22);
            colorPaletteToolStripMenuItem.Text = "Color Palette";
            colorPaletteToolStripMenuItem.Click += colorPaletteToolStripMenuItem_Click;
            // 
            // allColorPalettesToolStripMenuItem
            // 
            allColorPalettesToolStripMenuItem.Name = "allColorPalettesToolStripMenuItem";
            allColorPalettesToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+Alt+S";
            allColorPalettesToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.Shift | Keys.S;
            allColorPalettesToolStripMenuItem.Size = new Size(282, 22);
            allColorPalettesToolStripMenuItem.Text = "All Color Palettes";
            allColorPalettesToolStripMenuItem.Click += allColorPalettesToolStripMenuItem_Click;
            // 
            // backgroundColor
            // 
            backgroundColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            backgroundColor.BackColor = Color.FromArgb(0, 128, 0);
            backgroundColor.Location = new Point(1089, 57);
            backgroundColor.MinimumSize = new Size(45, 23);
            backgroundColor.Name = "backgroundColor";
            backgroundColor.Size = new Size(45, 23);
            backgroundColor.TabIndex = 109;
            backgroundColor.TabStop = false;
            backgroundColor.Tag = "0";
            backgroundColor.Click += backgroundColor_Click;
            // 
            // centerColor
            // 
            centerColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            centerColor.BackColor = Color.FromArgb(255, 0, 0);
            centerColor.Location = new Point(1089, 135);
            centerColor.Name = "centerColor";
            centerColor.Size = new Size(45, 23);
            centerColor.TabIndex = 109;
            centerColor.TabStop = false;
            centerColor.Tag = "2";
            centerColor.Click += centerColor_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Location = new Point(12, 653);
            button3.Name = "button3";
            button3.Size = new Size(86, 27);
            button3.TabIndex = 103;
            button3.Text = "Apply Palette";
            button3.UseVisualStyleBackColor = true;
            button3.Click += BapplyPalette_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button4.Location = new Point(999, 179);
            button4.Name = "button4";
            button4.Size = new Size(84, 23);
            button4.TabIndex = 110;
            button4.Text = "Flip";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Flip_Click;
            // 
            // BsavePng
            // 
            BsavePng.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BsavePng.Location = new Point(999, 218);
            BsavePng.Name = "BsavePng";
            BsavePng.Size = new Size(84, 23);
            BsavePng.TabIndex = 111;
            BsavePng.Text = "Save PNG";
            BsavePng.UseVisualStyleBackColor = true;
            BsavePng.Click += Save_Click;
            //
            // Lspacing
            //
            Lspacing.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Lspacing.AutoSize = true;
            Lspacing.Location = new Point(999, 264);
            Lspacing.Name = "Lspacing";
            Lspacing.Size = new Size(75, 15);
            Lspacing.TabIndex = 112;
            Lspacing.Text = "Spacing (px):";
            //
            // Nspacing
            //
            Nspacing.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Nspacing.Location = new Point(1089, 260);
            Nspacing.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            Nspacing.Name = "Nspacing";
            Nspacing.Size = new Size(45, 23);
            Nspacing.TabIndex = 113;
            Nspacing.Value = new decimal(new int[] { 1, 0, 0, 0 });
            Nspacing.ValueChanged += Nspacing_ValueChanged;
            //
            // LviewMode
            //
            LviewMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LviewMode.AutoSize = true;
            LviewMode.Location = new Point(795, 39);
            LviewMode.Name = "LviewMode";
            LviewMode.Size = new Size(155, 15);
            LviewMode.TabIndex = 114;
            LviewMode.Text = "View: Reconstructed Frames";
            //
            // spriteSheetPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1146, 690);
            Controls.Add(LviewMode);
            Controls.Add(Nspacing);
            Controls.Add(Lspacing);
            Controls.Add(button4);
            Controls.Add(BsavePng);
            Controls.Add(centerColor);
            Controls.Add(backgroundColor);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(Bbackground);
            Controls.Add(BrightPalette);
            Controls.Add(button3);
            Controls.Add(BleftPalette);
            Controls.Add(Lpalletecount);
            Controls.Add(Lpallete);
            Controls.Add(picPalatte);
            Controls.Add(Lfile);
            Controls.Add(pic);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1162, 729);
            Name = "spriteSheetPage";
            Text = "spriteSheetPage";
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPalatte).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)backgroundColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)centerColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)Nspacing).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pic;
        private Button BrightPalette;
        private Button BleftPalette;
        private Label Lpalletecount;
        private Label Lpallete;
        private PictureBox picPalatte;
        private Label Lfile;
        private Button Bbackground;
        private Button button1;
        private Button button2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem spriteSheetToolStripMenuItem;
        private ToolStripMenuItem colorPaletteToolStripMenuItem;
        private ToolStripMenuItem allColorPalettesToolStripMenuItem;
        private ToolStripMenuItem saveEachLayerSeToolStripMenuItem;
        private PictureBox backgroundColor;
        private PictureBox centerColor;
        private Button button3;
        private Button button4;
        private Button BsavePng;
        private Label Lspacing;
        private NumericUpDown Nspacing;
        private Label LviewMode;
    }
}
