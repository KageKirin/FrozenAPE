using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

#nullable enable

namespace FrozenAPE
{
    public class RigVerifier : IRigVerifier
    {
        /// <summary>
        /// A precision cutoff κ defined as scalar factor applied prior to converting the comparison terms to integer
        /// i.e. instead of using a custom ε at milli-unit level in with a double-precision floating-point comparison
        /// We proceed to an integer comparison after multiplying the comparison terms with this precision cutoff.
        /// The cutoff is currently set to 10k which is seemingly precise enough for all our needs.
        /// </summary>
        const int k_CutoffPrecision = 10000;
        public virtual bool CheckPose(Transform[] transforms, in IEnumerable<PosedBone> posedBones)
        {
            List<bool> matches = new();
            foreach (var posedBone in posedBones)
            {
                var transform = transforms.Where(t => t.name == posedBone.targetBone).FirstOrDefault();
                if (transform == null)
                {
                    Debug.LogWarning($"could not retrieve transform for {posedBone.targetBone}.");
                    continue;
                }

                if (posedBone.rotation is not null)
                {
                    var match = (int3)(posedBone.rotation * k_CutoffPrecision) == (int3)((float3)transform.eulerAngles * k_CutoffPrecision);
                    matches.Add(match.x);
                    matches.Add(match.y);
                    matches.Add(match.z);
                    if (!(match.x && match.y && match.z))
                        Debug.LogError(
                            $"rotation mismatch for {posedBone.targetBone}: {match}"
                                + $"\n Posed Bone rotation is {posedBone.rotation}"
                                + $"\n transform rotation is {transform.eulerAngles}"
                        );
                }

                if (posedBone.position is not null)
                {
                    var match = (int3)(posedBone.position * k_CutoffPrecision) == (int3)((float3)transform.position * k_CutoffPrecision);
                    matches.Add(match.x);
                    matches.Add(match.y);
                    matches.Add(match.z);
                    if (!(match.x && match.y && match.z))
                        Debug.LogError(
                            $"position mismatch for {posedBone.targetBone}: {match}"
                                + $"\n Posed Bone position is {posedBone.position}"
                                + $"\n transform position is {transform.position}"
                        );
                }

                if (posedBone.scaling is not null)
                {
                    var match = (int3)(posedBone.scaling * k_CutoffPrecision) == (int3)((float3)transform.localScale * k_CutoffPrecision);
                    matches.Add(match.x);
                    matches.Add(match.y);
                    matches.Add(match.z);
                    if (!(match.x && match.y && match.z))
                        Debug.LogError(
                            $"scaling mismatch for {posedBone.targetBone}: {match}"
                                + $"\n Posed Bone scaling is {posedBone.scaling}"
                                + $"\n transform scaling is {transform.localScale}"
                        );
                }
            }

            return matches.All(b => b);
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
