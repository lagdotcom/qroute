using System.IO;

namespace QuakeDemoFun
{
    internal class QSetAngleMessage : QMessage
    {
        public QSetAngleMessage(BinaryReader br)
        {
            ID = QMessageID.SetAngle;
            Angles = QAngles.Read(br);
        }

        public QAngles Angles { get; private set; }

        public override string ToString() => $"SetAngle {Angles}";
    }
}