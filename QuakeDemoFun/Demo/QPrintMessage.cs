using System.IO;

namespace QuakeDemoFun
{
    internal class QPrintMessage : QMessage
    {
        public QPrintMessage(BinaryReader br)
        {
            ID = QMessageID.Print;
            Text = br.ReadZString();
        }

        public string Text { get; private set; }

        public override string ToString() => $"Print {Text}";
    }
}