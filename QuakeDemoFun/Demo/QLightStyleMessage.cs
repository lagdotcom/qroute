using System.IO;

namespace QuakeDemoFun
{
    internal class QLightStyleMessage : QMessage
    {
        public QLightStyleMessage(BinaryReader br)
        {
            ID = QMessageID.LightStyle;
            Style = br.ReadByte();
            String = br.ReadZString();
        }

        public byte Style { get; private set; }
        public string String { get; private set; }

        public override string ToString() => $"LightStyle {Style} {String}";
    }
}