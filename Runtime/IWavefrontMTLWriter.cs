using System;
using UnityEngine;

namespace FrozenAPE
{
    public interface IWavefrontMTLWriter
    {
        /// <summary>
        /// create the MTL (ascii) material library from supplied materials
        /// </summary>
        /// <param name="name">name given to this material library. Note: it must match the name provided to the OBJ file as well as the filename written to disc</param>
        /// <param name="materials">array of Unity Material data, usually obtained from `MeshRenderer.sharedMaterials` or `SkinnedMeshRenderer.sharedMaterials`</param>
        /// <returns>string containing the MTL (ascii) material library</returns>
        string WriteMTL(string name, Material[] materials);
    }
}
