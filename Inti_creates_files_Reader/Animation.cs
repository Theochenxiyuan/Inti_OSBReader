using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inti_creates_files_Reader
{
    public class Animation
    {
        private int num;
        private List<float> time;
        private List<int> frame;
        private float AnimationLength;
        private Point centered;
        private Point dimension;

        public Animation(int index)
        {
            time = new List<float>();
            frame = new List<int>();
            num = index;
        }

        public void setTime(int index, int time)
        {
            this.time[index] = time;
        }
        public float getTime(int index)
        {
            return time[index];
        }
        public void setFrame(int index, int frame)
        {
            this.frame[index] = frame;
        }
        public int getFrame(int index)
        {
            return frame[index];
        }
        public int Size()
        {
            return frame.Count;
        }
        public float getLength()
        {
            return AnimationLength;
        }
        public int getIndex()
        {
            return num;
        }
        public void readAnimation(ref byte[] data, int offset, int index, int framesSize)
        {
            int Sanimation1 = BinaryData.ReadInt32(data, offset + (index * 0x1c), $"animation {index} record size"); //0x1c
            if (Sanimation1 <= 0)
                throw new InvalidDataException($"Animation {index} has an invalid record size: {Sanimation1}.");
            AnimationLength = BinaryData.ReadSingle(data, offset + 0x8 + (index * Sanimation1), $"animation {index} length");
            //Banimation2 = Banimation1 + Sanimation1;
            //Sanimation2 = NAnimation * BitConverter.ToInt32(data, Banimation2);//0x48 or 0x3c

            //Banimation3 = Banimation2 + Sanimation2;
            //Sanimation3 = NAnimation * BitConverter.ToInt32(data, Banimation3);
            //int offset = 0x0;


            // Banimation1 + 0x8 = animation length;
            int index2 = BinaryData.ReadInt32(data, offset + 0x18 + (index * Sanimation1), $"animation {index} data pointer");

            int index3 = BinaryData.ReadInt32(data, index2 + 0x10, $"animation {index} keyframe pointer");
            int Sdata3 = BinaryData.ReadInt32(data, index3, $"animation {index} keyframe size");
            int Sanimation = BinaryData.ReadInt32(data, index3 + 0x4, $"animation {index} keyframe count");
            if (Sdata3 < 0xc || Sanimation < 0 || Sanimation > 1_000_000)
                throw new InvalidDataException($"Animation {index} has an invalid keyframe table.");
            long keyframeEnd = (long)index3 + Sanimation * Sdata3 + 0xc;
            if (keyframeEnd > int.MaxValue)
                throw new InvalidDataException($"Animation {index} keyframe table is too large.");
            BinaryData.EnsureRange(data, index3, (int)(keyframeEnd - index3), $"animation {index} keyframes");

            for (int j = 1; j <= Sanimation; j++)
            {
                float t = BitConverter.ToSingle(data, index3 + j * Sdata3);
                if (t < 0 || t > AnimationLength || float.IsNaN(t))
                    continue;
                time.Add(t);

                int baseFrameOffset = BitConverter.ToInt32(data, index3 + 0x8); // data3[0x8]
                float frameFloat = BitConverter.ToSingle(data, index3 + j * Sdata3 + 0x8);
                int frameInt = BitConverter.ToInt32(data, index3 + j * Sdata3 + 0x8);

                int realFrame;
                if (frameInt >= 0 && frameInt < framesSize)
                {
                    realFrame = frameInt;
                }
                else if (!float.IsNaN(frameFloat) && !float.IsInfinity(frameFloat)
                           && frameFloat >= 0f && frameFloat <= 1f)
                {
                    realFrame = (int)Math.Floor(frameFloat * baseFrameOffset);
                }
                else if (!float.IsNaN(frameFloat) && Math.Abs(frameFloat - Math.PI * 2) < 0.001f)
                {
                    realFrame = -1;
                }
                else
                {
                    realFrame = frameInt;
                }
                frame.Add(realFrame);
            }

        }
        public void getMaxCentered(ref Frames frms)
        {
            dimension = frms.getMaxFrame();
            centered = frms.getMaxCenter();
        }
        public Point getGCenter()
        {
            return centered;
        }
        public Point getGDimension()
        {
            return dimension;
        }

    }
}
