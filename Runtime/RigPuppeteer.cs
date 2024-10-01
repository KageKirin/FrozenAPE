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
                    Debug.LogWarning($"could not find bone {posedBone.targetBone}. skipping.");
                    continue;
                }

                Debug.Log($"applying pose for bone {posedBone.targetBone} [{transforms[idx].name}]");
                if (posedBone.rotation is not null)
                {
                    Debug.Log($"\tposing rotation {transforms[idx].eulerAngles} to {posedBone.rotation}");
                    transforms[idx].eulerAngles = (Vector3)math.float3((double3)posedBone.rotation!);
                    Debug.Log($"\trotation is now {transforms[idx].eulerAngles}");
                }

                if (posedBone.position is not null)
                {
                    Debug.Log($"\tposing position {transforms[idx].position} to {posedBone.position}");
                    transforms[idx].position = (Vector3)math.float3((double3)posedBone.position!);
                    Debug.Log($"\tposition is now {transforms[idx].position}");
                }

                if (posedBone.scaling is not null)
                {
                    Debug.Log($"\tposing scale {transforms[idx].localScale} to {posedBone.scaling}");
                    transforms[idx].localScale = (Vector3)math.float3((double3)posedBone.scaling!);
                    Debug.Log($"\tscale is now {transforms[idx].localScale}");
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
                posedBone.rotation = (float3)transform.eulerAngles;
                posedBone.position = (float3)transform.position;
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
                transform.eulerAngles = (float3)transform.eulerAngles + random.NextFloat3(-30, 30);
                transform.position = (float3)transform.position + random.NextFloat3(-0.1f, 0.1f);
                transform.localScale = (float3)transform.localScale * random.NextFloat3(0.9f, 1.1f);
            }
        }
    }
}
