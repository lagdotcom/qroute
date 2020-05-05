using System;
using System.IO;

namespace QuakeDemoFun
{
    internal class QSoundMessage : QMessage
    {
        public QSoundMessage(BinaryReader br)
        {
            ID = QMessageID.Sound;
            Mask = (MessageMask)br.ReadByte();

            if (Mask.HasFlag(MessageMask.Volume)) Volume = br.ReadByte();
            if (Mask.HasFlag(MessageMask.Attenuation)) Attenuation = br.ReadByte();
            EntityChan = br.ReadInt16();
            SoundNum = br.ReadByte();
            Origin = QCoords.Read(br);
        }

        public MessageMask Mask { get; private set; }
        public byte? Volume { get; private set; }
        public byte? Attenuation { get; private set; }
        public short EntityChan { get; private set; }
        public byte SoundNum { get; private set; }
        public QCoords Origin { get; private set; }

        public override string ToString() => $"Sound {SoundNum} @{Origin}";

        [Flags]
        public enum MessageMask : byte
        {
            Volume = 0x01,
            Attenuation = 0x02,
        }
    }
}