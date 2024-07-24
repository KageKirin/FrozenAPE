using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace FrozenAPE
{
    public class TextureTGAWriter : ITextureWriter
    {
        public byte[] WriteTexture(Texture texture)
        {
            Assert.IsNotNull(texture);
            Assert.IsTrue(texture is Texture2D, "TextureTGAWriter only supports Texture2D");

            if (texture is Texture2D)
            {
                return ImageConversion.EncodeToTGA(texture as Texture2D);
            }

            return null;
        }

        public string NameTexture(Texture texture)
        {
            Assert.IsNotNull(texture);

            if (Path.GetExtension(texture.name).ToLowerInvariant() == "tga")
                return texture.name;

            return $"{texture.name}.tga";
        }
    }
}
