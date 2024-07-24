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
    public class TextureJPGWriter : BaseTextureWriter
    {
        protected override string FileExtension
        {
            get => "jpg";
        }

        protected override Func<
            NativeArray<byte>, //< raw image bytes
            Rendering.GraphicsFormat,
            uint, //< width
            uint, //< height
            uint, //< rowBytes
            NativeArray<byte> //< return
        > EncodingFunc
        {
            get =>
                (NativeArray<byte> rawImageBytes, Rendering.GraphicsFormat graphicsFormat, uint width, uint height, uint rowBytes) =>
                    ImageConversion.EncodeNativeArrayToJPG(
                        input: rawImageBytes,
                        format: graphicsFormat,
                        width: width,
                        height: height,
                        rowBytes: rowBytes,
                        quality: 100
                    );
        }
    }
}
