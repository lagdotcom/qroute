using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public abstract class Temp
    {
        public Temp(float duration)
        {
            Duration = duration;
        }

        public float Duration { get; set; }

        abstract public Temp Clone();

        abstract public void Draw(IDraw d);
    }
}
