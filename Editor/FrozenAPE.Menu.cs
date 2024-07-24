using System;
using System.IO;
using System.Linq;
using System.Text;
using FrozenAPE;
using UnityEditor;
using UnityEngine;

namespace FrozenAPE
{
    public class FrozenAPE_Menu : EditorWindow
    {
        [MenuItem("FrozenAPE/Export as Wavefront OBJ...")]
        [MenuItem("GameObject/FrozenAPE/Export as Wavefront OBJ...")]
        [MenuItem("Assets/FrozenAPE/Export as Wavefront OBJ...")]
        public static void ExportOBJ(MenuCommand menuCommand)
        {
            ITextureWriter texWriter = new TexturePNGWriter();
            IWavefrontOBJWriter objWriter = new WavefrontOBJWriter();
            IWavefrontMTLWriter mtlWriter = new WavefrontMTLWriter();

            if (Selection.activeObject == null)
            {
                Debug.LogError("Nothing selected! Please select a GameObject.");
                return;
            }

            GameObject go = (GameObject)Selection.activeObject;
            if (go != null)
            {
                foreach (var meshFilter in go.GetComponentsInChildren<MeshFilter>(true))
                {
                    if (meshFilter.sharedMesh == null)
                        continue;

                    var meshRenderer = meshFilter.gameObject.GetComponent<MeshRenderer>();

                    var targetPathObj = EditorUtility.SaveFilePanel(
                        "Export Wavefront OBJ",
                        null,
                        $"{meshFilter.gameObject.name}.obj",
                        "obj"
                    );
                    if (string.IsNullOrEmpty(targetPathObj))
                    {
                        Debug.LogError("Nothing selected! Please select a file to write to.");
                        continue;
                    }

                    var targetPathMtl = EditorUtility.SaveFilePanel(
                        "Export Wavefront MTL",
                        null,
                        $"{meshFilter.gameObject.name}.mtl",
                        "mtl"
                    );
                    if (string.IsNullOrEmpty(targetPathMtl))
                    {
                        Debug.LogError("Nothing selected! Please select a file to write to.");
                        continue;
                    }

                    var materials = meshRenderer != null ? meshRenderer.sharedMaterials : Array.Empty<Material>();
                    var obj = objWriter.WriteOBJ(Path.GetFileNameWithoutExtension(targetPathObj), meshFilter.sharedMesh, materials);
                    File.WriteAllText(targetPathObj, obj);

                    var mtl = mtlWriter.WriteMTL(Path.GetFileNameWithoutExtension(targetPathMtl), materials);
                    File.WriteAllText(targetPathMtl, mtl);

                    var textures = materials.Select(x => x.mainTexture);
                    foreach (var tex in textures)
                    {
                        var buf = texWriter.WriteTexture(tex);
                        File.WriteAllBytes(texWriter.NameTexture(tex), buf);
                    }
                }

                foreach (var skinnedMeshRenderer in go.GetComponentsInChildren<SkinnedMeshRenderer>(true))
                {
                    if (skinnedMeshRenderer.sharedMesh == null)
                        continue;

                    var targetPathObj = EditorUtility.SaveFilePanel(
                        "Export Wavefront OBJ",
                        null,
                        $"{skinnedMeshRenderer.gameObject.name}.obj",
                        "obj"
                    );
                    if (string.IsNullOrEmpty(targetPathObj))
                    {
                        Debug.LogError("Nothing selected! Please select a file to write to.");
                        continue;
                    }

                    var targetPathMtl = EditorUtility.SaveFilePanel(
                        "Export Wavefront MTL",
                        null,
                        $"{skinnedMeshRenderer.gameObject.name}.mtl",
                        "mtl"
                    );
                    if (string.IsNullOrEmpty(targetPathMtl))
                    {
                        Debug.LogError("Nothing selected! Please select a file to write to.");
                        continue;
                    }

                    var obj = objWriter.WriteOBJ(
                        Path.GetFileNameWithoutExtension(targetPathObj),
                        skinnedMeshRenderer.sharedMesh,
                        skinnedMeshRenderer.sharedMaterials
                    );
                    File.WriteAllText(targetPathObj, obj);

                    var mtl = mtlWriter.WriteMTL(Path.GetFileNameWithoutExtension(targetPathMtl), skinnedMeshRenderer.sharedMaterials);
                    File.WriteAllText(targetPathMtl, mtl);

                    var textures = skinnedMeshRenderer.sharedMaterials.Select(x => x.mainTexture);
                    foreach (var tex in textures)
                    {
                        var buf = texWriter.WriteTexture(tex);
                        File.WriteAllBytes(texWriter.NameTexture(tex), buf);
                    }
                }
                return;
            }

            Mesh mesh = (Mesh)Selection.activeObject;
            if (mesh != null)
            {
                var targetPathObj = EditorUtility.SaveFilePanel("Export Wavefront OBJ", null, $"{mesh.name}.obj", "obj");
                if (string.IsNullOrEmpty(targetPathObj))
                {
                    Debug.LogError("Nothing selected! Please select a file to write to.");
                    return;
                }

                var obj = objWriter.WriteOBJ(Path.GetFileNameWithoutExtension(targetPathObj), mesh, Array.Empty<Material>());
                File.WriteAllText(targetPathObj, obj);
                return;
            }
        }
    }
}
