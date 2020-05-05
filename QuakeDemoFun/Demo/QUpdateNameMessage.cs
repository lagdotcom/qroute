using System.IO;

namespace QuakeDemoFun
{
    internal class QUpdateNameMessage : QMessage
    {
        public QUpdateNameMessage(BinaryReader br)
        {
            ID = QMessageID.UpdateName;
            Player = br.ReadByte();
            Netname = br.ReadZString();
        }

        public byte Player { get; private set; }
        public string Netname { get; private set; }

        public override string ToString() => $"UpdateName {Player} {Netname}";
    }
}