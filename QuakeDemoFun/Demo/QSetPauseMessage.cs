using System.IO;

namespace QuakeDemoFun
{
    internal class QSetPauseMessage : QMessage
    {
        public QSetPauseMessage(BinaryReader br)
        {
            ID = QMessageID.SetPause;
            State = br.ReadByte();
        }

        public byte State { get; private set; }

        public override string ToString() => $"SetPause {State}";
    }
}