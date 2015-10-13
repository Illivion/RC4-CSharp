using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC4Tests
{
    internal class Utils
    {
        public static TestVector GetTestVector(int keyLength)
        {
            var filePath = "test-vector-" + keyLength + ".txt";

            var lines = File.ReadAllLines(filePath);

            var vector = new TestVector();

            var data = new List<Byte>();

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("key:"))
                {
                    var xIndex = trimmedLine.IndexOf("x", StringComparison.Ordinal);
                    var keyIndex = xIndex + 1;

                    var keyString = trimmedLine.Substring(keyIndex);

                    var key = StringToByteArray(keyString);

                    vector.Key = key;
                }
                else if (trimmedLine.StartsWith("DEC"))
                {
                    var startIndex = trimmedLine.IndexOf(":", StringComparison.Ordinal);

                    var dataSubstring = trimmedLine.Substring(startIndex + 1).Replace(" ","");

                    var dataBlock = StringToByteArray(dataSubstring);

                    data.AddRange(dataBlock);
                }
            }

            vector.Data = data.ToArray();

            return vector;
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }

    internal class TestVector
    {
        public Byte[] Key { get; set; }

        public Byte[] Data { get; set; }
    }
}
