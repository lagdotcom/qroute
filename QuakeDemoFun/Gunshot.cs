using System.Drawing;

namespace QuakeDemoFun
{
    class Gunshot : Temp
    {
        public Gunshot(QCoords org) : base(0.1f)
        {
            Origin = org;
        }

        public QCoords Origin { get; set; }

        public override Temp Clone() => new Gunshot(Origin);

        public override void Draw(IDraw d)
        {
            d.Cross(Origin, Color.Gray, 3);
        }
    }
}
