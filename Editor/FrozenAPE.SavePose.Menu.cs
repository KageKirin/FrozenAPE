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
    public class FrozenAPE_SavePoseMenu : EditorWindow
    {
        [MenuItem("FrozenAPE/Save Pose to JSON...")]
        [MenuItem("GameObject/FrozenAPE/Save Pose to JSON...")]
        private static void SavePoseJson(MenuCommand menuCommand)
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

            var path = EditorUtility.SaveFilePanel("Save Pose to JSON", null, "pose.json", "json");
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Nothing selected! Please select a file to write to.");
                return;
            }

            IRigPuppeteer rigPuppeteer = new RigPuppeteer();
            rigPuppeteer.SavePose(go.GetComponentsInChildren<Transform>(true), out var posedBones);

            PosedBoneContainer posedBoneContainer = new() { bones = new(posedBones) };
            var json = JsonSerialization.ToJson(posedBoneContainer);
            File.WriteAllText(path, json);
        }
    }
}
