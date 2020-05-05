using System.IO;

namespace QuakeDemoFun
{
    internal class QSpawnStaticSoundMessage : QMessage
    {
        public QSpawnStaticSoundMessage(BinaryReader br)
        {
            ID = QMessageID.SpawnStaticSound;
            Origin = QCoords.Read(br);
            SoundNum = br.ReadByte();
            Volume = br.ReadByte();
            Attenuation = br.ReadByte();
        }

        public QCoords Origin { get; private set; }
        public byte SoundNum { get; private set; }
        public byte Volume { get; private set; }
        public byte Attenuation { get; private set; }

        public override string ToString() => $"SpawnStaticSound #{SoundNum} @{Origin} v{Volume} a{Attenuation}";
    }
}