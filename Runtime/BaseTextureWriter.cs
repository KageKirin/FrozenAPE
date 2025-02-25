using System;
using System.IO;
using System.Linq;
using System.Text;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Rendering = UnityEngine.Experimental.Rendering;

namespace FrozenAPE
{
    public abstract class BaseTextureWriter : ITextureWriter
    {
        public byte[] WriteTexture(Texture texture)
        {
            Assert.IsNotNull(texture);
            NativeArray<byte> imageBytes = default;
            uint texture_depth = 1;

            try
            {
                if (texture is Texture2D)
                {
                    imageBytes = (texture as Texture2D).GetPixelData<byte>(mipLevel: 0);
                }
                else if (texture is Texture3D)
                {
                    imageBytes = (texture as Texture3D).GetPixelData<byte>(mipLevel: 0);
                    texture_depth = (uint)(texture as Texture3D).depth;
                }
                else if (texture is Texture2DArray)
                {
                    imageBytes = (texture as Texture2DArray).GetPixelData<byte>(mipLevel: 0, element: 0);
                    texture_depth = (uint)(texture as Texture2DArray).depth;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"failed to access texture data using Texture.GetPixelData<byte>(): {ex}");
            }

            // alternative (safer, slower) approach to get pixel data
            if (imageBytes != null && imageBytes.Length > 0)
            {
                Color32[] colors = default;
                if (texture is Texture2D)
                {
                    colors = (texture as Texture2D).GetPixels32(miplevel: 0);
                }
                else if (texture is Texture3D)
                {
                    colors = (texture as Texture3D).GetPixels32(miplevel: 0);
                    texture_depth = (uint)(texture as Texture3D).depth;
                }
                else if (texture is Texture2DArray)
                {
                    colors = (texture as Texture2DArray).GetPixels32(miplevel: 0, arrayElement: 0);
                    texture_depth = (uint)(texture as Texture2DArray).depth;
                }

                if (colors != null && colors.Length > 0)
                {
                    imageBytes = new NativeArray<byte>(colors.Length * 4, Allocator.Temp);
                    for (int i = 0; i < colors.Length; i++)
                    {
                        imageBytes[i * 4 + 0] = colors[i].r;
                        imageBytes[i * 4 + 1] = colors[i].g;
                        imageBytes[i * 4 + 2] = colors[i].b;
                        imageBytes[i * 4 + 3] = colors[i].a;
                    }
                }
            }

            if (imageBytes != null && imageBytes.Length > 0)
            {
                using NativeArray<byte> bytes = EncodingFunc(
                    imageBytes,
                    texture.graphicsFormat,
                    (uint)texture.width,
                    (uint)texture.height * texture_depth,
                    0
                );

                if (bytes != null && bytes.Length > 0)
                    return bytes.ToArray();
            }
            return Array.Empty<byte>();
        }

        public string NameTexture(Texture texture)
        {
            Assert.IsNotNull(texture);

            if (Path.GetExtension(texture.name).ToLowerInvariant() == FileExtension)
                return texture.name;

            return $"{texture.name}.{FileExtension}";
        }

        /// <summary>
        /// the file extension, lowercase without '.'
        /// </summary>
        protected abstract string FileExtension { get; }

        /// <summary>
        /// the encoding function
        /// signature matches ImageConversion.EncodeNativeArrayTo[PNG|TGA|JPG|EXR]
        /// </summary>
        protected abstract Func<
            NativeArray<byte>, //< raw image bytes
            Rendering.GraphicsFormat,
            uint, //< width
            uint, //< height
            uint, //< rowBytes
            NativeArray<byte> //< return
        > EncodingFunc { get; }
    }
}
