using System.Drawing;

namespace QuakeDemoFun
{
    public interface IDraw
    { 
        void Cross(QCoords org, Color c, int size);

        void Dot(QCoords org, Color c);

        void Line(QCoords from, QCoords to, Color c);
    }
}
