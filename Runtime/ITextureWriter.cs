using System;
using UnityEngine;

namespace FrozenAPE
{
    public interface ITextureWriter
    {
        /// <summary>
        /// write the provided texture to a byte array
        /// data format is implementation specific
        /// </summary>
        /// <param name="texture">Unity Texture to serialize to bytes</returns>
        /// <returns>data array containing the serialized texture</returns>
        byte[] WriteTexture(Texture texture);

        /// <summary>
        /// returns the name the texture would take when written to disc, i.e. with the added file extension.
        /// returned file extension is implementation specific to match encoding
        /// </summary>
        /// <param name="texture">Unity Texture to get the name for</param>
        /// <returns>A fitting name for the texture (usually texture.name), with the correct file extension</returns>
        string NameTexture(Texture texture);
    }
}
