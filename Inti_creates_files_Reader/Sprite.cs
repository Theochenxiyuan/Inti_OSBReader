using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inti_creates_files_Reader
{
    public class Sprite
    {
        private Bitmap image;
        public Sprite(int width, int height) {
            image = new Bitmap(width, height);
        }
        public void setImage(Bitmap image)
        {
            this.image = image;
        }
        public Bitmap getImage() {
            return image;
        }

        public void readImage(ref byte[] data, int index)
        {
            long byteCount = (long)image.Width * image.Height * 4;
            if (byteCount > int.MaxValue)
                throw new InvalidDataException("Sprite sheet is too large.");
            BinaryData.EnsureRange(data, index, (int)byteCount, "sprite sheet pixels");

            int offset = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    byte b = data[index + offset];
                    byte g = data[index + offset + 1];
                    byte r = data[index + offset + 2];
                    byte a = data[index + offset + 3];

                    image.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                    offset += 4;
                }
            }
        }

        public byte[] WriteImage(string path)
        {
            try{
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "PNG files(*.png)|*.png";

                if (dialog.ShowDialog() == DialogResult.OK){
                    Bitmap original = new Bitmap(dialog.FileName);
                    byte[] originalByte = new byte[original.Width * original.Height*4];

                    MessageBox.Show(original.Width.ToString() + "-" + original.Height.ToString());
                    for (int y = 0; y < original.Height; y++){
                        for (int x = 0; x < original.Width; x++){
                            Color color = original.GetPixel(x, y);
                            originalByte[x + y * original.Height] = color.B;
                            originalByte[x + y * original.Height+1] = color.G;
                            originalByte[x + y * original.Height+2] = color.R;
                            originalByte[x + y * original.Height+3] = color.A;
                        }
                    }
                    return originalByte;
                }


            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public Bitmap extractPalette(string path){
            List<Color> palette = GetUniqueColors(new Bitmap(path));
            
            Bitmap newImage = image;
            for(int y = 0;y < newImage.Height; y++)
            {
                for(int x = 0;x < newImage.Width; x++)
                {
                    for(int c = 0; c < palette.Count; c++)
                    {
                        if (palette[c].Equals(newImage.GetPixel(x, y)))
                        {

                            newImage.SetPixel(x, y, Color.FromArgb(0xff, (c / 256) % 256, c / 256, c%256));
                            break;
                        }
                    }
                }
            }
            return newImage;

        }

        private List<Color> GetUniqueColors(Bitmap image)
        {
            List<Color> uniqueColor = new List<Color>();

            Color color = new Color();


            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    color = image.GetPixel(x, y);
                    if (!uniqueColor.Contains(color))
                        uniqueColor.Add(color);
                }
            }
            return uniqueColor;
        }

    }
}
