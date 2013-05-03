using System;

namespace Animaonline.ILTools
{
    internal static class OperandReader
    {
        public static int ReadInt16(byte[] il, ref int position)
        {
            var i1 = il[position];
            var i2 = il[position + 1] << 8;

            position += sizeof(Int16);

            return i1 | i2;
        }

        public static ushort ReadUInt16(byte[] il, ref int position)
        {
            var i1 = il[position];
            var i2 = il[position + 1] << 8;

            position += sizeof(ushort);

            return (ushort)(i1 | i2);
        }

        public static int ReadInt32(byte[] il, ref int position)
        {
            var i1 = il[position];
            var i2 = il[position + 1] << 8;
            var i3 = il[position + 2] << 16;
            var i4 = il[position + 3] << 24;

            position += sizeof(int);

            return i1 | i2 | i3 | i4;
        }

        public static ulong ReadInt64(byte[] il, ref int position)
        {
            var i1 = il[position];
            var i2 = il[position + 1] << 8;
            var i3 = il[position + 2] << 16;
            var i4 = il[position + 3] << 24;
            var i5 = il[position + 4] << 32;
            var i6 = il[position + 5] << 40;
            var i7 = il[position + 6] << 48;
            var i8 = il[position + 7] << 56;

            position += sizeof(ulong);

            return (ulong)(i1 | i2 | i3 | i4 | i5 | i6 | i7 | i8);
        }

        public static double ReadDouble(byte[] il, ref int position)
        {
            var i1 = il[position];
            var i2 = il[position + 1] << 8;
            var i3 = il[position + 2] << 16;
            var i4 = il[position + 3] << 24;
            var i5 = il[position + 4] << 32;
            var i6 = il[position + 5] << 40;
            var i7 = il[position + 6] << 48;
            var i8 = il[position + 7] << 56;

            position += sizeof(double);

            return i1 | i2 | i3 | i4 | i5 | i6 | i7 | i8;
        }

        public static sbyte ReadSByte(byte[] il, ref int position)
        {
            return (sbyte)il[position++];
        }

        public static byte ReadByte(byte[] il, ref int position)
        {
            return il[position++];
        }

        public static Single ReadSingle(byte[] il, ref int position)
        {
            return ((il[position++] | (il[position++] << 8)) | (il[position++] << 0x10)) | (il[position++] << 0x18);
        }
    }
}