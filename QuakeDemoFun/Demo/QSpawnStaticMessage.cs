using System.IO;

namespace QuakeDemoFun
{
    internal class QSpawnStaticMessage : QMessage
    {
        public QSpawnStaticMessage(BinaryReader br)
        {
            ID = QMessageID.SpawnStatic;
            ModelIndex = br.ReadByte();
            Frame = br.ReadByte();
            Colormap = br.ReadByte();
            Skin = br.ReadByte();
            Origin = new QCoords();
            Angles = new QAngles();

            Origin.X = br.ReadCoord();
            Angles.X = br.ReadAngle();
            Origin.Y = br.ReadCoord();
            Angles.Y = br.ReadAngle();
            Origin.Z = br.ReadCoord();
            Angles.Z = br.ReadAngle();
        }

        public byte ModelIndex { get; private set; }
        public byte Frame { get; private set; }
        public byte Colormap { get; private set; }
        public byte Skin { get; private set; }
        public QCoords Origin { get; private set; }
        public QAngles Angles { get; private set; }

        public override string ToString() => $"SpawnStatic #{ModelIndex} @{Origin}{Angles}";
    }
}