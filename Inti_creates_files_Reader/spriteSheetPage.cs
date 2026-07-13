
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inti_creates_files_Reader
{
    public partial class spriteSheetPage : Form
    {
        private OSB obj;
        private bool[] is_visible = new bool[3];
        private Bitmap[] displayedBitmaps;
        private int pltIndex;
        private Bitmap uncolored;
        private readonly List<FramePlacement> framePlacements = new List<FramePlacement>();

        private int FrameSpacing => (int)Nspacing.Value;

        private sealed record FramePlacement(int Index, int X, int Y, int Width, int Height, int CenterX, int CenterY);

        public spriteSheetPage(ref OSB obj)
        {
            InitializeComponent();
            this.obj = obj;
            Lfile.Text = "File: " + obj.getName();
            Text = obj.getName() + " - Sprite Sheet";
            Nspacing.Enabled = obj.isOSB();
            button1.Enabled = obj.isOSB();
            button1.Text = obj.isOSB() ? "Show Original Atlas" : "Original Atlas";
            LviewMode.Text = obj.isOSB() ? "View: Reconstructed Frames" : "View: Original Atlas";

            displayedBitmaps = new Bitmap[3];
            pltIndex = -1;

            if (obj.plts.Size() != 0)
            {
                pltIndex = -1;
                displayPallete(obj.plts.getPalette(pltIndex));
                Lpalletecount.Text = (pltIndex + 1) + " out of " + obj.plts.Size();


            }
            is_visible[0] = true;
            is_visible[1] = true;
            is_visible[2] = true;
            createSpriteSheet();
        }

        private void DisposeSpriteSheetImages()
        {
            Image? preview = pic.Image;
            pic.Image = null;
            preview?.Dispose();

            var images = new HashSet<Bitmap>(ReferenceEqualityComparer.Instance);
            foreach (Bitmap? bitmap in displayedBitmaps)
            {
                if (bitmap != null)
                    images.Add(bitmap);
            }
            if (uncolored != null)
                images.Add(uncolored);

            foreach (Bitmap bitmap in images)
                bitmap.Dispose();

            displayedBitmaps = new Bitmap[3];
            uncolored = null!;
        }

        private void ReplaceSpriteBitmap(Bitmap bitmap)
        {
            Bitmap? previous = displayedBitmaps[1];
            displayedBitmaps[1] = bitmap;
            if (previous != null && !ReferenceEquals(previous, uncolored) && !ReferenceEquals(previous, bitmap))
                previous.Dispose();
        }

        private void createSpriteSheet()
        {
            if (obj.isOSB())
            {
                int width = 0;
                int height = 0;
                int heightFix = 0;
                int maxWidth;

                int frameCount = obj.frames.Size();
                if (frameCount == 0) return;

                DisposeSpriteSheetImages();
                framePlacements.Clear();

                int avgWidth = 0;
                int avgHeight = 0;
                for (int i = 0; i < frameCount; i++)
                {
                    Bitmap frm = obj.getFrame(i, pltIndex);
                    Point center = obj.frames.getCenter(i);
                    int ew = center.X < 0 ? frm.Width - center.X : Math.Max(frm.Width, center.X + 1);
                    int eh = center.Y < 0 ? frm.Height - center.Y : Math.Max(frm.Height, center.Y + 1);
                    avgWidth += ew;
                    avgHeight += eh;
                    frm.Dispose();
                }
                avgWidth /= frameCount;
                avgHeight /= frameCount;

                int cols = Math.Max(1, (int)Math.Ceiling(Math.Sqrt((double)frameCount * avgWidth / avgHeight)));
                cols = Math.Min(cols, frameCount);
                maxWidth = cols * (avgWidth + FrameSpacing);

                int currX = 0, currRowHeight = 0;
                for (int i = 0; i < frameCount; i++)
                {
                    Bitmap frm = obj.getFrame(i, pltIndex);
                    Point center = obj.frames.getCenter(i);

                    int effectiveWidth = center.X < 0 ? frm.Width - center.X : Math.Max(frm.Width, center.X + 1);
                    int effectiveHeight = center.Y < 0 ? frm.Height - center.Y : Math.Max(frm.Height, center.Y + 1);

                    if (maxWidth < currX + effectiveWidth + FrameSpacing)
                    {
                        heightFix += currRowHeight + FrameSpacing;
                        currRowHeight = 0;
                        currX = 0;
                    }

                    currRowHeight = Math.Max(currRowHeight, effectiveHeight);
                    currX += effectiveWidth + FrameSpacing;
                    frm.Dispose();
                }

                height = heightFix + currRowHeight + FrameSpacing;
                width = maxWidth;

                displayedBitmaps[0] = new Bitmap(width, height);
                displayedBitmaps[1] = new Bitmap(width, height);
                displayedBitmaps[2] = new Bitmap(width, height);

                // Pass 2: draw frames and centers
                int indexX = 0, indexY = 0, tempy = 0;

                for (int i = 0; i < frameCount; i++)
                {
                    Bitmap bmp = obj.getFrame(i, pltIndex);
                    Point center = obj.frames.getCenter(i);

                    int effectiveWidth = center.X < 0 ? bmp.Width - center.X : Math.Max(bmp.Width, center.X + 1);
                    int effectiveHeight = center.Y < 0 ? bmp.Height - center.Y : Math.Max(bmp.Height, center.Y + 1);

                    if (indexX + effectiveWidth + FrameSpacing > width)
                    {
                        indexX = 0;
                        indexY += tempy + FrameSpacing;
                        tempy = 0;
                    }

                    int leftPad = center.X < 0 ? -center.X : 0;
                    int topPad = center.Y < 0 ? -center.Y : 0;

                    int drawX = indexX + leftPad;
                    int drawY = indexY + topPad;
                    framePlacements.Add(new FramePlacement(i, drawX, drawY, bmp.Width, bmp.Height, center.X, center.Y));

                    // Background
                    using (Graphics g = Graphics.FromImage(displayedBitmaps[0]))
                    using (Brush br = new SolidBrush(backgroundColor.BackColor))
                        g.FillRectangle(br, drawX, drawY, bmp.Width, bmp.Height);

                    // Frame
                    using (Graphics g = Graphics.FromImage(displayedBitmaps[1]))
                        g.DrawImage(bmp, drawX, drawY);

                    // Center
                    int dotX = center.X < 0 ? indexX : indexX + center.X;
                    int dotY = center.Y < 0 ? indexY : indexY + center.Y;
                    if (dotX >= 0 && dotX < width && dotY >= 0 && dotY < height)
                        displayedBitmaps[2].SetPixel(dotX, dotY, centerColor.BackColor);

                    tempy = Math.Max(tempy, effectiveHeight);
                    indexX += effectiveWidth + FrameSpacing;
                    bmp.Dispose();
                }
            }
            else
            {
                DisposeSpriteSheetImages();
                framePlacements.Clear();
                displayedBitmaps[1] = new Bitmap(obj.spritesheet.getImage());
                is_visible[0] = false;
                is_visible[2] = false;
            }

            uncolored = displayedBitmaps[1];
            displaySpriteSheet();
        }

        private void BapplyPalette_Click(object sender, EventArgs e)
        {
            if (picPalatte.Image == null)
                return;
            if (is_visible[1])
            {
                if (pltIndex == -1) ReplaceSpriteBitmap(uncolored);
                else ReplaceSpriteBitmap(obj.applyColor(ref uncolored, pltIndex));
            }
            else
            {
                Bitmap bmp = obj.spritesheet.getImage();
                if (pltIndex == -1) ReplaceSpriteBitmap(new Bitmap(bmp));
                else ReplaceSpriteBitmap(obj.applyColor(ref bmp, pltIndex));
            }
            displaySpriteSheet();
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
                Image? previous = picPalatte.Image;
                picPalatte.Image = plt;
                previous?.Dispose();
            }

        }

        public void displaySpriteSheet()
        {
            Bitmap bmp = new Bitmap(displayedBitmaps[1].Width, displayedBitmaps[1].Height);
            if (is_visible[1])
            {
                if (is_visible[0])
                {
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.DrawImage(displayedBitmaps[0], 0, 0);
                    }
                }
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawImage(displayedBitmaps[1], 0, 0);
                }

                if (is_visible[2])
                {
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.DrawImage(displayedBitmaps[2], 0, 0);
                    }
                }

            }
            else
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawImage(displayedBitmaps[1], 0, 0);
                }
            }
            Image? previous = pic.Image;
            pic.Image = bmp;
            previous?.Dispose();
            pic.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Properties.Settings.Default.PathSave;
            saveFileDialog.FileName = obj.name + "_spriteSheet.png";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.Save();

                pic.Image.Save(saveFileDialog.FileName);
                SaveFrameMetadata(saveFileDialog.FileName);
            }

        }

        private void SaveAll_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.SelectedPath = Properties.Settings.Default.PathSave;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PathSave = folder.SelectedPath;
                Properties.Settings.Default.Save();

                for (int i = 0; i < displayedBitmaps.Length; i++)
                {
                    if (is_visible[i])
                    {
                        if (i == 0)
                            displayedBitmaps[i].Save(Path.Combine(folder.SelectedPath, obj.name + "_background.png"));
                        else if (i == 1)
                            displayedBitmaps[i].Save(Path.Combine(folder.SelectedPath, obj.name + "_spriteSheet.png"));
                        else if (i == 2)
                            displayedBitmaps[i].Save(Path.Combine(folder.SelectedPath, obj.name + "_centerpoints.png"));
                    }
                }
                SaveFrameMetadata(Path.Combine(folder.SelectedPath, obj.name + "_spriteSheet.png"));
            }

        }





        private void Bbackground_Click(object sender, EventArgs e)
        {
            if (obj.isOSB())
            {
                is_visible[0] = !is_visible[0];
                displaySpriteSheet();
            }

        }
        private void Sprite_Click(object sender, EventArgs e)
        {
            if (is_visible[1])
            {
                ReplaceSpriteBitmap(new Bitmap(obj.spritesheet.getImage()));
                LviewMode.Text = "View: Original Atlas";
                button1.Text = "Show Reconstructed";
            }
            else
            {
                ReplaceSpriteBitmap(uncolored);
                LviewMode.Text = "View: Reconstructed Frames";
                button1.Text = "Show Original Atlas";
            }
            is_visible[1] = !is_visible[1];
            displaySpriteSheet();

        }
        private void Center_Click(object sender, EventArgs e)
        {
            if (obj.isOSB())
            {
                is_visible[2] = !is_visible[2];
                displaySpriteSheet();
            }
        }

        private void colorPaletteToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void allColorPalettesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void backgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();

            if (color.ShowDialog() == DialogResult.OK)
            {
                backgroundColor.BackColor = color.Color;
                Bitmap bmp = displayedBitmaps[0];
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        if (bmp.GetPixel(x, y).A > 0) bmp.SetPixel(x, y, color.Color);
                    }
                }
                displaySpriteSheet();
                Properties.Settings.Default.colorBackground = color.Color.ToArgb();
                Properties.Settings.Default.Save();
            }
        }

        private void centerColor_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();

            if (color.ShowDialog() == DialogResult.OK)
            {
                centerColor.BackColor = color.Color;
                Bitmap bmp = displayedBitmaps[2];
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        if (bmp.GetPixel(x, y).A > 0) bmp.SetPixel(x, y, color.Color);
                    }
                }
                displaySpriteSheet();
                Properties.Settings.Default.colorCenter = color.Color.ToArgb();
                Properties.Settings.Default.Save();
            }
        }

        private void Flip_Click(object sender, EventArgs e)
        {
            if (is_visible[1]){
                obj.frames.FlipO();
                createSpriteSheet();
                pic.Invalidate();
            }
            
        }

        private void Nspacing_ValueChanged(object sender, EventArgs e)
        {
            if (obj.isOSB())
            {
                is_visible[1] = true;
                LviewMode.Text = "View: Reconstructed Frames";
                button1.Text = "Show Original Atlas";
                createSpriteSheet();
            }
        }

        private void SaveFrameMetadata(string spriteSheetPath)
        {
            if (!obj.isOSB() || !is_visible[1]) return;

            string basePath = Path.Combine(
                Path.GetDirectoryName(spriteSheetPath) ?? "",
                Path.GetFileNameWithoutExtension(spriteSheetPath));

            var animations = obj.animations.Select((animation, animationIndex) => new
            {
                index = animationIndex,
                length = animation.getLength(),
                frames = Enumerable.Range(0, animation.Size()).Select(sequenceIndex => new
                {
                    sequenceIndex,
                    frameIndex = animation.getFrame(sequenceIndex),
                    time = animation.getTime(sequenceIndex)
                })
            });

            var metadata = new
            {
                file = obj.getName(),
                view = "Reconstructed Frames",
                spacing = FrameSpacing,
                sheet = new { width = displayedBitmaps[1].Width, height = displayedBitmaps[1].Height },
                frames = framePlacements.Select(frame => new
                {
                    index = frame.Index,
                    x = frame.X,
                    y = frame.Y,
                    width = frame.Width,
                    height = frame.Height,
                    centerX = frame.CenterX,
                    centerY = frame.CenterY
                }),
                animations
            };

            File.WriteAllText(basePath + ".json", JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true }));

            var csv = new StringBuilder("FrameIndex,X,Y,Width,Height,CenterX,CenterY,AnimationUses\r\n");
            foreach (FramePlacement frame in framePlacements)
            {
                string uses = string.Join(";", obj.animations.SelectMany((animation, animationIndex) =>
                    Enumerable.Range(0, animation.Size())
                        .Where(sequenceIndex => animation.getFrame(sequenceIndex) == frame.Index)
                        .Select(sequenceIndex => $"{animationIndex}:{sequenceIndex}")));
                csv.AppendLine(string.Join(",", new[]
                {
                    frame.Index.ToString(CultureInfo.InvariantCulture),
                    frame.X.ToString(CultureInfo.InvariantCulture),
                    frame.Y.ToString(CultureInfo.InvariantCulture),
                    frame.Width.ToString(CultureInfo.InvariantCulture),
                    frame.Height.ToString(CultureInfo.InvariantCulture),
                    frame.CenterX.ToString(CultureInfo.InvariantCulture),
                    frame.CenterY.ToString(CultureInfo.InvariantCulture),
                    $"\"{uses}\""
                }));
            }
            File.WriteAllText(basePath + ".csv", csv.ToString());
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DisposeSpriteSheetImages();
            Image? palette = picPalatte.Image;
            picPalatte.Image = null;
            palette?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
