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
            Debug.Log(
                $"reading texture (w:{texture.width}, h:{texture.height}, d:{GetTextureDepth(texture)}, f:{texture.graphicsFormat}):",
                texture
            );

            NativeArray<byte> imageBytes = FetchPixels(texture);
            Debug.Log($"image data {imageBytes}: {imageBytes.Length} bytes");

            if (imageBytes != null && imageBytes.Length > 0)
            {
                using NativeArray<byte> encodedBytes = EncodingFunc(
                    imageBytes,
                    texture.graphicsFormat,
                    (uint)texture.width,
                    (uint)texture.height * (uint)GetTextureDepth(texture),
                    0
                );

                Debug.Log($"encoded data {encodedBytes}: {encodedBytes.Length} bytes");
                if (encodedBytes != null && encodedBytes.Length > 0)
                    return encodedBytes.ToArray();
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
        /// signature matches ImageConversion.EncodeArrayTo[PNG|TGA|JPG|EXR]
        /// </summary>
        protected abstract Func<
            byte[], //< raw image bytes
            Rendering.GraphicsFormat,
            uint, //< width
            uint, //< height
            uint, //< rowBytes
            byte[] //< return
        > EncodingFunc { get; }

        /// <summary>
        /// retrieves the texture's depth (3D: depth, 2DArray: #slices)
        /// defaults to 1 for 1D/2D textures
        /// </summary>
        /// <param name="texture">texture to get data from</param>
        /// <returns>texture depth/#slices if applicable, else 1</returns>
        protected virtual int GetTextureDepth(Texture texture)
        {
            if (texture is Texture3D)
                return (texture as Texture3D).depth;

            if (texture is Texture2DArray)
                return (texture as Texture2DArray).depth;

            return 1;
        }

        /// <summary>
        /// fetches pixels from the given texture using .GetPixelData()
        /// returns a native array containing the data
        /// </summary>
        /// <param name="texture">texture to read (must be set to readable)</param>
        /// <returns>native array containing the data</returns>
        protected virtual NativeArray<byte> FetchPixelsFast(Texture texture)
        {
            Debug.Log("fetching data through Texture.GetPixelData()");

            try
            {
                if (texture is Texture2D)
                {
                    return (texture as Texture2D).GetPixelData<byte>(mipLevel: 0);
                }
                else if (texture is Texture3D)
                {
                    return (texture as Texture3D).GetPixelData<byte>(mipLevel: 0);
                }
                else if (texture is Texture2DArray)
                {
                    return (texture as Texture2DArray).GetPixelData<byte>(mipLevel: 0, element: 0);
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"failed to access texture data using Texture.GetPixelData<byte>(): {ex}");
            }

            NativeArray<byte> empty = default;
            return empty;
        }

        /// <summary>
        /// fetches pixels from the given texture using .GetPixels32()
        /// returns a native array containing the data
        /// </summary>
        /// <param name="texture">texture to read (must be set to readable)</param>
        /// <returns>native array containing the data</returns>
        protected virtual NativeArray<byte> FetchPixels(Texture texture)
        {
            Debug.Log("fetching data through Texture.GetPixels32()");

            try
            {
                Color32[] colors = default;
                if (texture is Texture2D)
                {
                    colors = (texture as Texture2D).GetPixels32(miplevel: 0);
                }
                else if (texture is Texture3D)
                {
                    colors = (texture as Texture3D).GetPixels32(miplevel: 0);
                }
                else if (texture is Texture2DArray)
                {
                    colors = (texture as Texture2DArray).GetPixels32(miplevel: 0, arrayElement: 0);
                }

                if (colors != null && colors.Length > 0)
                {
                    int textureDepth = GetTextureDepth(texture);
                    NativeArray<byte> imageBytes = new NativeArray<byte>(colors.Length * textureDepth * 4, Allocator.Persistent);
                    for (int i = 0; i < colors.Length * textureDepth; i++)
                    {
                        imageBytes[i * 4 + 0] = colors[i].r;
                        imageBytes[i * 4 + 1] = colors[i].g;
                        imageBytes[i * 4 + 2] = colors[i].b;
                        imageBytes[i * 4 + 3] = colors[i].a;
                    }
                    return imageBytes;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"failed to access texture data using Texture.GetPixels32(): {ex}");
            }

            NativeArray<byte> empty = default;
            return empty;
        }
    }
}
