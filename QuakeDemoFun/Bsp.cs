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
                    Version = br.ReadInt32();
                    if (Version != 0x1d) throw new NotImplementedException($"BSP version: {Version:X} != 1D");

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
                    for (var i = 0; i < planes_e.Size / 20; i++)
                        planes.Add(new Plane(br));

                    br.BaseStream.Seek(vertices_e.Offset, SeekOrigin.Begin);
                    Vertices = new List<Vertex>();
                    for (var i = 0; i < vertices_e.Size / 12; i++)
                        Vertices.Add(new Vertex(br));

                    br.BaseStream.Seek(edges_e.Offset, SeekOrigin.Begin);
                    Edges = new List<Edge>();
                    for (var i = 0; i < edges_e.Size / 4; i++)
                        Edges.Add(new Edge(br));
                }
            }
        }

        public int Version { get; private set; }

        public List<Edge> Edges { get; private set; }
        public List<Vertex> Vertices { get; private set; }

        public struct DirEntry
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

        public struct Plane
        {
            public Plane(BinaryReader br)
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

            public override string ToString() => $"[{NormalX} {NormalY} {NormalZ}] d{Distance} {Type}";
        }

        public enum PlaneType : int
        {
            AxialX = 0,
            AxialY,
            AxialZ,
            NonAxialX,
            NonAxialY,
            NonAxialZ,
        }

        public struct Vertex
        {
            public Vertex(BinaryReader br)
            {
                X = br.ReadSingle();
                Y = br.ReadSingle();
                Z = br.ReadSingle();
            }

            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public override string ToString() => $"({X} {Y} {Z})";
        }

        public struct Edge
        {
            public Edge(BinaryReader br)
            {
                A = br.ReadInt16();
                B = br.ReadInt16();
            }

            public short A { get; set; }
            public short B { get; set; }

            public override string ToString() => $"{A}-{B}";
        }
    }
}
