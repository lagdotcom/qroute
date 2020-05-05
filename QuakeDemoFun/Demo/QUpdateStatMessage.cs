using System.IO;

namespace QuakeDemoFun
{
    internal class QUpdateStatMessage : QMessage
    {
        public QUpdateStatMessage(BinaryReader br)
        {
            ID = QMessageID.UpdateStat;
            Index = (StatIndex)br.ReadByte();
            Value = br.ReadInt32();
        }

        public StatIndex Index { get; private set; }
        public int Value { get; private set; }

        public override string ToString() => $"UpdateStat {Index} {Value}";
    }
}