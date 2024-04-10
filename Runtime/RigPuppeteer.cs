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

                if (posedBone.rotationOffset is not null)
                {
                    transforms[idx].eulerAngles += (Vector3)math.float3((double3)posedBone.rotationOffset!);
                }

                if (posedBone.positionOffset is not null)
                {
                    transforms[idx].position += (Vector3)math.float3((double3)posedBone.positionOffset!);
                }

                if (posedBone.scalingFactor is not null)
                {
                    float3 scale = transforms[idx].localScale;
                    scale *= math.float3((double3)posedBone.scalingFactor!);
                    transforms[idx].localScale = scale;
                }
            }
        }
    }
}
