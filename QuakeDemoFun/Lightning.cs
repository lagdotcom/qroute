using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    class Lightning : Temp
    {
        public Lightning(QCoords from, QCoords to) : base(0.1f)
        {
            From = from;
            To = to;
        }

        public QCoords From { get; set; }
        public QCoords To { get; set; }

        public override Temp Clone() => new Lightning(From, To);

        public override void Draw(IDraw d)
        {
            d.Line(From, To, Color.Cyan);
        }
    }
}
