using System;
using System.IO;

namespace QuakeDemoFun
{
    internal class QUpdateEntityMessage : QMessage
    {
        public QUpdateEntityMessage(BinaryReader br, byte code)
        {
            ID = QMessageID.UpdateEntity;

            short rawmask;
            if ((code & 0x01) == 0x01)
                rawmask = (short)((br.ReadByte() << 8) + code);
            else
                rawmask = code;

            Mask = (MessageMask)rawmask;
            
            Entity = Mask.HasFlag(MessageMask.Entity) ? br.ReadInt16() : br.ReadByte();
            if (Mask.HasFlag(MessageMask.ModelIndex)) ModelIndex = br.ReadByte();
            if (Mask.HasFlag(MessageMask.Frame)) Frame = br.ReadByte();
            if (Mask.HasFlag(MessageMask.Colormap)) Colormap = br.ReadByte();
            if (Mask.HasFlag(MessageMask.Skin)) Skin = br.ReadByte();
            if (Mask.HasFlag(MessageMask.Effects)) Effects = (EntityEffects)br.ReadByte();
            if (Mask.HasFlag(MessageMask.OriginX)) OriginX = br.ReadCoord();
            if (Mask.HasFlag(MessageMask.AnglesX)) AnglesX = br.ReadAngle();
            if (Mask.HasFlag(MessageMask.OriginY)) OriginY = br.ReadCoord();
            if (Mask.HasFlag(MessageMask.AnglesY)) AnglesY = br.ReadAngle();
            if (Mask.HasFlag(MessageMask.OriginZ)) OriginZ = br.ReadCoord();
            if (Mask.HasFlag(MessageMask.AnglesZ)) AnglesZ = br.ReadAngle();
        }

        public MessageMask Mask { get; private set; }

        public short Entity { get; private set; }
        public byte? ModelIndex { get; private set; }
        public byte? Frame { get; private set; }
        public byte? Colormap { get; private set; }
        public byte? Skin { get; private set; }
        public EntityEffects? Effects { get; private set; }
        public double? OriginX { get; private set; }
        public double? OriginY { get; private set; }
        public double? OriginZ { get; private set; }
        public double? AnglesX { get; private set; }
        public double? AnglesY { get; private set; }
        public double? AnglesZ { get; private set; }

        public override string ToString() => $"UpdateEntity {Entity}";

        [Flags]
        public enum MessageMask : ushort
        {
            Entity = 0x4000,
            ModelIndex = 0x0400,
            Frame = 0x0040,
            Colormap = 0x0800,
            Skin = 0x1000,
            Effects = 0x2000,
            OriginX = 0x0002,
            AnglesX = 0x0100,
            OriginY = 0x0004,
            AnglesY = 0x0010,
            OriginZ = 0x0008,
            AnglesZ = 0x0200,
            New = 0x0020,
        }
    }
}