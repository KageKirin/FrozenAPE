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
    public class FrozenAPE_CreatePoseMenu : EditorWindow
    {
        [MenuItem("FrozenAPE/Create Sample Pose JSON...")]
        private static void CreateSamplePoseJson(MenuCommand menuCommand)
        {
            var path = EditorUtility.SaveFilePanel("Create Sample Pose JSON", null, "pose.json", "json");
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Nothing selected! Please select a file to write to.");
                return;
            }

            var json = JsonSerialization.ToJson(SamplePosedContainer);
            File.WriteAllText(path, json);
        }

        static PosedBoneContainer SamplePosedContainer =
            new()
            {
                bones = new()
                {
                    new() { targetBone = "shoulder_L", rotation = math.double3(x: 0, y: 0, z: 55.076), },
                    new() { targetBone = "finger_L_thu01", rotation = math.double3(x: 60, y: 0, z: 0), },
                    new() { targetBone = "finger_L_thu02", rotation = math.double3(x: 0, y: -13, z: 0), },
                    new() { targetBone = "finger_L_thu03", rotation = math.double3(x: 0, y: -13, z: 0), },
                    new() { targetBone = "finger_L_for01", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_for02", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_for03", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_mid01", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_mid02", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_mid03", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_thi01", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_thi02", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_thi03", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_lit01", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_lit02", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "finger_L_lit03", rotation = math.double3(x: 0, y: 0, z: 26.6), },
                    new() { targetBone = "shoulder_R", rotation = math.double3(x: 0, y: 0, z: -55.076), },
                    new() { targetBone = "finger_R_thu01", rotation = math.double3(x: 60, y: 0, z: 0), },
                    new() { targetBone = "finger_R_thu02", rotation = math.double3(x: 0, y: 13, z: 0), },
                    new() { targetBone = "finger_R_thu03", rotation = math.double3(x: 0, y: 13, z: 0), },
                    new() { targetBone = "finger_R_for01", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_for02", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_for03", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_for01", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_mid02", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_mid03", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_thi01", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_thi02", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_thi03", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_lit01", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_lit02", rotation = math.double3(x: 0, y: 0, z: -26.6), },
                    new() { targetBone = "finger_R_lit03", rotation = math.double3(x: 0, y: 0, z: -26.6), }
                }
            };
    }
}
