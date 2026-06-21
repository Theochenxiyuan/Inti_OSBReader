using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using ImageMagick.Factories;
using ImageMagick;


namespace Inti_creates_files_Reader
{
    public partial class MainPage : Form
    {
        private OSB obj;
        private int pltIndex;
        private int animationIndex;
        private System.Windows.Forms.Timer animationTimer = new System.Windows.Forms.Timer();
        private Image? frame;
        private int frameIndex;
        private DateTime startTime;
        private readonly string[] readableExtensions = { ".osb", ".scb" };
        private const int MaxRecentFiles = 10;
        private string currentFilePath = "";
        private bool isPaused = false;
        private readonly System.Windows.Forms.Timer filterTimer = new System.Windows.Forms.Timer();
        private string currentSearch = "";
        private Dictionary<string, List<string>> externalPaletteMap = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);


        public MainPage()

        {
            InitializeComponent();
            obj = new OSB("");

            CLoop.Checked = Properties.Settings.Default.Loop;
            Tspeed.Text = (Properties.Settings.Default.timeSpeed).ToString();

        }

        private void Tspeed_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(Tspeed.Text, out int speed) || speed < 1)
            {
                Tspeed.Text = Properties.Settings.Default.timeSpeed.ToString();
                return;
            }
            Properties.Settings.Default.timeSpeed = speed;
            Properties.Settings.Default.Save();
        }

        private void CLoop_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Loop = CLoop.Checked;
            Properties.Settings.Default.Save();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            pic.Image = null;
            pic.SizeMode = PictureBoxSizeMode.Normal;
            pic.Paint += pic_Paint;
            SetControlsEnabled(false);
            UpdateStatusBar();
            BuildRecentFilesMenu();
            BuildRecentFoldersMenu();
            Lpalletecount.Text = "—";
            filterTextBox.Text = Properties.Settings.Default.fileFilter;
            LoadExternalPaletteMap();
            filterTimer.Interval = 500;
            filterTimer.Tick += filterTimer_Tick;
            AllowDrop = true;
            DragEnter += MainPage_DragEnter;
            DragDrop += MainPage_DragDrop;

            if (Directory.Exists(Properties.Settings.Default.pathDir))
                LoadFolder(Properties.Settings.Default.pathDir);
        }

        private void Bopen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Properties.Settings.Default.pathOpen;
            dialog.Filter = "Readable files (*.osb;*.scb)|*.osb;*.scb|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.pathOpen = Path.GetDirectoryName(dialog.FileName);
                Properties.Settings.Default.Save();
                LoadFile(dialog.FileName);

            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.InitialDirectory = Directory.Exists(Properties.Settings.Default.pathDir) ? Properties.Settings.Default.pathDir : Properties.Settings.Default.pathOpen;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.pathDir = folder.SelectedPath;
                Properties.Settings.Default.pathOpen = folder.SelectedPath;
                Properties.Settings.Default.Save();
                LoadFolder(folder.SelectedPath);
                AddRecentFolder(folder.SelectedPath);
            }
        }

        private void LoadFile(string fileName)
        {
            currentFilePath = fileName;
            animationTimer.Stop();
            AnimationList.Items.Clear();
            Lpalletecount.Text = "0 out of 0";
            pltIndex = -1;
            picPalatte.Image = null;
            pic.Image = null;
            frameIndex = 0;
            frame = null;
            animationIndex = 0;
            TcurrFrame.Text = "";

            obj = new OSB(fileName);
            obj.readData();
            if (obj.isOSB())
            {
                for (int i = 0; i < obj.animations.Count(); i++)
                {
                    ListViewItem item = new ListViewItem("Animation " + (i + 1));
                    AnimationList.Items.Add(item);
                }
                AnimationList_SizeChanged(AnimationList, EventArgs.Empty);

                if (AnimationList.Items.Count > 0)
                {
                    AnimationList.Items[0].Selected = true;
                    AnimationList.Items[0].Focused = true;
                    AnimationList.Select();
                    PlayAnimation(0);
                }

            }
            else spriteSheetToolStripMenuItem_Click(spriteSheetToolStripMenuItem, EventArgs.Empty);

            if (obj.plts.Size() != 0)
            {
                pltIndex = -1;
                displayPallete(obj.plts.getPalette(pltIndex));
                Lpalletecount.Text = (pltIndex + 1) + " out of " + obj.plts.Size();
            }
            else
            {
                Lpalletecount.Text = "—";
            }

            var palettePaths = GetCurrentExternalPalettes();
            if (palettePaths.Count > 0)
                ApplyExternalPalettes(palettePaths);

            UpdatePaletteSourceLabel();
            Text = obj.getName() + " - Inti OSB Reader";
            isPaused = false;
            UpdatePlayPauseButton();
            SetControlsEnabled(true);
            UpdateStatusBar();
            AddRecentFile(fileName);
        }

        private void AddRecentFile(string fileName)
        {
            var recent = GetRecentFiles();
            recent.RemoveAll(path => path.Equals(fileName, StringComparison.OrdinalIgnoreCase));
            recent.Insert(0, fileName);
            if (recent.Count > MaxRecentFiles)
                recent.RemoveRange(MaxRecentFiles, recent.Count - MaxRecentFiles);

            Properties.Settings.Default.recentFiles = string.Join("|", recent);
            Properties.Settings.Default.Save();
            BuildRecentFilesMenu();
        }

        private List<string> GetRecentFiles()
        {
            string stored = Properties.Settings.Default.recentFiles;
            if (string.IsNullOrEmpty(stored)) return new List<string>();
            return stored.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private void BuildRecentFilesMenu()
        {
            recentFilesToolStripMenuItem.DropDownItems.Clear();
            var recent = GetRecentFiles();
            if (recent.Count == 0)
            {
                recentFilesToolStripMenuItem.Enabled = false;
                return;
            }

            recentFilesToolStripMenuItem.Enabled = true;
            foreach (string path in recent)
            {
                string display = path;
                if (display.Length > 60)
                    display = "..." + display.Substring(display.Length - 57);

                ToolStripMenuItem item = new ToolStripMenuItem(display);
                item.ToolTipText = path;
                item.Click += (sender, e) =>
                {
                    if (File.Exists(path))
                    {
                        Properties.Settings.Default.pathOpen = Path.GetDirectoryName(path);
                        Properties.Settings.Default.Save();
                        LoadFile(path);
                    }
                    else
                    {
                        MessageBox.Show($"File not found:\n{path}", "Recent File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        var list = GetRecentFiles();
                        list.RemoveAll(p => p.Equals(path, StringComparison.OrdinalIgnoreCase));
                        Properties.Settings.Default.recentFiles = string.Join("|", list);
                        Properties.Settings.Default.Save();
                        BuildRecentFilesMenu();
                    }
                };
                recentFilesToolStripMenuItem.DropDownItems.Add(item);
            }

            recentFilesToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            ToolStripMenuItem clearItem = new ToolStripMenuItem("Clear Recent Files");
            clearItem.Click += (sender, e) =>
            {
                Properties.Settings.Default.recentFiles = "";
                Properties.Settings.Default.Save();
                BuildRecentFilesMenu();
            };
            recentFilesToolStripMenuItem.DropDownItems.Add(clearItem);
        }

        private void AddRecentFolder(string folderPath)
        {
            var recent = GetRecentFolders();
            recent.RemoveAll(path => path.Equals(folderPath, StringComparison.OrdinalIgnoreCase));
            recent.Insert(0, folderPath);
            if (recent.Count > MaxRecentFiles)
                recent.RemoveRange(MaxRecentFiles, recent.Count - MaxRecentFiles);

            Properties.Settings.Default.recentFolders = string.Join("|", recent);
            Properties.Settings.Default.Save();
            BuildRecentFoldersMenu();
        }

        private List<string> GetRecentFolders()
        {
            string stored = Properties.Settings.Default.recentFolders;
            if (string.IsNullOrEmpty(stored)) return new List<string>();
            return stored.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private void BuildRecentFoldersMenu()
        {
            recentFoldersToolStripMenuItem.DropDownItems.Clear();
            var recent = GetRecentFolders();
            if (recent.Count == 0)
            {
                recentFoldersToolStripMenuItem.Enabled = false;
                return;
            }

            recentFoldersToolStripMenuItem.Enabled = true;
            foreach (string path in recent)
            {
                string display = path;
                if (display.Length > 60)
                    display = "..." + display.Substring(display.Length - 57);

                ToolStripMenuItem item = new ToolStripMenuItem(display);
                item.ToolTipText = path;
                item.Click += (sender, e) =>
                {
                    if (Directory.Exists(path))
                    {
                        Properties.Settings.Default.pathDir = path;
                        Properties.Settings.Default.pathOpen = path;
                        Properties.Settings.Default.Save();
                        LoadFolder(path);
                        AddRecentFolder(path);
                    }
                    else
                    {
                        MessageBox.Show($"Folder not found:\n{path}", "Recent Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        var list = GetRecentFolders();
                        list.RemoveAll(p => p.Equals(path, StringComparison.OrdinalIgnoreCase));
                        Properties.Settings.Default.recentFolders = string.Join("|", list);
                        Properties.Settings.Default.Save();
                        BuildRecentFoldersMenu();
                    }
                };
                recentFoldersToolStripMenuItem.DropDownItems.Add(item);
            }

            recentFoldersToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            ToolStripMenuItem clearItem = new ToolStripMenuItem("Clear Recent Folders");
            clearItem.Click += (sender, e) =>
            {
                Properties.Settings.Default.recentFolders = "";
                Properties.Settings.Default.Save();
                BuildRecentFoldersMenu();
            };
            recentFoldersToolStripMenuItem.DropDownItems.Add(clearItem);
        }

        private void SetControlsEnabled(bool enabled)
        {
            saveAllPaletteToolStripMenuItem.Enabled = enabled;
            saveToolStripMenuItem.Enabled = enabled;
            spriteSheetToolStripMenuItem.Enabled = enabled;
            BleftPalette.Enabled = enabled;
            BrightPalette.Enabled = enabled;
            Flip.Enabled = enabled;
            BplayPause.Enabled = enabled;
            BsaveGif.Enabled = enabled;
            BspriteSheet.Enabled = enabled;
            Tspeed.Enabled = enabled;
        }

        private void UpdateStatusBar()
        {
            if (obj == null || string.IsNullOrEmpty(obj.name))
            {
                statusLabel.Text = "Ready";
                return;
            }

            string animCount = obj.isOSB() ? obj.animations.Count.ToString() : "0";
            string frameCount = obj.frames != null ? obj.frames.Size().ToString() : "0";
            string pltCount = obj.plts != null ? obj.plts.Size().ToString() : "0";
            statusLabel.Text = $"{obj.name} | Animations: {animCount} | Frames: {frameCount} | Palettes: {pltCount}";
        }


        private void LoadFolder(string folderPath)
        {
            FolderTree.BeginUpdate();
            FolderTree.Nodes.Clear();

            string rootName = Path.GetFileName(folderPath.TrimEnd(Path.DirectorySeparatorChar));
            if (string.IsNullOrEmpty(rootName)) rootName = folderPath;
            TreeNode root = new TreeNode(rootName);
            root.Tag = folderPath;
            AddFolderNodes(root, folderPath);
            FolderTree.Nodes.Add(root);
            if (!string.IsNullOrWhiteSpace(currentSearch))
                root.ExpandAll();
            else
                root.Expand();

            FolderTree.EndUpdate();
        }

        private bool AddFolderNodes(TreeNode parent, string folderPath)
        {
            bool hasReadableFiles = false;

            try
            {
                foreach (string directory in Directory.EnumerateDirectories(folderPath).OrderBy(Path.GetFileName))
                {
                    TreeNode directoryNode = new TreeNode(Path.GetFileName(directory));
                    directoryNode.Tag = directory;
                    directoryNode.ForeColor = Color.DarkSlateGray;
                    if (AddFolderNodes(directoryNode, directory))
                    {
                        parent.Nodes.Add(directoryNode);
                        hasReadableFiles = true;
                    }
                }

                foreach (string file in Directory.EnumerateFiles(folderPath).Where(IsReadableFile).OrderBy(Path.GetFileName))
                {
                    TreeNode fileNode = new TreeNode(Path.GetFileName(file));
                    fileNode.Tag = file;
                    fileNode.ForeColor = SystemColors.WindowText;
                    parent.Nodes.Add(fileNode);
                    hasReadableFiles = true;
                }

            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (IOException)
            {
            }

            return hasReadableFiles;
        }

        private bool IsReadableFile(string filePath)
        {
            if (!readableExtensions.Contains(Path.GetExtension(filePath), StringComparer.OrdinalIgnoreCase))
                return false;

            string fileName = Path.GetFileName(filePath);
            string filter = Properties.Settings.Default.fileFilter;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                string[] filters = filter.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string f in filters)
                {
                    if (fileName.Contains(f.Trim(), StringComparison.OrdinalIgnoreCase))
                        return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(currentSearch))
            {
                if (!fileName.Contains(currentSearch, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        private void filterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filterTimer.Stop();
                ApplyFilter();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                filterTimer.Stop();
                filterTimer.Start();
            }
        }

        private void filterTimer_Tick(object sender, EventArgs e)
        {
            filterTimer.Stop();
            ApplyFilter();
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filterTimer.Stop();
                ApplyFilter();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                filterTimer.Stop();
                filterTimer.Start();
            }
        }

        private void ApplyFilter()
        {
            Properties.Settings.Default.fileFilter = filterTextBox.Text;
            Properties.Settings.Default.Save();
            currentSearch = searchTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.pathDir) && Directory.Exists(Properties.Settings.Default.pathDir))
                LoadFolder(Properties.Settings.Default.pathDir);
        }

        private void FolderTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TryLoadSelectedNode(e.Node);
        }

        private void FolderTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TryLoadSelectedNode(FolderTree.SelectedNode);
                e.Handled = true;
            }
        }

        private void TryLoadSelectedNode(TreeNode? node)
        {
            if (node == null) return;
            string? filePath = node.Tag as string;
            if (filePath == null || Directory.Exists(filePath)) return;
            if (filePath.Equals(currentFilePath, StringComparison.OrdinalIgnoreCase)) return;

            Properties.Settings.Default.pathOpen = Path.GetDirectoryName(filePath);
            Properties.Settings.Default.Save();
            LoadFile(filePath);
        }



        private void openColorPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "osb files (*.osb)|*.osb|scb files (*.scb)|*.scb|txt2 files (*.txt2)|*.txt2";
            dialog.InitialDirectory = Properties.Settings.Default.pathOpen;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.pathOpen = Path.GetDirectoryName(dialog.FileName);
                Properties.Settings.Default.Save();
                AddExternalPaletteForCurrentFile(dialog.FileName);
                ApplyExternalPalettes(GetCurrentExternalPalettes());
                UpdatePaletteSourceLabel();
                RenderCurrentFrame();
            }
        }

        private void BaddPalette_Click(object sender, EventArgs e)
        {
            openColorPaletteToolStripMenuItem_Click(sender, e);
        }

        private void BclearPalette_Click(object sender, EventArgs e)
        {
            RemoveLastExternalPaletteForCurrentFile();
            if (!string.IsNullOrEmpty(currentFilePath))
                LoadFile(currentFilePath);
        }

        private void ApplyExternalPalettes(List<string> palettePaths)
        {
            foreach (string palettePath in palettePaths)
            {
                if (!File.Exists(palettePath)) continue;

                OSB obj2 = new OSB(palettePath);
                obj2.plts = obj.plts;
                obj2.readData();
                obj.plts = obj2.plts;
            }

            if (obj.plts.Size() != 0)
            {
                pltIndex = -1;
                displayPallete(obj.plts.getPalette(pltIndex));
                Lpalletecount.Text = (pltIndex + 1) + " out of " + obj.plts.Size();
            }
        }

        private void UpdatePaletteSourceLabel()
        {
            var palPaths = GetCurrentExternalPalettes();
            int count = palPaths.Count(p => File.Exists(p));
            if (count == 0)
            {
                LpaletteSource.Text = "Palette: internal";
                return;
            }

            string firstName = Path.GetFileName(palPaths[0]);
            if (count == 1)
                LpaletteSource.Text = "Palette: " + firstName;
            else
                LpaletteSource.Text = $"Palettes ({count}): {firstName} +{count - 1} more";
        }

        private List<string> GetCurrentExternalPalettes()
        {
            if (string.IsNullOrEmpty(currentFilePath)) return new List<string>();
            externalPaletteMap.TryGetValue(currentFilePath, out List<string>? palPaths);
            return palPaths != null ? new List<string>(palPaths) : new List<string>();
        }

        private void AddExternalPaletteForCurrentFile(string palettePath)
        {
            if (string.IsNullOrEmpty(currentFilePath)) return;
            if (!externalPaletteMap.TryGetValue(currentFilePath, out List<string>? list))
            {
                list = new List<string>();
                externalPaletteMap[currentFilePath] = list;
            }
            list.Add(palettePath);
            SaveExternalPaletteMap();
        }

        private void RemoveLastExternalPaletteForCurrentFile()
        {
            if (string.IsNullOrEmpty(currentFilePath)) return;
            if (externalPaletteMap.TryGetValue(currentFilePath, out List<string>? list))
            {
                if (list.Count > 0)
                    list.RemoveAt(list.Count - 1);
                if (list.Count == 0)
                    externalPaletteMap.Remove(currentFilePath);
                SaveExternalPaletteMap();
            }
        }

        private void ClearExternalPalettesForCurrentFile()
        {
            if (string.IsNullOrEmpty(currentFilePath)) return;
            externalPaletteMap.Remove(currentFilePath);
            SaveExternalPaletteMap();
        }

        private void LoadExternalPaletteMap()
        {
            externalPaletteMap.Clear();
            string stored = Properties.Settings.Default.externalPalettePath;
            if (string.IsNullOrEmpty(stored)) return;

            string[] parts = stored.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i + 1 < parts.Length; i += 2)
            {
                string osbPath = parts[i];
                string[] palettes = parts[i + 1].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                externalPaletteMap[osbPath] = new List<string>(palettes);
            }
        }

        private void SaveExternalPaletteMap()
        {
            var pairs = new List<string>();
            foreach (var kvp in externalPaletteMap)
            {
                pairs.Add(kvp.Key);
                pairs.Add(string.Join(";", kvp.Value));
            }
            Properties.Settings.Default.externalPalettePath = string.Join("|", pairs);
            Properties.Settings.Default.Save();
        }

        private void AnimationList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected) return;
            PlayAnimation(e.ItemIndex);
        }

        private void AnimationList_SizeChanged(object sender, EventArgs e)
        {
            Animation.Width = Math.Max(20, AnimationList.ClientSize.Width - 4);
        }

        private void PlayAnimation(int index)
        {
            animationTimer.Stop();
            pic.Image = null;

            animationIndex = index;
            frameIndex = 0;

            startTime = DateTime.Now;
            RenderCurrentFrame();

            animationTimer.Interval = 10;

            animationTimer.Tick -= AnimationTimer_Tick;
            animationTimer.Tick += AnimationTimer_Tick;

            animationTimer.Start();
            isPaused = false;
            UpdatePlayPauseButton();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (animationIndex < 0 || animationIndex >= obj.animations.Count || obj.animations[animationIndex].Size() == 0)
                return;

            double elapsed = (DateTime.Now - startTime).TotalSeconds * Properties.Settings.Default.timeSpeed;

            if (CLoop.Checked)
            {
                if (elapsed >= obj.animations[animationIndex].getLength())
                {
                    startTime = DateTime.Now;
                    frameIndex = 0;
                }
            }

            if (elapsed >= obj.animations[animationIndex].getLength()) return;

            while ((frameIndex + 1) < obj.animations[animationIndex].Size() && obj.animations[animationIndex].getTime(frameIndex + 1) <= elapsed) 
                frameIndex++;

            if (obj.animations[animationIndex].getFrame(frameIndex) >= obj.frames.Size() || obj.animations[animationIndex].getFrame(frameIndex) < 0)
                return;

            RenderCurrentFrame();
        }

        private void RenderCurrentFrame()
        {
            if (animationIndex < 0 || animationIndex >= obj.animations.Count || obj.animations[animationIndex].Size() == 0)
                return;

            if (frameIndex < 0 || frameIndex >= obj.animations[animationIndex].Size())
                return;

            int sourceFrame = obj.animations[animationIndex].getFrame(frameIndex);
            if (sourceFrame >= obj.frames.Size() || sourceFrame < 0)
                return;

            TcurrFrame.Text = obj.animations[animationIndex].getFrame(frameIndex).ToString();
            frame = obj.getFrameCentered(animationIndex, sourceFrame, pltIndex);

            pic.Invalidate();
            pic.Update();
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            if (frame == null) return;
            e.Graphics.Clear(pic.BackColor);
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.SmoothingMode = SmoothingMode.None;

            Rectangle destRect = GetZoomedRectangle(pic, frame);
            e.Graphics.DrawImage(frame, destRect);
        }

        private Rectangle GetZoomedRectangle(PictureBox pb, Image img)
        {
            float imageAspect = (float)img.Width / img.Height;
            float boxAspect = (float)pb.Width / pb.Height;

            int drawWidth, drawHeight;

            if (imageAspect > boxAspect)
            {
                drawWidth = pb.Width;
                drawHeight = (int)(pb.Width / imageAspect);
            }
            else
            {
                drawHeight = pb.Height;
                drawWidth = (int)(pb.Height * imageAspect);
            }

            int posX = (pb.Width - drawWidth) / 2;
            int posY = (pb.Height - drawHeight) / 2;

            return new Rectangle(posX, posY, drawWidth, drawHeight);
        }


        private void BrightPalette_Click(object sender, EventArgs e)
        {
            int num = obj.plts.Size();
            if (num >= 1)
            {
                if (pltIndex == num - 1) pltIndex = -1;
                else pltIndex++;

                displayPallete(obj.plts.getPalette(pltIndex));
                Lpalletecount.Text = (pltIndex + 1) + " out of " + num;
                RenderCurrentFrame();
            }
        }

        private void BleftPalette_Click(object sender, EventArgs e)
        {
            int num = obj.plts.Size();
            if (num >= 1)
            {
                if (pltIndex == -1) pltIndex = num - 1;
                else pltIndex--;

                displayPallete(obj.plts.getPalette(pltIndex));
                Lpalletecount.Text = (pltIndex + 1) + " out of " + num;
                RenderCurrentFrame();
            }
        }

        private void BplayPause_Click(object sender, EventArgs e)
        {
            TogglePlayPause();
        }

        private void TogglePlayPause()
        {
            if (obj == null || obj.animations == null || obj.animations.Count == 0)
                return;

            if (isPaused)
            {
                animationTimer.Start();
                isPaused = false;
            }
            else
            {
                animationTimer.Stop();
                isPaused = true;
            }
            UpdatePlayPauseButton();
        }

        private void UpdatePlayPauseButton()
        {
            if (BplayPause == null) return;
            BplayPause.Text = isPaused ? "Play" : "Pause";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space && obj != null && obj.animations != null && obj.animations.Count > 0)
            {
                TogglePlayPause();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainPage_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null && files.Any(IsReadableFile))
                    e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainPage_DragDrop(object sender, DragEventArgs e)
        {
            string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];
            string? file = files?.FirstOrDefault(IsReadableFile);
            if (file != null && File.Exists(file))
            {
                Properties.Settings.Default.pathOpen = Path.GetDirectoryName(file);
                Properties.Settings.Default.Save();
                LoadFile(file);
            }
        }

        private void displayPallete(Bitmap oplt)
        {
            if (oplt == null)
                return;
            Bitmap plt = new Bitmap(picPalatte.Width, picPalatte.Height);
            using (Graphics graphics = Graphics.FromImage(plt))
            {
                for (int y = 0; y < oplt.Height; y++)
                {
                    for (int x = 0; x < oplt.Width; x++)
                    {
                        using (Brush brush = new SolidBrush(oplt.GetPixel(x, y)))
                            graphics.FillRectangle(brush, x * (picPalatte.Width / oplt.Width), y * (picPalatte.Height / oplt.Height), (picPalatte.Width / oplt.Width), (picPalatte.Height / oplt.Height));
                    }
                }
                picPalatte.Image = plt;
            }
        }

        private void spriteSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spriteSheetPage form = new spriteSheetPage(ref obj);

            form.Show();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.InitialDirectory = Properties.Settings.Default.PathSave;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = folder.SelectedPath;
                Properties.Settings.Default.Save();

                obj.Doc(folder.SelectedPath);
            }
        }

        private void fileHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Properties.Settings.Default.PathSave;
            saveFileDialog.FileName = obj.name + "_header.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.Save();

                obj.headerDoc(saveFileDialog.FileName);
            }
        }

        private void spriteSheetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Properties.Settings.Default.PathSave;
            saveFileDialog.FileName = obj.name + "_SpriteSheet.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.Save();

                obj.spriteDoc(saveFileDialog.FileName);
            }
        }

        private void animationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Properties.Settings.Default.PathSave;
            saveFileDialog.FileName = obj.name + "_Animation.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.Save();

                obj.AnimationDoc(saveFileDialog.FileName);
            }
        }

        private void framesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Properties.Settings.Default.PathSave;
            saveFileDialog.FileName = obj.name + "_Frames.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.Save();

                obj.FramesDoc(saveFileDialog.FileName);
            }
        }

        private void colorPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Properties.Settings.Default.PathSave;
            saveFileDialog.FileName = obj.name + "_ColorPalettes.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.Save();

                obj.pltsDoc(saveFileDialog.FileName);
            }
        }

        private void fremesRelatedDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Properties.Settings.Default.PathSave;
            saveFileDialog.FileName = obj.name + "_FramesR.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.Save();

                obj.framesrelatedDoc(saveFileDialog.FileName);
            }
        }

        private void saveAnimationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (obj.animations[animationIndex] != null) createGif();

        }

        public void createGif()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Properties.Settings.Default.PathSave;
            saveFileDialog.FileName = obj.name + "_Animation_" + (obj.animations[animationIndex].getIndex() + 1) + ".gif";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.Save();

                ExportGif(animationIndex, saveFileDialog.FileName);
            }
        }

        private void saveAllAnimationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AnimationList.Items.Count != 0)
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                folder.SelectedPath = Properties.Settings.Default.PathSave;

                if (folder.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PathSave = folder.SelectedPath;
                    Properties.Settings.Default.Save();

                    for (int i = 0; i < AnimationList.Items.Count; i++)
                        ExportGif(i, Path.Combine(folder.SelectedPath, obj.name + "_Animation_" + (i + 1)));
                }
            }


        }

        private void savePaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (obj.plts.Size() != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Properties.Settings.Default.PathSave;
                saveFileDialog.FileName = obj.name + "_palette_" + (pltIndex + 1) + ".png";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PathSave = Path.GetDirectoryName(saveFileDialog.FileName);
                    Properties.Settings.Default.Save();

                    obj.plts.getPalette(pltIndex).Save(saveFileDialog.FileName);
                }
            }
        }

        private void saveAllPaletteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (obj.plts.Size() != 0)
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                folder.SelectedPath = Properties.Settings.Default.PathSave;

                if (folder.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PathSave = folder.SelectedPath;
                    Properties.Settings.Default.Save();

                    for (int i = 0; i < obj.plts.Size(); i++)
                    {
                        obj.plts.getPalette(i).Save(Path.Combine(folder.SelectedPath, obj.name + "_palette_" + (i + 1) + ".png"));
                    }
                }
            }

        }

        public void ExportGif(int animation, string path)
        {
            int frameCount = obj.animations[animation].Size();
            Bitmap[] renderedFrames = new Bitmap[frameCount];
            uint[] delays = new uint[frameCount];
            var m = new MagickFactory();

            for (int i = 0; i < frameCount; i++)
            {
                int frameIdx = obj.animations[animation].getFrame(i);
                if (frameIdx < 0 || frameIdx >= obj.frames.Size())
                    renderedFrames[i] = new Bitmap(1, 1);
                else
                    renderedFrames[i] = obj.getFrameCentered(animation, frameIdx, pltIndex);

                float currTime = obj.animations[animation].getTime(i);
                float nextTime = (i + 1 < frameCount) ? obj.animations[animation].getTime(i + 1) : obj.animations[animation].getLength();
                float durationSeconds = (nextTime - currTime) / Properties.Settings.Default.timeSpeed;
                delays[i] = (uint)Math.Max(2, Math.Round(durationSeconds * 100));
            }

            using (MagickImageCollection collection = new MagickImageCollection())
            {
                for (int i = 0; i < frameCount; i++)
                {
                    MagickImage image = new MagickImage(m.Image.Create(renderedFrames[i]));
                    image.AnimationDelay = delays[i];
                    image.GifDisposeMethod = GifDisposeMethod.Background;
                    collection.Add(image);

                    if (renderedFrames[i] != null)
                        renderedFrames[i].Dispose();
                }

                if (collection.Count > 0)
                {
                    collection[0].AnimationIterations = (uint)(CLoop.Checked ? 0 : 1);
                }

                collection.OptimizeTransparency();
                collection.Quantize(new QuantizeSettings()
                {
                    DitherMethod = DitherMethod.No,
                    Colors = 256
                });
                collection.Write(path, MagickFormat.Gif);
            }
        }

        private void Flip_Click(object sender, EventArgs e)
        {
            obj.frames.FlipO();
            RenderCurrentFrame();
        }

        private void BspriteSheet_Click(object sender, EventArgs e)
        {
            spriteSheetToolStripMenuItem_Click(sender, e);
        }
    }
}
