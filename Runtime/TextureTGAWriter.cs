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
    public class TextureTGAWriter : BaseTextureWriter
    {
        protected override string FileExtension
        {
            get => "tga";
        }

        protected override Func<
            byte[], //< raw image bytes
            Rendering.GraphicsFormat,
            uint, //< width
            uint, //< height
            uint, //< rowBytes
            byte[] //< return
        > EncodingFunc
        {
            get => ImageConversion.EncodeArrayToTGA;
        }
    }
}
