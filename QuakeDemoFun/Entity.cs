namespace QuakeDemoFun
{
    public class Entity
    {
        public Entity()
        {
            Angles = new QAngles();
            Origin = new QCoords();
        }

        public short Number { get; set; }
        public GameState Parent { get; set; }

        public QAngles Angles { get; set; }
        public byte Colormap { get; set; }
        public EntityEffects Effects { get; set; }
        public byte Frame { get; set; }
        public byte ModelIndex { get; set; }
        public QCoords Origin { get; set; }
        public byte Skin { get; set; }

        public string Model
        {
            get
            {
                int lookup = ModelIndex - 1;
                if (lookup >= 0 && lookup < Parent.Parent.Models.Count) return Parent.Parent.Models[lookup];
                return "?";
            }
        }

        public override string ToString() => $"#{Number} @{Origin} {Model}";

        public Entity Clone() => new Entity()
        {
            Number = Number,
            Parent = Parent,
            Angles = Angles.Clone(),
            Colormap = Colormap,
            Effects = Effects,
            Frame = Frame,
            ModelIndex = ModelIndex,
            Origin = Origin.Clone(),
            Skin = Skin,
        };
    }
}
