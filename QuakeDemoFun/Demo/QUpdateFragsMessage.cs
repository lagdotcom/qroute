using System.IO;

namespace QuakeDemoFun
{
    internal class QUpdateFragsMessage : QMessage
    {
        public QUpdateFragsMessage(BinaryReader br)
        {
            ID = QMessageID.UpdateFrags;
            Player = br.ReadByte();
            Frags = br.ReadInt16();
        }

        public byte Player { get; private set; }
        public short Frags { get; private set; }

        public override string ToString() => $"UpdateFrags {Player} {Frags}";
    }
}