using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public class Player
    {
        public short Entity { get; set; }
        public string Netname { get; set; }
        public byte Colors { get; set; }

        public int Top => Colors / 16;
        public int Pants => Colors % 16;

        public override string ToString() => $"{Netname} {Top}/{Pants}";
    }
}
