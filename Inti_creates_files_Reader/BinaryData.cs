namespace Inti_creates_files_Reader
{
    internal static class BinaryData
    {
        public static void EnsureRange(byte[] data, int offset, int length, string section)
        {
            if (offset < 0 || length < 0 || (long)offset + length > data.Length)
                throw new InvalidDataException($"Invalid {section} range (offset 0x{offset:X}, length {length}, file size {data.Length}).");
        }

        public static int ReadInt32(byte[] data, int offset, string field)
        {
            EnsureRange(data, offset, sizeof(int), field);
            return BitConverter.ToInt32(data, offset);
        }

        public static float ReadSingle(byte[] data, int offset, string field)
        {
            EnsureRange(data, offset, sizeof(float), field);
            return BitConverter.ToSingle(data, offset);
        }
    }
}
