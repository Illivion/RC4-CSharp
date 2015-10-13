using System;

namespace RC4
{
    internal static class ByteArrayExtensions
    {
        public static void Swap(this Byte[] array, int index1, int index2)
        {
            Byte temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}