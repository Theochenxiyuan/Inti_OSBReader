
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace Inti_creates_files_Reader
{
    public class OSB
    {
        private bool is_OSB;
        private byte[] data;
        public string name;
        public ColorPalettes plts;
        public List<Animation> animations;
        public Sprite spritesheet;
        public Frames frames;
        private int Size;

        //private byte[] header;
        private int SHeader;
        //private int Sanimation1;
        //private int Sanimation2;
        //private int Sanimation3;

        //private int Banimation1;
        //private int Banimation2;
        //private int Banimation3;

        private int SFramesData;
        private int BFrameData;
        private int NAnimation;
        private int NFrames;
        private int Frame28;

        private int IHSpritesheet;
        private int SPalette;
        private int SPaletteData;
        private int BPaletteOriginal;
        private int BPaletteDataModified;

        private int SSpritesheet;
        private int BSpriteSheet;
        private int spriteSheetWidth;
        private int spriteSheetHeight;


        private Point centerMax;
        private Point FrameMax;

        public OSB(string filePath){
            if (File.Exists(filePath))
            {
                data = File.ReadAllBytes(filePath);
                name = Path.GetFileName(filePath);
                animations = new List<Animation>();
                frames = new Frames();
                plts = new ColorPalettes();
                centerMax = new Point();
                FrameMax = new Point();
                is_OSB = true;
            }
        }

        public void readData(){
            SHeader = BitConverter.ToInt32(data, 0x0);
            if(SHeader == 0x6c) {
                SFramesData = BitConverter.ToInt32(data, 0x18);
                BFrameData = BitConverter.ToInt32(data, 0x1c);
                NAnimation = BitConverter.ToInt32(data, 0x20);
                NFrames = BitConverter.ToInt32(data, 0x24);//also at the index pointed in IFrameData
                Frame28 = BitConverter.ToInt32(data, 0x28);
                //0x28 related to frames(same length) still unknown what this data do

                IHSpritesheet = BitConverter.ToInt32(data, 0x4c);
                Size = BitConverter.ToInt32(data, 0x50);//50 & 54 & 58 & 5c
                SPalette = BitConverter.ToInt32(data, 0x60);
                BPaletteOriginal = BitConverter.ToInt32(data, 0x64);
                BPaletteDataModified = BitConverter.ToInt32(data, 0x68);

                //spritesheet header
                BSpriteSheet = BitConverter.ToInt32(data, IHSpritesheet);
                SSpritesheet = BitConverter.ToInt32(data, IHSpritesheet + 0x4);
                //IHSpritesheet + 0x8 is an ID ???
                spriteSheetWidth = BitConverter.ToInt32(data, IHSpritesheet + 0xc);
                spriteSheetHeight = BitConverter.ToInt32(data, IHSpritesheet + 0x10);
                //IHSpritesheet + 0x14 ???

            }
            else if(SHeader == 0x38){
                SSpritesheet = BitConverter.ToInt32(data, 0x4);
                // 0x8 ??? width of seperation between pixels ???
                spriteSheetWidth = BitConverter.ToInt32(data, 0xc);

                spriteSheetHeight = BitConverter.ToInt32(data, 0x10);
                BSpriteSheet = BitConverter.ToInt32(data, 0x14);
                SFramesData = BitConverter.ToInt32(data, 0x18);
                BFrameData = BitConverter.ToInt32(data, 0x1c);

                NAnimation = BitConverter.ToInt32(data, 0x20);
                NFrames = BitConverter.ToInt32(data, 0x24);//also at the index pointed in IFrameData
                Frame28 = BitConverter.ToInt32(data, 0x28);
            }else if(SHeader == 0x8c)
            {
                is_OSB = false;
                spriteSheetWidth = BitConverter.ToInt16(data, 0x48);
                spriteSheetHeight = BitConverter.ToInt16(data, 0x4a);
                BSpriteSheet = BitConverter.ToInt32(data, 0x4c);

                //SPalette = BitConverter.ToInt32(data, 0x70);//real height
                BPaletteOriginal = BitConverter.ToInt32(data, 0x74);
                BPaletteDataModified = BitConverter.ToInt32(data, 0x80);

                plts.readPaletteSCB(ref data);

                spritesheet = new Sprite(spriteSheetWidth, spriteSheetHeight);
                spritesheet.readImage(ref data, BSpriteSheet);
                spritesheet.getImage().Save("C:\\Users\\Hasan\\Downloads\\S\\a.png");

            }else if( SHeader == 0x60)
            {
                SFramesData = BitConverter.ToInt32(data, 0x18);
                BFrameData = BitConverter.ToInt32(data, 0x1c);
                NAnimation = BitConverter.ToInt32(data, 0x20);
                NFrames = BitConverter.ToInt32(data, 0x24);
                Frame28 = BitConverter.ToInt32(data, 0x28);
                

                IHSpritesheet = BitConverter.ToInt32(data, 0x4c);
                Size = BitConverter.ToInt32(data, 0x50);//50 & 54 & 58 & 5c

                //spritesheet header
                BSpriteSheet = BitConverter.ToInt32(data, IHSpritesheet);
                SSpritesheet = BitConverter.ToInt32(data, IHSpritesheet + 0x4);
                
                spriteSheetWidth = BitConverter.ToInt32(data, IHSpritesheet + 0xc);
                spriteSheetHeight = BitConverter.ToInt32(data, IHSpritesheet + 0x10);
            }

            if (SPalette != 0)
            {
                if (BPaletteOriginal < BPaletteDataModified)
                    plts.readPaletteB(ref data, BPaletteOriginal, BPaletteDataModified);
                plts.readPaletteG(ref data, BPaletteDataModified);
            }
            

            if (SSpritesheet != 0)
            {
                spritesheet = new Sprite(spriteSheetWidth, spriteSheetHeight);
                spritesheet.readImage(ref data, BSpriteSheet);
            }
            if(NFrames != 0)
            {
                frames = new Frames();
                frames.readFrames(ref data, spritesheet.getImage(), BFrameData,name);
            }


            if (NAnimation != 0)
            {
                animations = new List<Animation>();
                Frames aniFrame;
                for (int i = 0; i < NAnimation; i++)
                {
                    aniFrame = new Frames();
                    Animation a = new Animation(i);
                    a.readAnimation(ref data, SHeader, i, frames.Size());
                    for(int j = 0;j < a.Size(); j++)
                    {
                        aniFrame.addFrame(frames.getFrame(a.getFrame(j)), frames.getCenter(a.getFrame(j)));
                    }
                    

                    a.getMaxCentered(ref aniFrame);
                    animations.Add(a);
                }
            }

            
        }
        public bool isOSB()
        {
            return is_OSB;
        }

        public string getName()
        {
            return name;
        }


        public Bitmap getFrame(int index, int paletteIndex = -1)
        {
            if (paletteIndex != -1)
                return applyColor(index, paletteIndex);
            else
                return frames.getFrame(index);
            
        }

        public Bitmap applyColor(int frameIndex, int paletteIndex)
        {
            if (frameIndex < 0 || frameIndex >= frames.Size())
                return null;
            Bitmap bmp = new Bitmap(frames.getFrame(frameIndex));    
            Bitmap plt = plts.getPalette(paletteIndex);
            Color color;

            int indexX;
            int indexY;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    color = bmp.GetPixel(x, y);
                    if (color.B > 0) { bmp.SetPixel(x, y, color); continue; }
                    else if (plt.Height == 1 && color.G > 0) { bmp.SetPixel(x, y, color); continue; }

                    indexX = (color.R + color.G * plt.Height) % 256;
                    indexY = (color.R + color.G * plt.Height) / 256;
                    if (indexX < plt.Width) color = plt.GetPixel(indexX, indexY);
                    else color = plt.GetPixel(0, 0);

                    bmp.SetPixel(x, y, color);
                }
            }
            return bmp;
        }

        public Bitmap applyColor(ref Bitmap image, int paletteIndex)
        {
            Bitmap bmp = new Bitmap(image);
            Bitmap plt = plts.getPalette(paletteIndex);
            Color color;

            int indexX;
            int indexY;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    color = bmp.GetPixel(x, y);
                    if (color.B > 0) { bmp.SetPixel(x, y, color); continue; }
                    else if (plt.Height == 1 && color.G > 0) { bmp.SetPixel(x, y, color); continue; }

                    indexX = (color.R + color.G * plt.Height) % 256;
                    indexY = (color.R + color.G * plt.Height) / 256;
                    if (indexX < plt.Width) color = plt.GetPixel(indexX, indexY);
                    else color = plt.GetPixel(0, 0);

                    bmp.SetPixel(x, y, color);
                }
            }
            return bmp;
        }

        public Bitmap getFrameCentered(int animationIndex, int frameIndex, int pltIndex)
        {
            int gCenterX = animations[animationIndex].getGCenter().X;
            int gCenterY = animations[animationIndex].getGCenter().Y;
            int gDimX = animations[animationIndex].getGDimension().X;
            int gDimY = animations[animationIndex].getGDimension().Y;

            int width = gCenterX + gDimX;
            int height = gCenterY + gDimY;

            Bitmap bmp = new Bitmap(width, height);
            Bitmap frameBmp = getFrame(frameIndex, pltIndex);

            Point frameCenter = frames.getCenter(frameIndex);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);

                int drawX, drawY;

                if (frames.getFlip()) drawX = gDimX - frameCenter.X;
                else drawX = gCenterX - frameCenter.X;

                drawY = gCenterY - frameCenter.Y;

                g.DrawImage(frameBmp, drawX, drawY);
            }

            return bmp;
        }

        public void Doc(string folder)
        {
            if(is_OSB)
            {
                FramesDoc(folder + "_Frames.txt");
                AnimationDoc(folder + "_Animation.txt");
                framesrelatedDoc(folder + "_FramesR.txt");
            }
            headerDoc(folder + "_header.txt");
            
            pltsDoc(folder + "_ColorPalettes.txt");
            spriteDoc(folder + "_SpriteSheet.txt");
            
        }
        public void headerDoc(string file)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine("offset: 0x0");
                int length = BitConverter.ToInt32(data, 0);
                for (int i = 0; i < length; i += 16)
                {
                    int count = Math.Min(16, length - i);

                    string line = BitConverter.ToString(data, i, count).Replace("-", " ");

                    sw.WriteLine(line);
                }
                sw.WriteLine();

                sw.WriteLine("0x0 block size: " + length);
                sw.WriteLine("0x4 Image size (width x height x 4): " + BitConverter.ToInt32(data, 0x4));
                sw.WriteLine("0x8 ???: " + BitConverter.ToInt32(data, 0x8));
                sw.WriteLine("0xC - 0x10 Image dimensions: " + BitConverter.ToInt32(data, 0xc) + "-" + BitConverter.ToInt32(data, 0x10));
                sw.WriteLine("0x14 Image Start at offset: 0x" + BitConverter.ToInt32(data, 0x14).ToString("X"));
                sw.WriteLine("0x18 Rearrange Image data size: 0x" + BitConverter.ToInt32(data, 0x18).ToString("X"));
                sw.WriteLine("0x1C Rearrange Image data start at offset: 0x" + BitConverter.ToInt32(data, 0x1c).ToString("X"));
                sw.WriteLine("0x20 # animations: " + BitConverter.ToInt32(data, 0x20));
                sw.WriteLine("0x24 # Frames: " + BitConverter.ToInt32(data, 0x24));
                sw.WriteLine("0x28 offset to values point to Image header?(same # frames): 0x" + BitConverter.ToInt32(data, 0x28).ToString("X"));
                sw.WriteLine("0x2c hitbox size (width x height x 4)?: " + BitConverter.ToInt32(data, 0x2c));
                sw.WriteLine("0x30 ???: " + BitConverter.ToInt32(data, 0x30));
                sw.WriteLine("0x34 Image start at offset: 0x" + BitConverter.ToInt32(data, 0x34).ToString("X"));
                if (length == 0x6c)
                {
                    
                    sw.WriteLine("0x2C ???: " + BitConverter.ToInt32(data, 0x2c));

                    sw.WriteLine("0x30 ???: " + BitConverter.ToInt32(data, 0x30));
                    sw.WriteLine("0x34 ???: " + BitConverter.ToInt32(data, 0x34));
                    sw.WriteLine("0x38 ???: " + BitConverter.ToInt32(data, 0x38));
                    sw.WriteLine("0x3C ???: " + BitConverter.ToInt32(data, 0x3c));

                    sw.WriteLine("0x40 ???: " + BitConverter.ToInt16(data, 0x40));
                    sw.WriteLine("0x42 ???: " + BitConverter.ToInt16(data, 0x42));
                    sw.WriteLine("0x44 ???: " + BitConverter.ToInt16(data, 0x44));
                    sw.WriteLine("0x46 ???: " + BitConverter.ToInt16(data, 0x46));
                    sw.WriteLine("0x48 ???: " + BitConverter.ToInt16(data, 0x48));
                    sw.WriteLine("0x4a ???: " + BitConverter.ToInt16(data, 0x4a));
                    sw.WriteLine("0x4C pointer to the value of index for Image: 0x" + BitConverter.ToInt32(data, 0x4c).ToString("X"));

                    sw.WriteLine("0x50 FileSize: 0x" + BitConverter.ToInt32(data, 0x50).ToString("X"));
                    sw.WriteLine("0x54 FileSize: 0x" + BitConverter.ToInt32(data, 0x54).ToString("X"));
                    sw.WriteLine("0x58 FileSize: 0x" + BitConverter.ToInt32(data, 0x58).ToString("X"));
                    sw.WriteLine("0x5C FileSize: 0x" + BitConverter.ToInt32(data, 0x5c).ToString("X"));

                    sw.WriteLine("0x60 Palette size?: " + BitConverter.ToInt32(data, 0x60));
                    sw.WriteLine("0x64 Base palette offset?: 0x" + BitConverter.ToInt32(data, 0x64).ToString("X"));
                    sw.WriteLine("0x68 palettes ?: 0x" + BitConverter.ToInt32(data, 0x68).ToString("X"));
                }
            }
        }
        public void AnimationDoc(string file)
        {
            if (NAnimation > 0 && is_OSB)
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    int offset;
                    int length = BitConverter.ToInt32(data, 0);


                    for (int i = 0; i < animations.Count; i++)
                    {
                        sw.WriteLine("animation: " + (i + 1) + "\n");

                        offset = length + i * 0x1c;
                        sw.WriteLine("header offset: 0x" + offset.ToString("X") + "\n");

                        

                        int length1 = BitConverter.ToInt32(data, length + i * BitConverter.ToInt32(data, offset));
                        for (int j = 0; j < length1; j += 16)
                        {
                            int count = Math.Min(16, length1 - j);
                            string line = BitConverter.ToString(data, length + j + i * length1, count).Replace("-", " ");


                            sw.WriteLine(line);
                        }
                        sw.WriteLine();

                        sw.WriteLine("0x0 block size: 0x" + length1.ToString("X"));
                        sw.WriteLine("0x4 ID???: 0x" + BitConverter.ToInt32(data, length + i * length1 + 0x4).ToString("X"));
                        sw.WriteLine("0x8 animation length: " + BitConverter.ToSingle(data, length + i * length1 + 0x8));
                        sw.WriteLine("0xC animation index: " + BitConverter.ToInt32(data, length + i * length1 + 0xc));
                        sw.WriteLine("0x10 next animation index: " + BitConverter.ToInt32(data, length + i * length1 + 0x10));
                        sw.WriteLine("0x14 layer: " + BitConverter.ToInt32(data, length + i * length1 + 0x14));
                        sw.WriteLine("0x18 offset to next data: 0x" + BitConverter.ToInt32(data, length + i * length1 + 0x18).ToString("X"));


                        sw.WriteLine("\nchannel descriptor offset: 0x" + BitConverter.ToInt32(data, length + i * length1 + 0x18).ToString("X") + "\n");

                        int offset2 = BitConverter.ToInt32(data, length + length1 - 0x4 + i * length1);
                        int length2 = BitConverter.ToInt32(data, offset2);

                        for (int j = 0; j < length2; j += 16)
                        {
                            int count = Math.Min(16, length2 - j);

                            string line = BitConverter.ToString(data, offset2 + j, count).Replace("-", " ");

                            sw.WriteLine(line);
                        }
                        sw.WriteLine();

                        sw.WriteLine("0x0 block size: 0x" + length2.ToString("X"));
                        sw.WriteLine("0x4 type hash: 0x" + BitConverter.ToInt32(data, offset2 + 0x4).ToString("X"));
                        sw.WriteLine("0x8 flags: " + BitConverter.ToInt32(data, offset2 + 0x8));
                        sw.WriteLine("0xC sub-track count: " + BitConverter.ToInt32(data, offset2 + 0xc));
                        sw.WriteLine("0x10 offset to next data: 0x" + BitConverter.ToInt32(data, offset2 + 0x10).ToString("X"));

                        if (length2 == 0x3c)
                        {
                            sw.WriteLine("0x14 ???: " + BitConverter.ToSingle(data, offset2 + 0x14));
                            sw.WriteLine("0x18 ???: " + BitConverter.ToSingle(data, offset2 + 0x18));
                            sw.WriteLine("0x1C ???: " + BitConverter.ToInt32(data, offset2 + 0x1c));
                            sw.WriteLine("0x20 id ?: " + BitConverter.ToInt32(data, offset2 + 0x20).ToString("X"));
                            sw.WriteLine("0x24 ???: " + BitConverter.ToInt32(data, offset2 + 0x24));
                            sw.WriteLine("0x28 ???: " + BitConverter.ToInt32(data, offset2 + 0x28));
                            sw.WriteLine("0x2C ???: " + BitConverter.ToInt32(data, offset2 + 0x2c));
                            sw.WriteLine("0x30 ???: " + BitConverter.ToInt32(data, offset2 + 0x30));
                            sw.WriteLine("0x34 ???: " + BitConverter.ToInt32(data, offset2 + 0x34));
                            sw.WriteLine("0x38 ???: " + BitConverter.ToInt32(data, offset2 + 0x38));
                        }
                        else if (length2 == 0x48)
                        {
                            sw.WriteLine("0x14 Min???: " + BitConverter.ToSingle(data, offset2 + 0x14));
                            sw.WriteLine("0x18 Max???: " + BitConverter.ToSingle(data, offset2 + 0x18));
                            sw.WriteLine("0x1C ???: " + BitConverter.ToInt32(data, offset2 + 0x1c));
                            sw.WriteLine("0x20 ???: " + BitConverter.ToInt32(data, offset2 + 0x20).ToString("X"));
                            sw.WriteLine("0x24 ???: " + BitConverter.ToInt32(data, offset2 + 0x24));
                            sw.WriteLine("0x28 ???: " + BitConverter.ToInt32(data, offset2 + 0x28));
                            sw.WriteLine("0x2C ???: " + BitConverter.ToInt32(data, offset2 + 0x2c));
                            sw.WriteLine("0x30 ???: " + BitConverter.ToInt32(data, offset2 + 0x30));
                            sw.WriteLine("0x34 ???: " + BitConverter.ToInt32(data, offset2 + 0x34));
                            sw.WriteLine("0x38 ???: " + BitConverter.ToInt32(data, offset2 + 0x38));
                            sw.WriteLine("0x3C ???: 0x" + BitConverter.ToInt32(data, offset2 + 0x3c).ToString("X"));
                            sw.WriteLine("0x40 ???: 0x" + BitConverter.ToInt32(data, offset2 + 0x40).ToString("X"));
                            sw.WriteLine("0x44 frame rate: " + BitConverter.ToInt32(data, offset2 + 0x44));
                        }
                        sw.WriteLine("\nkeyframe table offset: 0x" + BitConverter.ToInt32(data, offset2 + 0x10).ToString("X") + "\n");

                        int offset3 = BitConverter.ToInt32(data, offset2 + 0x10);
                        int length3 = BitConverter.ToInt32(data, offset3);
                        int count3 = BitConverter.ToInt32(data, offset3 + 0x4);

                        for (int j = 0; j <= count3; j++)
                        {
                            string line = BitConverter.ToString(data, offset3 + (j * 0x14), length3).Replace("-", " ");

                            sw.WriteLine(line);
                        }
                        sw.WriteLine();

                        sw.WriteLine("0x0 size of each keyframe entry: 0x" + length3.ToString("X"));
                        sw.WriteLine("0x4 # keyframes: " + count3);
                        sw.WriteLine("0x8 base frame offset: " + BitConverter.ToInt32(data, offset3 + 0x8));
                        sw.WriteLine("0xC Interpolation flag???: " + BitConverter.ToInt32(data, offset3 + 0xC));
                        sw.WriteLine("0x10 ???: " + BitConverter.ToInt32(data, offset3 + 0x10));
                        sw.WriteLine();
                        //sw.Write("Animaton set of frames " + (i + 1) + ": ");
                        //sw.WriteLine("\n\t(time, frame)");

                        for (int j = 0; j < count3; j++)
                        {
                            sw.WriteLine("0x" + (length3 + j * length3).ToString("X") + " time: " + BitConverter.ToSingle(data, offset3 + length3 + j * length3));
                            sw.WriteLine("0x" + (length3 + j * length3 + 0x4).ToString("X") + " Interpolation type???: " + BitConverter.ToInt32(data, offset3 + length3 + j * length3 + 0x4));
                            sw.WriteLine("0x" + (length3 + j * length3 + 0x8).ToString("X") + " frame index: " + BitConverter.ToInt32(data, offset3 + length3 + j * length3 + 0x8));
                            sw.WriteLine("0x" + (length3 + j * length3 + 0xC).ToString("X") + " ???: " + BitConverter.ToInt32(data, offset3 + length3 + j * length3 + 0xC));
                            sw.WriteLine("0x" + (length3 + j * length3 + 0x10).ToString("X") + " ???: " + BitConverter.ToInt32(data, offset3 + length3 + j * length3 + 0x10));
                            sw.WriteLine();
                            //if (j!= count3 - 1)
                            //    sw.Write("\n\t(" + BitConverter.ToSingle(data, offset3 + length3 + j * length3) + "," + BitConverter.ToInt32(data, offset3 + length3 + 0x8 + j * length3) + "), ");
                            //else
                            //    sw.Write("\n\t(" + BitConverter.ToSingle(data, offset3 + length3 + j * length3) + "," + BitConverter.ToInt32(data, offset3 + length3 + 0x8 + j * length3) + ")\n");
                        }
                        sw.WriteLine("---------------------------------------------------------------------------------------------\n");
                    }
                }
            }
            
        }
        public void FramesDoc(string file)
        {
            if (NFrames > 0 && is_OSB)
            {

                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine("Offset: 0x" + BFrameData.ToString("X"));
                    sw.WriteLine();

                    sw.WriteLine("# frames: " + NFrames);
                    sw.WriteLine();
                    int length = BitConverter.ToInt32(data, BFrameData + 0x4);

                    int offset = BFrameData + 0x4;
                    for (int i = 0; i < NFrames; i++)
                    {

                        sw.WriteLine("Frame " + (i + 1));
                        sw.WriteLine();

                        sw.WriteLine("Data offset: 0x" + offset.ToString("X"));
                        sw.WriteLine();



                        for (int j = 0; j < length; j += 16)
                        {
                            int count = Math.Min(16, length - j);

                            string line = BitConverter.ToString(data, offset + j, count).Replace("-", " ");

                            sw.WriteLine(line);
                        }
                        sw.WriteLine();
                        int recOffset = BitConverter.ToInt32(data, offset + 0x8);
                        int triOffset = BitConverter.ToInt32(data, offset + 0xC);
                        int numPointRec = BitConverter.ToInt32(data, offset + 0x10);
                        int numPointTri = BitConverter.ToInt32(data, offset + 0x14);
                        sw.WriteLine("0x0 block size: 0x" + BitConverter.ToInt32(data, offset).ToString("X"));
                        sw.WriteLine("0x4 flags???: " + BitConverter.ToInt32(data, offset + 0x4));
                        sw.WriteLine("0x8 offset of rectancle crops (value + current offset): " + recOffset.ToString("X") + " + " + (offset + 0x8).ToString("X") + " = " + (recOffset + (offset + 0x8)).ToString("X"));
                        sw.WriteLine("0xC offset of tringle crops (value + current offset): " + triOffset.ToString("X") + " + " + (offset + 0xC).ToString("X") + " = " + (triOffset + (offset + 0xC)).ToString("X"));
                        sw.WriteLine("0x10 # rectancles points: " + numPointRec);
                        sw.WriteLine("0x14 # tringles points: " + numPointTri);
                        sw.WriteLine("\n");


                        sw.WriteLine("Rectancle points offset: 0x" + (recOffset + offset).ToString("X") + " - 0x8 = 0x" + (recOffset + offset - 0x8).ToString("X"));
                        sw.WriteLine();

                        for (int j = 0; j < numPointRec * 0x14; j += 0x14)
                        {
                            string line = BitConverter.ToString(data, recOffset + offset + j, 0x14).Replace("-", " ");
                            sw.WriteLine(line);



                        }
                        sw.WriteLine();

                        for (int j = 0; j < numPointRec * 0x14; j += 0x14)
                        {
                            sw.Write("0x" + (j).ToString("X") + " - 0x" + (j + 0x4).ToString("X") + " Generated frame point: highest point (" + frames.getCenter(i).X + "," + frames.getCenter(i).Y + ")");
                            sw.Write(" - (" + BitConverter.ToSingle(data, recOffset + offset + j) + "," + BitConverter.ToSingle(data, recOffset + offset + j + 0x4) + ")");
                            sw.WriteLine(" = (" + (frames.getCenter(i).X - BitConverter.ToSingle(data, recOffset + offset + j)) + "," + (frames.getCenter(i).Y - BitConverter.ToSingle(data, recOffset + offset + j + 0x4)) + ")");

                            sw.WriteLine("0x" + (j + 0x8).ToString("X") + "z-depth???: " + BitConverter.ToInt32(data, recOffset + offset + j + 0x8));

                            sw.Write("0x" + (j + 0xC).ToString("X") + " - 0x" + (j + 0x10).ToString("X") + " Optimized Image points: (" + BitConverter.ToSingle(data, recOffset + offset + j + 0xC) + "," + BitConverter.ToSingle(data, recOffset + offset + j + 0x10) + ")");
                            sw.WriteLine(" * Image dimensions (" + spritesheet.getImage().Width + "," + spritesheet.getImage().Height + ")= (" + BitConverter.ToSingle(data, recOffset + offset + j + 0xC) * spritesheet.getImage().Width + "," + BitConverter.ToSingle(data, recOffset + offset + j + 0x10) * spritesheet.getImage().Height + ")");
                            sw.WriteLine();
                        }
                        sw.WriteLine("\n");

                        sw.WriteLine("Trilngle points offset: 0x" + (triOffset + offset).ToString("X"));
                        sw.WriteLine();

                        for (int j = 0; j < numPointTri * 0x2; j += 0x6)
                        {
                            string line = BitConverter.ToString(data, triOffset + offset + j, 0x6).Replace("-", " ");

                            sw.WriteLine(line);
                        }
                        sw.WriteLine();

                        for (int j = 0; j < numPointTri * 0x2; j += 0x6)
                        {
                            sw.WriteLine("0x" + (j).ToString("X") + ": " + BitConverter.ToInt16(data, triOffset + offset + j));
                            sw.WriteLine("0x" + (j + 0x2).ToString("X") + ": " + BitConverter.ToInt16(data, triOffset + offset + j + 0x2));
                            sw.WriteLine("0x" + (j + 0x4).ToString("X") + ": " + BitConverter.ToInt16(data, triOffset + offset + j + 0x4));
                            sw.WriteLine();
                        }
                        sw.WriteLine('\n');

                        sw.WriteLine("---------------------------------------------------------------------------------------------\n");
                        offset += length;
                    }
                }
            }
        }

        public void spriteDoc(string file)
        {
            if(SHeader == 0x6c)
            {
                using(StreamWriter sw = new StreamWriter(file))
                {
                    int offset = IHSpritesheet;
                    sw.WriteLine("Header offset: 0x" + offset.ToString("X"));
                    sw.WriteLine();

                    
                    sw.WriteLine(BitConverter.ToString(data, offset, 0x18).Replace("-", " "));
                    sw.WriteLine();

                    int length = BitConverter.ToInt32(data, offset + 0x4);
                    sw.WriteLine("0x0 Image offset: 0x" + BitConverter.ToInt32(data, offset).ToString("X"));
                    sw.WriteLine("0x4 Image size (width * height * 4 bytes): " + length);
                    sw.WriteLine("0x8 ID ???: 0x" + BitConverter.ToInt32(data, offset + 0x8).ToString("X"));
                    sw.WriteLine("0xC Width: " + BitConverter.ToInt32(data, offset + 0xC));
                    sw.WriteLine("0x10 Height: " + BitConverter.ToInt32(data, offset + 0x10));
                    sw.WriteLine("0x14 ???: " + BitConverter.ToInt32(data, offset + 0x14));
                    sw.WriteLine();

                    offset = BitConverter.ToInt32(data, offset);
                    sw.WriteLine("Image offset: 0x" + offset.ToString("X"));
                    sw.WriteLine();

                    sw.WriteLine("(Blue, Green, Red, Alpha)");

                    for (int j = 0; j < length; j += 0x16)
                    {
                        int count = Math.Min(16, length - j);
                        string line = BitConverter.ToString(data, offset + j, count).Replace("-", " ");

                        sw.WriteLine(line);
                    }
                }
                

            }
        }
        public void pltsDoc(string file)
        {
            if(SPalette > 0)
            {
                using(StreamWriter  sw = new StreamWriter(file))
                {
                    if (SHeader == 0x6c)
                    {
                        int offset = BPaletteOriginal;
                        sw.WriteLine("Base palette offset: 0x" + offset.ToString("X"));
                        sw.WriteLine();

                        sw.WriteLine("(Blue, Green, Red, Alpha)");

                        int length = BPaletteDataModified - BPaletteOriginal;
                        for (int j = 0; j < length; j += 0x16)
                        {
                            int count = Math.Min(16, length - j);
                            string line = BitConverter.ToString(data, offset + j, count).Replace("-", " ");

                            sw.WriteLine(line);
                        }
                        sw.WriteLine("\n");

                        offset = BPaletteDataModified;
                        sw.WriteLine("palette offset: 0x" + offset.ToString("X"));
                    }
                    else if (SHeader == 0x38) { 
                    
                    } else if (SHeader == 0x8c) { 
                    
                    }
                    

                }
            }
        }

        public void framesrelatedDoc(string file)
        {
            if (is_OSB)
            {
                using(StreamWriter sw = new StreamWriter(file))
                {
                    int offset = BitConverter.ToInt32(data, 0x28);
                    for (int i = 0; i < NFrames; i++)
                    {
                        string line = BitConverter.ToString(data, offset, 16).Replace("-", " ");
                        sw.WriteLine(line);
                        offset += 16;
                    }
                }
                
            }
        }
    }
}
