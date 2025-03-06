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
    public class FrozenAPE_PoseFromDeltaMenu : EditorWindow
    {
        [MenuItem("FrozenAPE/Pose from ΔJSON...")]
        [MenuItem("GameObject/FrozenAPE/Pose from ΔJSON...")]
        // [MenuItem("Assets/FrozenAPE/Pose from ΔJSON...")] //< assets are readonly
        public static void PoseFromDeltaJson(MenuCommand menuCommand)
        {
            if (Selection.activeObject == null)
            {
                Debug.LogError("Nothing selected! Please select a GameObject.");
                return;
            }

            GameObject go = (GameObject)Selection.activeObject;
            if (go == null)
            {
                Debug.LogError("Only GameObjects can be posed. Please select a GameObject.");
                return;
            }

            var path = EditorUtility.OpenFilePanel("Load Δpose from JSON", string.Empty, "json");
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("No file selected.");
                return;
            }

            var json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json))
            {
                Debug.LogError($"Failed to read {path}.");
                return;
            }

            var posedBones = JsonSerialization.FromJson<PosedBoneContainer>(json);
            if (posedBones.bones.Count == 0)
            {
                Debug.LogError($"Could not read posed bones in {path}.");
                return;
            }

            IRigPuppeteer rigPuppeteer = new RigPuppeteer();
            rigPuppeteer.PoseFromDelta(go.GetComponentsInChildren<Transform>(true), posedBones.bones);
        }
}
