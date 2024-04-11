using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FrozenAPE;
using Unity.Mathematics;
using Unity.Serialization.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace FrozenAPE
{
    public class FrozenAPE_FreezePoseMenu : EditorWindow
    {
        [MenuItem("FrozenAPE/Freeze Current Pose")]
        [MenuItem("GameObject/Freeze Current Pose")]
        private static void FreezeCurrentPose(MenuCommand menuCommand)
        {
            if (Selection.activeObject == null)
            {
                Debug.LogError("Nothing selected! Please select a GameObject.");
                return;
            }

            GameObject go = (GameObject)Selection.activeObject;
            if (go == null)
            {
                Debug.LogError("Only GameObjects can be frozen. Please select a GameObject.");
                return;
            }

            IPoseFreezer poseFreezer = new PoseFreezer();
            var frozenMeshMaterials = poseFreezer.Freeze(go);

            GameObject frozenGo = new() { name = $"frozen_{go.name}" };
            frozenGo.transform.SetPositionAndRotation(go.transform.position, go.transform.rotation);
            frozenGo.transform.localScale = go.transform.localScale;

            foreach (var meshMaterials in frozenMeshMaterials)
            {
                var (mesh, materials) = meshMaterials;
                GameObject meshObject = new($"frozen_{mesh.name}");
                meshObject.transform.parent = frozenGo.transform;

                var meshFilter = meshObject.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = mesh;

                var meshRenderer = meshObject.AddComponent<MeshRenderer>();
                meshRenderer.sharedMaterials = materials;
            }

            Selection.activeObject = frozenGo;
        }
    }
}
