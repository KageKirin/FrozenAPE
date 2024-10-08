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
    public class FrozenAPE_PoseMenu : EditorWindow
    {
        [MenuItem("FrozenAPE/Pose from JSON...")]
        [MenuItem("GameObject/FrozenAPE/Pose from JSON...")]
        // [MenuItem("Assets/FrozenAPE/Pose from JSON...")] //< assets are readonly
        public static void PoseFromJson(MenuCommand menuCommand)
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

            var path = EditorUtility.OpenFilePanel("Load pose from JSON", string.Empty, "json");
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
            rigPuppeteer.Pose(go.GetComponentsInChildren<Transform>(true), posedBones.bones);
        }

        [MenuItem("FrozenAPE/Pose in WORLD SPACE from JSON...")]
        [MenuItem("GameObject/FrozenAPE/Pose in WORLD SPACE from JSON...")]
        // [MenuItem("Assets/FrozenAPE/Pose in WORLD SPACE from JSON...")] //< assets are readonly
        public static void PoseInWorldSpaceFromJson(MenuCommand menuCommand)
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

            var path = EditorUtility.OpenFilePanel("Load pose from JSON", string.Empty, "json");
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
            rigPuppeteer.PoseInWorldSpace(go.GetComponentsInChildren<Transform>(true), posedBones.bones);
        }
    }
}
