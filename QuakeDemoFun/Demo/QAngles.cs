using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public class QAngles
    {
        public static QAngles Read(BinaryReader br)
        {
            QAngles ang = new QAngles
            {
                X = br.ReadAngle(),
                Y = br.ReadAngle(),
                Z = br.ReadAngle(),
            };

            return ang;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public override string ToString() => $"[{X} {Y} {Z}]";

        public QAngles Clone() => new QAngles() { X = X, Y = Y, Z = Z };
    }
}
