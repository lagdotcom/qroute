using System.Drawing;

namespace QuakeDemoFun
{
    public class Footstep : Temp
    {
        private static readonly Color StepColor = Color.FromArgb(40, 40, 40);

        public Footstep(QCoords org) : base(0.5f)
        {
            Origin = org;
        }

        public QCoords Origin { get; set; }

        public override Temp Clone() => new Footstep(Origin);

        public override void Draw(IDraw d)
        {
            d.Dot(Origin, StepColor);
        }
    }
}
