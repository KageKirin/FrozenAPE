using System;
using System.Collections.Generic;
using UnityEngine;
using MeshMaterials = System.Tuple<UnityEngine.Mesh, UnityEngine.Material[]>;

namespace FrozenAPE
{
    /// <summary>
    /// freezes meshes for a specific pose
    /// </summary>
    public interface IPoseFreezer
    {
        /// <summary>
        /// freezes all SkinnedMeshRenderer meshes into static meshes
        /// </summary>
        /// <param name="go">GameObject hierarchy root for which to look for SkinnedMeshRenderers</param>
        /// <returns>array/list/set of frozen meshes and their respective materials (tuple mesh, materials)</returns>


        IEnumerable<MeshMaterials> Freeze(GameObject go);

        MeshMaterials Freeze(SkinnedMeshRenderer skinnedMeshRenderer);
    }
}
