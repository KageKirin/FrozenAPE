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
    public class FrozenAPE_RandomizePoseMenu : EditorWindow
    {
        [MenuItem("FrozenAPE/Randomize Pose")]
        [MenuItem("GameObject/FrozenAPE/Randomize Pose")]
        public static void RandomizePose(MenuCommand menuCommand)
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

            IRigPuppeteer rigPuppeteer = new RigPuppeteer();
            rigPuppeteer.RandomizePose(go.GetComponentsInChildren<Transform>(true));
        }
    }
}
