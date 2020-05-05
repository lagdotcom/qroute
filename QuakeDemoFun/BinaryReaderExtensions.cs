using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    static class BinaryReaderExtensions
    {
        public static string ReadLine(this BinaryReader br)
        {
            string line = "";

            while (true)
            {
                char c = br.ReadChar();
                if (c == '\xa') return line;
                line += c;
            }
        }

        public static string ReadZString(this BinaryReader br)
        {
            List<byte> bytes = new List<byte>();

            while (true)
            {
                byte b = br.ReadByte();
                if (b == 0) return Encoding.ASCII.GetString(bytes.ToArray());
                bytes.Add(b);
            }
        }

        public static string[] ReadZStringList(this BinaryReader br)
        {
            List<string> strings = new List<string>();

            while (true)
            {
                string s = br.ReadZString();
                if (s.Length == 0) return strings.ToArray();
                strings.Add(s);
            }
        }

        public static double ReadCoord(this BinaryReader br)
        {
            return br.ReadInt16() * 0.125;
        }

        public static double ReadAngle(this BinaryReader br)
        {
            return br.ReadByte() * 360 / 256;
        }
    }
}
