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
                    continue;

                if (posedBone.rotation is not null)
                {
                    transforms[idx].eulerAngles = (Vector3)math.float3((double3)posedBone.rotation!);
                }

                if (posedBone.position is not null)
                {
                    transforms[idx].position = (Vector3)math.float3((double3)posedBone.position!);
                }

                if (posedBone.scaling is not null)
                {
                    transforms[idx].localScale = (Vector3)math.float3((double3)posedBone.scaling!);
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
