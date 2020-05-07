using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public class ModelInfo
    {
        public static readonly Dictionary<ModelType, Brush> DefaultBackground = new Dictionary<ModelType, Brush>()
        {
            { ModelType.Enemy, Brushes.Red },
            { ModelType.Item, Brushes.Brown },
            { ModelType.Missile, Brushes.Gray },
            { ModelType.Gib, Brushes.DarkRed },
        };

        public static readonly Dictionary<ModelType, int> DefaultSize = new Dictionary<ModelType, int>()
        {
            { ModelType.Enemy, 32 },
            { ModelType.Item, 32 },
            { ModelType.Missile, 16 },
            { ModelType.Gib, 8 },
        };

        public ModelInfo(string model)
        {
            Model = model;

            Type = ModelType.Misc;
            Background = Brushes.Gray;
            Foreground = Brushes.White;
            Size = 32;
            Source = GameSource.Quake;

            DeadFrames = new List<int>();
        }

        public string Model { get; set; }
        public Brush Background { get; set; }
        public Brush Foreground { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public GameSource Source { get; set; }
        public ModelType Type { get; set; }
        public List<int> DeadFrames { get; set; }

        public override string ToString() => $"{Name} {Type} - {Source}";
    }
}
