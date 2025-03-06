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
            PosedBoneContainer deltaBoneContainer = new() { bones = new() };

            foreach (var key in refDictPosedBones.Keys.Intersect(dictPosedBones.Keys))
            {
                PosedBone deltaBone =
                    new()
                    {
                        name = key,
                        position = default,
                        rotation = default,
                        scaling = default
                    };

                if (refDictPosedBones[key].position is not null && refDictPosedBones[key].position is not null)
                {
                    double3 delta = (double3)(dictPosedBones[key].position) - (double3)(refDictPosedBones[key].position);
                    delta = math.double3(Math.Round(delta.x, 7), Math.Round(delta.y, 7), Math.Round(delta.z, 7));

                    if (math.all(delta != double3.zero))
                        deltaBone.position = delta;
                }

                if (refDictPosedBones[key].rotation is not null && refDictPosedBones[key].rotation is not null)
                {
                    double3 delta = (double3)(dictPosedBones[key].rotation) - (double3)(refDictPosedBones[key].rotation);
                    delta = math.double3(Math.Round(delta.x, 7), Math.Round(delta.y, 7), Math.Round(delta.z, 7));

                    if (math.all(delta != double3.zero))
                        deltaBone.rotation = delta;
                }

                if (refDictPosedBones[key].scaling is not null && refDictPosedBones[key].scaling is not null)
                {
                    double3 delta = (double3)(dictPosedBones[key].scaling) - (double3)(refDictPosedBones[key].scaling);
                    delta = math.double3(Math.Round(delta.x, 7), Math.Round(delta.y, 7), Math.Round(delta.z, 7));

                    if (math.all(delta != double3.zero))
                        deltaBone.scaling = delta;
                }

                if (deltaBone.position is not null || deltaBone.rotation is not null || deltaBone.scaling is not null)
                    deltaBoneContainer.bones.Add(deltaBone);
            }

            var json = JsonSerialization.ToJson(deltaBoneContainer);
            File.WriteAllText(path, json);
        }
    }
}
