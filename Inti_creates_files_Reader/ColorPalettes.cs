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
            int count = (end - start) / 4;
            int height = count / 257 + 1;
            int width = count > 256? 256 : count % 257;
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
            int PaletteCount = BitConverter.ToInt32(data, index + 0x1c);
            if (PaletteCount <= 0)
            {
                plts.Add(basePlt);
                return;
            }
            int colorIndex = BitConverter.ToInt32(data, index + 0x20);
            int colorCount = BitConverter.ToInt32(data, index + 0x24);
            int start = BitConverter.ToInt32(data, index + 0x28) + index;

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
            int offset = BitConverter.ToInt32(data, 0x74);
            int realHeight = BitConverter.ToInt32(data, 0x70);

            basePlt = new Bitmap(256, findHeight(realHeight));
            int end = basePlt.Width * realHeight * 4 + offset;

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
            while (data[currntChange + 0x3] != 0xff && currntChange < BitConverter.ToInt32(data, 0x48))
            {

                for (int findY = BitConverter.ToInt32(data, 0x40); findY < BitConverter.ToInt32(data, 0x74); findY += 4)
                {
                    if (BitConverter.ToInt32(data, findY) == BitConverter.ToInt32(data, currntChange))
                    {
                        int pltsNumber = BitConverter.ToInt32(data, currntChange + 0x1c);
                        for (int i = 0; i < pltsNumber; i++)
                        {
                            Bitmap plt;
                            int y;
                            if (plts.Count > i) plt = plts[i];
                            else plt = new Bitmap(basePlt);
                            offset = end + BitConverter.ToInt32(data, currntChange + 0x28);


                            for (int x = BitConverter.ToInt32(data, currntChange + 0x20); x < BitConverter.ToInt32(data, currntChange + 0x24) + BitConverter.ToInt32(data, currntChange + 0x20); x++)
                            {
                                byte b = data[offset];
                                byte g = data[offset+ 1];
                                byte r = data[offset + 2];
                                byte a = data[offset + 3];
                                offset += 4;

                                plt.SetPixel(x, Convert.ToInt32(data[findY - 0x3]), Color.FromArgb(a, r, g, b));
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
