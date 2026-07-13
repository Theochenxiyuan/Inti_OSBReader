using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Inti_creates_files_Reader
{
    public class ColorPalettes
    {
        private Bitmap basePlt;
        private List<Bitmap> plts;

        public ColorPalettes() {
            basePlt = new Bitmap(256,1);
            plts = new List<Bitmap>();
        }

        public Bitmap getPalette()
        {
            return basePlt;
        }
        public void setPalette(Bitmap palette)
        {
            this.basePlt = palette;
        }
        public int Size()
        {
            return plts.Count;
        }


        public Bitmap getPalette(int index)
        {
            if (index < -1 || index >= plts.Count)
                return null;
            else if (index == -1)
                return basePlt;
            return plts[index];
        }
        public void addPalette(Bitmap palette)
        {
            plts.Add(palette);
        }
        public void removePalette(int index)
        {
            plts.RemoveAt(index);
        }

        public void readPaletteB(ref byte[] data, int start, int end)
        {
            if (end <= start || (end - start) % 4 != 0)
                throw new InvalidDataException("Base palette has an invalid data range.");
            BinaryData.EnsureRange(data, start, end - start, "base palette");
            int count = (end - start) / 4;
            int height = count / 257 + 1;
            int width = count > 256? 256 : count % 257;
            if (width <= 0 || height <= 0 || height > 32768)
                throw new InvalidDataException($"Invalid base palette dimensions: {width}x{height}.");
            basePlt = new Bitmap(width, height);

            int offset = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte b = data[start + offset];
                    byte g = data[start + offset + 1];
                    byte r = data[start + offset + 2];
                    byte a = data[start + offset + 3];

                    offset += 4;
                    basePlt.SetPixel(x,y,Color.FromArgb(a, r, g, b));
                }
            }
            
        }

        public void readPaletteG(ref byte[] data, int index)
        {
            BinaryData.EnsureRange(data, index, 0x2c, "palette header");
            int PaletteCount = BitConverter.ToInt32(data, index + 0x1c);
            if (PaletteCount < 0 || PaletteCount > 1_000_000)
                throw new InvalidDataException($"Invalid palette count: {PaletteCount}.");
            if (PaletteCount <= 0)
            {
                plts.Add(basePlt);
                return;
            }
            int colorIndex = BitConverter.ToInt32(data, index + 0x20);
            int colorCount = BitConverter.ToInt32(data, index + 0x24);
            int start = BitConverter.ToInt32(data, index + 0x28) + index;
            if (colorIndex < 0 || colorCount < 0 || colorIndex + colorCount > basePlt.Width)
                throw new InvalidDataException("Palette color range is outside the base palette.");
            long paletteBytes = (long)PaletteCount * colorCount * basePlt.Height * 4;
            if (paletteBytes > int.MaxValue)
                throw new InvalidDataException("Palette data is too large.");
            BinaryData.EnsureRange(data, start, (int)paletteBytes, "palette colors");

            int offset = 0;
            Bitmap plt;
            byte r, g, b, a;
            for (int i = 0; i < PaletteCount; i++)
            {
                plt = new Bitmap(basePlt);
                for (int y = 0; y < plt.Height; y++)
                {
                    for (int x = colorIndex; x < (colorCount + colorIndex); x++)
                    {
                        b = data[start + offset];
                        g = data[start + offset + 1];
                        r = data[start + offset + 2];
                        a = data[start + offset + 3];
                        offset += 4;

                        plt.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                    }

                }
                plts.Add(plt);
            }
        }
        public void readPaletteSCB(ref byte[] data)
        {
            BinaryData.EnsureRange(data, 0, 0x84, "SCB palette header");
            int offset = BitConverter.ToInt32(data, 0x74);
            int realHeight = BitConverter.ToInt32(data, 0x70);
            if (realHeight <= 0 || realHeight > 32768)
                throw new InvalidDataException($"Invalid SCB palette height: {realHeight}.");
            long basePaletteBytes = (long)basePlt.Width * realHeight * 4;
            if (basePaletteBytes > int.MaxValue)
                throw new InvalidDataException("SCB base palette is too large.");
            BinaryData.EnsureRange(data, offset, (int)basePaletteBytes, "SCB base palette");

            basePlt = new Bitmap(256, findHeight(realHeight));
            int end = (int)basePaletteBytes + offset;

            for (int y = 0; y < realHeight; y++)
            {
                for (int x = 0; x < basePlt.Width; x++)
                {
                    byte b = data[offset];
                    byte g = data[offset + 1];
                    byte r = data[offset + 2];
                    byte a = data[offset + 3];
                    offset += 4;

                    basePlt.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            int currntChange = end;
            int changeEnd = BitConverter.ToInt32(data, 0x48);
            while (currntChange < changeEnd)
            {
                BinaryData.EnsureRange(data, currntChange, 0x34, "SCB palette change");
                if (data[currntChange + 0x3] == 0xff)
                    break;

                for (int findY = BitConverter.ToInt32(data, 0x40); findY < BitConverter.ToInt32(data, 0x74); findY += 4)
                {
                    BinaryData.EnsureRange(data, findY - 0x3, 7, "SCB palette row mapping");
                    if (BitConverter.ToInt32(data, findY) == BitConverter.ToInt32(data, currntChange))
                    {
                        int pltsNumber = BitConverter.ToInt32(data, currntChange + 0x1c);
                        int firstColor = BitConverter.ToInt32(data, currntChange + 0x20);
                        int colorCount = BitConverter.ToInt32(data, currntChange + 0x24);
                        if (pltsNumber < 0 || pltsNumber > 1_000_000 || firstColor < 0 || colorCount < 0 || firstColor + colorCount > basePlt.Width)
                            throw new InvalidDataException("SCB palette change has invalid dimensions.");
                        for (int i = 0; i < pltsNumber; i++)
                        {
                            Bitmap plt;
                            int y;
                            if (plts.Count > i) plt = plts[i];
                            else plt = new Bitmap(basePlt);
                            offset = end + BitConverter.ToInt32(data, currntChange + 0x28);
                            BinaryData.EnsureRange(data, offset, colorCount * 4, "SCB palette change colors");

                            int paletteRow = data[findY - 0x3];
                            if (paletteRow >= plt.Height)
                                throw new InvalidDataException($"SCB palette row {paletteRow} is outside the palette.");


                            for (int x = firstColor; x < colorCount + firstColor; x++)
                            {
                                byte b = data[offset];
                                byte g = data[offset+ 1];
                                byte r = data[offset + 2];
                                byte a = data[offset + 3];
                                offset += 4;

                                plt.SetPixel(x, paletteRow, Color.FromArgb(a, r, g, b));
                            }
                            if ((plts.Count > i)) plts[i] = plt;
                            else plts.Add(plt);
                        }
                    }

                }

                currntChange += 0x34;
            }
        }

        public Color getColor(int index, int colorIndex = -1)
        {
            int height = index / 256 ;
            int width = index > 255 ? 255 : index % 256;

            if (colorIndex == -1)
                return basePlt.GetPixel(width,height);
            else
                return plts[colorIndex].GetPixel(width, height);
        }

        private int findHeight(int num, int val = 1)
        {
            if (num > val) val = findHeight(num, val * 2);
            return val;
        }
    }
}
