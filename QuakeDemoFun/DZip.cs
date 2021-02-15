using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuakeDemoFun
{
    class DZip : IDisposable
    {
        const string Magic = "DZ";
        const byte MajorVersion = 2;
        const byte MinorVersion = 9;
        const byte V1DirEntrySize = 32;
        const byte V2DirEntrySize = 24;
        const int BlockSize = 32768;
        const int BufferSize = 16384;

        private readonly FileStream stream;
        private readonly BinaryReader br;

        public DZip(string fileName)
        {
            Entries = new Dictionary<string, DirEntry>();
            stream = File.OpenRead(fileName);
            br = new BinaryReader(stream);

            Read();
        }

        public Dictionary<string, DirEntry> Entries { get; private set; }

        public bool Contains(string path) => Entries.ContainsKey(path);

        public Stream GetFile(string path)
        {
            DirEntry e = Entries[path];
            stream.Seek(e.Offset, SeekOrigin.Begin);

            bool demoMode = false;
            switch (e.Type)
            {
                case DirEntryType.Store:
                    // TODO: this is wrong
                    byte[] raw = br.ReadBytes((int)e.Real);
                    return new MemoryStream(raw);

                case DirEntryType.DEM:
                case DirEntryType.Nehahra:
                    demoMode = true;
                    break;
            }

            byte[] compressed = br.ReadBytes((int)e.Size);
            //if (!demoMode)
                return new ZlibStream(new MemoryStream(compressed), CompressionMode.Decompress);
        }

        private void Read()
        {
            uint fsize = (uint)br.BaseStream.Length;
            string magic = Encoding.ASCII.GetString(br.ReadBytes(2));
            if (magic != Magic) throw new FormatException($"Invalid DZip file: missing {Magic}");
            byte major = br.ReadByte();
            byte minor = br.ReadByte();
            if (major > MajorVersion) throw new FormatException($"Invalid DZip file: version is {major}.{minor}, this supports {MajorVersion}.{MinorVersion}");
            uint offset = br.ReadUInt32();
            uint numfiles = br.ReadUInt32();

            int entrySize = (major == 1) ? V1DirEntrySize : V2DirEntrySize;
            if ((numfiles & 0xF8000000) == 0xF8000000 || offset >= fsize || numfiles * entrySize > fsize)
                throw new FormatException($"Invalid DZip file: missing directory");

            br.BaseStream.Seek(offset, SeekOrigin.Begin);
            for (var i = 0; i < numfiles; i++)
            {
                DirEntry e = new DirEntry(br);
                Entries[e.Name] = e;
            }
        }

        public struct DirEntry
        {
            public DirEntry(BinaryReader br)
            {
                Offset = br.ReadUInt32();
                Size = br.ReadUInt32();
                Real = br.ReadUInt32();
                Len = br.ReadUInt16();
                Pak = br.ReadUInt16();
                CRC = br.ReadUInt32();
                Type = (DirEntryType)br.ReadUInt32();
                Date = br.ReadUInt32();
                Inter = br.ReadUInt32();
                Name = br.ReadZString().Replace('/', '\\');
            }

            public uint Offset{ get; set; }
            public uint Size { get; set; }
            public uint Real { get; set; }
            public ushort Len { get; set; }
            public ushort Pak { get; set; }
            public uint CRC { get; set; }
            public DirEntryType Type { get; set; }
            public uint Date { get; set; }
            public uint Inter { get; set; }
            public string Name { get; set; }

            public override string ToString() => $"{Name} @{Offset}+{Size}";
        }

        public enum DirEntryType : uint
        {
            Normal,
            DEMv1,
            TXT,
            PAK,
            DZ,
            DEM,
            Nehahra,
            Dir,
            Store,
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Entries.Clear();
                    br.Dispose();
                    stream.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
