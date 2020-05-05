using System.IO;

namespace QuakeDemoFun
{
    internal class QServerInfoMessage : QMessage
    {
        public QServerInfoMessage(BinaryReader br)
        {
            ID = QMessageID.ServerInfo;
            Version = br.ReadInt32();
            MaxClients = br.ReadByte();
            Multi = br.ReadByte();
            Mapname = br.ReadZString();
            Models = br.ReadZStringList();
            Sounds = br.ReadZStringList();
        }

        public int Version { get; private set; }
        public byte MaxClients { get; private set; }
        public byte Multi { get; private set; }
        public string Mapname { get; private set; }
        public string[] Models { get; private set; }
        public string[] Sounds { get; private set; }

        public override string ToString() => $"ServerInfo v{Version} on {Mapname}";
    }
}