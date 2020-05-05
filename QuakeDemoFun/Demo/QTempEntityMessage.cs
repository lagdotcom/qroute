using System.IO;

namespace QuakeDemoFun
{
    internal class QTempEntityMessage : QMessage
    {
        public QTempEntityMessage(BinaryReader br)
        {
            ID = QMessageID.TempEntity;
            Type = (TempEntityType)br.ReadByte();

            switch (Type)
            {
                case TempEntityType.Spike:
                case TempEntityType.SuperSpike:
                case TempEntityType.Gunshot:
                case TempEntityType.Explosion:
                case TempEntityType.TarExplosion:
                case TempEntityType.WizSpike:
                case TempEntityType.KnightSpike:
                case TempEntityType.LavaSplash:
                case TempEntityType.Teleport:
                    Origin = QCoords.Read(br);
                    return;

                case TempEntityType.Lightning1:
                case TempEntityType.Lightning2:
                case TempEntityType.Lightning3:
                case TempEntityType.Unknown_13:
                    Entity = br.ReadInt16();
                    Origin = QCoords.Read(br);
                    TraceEndpos = QCoords.Read(br);
                    return;

                case TempEntityType.Unknown_12:
                    Origin = QCoords.Read(br);
                    Color = br.ReadByte();
                    Range = br.ReadByte();
                    return;
            }
        }

        public TempEntityType Type { get; private set; }
        public short? Entity { get; private set; }
        public QCoords Origin { get; private set; }
        public QCoords TraceEndpos { get; private set; }
        public byte? Color { get; private set; }
        public byte? Range { get; private set; }

        public override string ToString() => $"TempEntity {Type} @{Origin}";
    }
}