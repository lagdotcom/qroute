using System.IO;

namespace QuakeDemoFun
{
    internal class QSetViewMessage : QMessage
    {
        public QSetViewMessage(BinaryReader br)
        {
            ID = QMessageID.SetView;
            Entity = br.ReadInt16();
        }

        public short Entity { get; private set; }

        public override string ToString() => $"SetView {Entity}";
    }
}