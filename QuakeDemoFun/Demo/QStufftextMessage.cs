using System.IO;

namespace QuakeDemoFun
{
    internal class QStufftextMessage : QMessage
    {
        public QStufftextMessage(BinaryReader br)
        {
            ID = QMessageID.Stufftext;
            Text = br.ReadZString();
        }

        public string Text { get; private set; }

        public override string ToString() => $"Stufftext {Text}";
    }
}