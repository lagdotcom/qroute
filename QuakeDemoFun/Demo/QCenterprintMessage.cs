using System.IO;

namespace QuakeDemoFun
{
    internal class QCenterprintMessage : QMessage
    {
        public QCenterprintMessage(BinaryReader br)
        {
            ID = QMessageID.Centerprint;
            Message = br.ReadZString();
        }

        public string Message { get; private set; }

        public override string ToString() => $"Centerprint {Message}";
    }
}