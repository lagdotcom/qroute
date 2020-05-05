using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public class QCoords
    {
        public static QCoords Read(BinaryReader br)
        {
            QCoords org = new QCoords
            {
                X = br.ReadCoord(),
                Y = br.ReadCoord(),
                Z = br.ReadCoord(),
            };

            return org;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public override string ToString() => $"({X} {Y} {Z})";

        public QCoords Clone() => new QCoords() { X = X, Y = Y, Z = Z };
    }
}
