using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FrozenAPE
{
    public class WavefrontMTLWriter : IWavefrontMTLWriter
    {
        public StringBuilder WriteMTL(string name, Material[] materials, StringBuilder sb)
        {
            sb.AppendLine($"# material lib {name}");

            foreach (var mat in materials.Distinct())
            {
                var mainColor = mat.color;
                var mainTexture = mat.mainTexture;
                sb.AppendLine()
                    .AppendLine($"newmtl {mat.name}")
                    .AppendLine("illum 1") // flat material, no highlights
                    .AppendLine($"Ka  0.0000  0.0000  0.0000") // TODO: fill with correct values (ambient color)
                    .AppendLine($"Kd  {mainColor.r} {mainColor.g} {mainColor.b}") // diffuse color
                    .AppendLine($"Ks  {mainColor.r} {mainColor.g} {mainColor.b}") // diffuse color
                    .AppendLine($"d   {mainColor.a}") // alpha
                    .AppendLine($"Ks  0.0000  0.0000  0.0000") // TODO: fill with correct values (specular color)
                    .AppendLine($"Ns  0.0000") // TODO: fill with correct values (shininess)
                    .AppendLine($"map_Ka {mainTexture.name}.png")
                    .AppendLine($"map_Kd {mainTexture.name}.png")
                    .AppendLine($"map_Ks {mainTexture.name}.png");
            }

            return sb;
        }
    }
}
