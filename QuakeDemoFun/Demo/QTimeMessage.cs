using System.IO;

namespace QuakeDemoFun
{
    internal class QTimeMessage : QMessage
    {
        public QTimeMessage(BinaryReader br)
        {
            ID = QMessageID.Time;
            Time = br.ReadSingle();
        }

        public float Time { get; private set; }

        public override string ToString() => $"Time {Time}";
    }
}