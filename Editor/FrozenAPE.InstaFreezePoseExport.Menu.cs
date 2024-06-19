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
    public class FrozenAPE_InstaFreezePoseExportMenu : EditorWindow
    {
        [MenuItem("FrozenAPE/Insta Pose, Freeze and Export from JSON...")]
        [MenuItem("GameObject/FrozenAPE/Insta Pose, Freeze and Export from JSON...")]
        private static void InstaPoseFreezeExportFromJson(MenuCommand menuCommand)
        {
            FrozenAPE_PoseMenu.PoseFromJson(menuCommand);
            FrozenAPE_FreezePoseMenu.FreezeCurrentPose(menuCommand);
            FrozenAPE_Menu.ExportOBJ(menuCommand);
        }

        [MenuItem("FrozenAPE/Insta Random Pose, Freeze and Export")]
        [MenuItem("GameObject/FrozenAPE/Insta Random Pose, Freeze and Export")]
        private static void InstaRandomPoseFreezeExportFromJson(MenuCommand menuCommand)
        {
            FrozenAPE_RandomizePoseMenu.RandomizePose(menuCommand);
            FrozenAPE_FreezePoseMenu.FreezeCurrentPose(menuCommand);
            FrozenAPE_Menu.ExportOBJ(menuCommand);
        }
    }
}
