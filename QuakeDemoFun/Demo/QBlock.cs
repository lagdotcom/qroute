using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public class QBlock
    {
        public QBlock()
        {
            Messages = new List<QMessage>();
        }

        public float AngleX { get; private set; }
        public float AngleY { get; private set; }
        public float AngleZ { get; private set; }

        public List<QMessage> Messages { get; private set; }

        public static QBlock Read(BinaryReader br)
        {
            QBlock block = new QBlock();

            int length = br.ReadInt32();
            block.AngleX = br.ReadSingle();
            block.AngleY = br.ReadSingle();
            block.AngleZ = br.ReadSingle();

            long end = br.BaseStream.Position + length;
            while (br.BaseStream.Position < end)
            {
                QMessage msg = QMessage.Read(br);
                block.Messages.Add(msg);
            }

            return block;
        }

        public override string ToString() => $"-- Block: [{AngleX} {AngleY} {AngleZ}]";
    }
}
