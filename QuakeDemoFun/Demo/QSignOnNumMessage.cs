using System.IO;

namespace QuakeDemoFun
{
    internal class QSignOnNumMessage : QMessage
    {
        public QSignOnNumMessage(BinaryReader br)
        {
            ID = QMessageID.SignOnNum;
            SignOn = br.ReadByte();
        }

        public byte SignOn { get; private set; }

        public override string ToString() => $"SignOnNum {SignOn}";
    }
}