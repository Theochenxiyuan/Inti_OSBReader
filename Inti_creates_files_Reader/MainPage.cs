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
        private Image frame;
        private int frameIndex;
        private DateTime startTime;

        public MainPage()
        {
            InitializeComponent();
            obj = new OSB("");

            CLoop.Checked = Properties.Settings.Default.Loop;
            Tspeed.Text = (Properties.Settings.Default.timeSpeed).ToString();

        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            pic.Image = null;
            pic.SizeMode = PictureBoxSizeMode.Normal;
            pic.Paint += pic_Paint;


        }
        private void Bopen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Properties.Settings.Default.pathOpen;
            dialog.Filter = "Readable files|*.osb;*.scb|;All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.pathOpen = Path.GetDirectoryName(dialog.FileName);
                Properties.Settings.Default.Save();
                AnimationList.Items.Clear();
                Lpalletecount.Text = "0 out of 0";
                pltIndex = -1;
                picPalatte.Image = null;
                pic.Image = null;
                frameIndex = 0;
                frame = null;
                animationIndex = 0;

                obj = new OSB(dialog.FileName);
                obj.readData();
                if (obj.isOSB())
                {
                    for (int i = 0; i < obj.animations.Count(); i++)
                    {
                        ListViewItem item = new ListViewItem("Animation " + (i + 1));
                        AnimationList.Items.Add(item);
                    }
                }
                else spriteSheetToolStripMenuItem_Click(spriteSheetToolStripMenuItem, EventArgs.Empty);



                if (obj.plts.Size() != 0)
                {
                    pltIndex = -1;
                    displayPallete(obj.plts.getPalette(pltIndex));
                    Lpalletecount.Text = (pltIndex + 1) + " out of " + obj.plts.Size();
                }
                Lfile.Text = "File: " + obj.getName();

            }
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

                OSB obj2 = new OSB(dialog.FileName);
                obj2.plts = obj.plts;
                obj2.readData();
                obj.plts = obj2.plts;

                if (obj.plts.Size() != 0)
                {
                    pltIndex = -1;
                    displayPallete(obj.plts.getPalette(pltIndex));
                    Lpalletecount.Text = (pltIndex + 1) + " out of " + obj.plts.Size();
                }
            }
        }

        private void AnimationList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected) return;

            animationTimer.Stop();
            pic.Image = null;

            animationIndex = e.ItemIndex;
            frameIndex = 0;

            startTime = DateTime.Now;

            animationTimer.Interval = 10;

            animationTimer.Tick -= AnimationTimer_Tick;
            animationTimer.Tick += AnimationTimer_Tick;

            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (animationIndex < 0 || animationIndex >= obj.animations.Count || obj.animations[animationIndex].Size() == 0)
                return;

            if (!string.IsNullOrEmpty(Tspeed.Text) && Tspeed.Text.All(char.IsDigit))
            {
                Properties.Settings.Default.timeSpeed = int.Parse(Tspeed.Text);
                Properties.Settings.Default.Loop = CLoop.Checked;
                Properties.Settings.Default.Save();


            }
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

            TcurrFrame.Text = obj.animations[animationIndex].getFrame(frameIndex).ToString();
            frame = obj.getFrameCentered(animationIndex, obj.animations[animationIndex].getFrame(frameIndex), pltIndex);

            pic.Invalidate();
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


        private void BapplyPalette_Click(object sender, EventArgs e)
        {
            if (picPalatte.Image == null)
                return;
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
                Brush brush;
                for (int y = 0; y < oplt.Height; y++)
                {
                    for (int x = 0; x < oplt.Width; x++)
                    {
                        brush = new SolidBrush(oplt.GetPixel(x, y));
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
            saveFileDialog.FileName = obj.name + "_ColoerPalettes.txt";

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
                        obj.plts.getPalette(i).Save(folder.SelectedPath + obj.name + "_palette_" + (i + 1) + ".png");
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

        private void pic_Click(object sender, EventArgs e)
        {
            if (obj.animations[animationIndex] != null)
                createGif();
        }

        private void Flip_Click(object sender, EventArgs e)
        {
            obj.frames.FlipO();
            pic.Invalidate();
        }
    }
}
