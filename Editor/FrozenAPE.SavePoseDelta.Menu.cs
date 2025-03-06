using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FrozenAPE;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Serialization.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace FrozenAPE
{
    public class FrozenAPE_SavePoseDeltaMenu : EditorWindow
    {
        [MenuItem("FrozenAPE/Save Pose Δ to JSON...")]
        [MenuItem("GameObject/FrozenAPE/Save Pose Δ to JSON...")]
        private static void SavePoseDeltaJson(MenuCommand menuCommand)
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

            var refpath = EditorUtility.OpenFilePanel("Load reference pose from JSON", string.Empty, "json");
            if (string.IsNullOrEmpty(refpath))
            {
                Debug.LogError("No file selected.");
                return;
            }

            var refjson = File.ReadAllText(refpath);
            if (string.IsNullOrEmpty(refjson))
            {
                Debug.LogError($"Failed to read {refpath}.");
                return;
            }

            var refPosedBones = JsonSerialization.FromJson<PosedBoneContainer>(refjson);
            if (refPosedBones.bones.Count == 0)
            {
                Debug.LogError($"Could not read posed bones in {refpath}.");
                return;
            }

            var path = EditorUtility.SaveFilePanel("Save Pose Δ to JSON", null, "pose.json", "json");
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Nothing selected! Please select a file to write to.");
                return;
            }

            IRigPuppeteer rigPuppeteer = new RigPuppeteer();
            rigPuppeteer.SavePose(go.GetComponentsInChildren<Transform>(true), out var posedBones);

            Dictionary<string, PosedBone> refDictPosedBones = refPosedBones.bones.ToDictionary(x => x.name, x => x);
            Dictionary<string, PosedBone> dictPosedBones = posedBones.ToDictionary(x => x.name, x => x);
            foreach (var kvp in refDictPosedBones)
            {
                if (dictPosedBones.ContainsKey(kvp.Key))
                {
                    PosedBone PoseFromDeltadBone = dictPosedBones[kvp.Key];
                    if (PoseFromDeltadBone.position is not null && kvp.Value.position is not null)
                    {
                        PoseFromDeltadBone.position -= kvp.Value.position;
                    }
                    else
                    {
                        PoseFromDeltadBone.position = null;
                    }

                    if (PoseFromDeltadBone.rotation is not null && kvp.Value.rotation is not null)
                    {
                        PoseFromDeltadBone.rotation -= kvp.Value.rotation;
                    }
                    else
                    {
                        PoseFromDeltadBone.rotation = null;
                    }

                    if (PoseFromDeltadBone.scaling is not null && kvp.Value.scaling is not null)
                    {
                        PoseFromDeltadBone.scaling -= kvp.Value.scaling;
                    }
                    else
                    {
                        PoseFromDeltadBone.scaling = null;
                    }

                    dictPosedBones[kvp.Key] = PoseFromDeltadBone;
                }
            }

            PosedBoneContainer deltaBoneContainer = new() { bones = new(dictPosedBones.Values) };
            var json = JsonSerialization.ToJson(deltaBoneContainer);
            File.WriteAllText(path, json);
        }
    }
}
