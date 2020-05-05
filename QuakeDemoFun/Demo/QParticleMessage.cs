using System.IO;

namespace QuakeDemoFun
{
    internal class QParticleMessage : QMessage
    {
        public QParticleMessage(BinaryReader br)
        {
            ID = QMessageID.Particle;
            Origin = QCoords.Read(br);
            VelX = br.ReadByte() * 0.0625;
            VelY = br.ReadByte() * 0.0625;
            VelZ = br.ReadByte() * 0.0625;
            Color = br.ReadByte();
            Count = br.ReadByte();
        }

        public QCoords Origin { get; private set; }
        public double VelX { get; private set; }
        public double VelY { get; private set; }
        public double VelZ { get; private set; }
        public byte Color { get; private set; }
        public byte Count { get; private set; }

        public override string ToString() => $"Particle @{Origin} x{Count}";
    }
}