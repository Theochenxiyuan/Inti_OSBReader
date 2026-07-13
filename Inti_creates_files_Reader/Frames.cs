using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inti_creates_files_Reader
{
    public class Frames
    {
        private List<Bitmap> frames;
        private List<Point> centers;
        private bool Flip = false;
        public Frames()
        {
            frames = new List<Bitmap>();
            centers = new List<Point>();
        }

        public Bitmap getFrame(int index)
        {
            if (index < 0 || index >= frames.Count)
                return null;
            else
            {
                Bitmap bmp = new Bitmap(frames[index]);
                if (Flip) bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);

                return bmp;
            }
                
        }
        public int Size()
        {
            return frames.Count;
        }
        public void addFrame(Bitmap bmp, Point center)
        {
            frames.Add(bmp);
            centers.Add(center);
        }

        public void DisposeImages()
        {
            foreach (Bitmap frame in frames)
                frame.Dispose();
            frames.Clear();
            centers.Clear();
        }

        public bool getFlip()
        {
            return Flip;
        }
        public void FlipO()
        {
            Flip = !Flip;
        }

        public Point getCenter(int index)
        {
            if (index < 0 || index >= centers.Count)
                return Point.Empty;

            if (Flip) return new Point(frames[index].Width - centers[index].X, centers[index].Y);
            else return centers[index];
        }
        public Point getMinCenter()
        {
            int minCenterX = centers.Any() ? centers.Min(p => p.X) : 0;
            int minCenterY = centers.Any() ? centers.Min(p => p.Y) : 0;
            return new Point(minCenterX, minCenterY);
        }

        public Point getMaxCenter()
        {
            int maxCenterX = centers.Any() ? centers.Max(p => p.X) : 0;
            int maxCenterY = centers.Any() ? centers.Max(p => p.Y) : 0;

            return new Point(maxCenterX, maxCenterY);
        }
        public Point getMaxFrame()
        {
            int maxWidth = 0;
            int maxHeight = 0;

            int width;
            int height;
            for (int i = 0; i < frames.Count; i++)
            {
                if (frames[i] != null)
                {
                    width = frames[i].Width - centers[i].X;
                    height = frames[i].Height - centers[i].Y;

                    if (width > maxWidth) maxWidth = width;
                    if (height > maxHeight) maxHeight = height;
                }
            }


            return new Point(maxWidth, maxHeight);
        }

        public void readFrames(ref byte[] data, Bitmap bmp, int index, string name)
        {
            int size = BinaryData.ReadInt32(data, index, "frame count");
            if (size < 0 || size > 1_000_000)
                throw new InvalidDataException($"Invalid frame count: {size}.");
            long headerLength = 0xcL + size * 0x18L;
            if (headerLength > int.MaxValue)
                throw new InvalidDataException("Frame table is too large.");
            BinaryData.EnsureRange(data, index, (int)headerLength, "frame table");
            int offset;
            offset = 0xc;

            int[,] frameIndex = new int[size, 2];
            List<Point> oPoint;
            List<Point> nPoint;

            for (int i = 0; i < size; i++)
            { // header
                frameIndex[i, 0] = BitConverter.ToInt32(data, index + offset) + (index + offset);
                frameIndex[i, 1] = BitConverter.ToInt32(data, index + offset + 0x8);
                if (frameIndex[i, 1] < 0 || frameIndex[i, 1] % 4 != 0)
                    throw new InvalidDataException($"Frame {i} has an invalid vertex count: {frameIndex[i, 1]}.");

                offset += 0x18;
            }

            for (int i = 0; i < size; i++)
            { // for all frames 
                if (frameIndex[i, 1] == 0)
                {
                    frames.Add(new Bitmap(1, 1));
                    centers.Add(new Point(0, 0));
                    continue;
                }

                oPoint = new List<Point>();
                nPoint = new List<Point>();
                int xMin = 0, yMin = 0, xMax = 0, yMax = 0;

                offset = frameIndex[i, 0];
                long vertexLength = frameIndex[i, 1] * 0x14L;
                if (vertexLength > int.MaxValue)
                    throw new InvalidDataException($"Frame {i} vertex data is too large.");
                BinaryData.EnsureRange(data, offset - 0x8, (int)vertexLength, $"frame {i} vertices");

                for (int j = 0; j < frameIndex[i, 1]; j++)
                { // get all points for a single Image
                    oPoint.Add(new Point(Convert.ToInt32(BitConverter.ToSingle(data, (offset + 0x4)) * bmp.Width), Convert.ToInt32(BitConverter.ToSingle(data, (offset + 0x8)) * bmp.Height)));
                    nPoint.Add(new Point(Convert.ToInt32(BitConverter.ToSingle(data, (offset - 0x8))), Convert.ToInt32(BitConverter.ToSingle(data, (offset - 0x4)))));

                    offset += 0x14;
                }
                xMin = nPoint.Min(p => p.X);
                xMax = nPoint.Max(p => p.X);
                yMin = nPoint.Min(p => p.Y);
                yMax = nPoint.Max(p => p.Y);

                if ((xMax - xMin) == 0 || (yMax - yMin) == 0) {
                    frames.Add(new Bitmap(1, 1));
                    centers.Add(new Point(0, 0));
                    continue;
                }

                centers.Add(new Point(xMax, yMax));
                Bitmap bitmap = new Bitmap((xMax - xMin) , (yMax - yMin));

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    for (int k = 0; k < nPoint.Count; k += 4)
                    { // create sections of the frame

                        int po1 = oPoint.GetRange(k, 4).Min(p => p.X);
                        int po2 = oPoint.GetRange(k, 4).Min(p => p.Y);
                        int po3 = oPoint.GetRange(k, 4).Max(p => p.X);
                        int po4 = oPoint.GetRange(k, 4).Max(p => p.Y);

                        int frameXMax = nPoint.GetRange(k, 4).Max(p => p.X);
                        int frameYMax = nPoint.GetRange(k, 4).Max(p => p.Y);


                        xMax = centers[i].X - frameXMax;
                        yMax = centers[i].Y - frameYMax;

                        Rectangle sourceRect = new Rectangle(po1, po2, po3 - po1, po4 - po2);
                        if (sourceRect.Width <= 0 || sourceRect.Height <= 0 ||
                            sourceRect.X < 0 || sourceRect.Y < 0 ||
                            sourceRect.Right > bmp.Width || sourceRect.Bottom > bmp.Height)
                            throw new InvalidDataException($"Frame {i} references pixels outside the sprite sheet.");

                        using Bitmap croppedBitmap = bmp.Clone(sourceRect, bmp.PixelFormat);

                        if (oPoint[k + 2].X > oPoint[k].X) croppedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        if (oPoint[k + 2].Y > oPoint[k].Y) croppedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                        g.DrawImage(croppedBitmap,
                                    new Rectangle(xMax, yMax, sourceRect.Width, sourceRect.Height),
                                    0, 0, croppedBitmap.Width, croppedBitmap.Height,
                                    GraphicsUnit.Pixel);
                    }
                }
                frames.Add(bitmap);
                
            }
        }

    }
}
