using System.IO;

namespace QuakeDemoFun
{
    internal class QCDTrackMessage : QMessage
    {
        public QCDTrackMessage(BinaryReader br)
        {
            ID = QMessageID.CDTrack;
            From = br.ReadByte();
            To = br.ReadByte();
        }

        public byte From { get; private set; }
        public byte To { get; private set; }

        public override string ToString() => $"CDTrack {From} -> {To}";
    }
}