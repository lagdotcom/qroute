namespace QuakeDemoFun
{
    public struct PackEntry
    {
        public PackEntry(string fileName, int offset, int size)
        {
            int i = fileName.IndexOf('\0');
            FileName = fileName.Substring(0, i);

            Offset = offset;
            Size = size;
        }

        public string FileName { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }

        public override string ToString() => $"{FileName} @{Offset} +{Size}";

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PackEntry)) return false;

            PackEntry other = (PackEntry)obj;
            return FileName == other.FileName && Offset == other.Offset && Size == other.Size;
        }

        public override int GetHashCode() => FileName.GetHashCode();
        public static bool operator ==(PackEntry left, PackEntry right) => left.Equals(right);
        public static bool operator !=(PackEntry left, PackEntry right) => !left.Equals(right);
    }
}
