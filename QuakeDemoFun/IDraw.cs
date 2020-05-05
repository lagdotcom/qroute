using System.Drawing;

namespace QuakeDemoFun
{
    public interface IDraw
    { 
        void Cross(QCoords org, Color c, int size);

        void Line(QCoords from, QCoords to, Color c);
    }
}
