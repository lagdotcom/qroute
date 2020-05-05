using System.IO;

namespace QuakeDemoFun
{
    internal class QUpdateColorsMessage : QMessage
    {
        public QUpdateColorsMessage(BinaryReader br)
        {
            ID = QMessageID.UpdateColors;
            Player = br.ReadByte();
            Colors = br.ReadByte();
        }

        public byte Player { get; private set; }
        public byte Colors { get; private set; }

        public override string ToString() => $"UpdateColors {Player} {Colors}";
    }
}