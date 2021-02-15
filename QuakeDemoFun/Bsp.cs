using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public class Bsp
    {
        public Bsp(string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    Read(br);
                }
            }
        }

        public Bsp(Stream stream)
        {
            using (BinaryReader br = new BinaryReader(stream))
            {
                Read(br);
            }
        }

        private void Read(BinaryReader br)
        {
            uint RawVersion = br.ReadUInt32();
            if (!Enum.IsDefined(typeof(BspVersion), RawVersion))
                throw new NotImplementedException($"Unsupported BSP version: {RawVersion:X}");
            Version = (BspVersion)RawVersion;

            DirEntry ents_e = new DirEntry(br);
            DirEntry planes_e = new DirEntry(br);
            DirEntry miptex_e = new DirEntry(br);
            DirEntry vertices_e = new DirEntry(br);
            DirEntry visilist_e = new DirEntry(br);
            DirEntry nodes_e = new DirEntry(br);
            DirEntry texinfo_e = new DirEntry(br);
            DirEntry faces_e = new DirEntry(br);
            DirEntry lightmap_e = new DirEntry(br);
            DirEntry clipnode_e = new DirEntry(br);
            DirEntry leaves_e = new DirEntry(br);
            DirEntry lface_e = new DirEntry(br);
            DirEntry edges_e = new DirEntry(br);
            DirEntry ledges_e = new DirEntry(br);
            DirEntry models_e = new DirEntry(br);

            br.BaseStream.Seek(planes_e.Offset, SeekOrigin.Begin);
            List<Plane> planes = new List<Plane>();
            for (var i = 0; i < planes_e.Size / Plane.Size(Version); i++)
                planes.Add(new Plane(br, Version));

            br.BaseStream.Seek(vertices_e.Offset, SeekOrigin.Begin);
            Vertices = new List<Vertex>();
            for (var i = 0; i < vertices_e.Size / Vertex.Size(Version); i++)
                Vertices.Add(new Vertex(br, Version));

            br.BaseStream.Seek(edges_e.Offset, SeekOrigin.Begin);
            Edges = new List<Edge>();
            for (var i = 0; i < edges_e.Size / Edge.Size(Version); i++)
                Edges.Add(new Edge(br, Version));
        }

        public BspVersion Version { get; private set; }

        internal List<Edge> Edges { get; private set; }
        internal List<Vertex> Vertices { get; private set; }

        internal struct DirEntry
        {
            public DirEntry(BinaryReader br)
            {
                Offset = br.ReadInt32();
                Size = br.ReadInt32();
            }

            public int Offset { get; set; }
            public int Size { get; set; }

            public override string ToString() => $"@{Offset} +{Size}";
        }

        internal struct Plane
        {
            public Plane(BinaryReader br, BspVersion version)
            {
                NormalX = br.ReadSingle();
                NormalY = br.ReadSingle();
                NormalZ = br.ReadSingle();
                Distance = br.ReadSingle();
                Type = (PlaneType)br.ReadInt32();
            }

            public float NormalX { get; set; }
            public float NormalY { get; set; }
            public float NormalZ { get; set; }
            public float Distance { get; set; }
            public PlaneType Type { get; set; }

            internal static int Size(BspVersion version)
            {
                return 20;
            }

            public override string ToString() => $"[{NormalX} {NormalY} {NormalZ}] d{Distance} {Type}";
        }

        internal enum PlaneType : int
        {
            AxialX = 0,
            AxialY,
            AxialZ,
            NonAxialX,
            NonAxialY,
            NonAxialZ,
        }

        internal struct Vertex
        {
            public Vertex(BinaryReader br, BspVersion version)
            {
                X = br.ReadSingle();
                Y = br.ReadSingle();
                Z = br.ReadSingle();
            }

            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            internal static int Size(BspVersion version)
            {
                return 12;
            }

            public override string ToString() => $"({X} {Y} {Z})";
        }

        internal struct Edge
        {
            public Edge(BinaryReader br, BspVersion version)
            {
                if (version == BspVersion.Quake2BSP)
                {
                    A = br.ReadUInt16();
                    B = br.ReadUInt16();
                }
                else
                {
                    A = br.ReadInt32();
                    B = br.ReadInt32();
                }
            }

            public int A { get; set; }
            public int B { get; set; }

            internal static int Size(BspVersion version)
            {
                if (version == BspVersion.Quake2BSP)
                    return 4;

                return 8;
            }

            public override string ToString() => $"{A}-{B}";
        }
    }
}
