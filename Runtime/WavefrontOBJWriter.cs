using System;
using System.Linq;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

namespace FrozenAPE
{
    public class WavefrontOBJWriter : IWavefrontOBJWriter
    {
        public string WriteOBJ(string name, Mesh mesh, Material[] materials)
        {
            StringBuilder sb = new();
            sb.AppendLine($"o {name}");

            sb.AppendLine().AppendLine("# materials");
            sb.AppendLine($"mtllib {name}.mtl");

            sb.AppendLine().AppendLine("# vertices");
            foreach (var v in mesh.vertices)
            {
                sb.AppendLine($"v {-v.x} {v.y} {v.z}");
            }

            sb.AppendLine().AppendLine("# normals");
            foreach (var vn in mesh.normals)
            {
                sb.AppendLine($"vn {-vn.x} {vn.y} {vn.z}");
            }

            sb.AppendLine().AppendLine("# texcoords");
            foreach (var vt in mesh.uv)
            {
                sb.AppendLine($"vt {vt.x} {vt.y}");
            }

            for (int submeshIndex = 0; submeshIndex < mesh.subMeshCount; submeshIndex++)
            {
                var desc = mesh.GetSubMesh(submeshIndex);
                var faceTag = desc.topology switch
                {
                    MeshTopology.Triangles => "f",
                    MeshTopology.Quads => "f",
                    MeshTopology.Lines => "l",
                    MeshTopology.LineStrip => "l",
                    MeshTopology.Points => "p",
                    _ => "f",
                };
                var elementsPerLine = desc.topology switch
                {
                    MeshTopology.Triangles => 3,
                    MeshTopology.Quads => 4,
                    MeshTopology.Lines => 2,
                    MeshTopology.LineStrip => desc.indexCount,
                    MeshTopology.Points => 1,
                    _ => 1,
                };
                Material mat = materials.ElementAtOrDefault(submeshIndex);

                sb.AppendLine().AppendLine($"# submesh {submeshIndex}").AppendLine($"g {mesh.name}_{submeshIndex}");

                if (mat != null)
                    sb.AppendLine($"usemtl {mat.name}");

                for (int i = 0; i < desc.indexCount; i += elementsPerLine)
                {
                    sb.Append($"{faceTag}");
                    for (int x = elementsPerLine - 1; x >= 0; x--)
                    {
                        int faceIndex = 1 + mesh.triangles[desc.indexStart + i + x]; // indices are 1 based
                        sb.Append($" {faceIndex}/{faceIndex}/{faceIndex}"); //< indices in order `v/vt/vn`
                    }
                    sb.AppendLine("");
                }
            }

            return sb.ToString();
        }
    }
}
