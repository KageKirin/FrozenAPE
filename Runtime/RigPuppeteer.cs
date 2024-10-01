using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

#nullable enable

namespace FrozenAPE
{
    public class RigPuppeteer : IRigPuppeteer
    {
        public virtual void Pose(Transform[] transforms, in IEnumerable<PosedBone> posedBones)
        {
            foreach (var posedBone in posedBones)
            {
                int idx = Array.FindIndex(transforms, t => t.name == posedBone.targetBone);
                if (idx < 0)
                {
                    Debug.LogWarning($"could not find bone `{posedBone.targetBone}`. skipping.");
                    continue;
                }

                Debug.Log($"applying pose for bone `{posedBone.targetBone}` [{transforms[idx].name}]");
                if (posedBone.rotation is not null)
                {
                    Debug.Log($"\tposing `{transforms[idx].name}` rotation {transforms[idx].localEulerAngles} to {posedBone.rotation}");
                    transforms[idx].localEulerAngles = (Vector3)math.float3((double3)posedBone.rotation!);
                    Debug.Log($"\t`{transforms[idx].name}` rotation is now {transforms[idx].localEulerAngles}");
                }

                if (posedBone.position is not null)
                {
                    Debug.Log($"\tposing `{transforms[idx].name}` position {transforms[idx].localPosition} to {posedBone.position}");
                    transforms[idx].localPosition = (Vector3)math.float3((double3)posedBone.position!);
                    Debug.Log($"\t`{transforms[idx].name}` position is now {transforms[idx].localPosition}");
                }

                if (posedBone.scaling is not null)
                {
                    Debug.Log($"\tposing `{transforms[idx].name}` scale {transforms[idx].localScale} to {posedBone.scaling}");
                    transforms[idx].localScale = (Vector3)math.float3((double3)posedBone.scaling!);
                    Debug.Log($"\t`{transforms[idx].name}` scale is now {transforms[idx].localScale}");
                }
            }
        }

        public virtual void PoseInWorldSpace(Transform[] transforms, in IEnumerable<PosedBone> posedBones)
        {
            foreach (var posedBone in posedBones)
            {
                int idx = Array.FindIndex(transforms, t => t.name == posedBone.targetBone);
                if (idx < 0)
                {
                    Debug.LogWarning($"could not find bone `{posedBone.targetBone}`. skipping.");
                    continue;
                }

                Debug.Log($"applying pose for bone `{posedBone.targetBone}` [{transforms[idx].name}]");
                if (posedBone.rotation is not null)
                {
                    Debug.Log($"\tposing `{transforms[idx].name}` WORLD SPACE rotation {transforms[idx].eulerAngles} to {posedBone.rotation}");
                    Debug.Log($"\tnote `{transforms[idx].name}` local rotation is {transforms[idx].localEulerAngles}");
                    transforms[idx].eulerAngles = (Vector3)math.float3((double3)posedBone.rotation!);
                    Debug.Log($"\t`{transforms[idx].name}` WORLD SPACE rotation is now {transforms[idx].eulerAngles}");
                    Debug.Log($"\tnote `{transforms[idx].name}` local rotation is now {transforms[idx].localEulerAngles}");
                }

                if (posedBone.position is not null)
                {
                    Debug.Log($"\tposing `{transforms[idx].name}` WORLD SPACE position {transforms[idx].position} to {posedBone.position}");
                    Debug.Log($"\tnote `{transforms[idx].name}` local position is {transforms[idx].localPosition}");
                    transforms[idx].position = (Vector3)math.float3((double3)posedBone.position!);
                    Debug.Log($"\t`{transforms[idx].name}` WORLD SPACE position is now {transforms[idx].position}");
                    Debug.Log($"\tnote `{transforms[idx].name}` local position is now {transforms[idx].localPosition}");
                }

                if (posedBone.scaling is not null)
                {
                    Debug.Log($"\tposing `{transforms[idx].name}` scale {transforms[idx].localScale} to {posedBone.scaling}");
                    transforms[idx].localScale = (Vector3)math.float3((double3)posedBone.scaling!);
                    Debug.Log($"\t`{transforms[idx].name}` scale is now {transforms[idx].localScale}");
                }
            }
        }

        /// <summary>
        /// saves the provided transforms into posed bones
        /// </summary>
        /// <param name="transforms">transforms as returned by `<![CDATA[GameObject.GetComponentsInChildren<Transform>(true)]]>`</param>
        /// <param name="posedBones">the transforms as posed bones</param>
        public virtual void SavePose(Transform[] transforms, out IEnumerable<PosedBone> posedBones)
        {
            List<PosedBone> posedBoneList = new();

            foreach (var transform in transforms)
            {
                PosedBone posedBone = new();
                posedBone.targetBone = transform.name;
                posedBone.rotation = (float3)transform.localEulerAngles;
                posedBone.position = (float3)transform.localPosition;
                posedBone.scaling = (float3)transform.localScale;

                posedBoneList.Add(posedBone);
            }

            posedBones = posedBoneList;
        }

        public virtual void RandomizePose(Transform[] transforms)
        {
            Unity.Mathematics.Random random = new((uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            foreach (var transform in transforms)
            {
                transform.localEulerAngles = (float3)transform.localEulerAngles + random.NextFloat3(-30, 30);
                transform.localPosition = (float3)transform.localPosition + random.NextFloat3(-0.1f, 0.1f);
                transform.localScale = (float3)transform.localScale * random.NextFloat3(0.9f, 1.1f);
            }
        }
    }
}
