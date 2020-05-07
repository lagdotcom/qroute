using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public static class Info
    {
        public static readonly Dictionary<string, ModelInfo> ModelInfos = new Dictionary<string, ModelInfo>();

        public static readonly Color[] QuakeColors = new Color[]
        {
            Color.White,
            Color.FromArgb(128, 64, 0),
            Color.FromArgb(128, 128, 255),
            Color.FromArgb(0, 64, 0),
            Color.FromArgb(128, 0, 0),
            Color.FromArgb(192, 192, 0),
            Color.FromArgb(255, 128, 0),
            Color.FromArgb(255, 192, 160),
            Color.FromArgb(144, 0, 80),
            Color.FromArgb(233, 88, 142),
            Color.FromArgb(255, 192, 144),
            Color.FromArgb(0, 128, 80),
            Color.FromArgb(255, 255, 0),
            Color.FromArgb(0, 0, 255),
        };

        public static readonly Dictionary<int, string> WeaponNames = new Dictionary<int, string>()
        {
            { 1, "S" },
            { 2, "SS" },
            { 4, "N" },
            { 8, "SN" },
            { 16, "GL" },
            { 32, "RL" },
            { 64, "Th" },
        };

        public static void Initialise(string directory)
        {
            // load model info
            LoadModelInfo(Path.Combine(directory, "models.txt"));
        }

        public static ModelInfo GetModelInfo(string model, byte skin)
        {
            string modelId = skin != 0 ? $"{model}:{skin}" : model;

            if (ModelInfos.ContainsKey(modelId)) return ModelInfos[modelId];

            ModelType type = ModelType.Misc;
            if (model.StartsWith("maps/")) type = ModelType.Map;

            return new ModelInfo(model) { Label = "?", Type = type };
        }

        private static void LoadModelInfo(string fileName)
        {
            using (StreamReader reader = File.OpenText(fileName))
            {
                ModelInfo inf = null;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();
                    if (line.Length == 0) continue;

                    if (line[0] == '*')
                    {
                        if (inf != null) ModelInfos[inf.Model] = inf;

                        inf = new ModelInfo(line.Substring(1));
                    }
                    else if (line.StartsWith("Name: "))
                    {
                        inf.Name = line.Substring(6);
                    }
                    else if (line.StartsWith("Label: "))
                    {
                        inf.Label = line.Substring(7);
                    }
                    else if (line.StartsWith("Type: "))
                    {
                        string typeName = line.Substring(6);
                        ModelType type = (ModelType)Enum.Parse(typeof(ModelType), typeName, true);
                        inf.Type = type;

                        if (ModelInfo.DefaultBackground.ContainsKey(type))
                            inf.Background = ModelInfo.DefaultBackground[type];

                        if (ModelInfo.DefaultSize.ContainsKey(type))
                            inf.Size = ModelInfo.DefaultSize[type];
                    }
                    else if (line.StartsWith("Source: "))
                    {
                        string sourceName = line.Substring(8);
                        inf.Source = (GameSource)Enum.Parse(typeof(GameSource), sourceName, true);
                    }
                    else if (line.StartsWith("Background: "))
                    {
                        string bgName = line.Substring(12);
                        inf.Background = new SolidBrush(Color.FromName(bgName));
                    }
                    else if (line.StartsWith("Foreground: "))
                    {
                        string fgName = line.Substring(12);
                        inf.Foreground = new SolidBrush(Color.FromName(fgName));
                    }
                    else if (line.StartsWith("Size: "))
                    {
                        inf.Size = int.Parse(line.Substring(6));
                    }
                    else if (line.StartsWith("Dead: "))
                    {
                        string[] frames = line.Substring(6).Replace(" ", "").Split(',');
                        foreach (string frame in frames)
                        {
                            int fval = int.Parse(frame);
                            inf.DeadFrames.Add(fval);
                        }
                    }
                    else
                        Console.WriteLine($"Unknown instruction in models.txt: {line}");
                }

                if (inf != null) ModelInfos[inf.Model] = inf;
            }
        }
    }
}
