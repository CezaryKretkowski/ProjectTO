

using OpenTK.Mathematics;

namespace ProjectTo.Modules.Scene;
public struct MeshData
    {
        public float[] Vertices;
        public uint[] Indices;
        public bool DrawElements;
    }

public struct Mesh
{
    public float[] Vertices;
    public float[] Normals;
    public float[] Uvs;

}

public class MeshLoader
{
     struct Tmp
    {
        public Vector3 vertex;
        public Vector3 normals;
        public Vector2 uvs;
    }

    public static Mesh LoadObj(string path,bool sortByFaces)
    {
        var mesh = LoadObj(path);
        var outVertices = new List<float>();
        mesh.DrawElements = false;
      
        foreach (var item in mesh.Indices)
        {
            uint i = item*8;
            outVertices.Add(mesh.Vertices[i]);
            outVertices.Add(mesh.Vertices[i+1]);
            outVertices.Add(mesh.Vertices[i+2]);
            outVertices.Add(mesh.Vertices[i+3]);
            outVertices.Add(mesh.Vertices[i+4]);
            outVertices.Add(mesh.Vertices[i+5]);
            outVertices.Add(mesh.Vertices[i+6]);
            outVertices.Add(mesh.Vertices[i+7]);
            
        }

        var outMesh = new Mesh();
        var vertList = new List<float>();
        var normList = new List<float>();
        var uvList = new List<float>();
        int ij = 0;
        while (ij<outVertices.Count)
        {
            vertList.Add(outVertices[ij++]);
            vertList.Add(outVertices[ij++]);
            vertList.Add(outVertices[ij++]);
            normList.Add(outVertices[ij++]);
            normList.Add(outVertices[ij++]);
            normList.Add(outVertices[ij++]);
            uvList.Add(outVertices[ij++]);
            uvList.Add(outVertices[ij++]);
        }
        // for (int i =0;i<outVertices.Count;i+=8)
        // {
        //     vertList.Add(outVertices[i]);
        //     vertList.Add(outVertices[i+1]);
        //     vertList.Add(outVertices[i+2]);
        //     normList.Add(outVertices[i+3]);
        //     normList.Add(outVertices[i+4]);
        //     normList.Add(outVertices[i+5]);
        //     uvList.Add(outVertices[i+6]);
        //     uvList.Add(outVertices[i+7]);
        // }

        outMesh.Vertices = vertList.ToArray();
        outMesh.Normals = normList.ToArray();
        outMesh.Uvs = normList.ToArray();
        return outMesh;
    }


    public static MeshData LoadObj(string path)
    {
        var vertBuffer = new List<Vector3>();
        var normalBuffer = new List<Vector3>();
        var uvBuffer = new List<Vector2>();
        var outIndices = new List<uint>();
        var outBuffer = new List<float>();
        var faceBuffer = new List<Vector3i>();
        var tmpBuffer = new List<Tmp>();
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.ToLower().Contains("v "))
                        {
                            var data = line.Substring(2);
                            var dataTable=data.Split(' ');
                            float x, y, z;
                            x = float.Parse(dataTable[0].Replace('.', ','));
                            y = float.Parse(dataTable[1].Replace('.', ','));
                            z = float.Parse(dataTable[2].Replace('.', ','));
                            var vert = new Vector3(x,y,z);
                            vertBuffer.Add(vert);
                            var tmp = new Tmp();
                            tmp.vertex = vert;
                            tmpBuffer.Add(tmp);
                            
                        }
                        if (line.ToLower().Contains("vn "))
                        {
                            var data = line.Substring(3);
                            var dataTable=data.Split(' ');
                            float x, y, z;
                            x = float.Parse(dataTable[0].Replace('.', ','));
                            y = float.Parse(dataTable[1].Replace('.', ','));
                            z = float.Parse(dataTable[2].Replace('.', ','));
                            var normal = new Vector3(x,y,z);
                            normalBuffer.Add(normal);
                        }

                        if (line.ToLower().Contains("vt "))
                        {
                            var data = line.Substring(3);
                            var dataTable=data.Split(' ');
                            float x, y;
                            x = float.Parse(dataTable[0].Replace('.', ','));
                            y = float.Parse(dataTable[1].Replace('.', ','));
                            var uv = new Vector2(x,y);
                            uvBuffer.Add(uv);
                        }
                        
                        if (line.ToLower().Contains("f "))
                        {
                            var data = line.Substring(2);
                            var dataTable = data.Split(' ');
                            var row = new Vector3i[3];
                            int i = 0;
                            foreach (var item in dataTable)
                            {
                                var indexes = item.Split('/');
                                var vertIndex = Int32.Parse(indexes[0]) - 1;
                                var normalIndex = Int32.Parse(indexes[2]) - 1;
                                var uvIndex = Int32.Parse(indexes[1]) - 1;
                                row[i] = new Vector3i(vertIndex,normalIndex,uvIndex);

                                
                                var tmp = new Tmp();
                                tmp.vertex = vertBuffer[vertIndex];
                                tmp.normals = normalBuffer[normalIndex];
                                tmp.uvs = uvBuffer[uvIndex];
                                tmpBuffer[vertIndex] = tmp;
                                faceBuffer.Add(row[0]);
                                i++;
                            }
                            outIndices.Add((uint)row[0].X);
                            outIndices.Add((uint)row[1].X);
                            outIndices.Add((uint)row[2].X); 
                            
                        }
                    }
                }
            }
        }
        else
        {
            throw new Exception("File not exist!");
        }

        foreach (var vertex in tmpBuffer)
        {
            
            var vert = vertex.vertex;
            var normal = vertex.normals;
            var uv = vertex.uvs;
            outBuffer.Add(vert.X);
            outBuffer.Add(vert.Y);
            outBuffer.Add(vert.Z);
            outBuffer.Add(normal.X);
            outBuffer.Add(normal.Y);
            outBuffer.Add(normal.Z);
            outBuffer.Add(uv.X);
            outBuffer.Add(uv.Y);
        }
        
        var mesh = new MeshData
        {
            Vertices = outBuffer.ToArray(),
            Indices = outIndices.ToArray(),
            DrawElements = true
        };
        return mesh;
    }
}