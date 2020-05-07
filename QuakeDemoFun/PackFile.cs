using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuakeDemoFun
{
    public partial class PackFile : IDisposable
    {
        private readonly FileStream stream;
        private readonly BinaryReader br;

        public PackFile(string fileName)
        {
            FileName = fileName;
            Entries = new Dictionary<string, PackEntry>();

            stream = File.OpenRead(fileName);
            br = new BinaryReader(stream);

            string magic = Encoding.ASCII.GetString(br.ReadBytes(4));
            if (magic != "PACK") throw new FormatException($"Invalid PAK file: got {magic}, expected PACK");

            int dirOffset = br.ReadInt32();
            int dirSize = br.ReadInt32();

            stream.Seek(dirOffset, SeekOrigin.Begin);
            for (var i = 0; i < dirSize / 64; i++)
            {
                string file = Encoding.ASCII.GetString(br.ReadBytes(56));
                int offset = br.ReadInt32();
                int size = br.ReadInt32();
                PackEntry e = new PackEntry(file, offset, size);

                Entries.Add(e.FileName, e);
            }
        }

        public string FileName { get; private set; }

        public Dictionary<string, PackEntry> Entries { get; private set; }

        public bool Contains(string path) => Entries.ContainsKey(path);

        public Stream GetFile(string path)
        {
            PackEntry e = Entries[path];
            stream.Seek(e.Offset, SeekOrigin.Begin);

            byte[] bytes = new byte[e.Size];
            stream.Read(bytes, 0, e.Size);
            return new MemoryStream(bytes);
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
