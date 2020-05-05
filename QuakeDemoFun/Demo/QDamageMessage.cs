using System.IO;

namespace QuakeDemoFun
{
    internal class QDamageMessage : QMessage
    {
        public QDamageMessage(BinaryReader br)
        {
            Save = br.ReadByte();
            Take = br.ReadByte();
            Origin = QCoords.Read(br);
        }

        public byte Save { get; private set; }
        public byte Take { get; private set; }
        public QCoords Origin { get; private set; }

        public override string ToString() => $"Damage s{Save} t{Take} @{Origin}";
    }
}