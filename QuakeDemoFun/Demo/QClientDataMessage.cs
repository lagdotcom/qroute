using System;
using System.IO;

namespace QuakeDemoFun
{
    internal class QClientDataMessage : QMessage
    {
        public QClientDataMessage(BinaryReader br)
        {
            ID = QMessageID.ClientData;
            Mask = (MessageMask)br.ReadInt16();

            if (Mask.HasFlag(MessageMask.ViewOfsZ)) ViewOfsZ = br.ReadByte(); // defaults to 22
            if (Mask.HasFlag(MessageMask.PunchAngleX)) PunchAngleX = br.ReadByte();
            if (Mask.HasFlag(MessageMask.AnglesX)) AnglesX = br.ReadByte();
            if (Mask.HasFlag(MessageMask.VelX)) VelX = br.ReadByte();
            if (Mask.HasFlag(MessageMask.AnglesY)) AnglesY = br.ReadByte();
            if (Mask.HasFlag(MessageMask.VelY)) VelY = br.ReadByte();
            if (Mask.HasFlag(MessageMask.AnglesZ)) AnglesZ = br.ReadByte();
            if (Mask.HasFlag(MessageMask.VelZ)) VelZ = br.ReadByte();
            Items = (ClientItems)br.ReadInt32();
            if (Mask.HasFlag(MessageMask.Weaponframe)) Weaponframe = br.ReadByte();
            if (Mask.HasFlag(MessageMask.Armorvalue)) Armorvalue = br.ReadByte();
            if (Mask.HasFlag(MessageMask.Weaponmodel)) Weaponmodel = br.ReadByte();
            Health = br.ReadInt16();
            Currentammo = br.ReadByte();
            Shells = br.ReadByte();
            Nails = br.ReadByte();
            Rockets = br.ReadByte();
            Cells = br.ReadByte();
            Weapon = br.ReadByte();
        }

        public MessageMask Mask { get; private set; }

        public byte? ViewOfsZ { get; private set; }
        public byte? PunchAngleX { get; private set; }
        public byte? AnglesX { get; private set; }
        public byte? AnglesY { get; private set; }
        public byte? AnglesZ { get; private set; }
        public byte? VelX { get; private set; }
        public byte? VelY { get; private set; }
        public byte? VelZ { get; private set; }
        public ClientItems Items { get; private set; }
        public byte? Weaponframe { get; private set; }
        public byte? Armorvalue { get; private set; }
        public byte? Weaponmodel { get; private set; }
        public short Health { get; private set; }
        public byte Currentammo { get; private set; }
        public byte Shells { get; private set; }
        public byte Nails { get; private set; }
        public byte Rockets { get; private set; }
        public byte Cells { get; private set; }
        public byte Weapon { get; private set; }

        public override string ToString() => $"ClientData {Mask:G}";

        [Flags]
        public enum MessageMask : ushort
        {
            ViewOfsZ = 0x0001,
            PunchAngleX = 0x0002,
            AnglesX = 0x0004,
            VelX = 0x0008,
            AnglesY = 0x0010,
            VelY = 0x0020,
            AnglesZ = 0x0040,
            VelZ = 0x0080,
            Weaponframe = 0x1000,
            Armorvalue = 0x2000,
            Weaponmodel = 0x4000,
        }

        [Flags]
        public enum ClientItems : uint
        {
            Shotgun = 0x00000001,
            SuperShotgun = 0x00000002,
            Nailgun = 0x00000004,
            SuperNailgun = 0x00000008,
            GrenadeLauncher = 0x00000010,
            RocketLauncher = 0x00000020,
            Lightning = 0x00000040,
            ExtraWeapon = 0x00000080,
            Shells = 0x00000100,
            Nails = 0x00000200,
            Rockets = 0x00000400,
            Cells = 0x00000800,

            Armor1 = 0x00002000,
            Armor2 = 0x00004000,
            Armor3 = 0x00008000,
            SuperHealth = 0x00010000,
            Key1 = 0x00020000,
            Key2 = 0x00040000,
            Invisibility = 0x00080000,
            Invulnerability = 0x00100000,
            Suit = 0x00200000,
            Quad = 0x00400000,
        }
    }
}