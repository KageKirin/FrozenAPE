using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FrozenAPE
{
    public class WavefrontMTLWriter : IWavefrontMTLWriter
    {
        ITextureWriter textureWriter;

        public WavefrontMTLWriter(ITextureWriter textureWriter)
        {
            this.textureWriter = textureWriter;
        }

        public string WriteMTL(string name, Material[] materials)
        {
            StringBuilder sb = new();
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
                    .AppendLine($"map_Ka {textureWriter.NameTexture(mainTexture)}")
                    .AppendLine($"map_Kd {textureWriter.NameTexture(mainTexture)}")
                    .AppendLine($"map_Ks {textureWriter.NameTexture(mainTexture)}");
            }

            return sb.ToString();
        }
    }
}
