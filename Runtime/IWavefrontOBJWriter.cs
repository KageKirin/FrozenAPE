using System;
using UnityEngine;

namespace FrozenAPE
{
    public interface IWavefrontOBJWriter
    {
        /// <summary>
        /// create the OBJ (ascii) representation from supplied mesh and materials
        /// </summary>
        /// <param name="name">name given to this model. Note: it must match the name provided to the MTL file as well as the filename written to disc</param>
        /// <param name="mesh">Unity Mesh data, usually obtained from `MeshFilter.sharedMesh` or `SkinnedMeshRenderer.sharedMesh`</param>
        /// <param name="materials">array of Unity Material data, usually obtained from `MeshRenderer.sharedMaterials` or `SkinnedMeshRenderer.sharedMaterials`</param>
        /// <returns>string containing the OBJ (ascii) representation</returns>
        string WriteOBJ(string name, Mesh mesh, Material[] materials);

        /// <summary>
        /// create the OBJ (ascii) representation from several provided meshes and and their respective materials
        /// this allows to to merge multiple meshes into 1, provided their vertices fit.
        /// </summary>
        /// <param name="name">name given to this model. Note: it must match the name provided to the MTL file as well as the filename written to disc</param>
        /// <param name="meshes">array Unity Mesh data, usually obtained from `MeshFilter.sharedMesh` or `SkinnedMeshRenderer.sharedMesh`</param>
        /// <param name="materials">array of Unity Material data, usually obtained from `MeshRenderer.sharedMaterials` or `SkinnedMeshRenderer.sharedMaterials`</param>
        /// <returns>string containing the OBJ (ascii) representation</returns>
        string WriteOBJ(string name, Mesh[] meshes, Material[] materials);
    }
}
