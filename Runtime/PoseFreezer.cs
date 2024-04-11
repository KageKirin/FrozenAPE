using System;
using System.Collections.Generic;
using UnityEngine;
using MeshMaterials = System.Tuple<UnityEngine.Mesh, UnityEngine.Material[]>;

namespace FrozenAPE
{
    public class PoseFreezer : IPoseFreezer
    {
        public virtual IEnumerable<MeshMaterials> Freeze(GameObject go)
        {
            List<MeshMaterials> frozenMeshes = new();
            foreach (var smr in go.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                if (smr.sharedMesh == null)
                    continue;

                frozenMeshes.Add(Freeze(smr));
            }

            return frozenMeshes;
        }

        public virtual MeshMaterials Freeze(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            Mesh frozenMesh = new() { name = $"{skinnedMeshRenderer.sharedMesh.name}" };

            skinnedMeshRenderer.BakeMesh(frozenMesh);
            return new MeshMaterials(frozenMesh, skinnedMeshRenderer.sharedMaterials);
        }
    }
}
