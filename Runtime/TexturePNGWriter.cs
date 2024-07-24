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
    public class TexturePNGWriter : BaseTextureWriter
    {
        protected override string FileExtension
        {
            get => "png";
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
            get => ImageConversion.EncodeNativeArrayToPNG;
        }
    }
}
