using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public class QDemo
    {
        public QDemo()
        {
            Blocks = new List<QBlock>();
        }

        public List<QBlock> Blocks { get; private set; }
        public string CDTrack { get; private set; }

        public static QDemo Load(string filename)
        {
            QDemo dem = new QDemo();
            using (FileStream stream = File.OpenRead(filename))
                using (BinaryReader br = new BinaryReader(stream))
                    dem.Read(br);

            return dem;
        }

        void Read(BinaryReader br)
        {
            CDTrack = br.ReadLine();
            
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                QBlock block = QBlock.Read(br);
                Blocks.Add(block);
            }
        }
    }
}
