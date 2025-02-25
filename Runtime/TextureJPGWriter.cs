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
            byte[], //< raw image bytes
            Rendering.GraphicsFormat,
            uint, //< width
            uint, //< height
            uint, //< rowBytes
            byte[] //< return
        > EncodingFunc
        {
            get =>
                (rawImageBytes, graphicsFormat, width, height, rowBytes) =>
                    ImageConversion.EncodeArrayToJPG(
                        array: rawImageBytes,
                        format: graphicsFormat,
                        width: width,
                        height: height,
                        rowBytes: rowBytes,
                        quality: 100
                    );
        }
    }
}
