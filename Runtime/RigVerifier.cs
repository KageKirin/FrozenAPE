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
        const double k_CutoffPrecision = 10000.0;

        public virtual bool CheckPose(Transform[] transforms, in IEnumerable<PosedBone> posedBones)
        {
            List<bool> matches = new();
            foreach (var posedBone in posedBones)
            {
                var transform = transforms.Where(t => t.name == posedBone.name).FirstOrDefault();
                if (transform == null)
                {
                    Debug.LogWarning($"could not retrieve transform for `{posedBone.name}`.");
                    continue;
                }

                if (posedBone.rotation is not null)
                {
                    double3 transform_angles = (float3)transform.localEulerAngles;
                    int3 transform_angles_comparand = (int3)(transform_angles * k_CutoffPrecision);
                    int3 posedBone_rotation_comparand = (int3)(posedBone.rotation * k_CutoffPrecision);

                    var match = posedBone_rotation_comparand == transform_angles_comparand;
                    matches.Add(match.x);
                    matches.Add(match.y);
                    matches.Add(match.z);
                    if (!(match.x && match.y && match.z))
                        Debug.Log($"transform `{transform.name}` angles: {transform_angles} [{transform_angles_comparand}]");
                    Debug.Log($"posedBone `{posedBone.name}` rotation: {posedBone.rotation} [{posedBone_rotation_comparand}]");
                    Debug.LogError(
                        $"rotation mismatch for `{posedBone.name}`: {match}"
                            + $"\n Posed Bone rotation is {posedBone.rotation}"
                            + $"\n transform rotation is {transform.localEulerAngles}"
                    );
                }

                if (posedBone.position is not null)
                {
                    double3 transform_position = (float3)transform.localPosition;
                    int3 transform_position_comparand = (int3)(transform_position * k_CutoffPrecision);
                    int3 posedBone_position_comparand = (int3)(posedBone.position * k_CutoffPrecision);

                    var match = posedBone_position_comparand == transform_position_comparand;
                    matches.Add(match.x);
                    matches.Add(match.y);
                    matches.Add(match.z);
                    if (!(match.x && match.y && match.z))
                        Debug.Log($"transform `{transform.name}` position: {transform_position} [{transform_position_comparand}]");
                    Debug.Log($"posedBone `{posedBone.name}` position: {posedBone.position} [{posedBone_position_comparand}]");
                    Debug.LogError(
                        $"position mismatch for `{posedBone.name}`: {match}"
                            + $"\n Posed Bone position is {posedBone.position}"
                            + $"\n transform position is {transform.localPosition}"
                    );
                }

                if (posedBone.scaling is not null)
                {
                    double3 transform_scale = (float3)transform.localScale;
                    int3 transform_scale_comparand = (int3)(transform_scale * k_CutoffPrecision);
                    int3 posedBone_scaling_comparand = (int3)(posedBone.scaling * k_CutoffPrecision);

                    var match = posedBone_scaling_comparand == transform_scale_comparand;
                    matches.Add(match.x);
                    matches.Add(match.y);
                    matches.Add(match.z);
                    if (!(match.x && match.y && match.z))
                        Debug.Log($"transform `{transform.name}` scale: {transform_scale} [{transform_scale_comparand}]");
                    Debug.Log($"transform `{transform.name}` scale: {posedBone.scaling} [{posedBone_scaling_comparand}]");
                    Debug.LogError(
                        $"scaling mismatch for `{posedBone.name}`: {match}"
                            + $"\n Posed Bone scaling is {posedBone.scaling}"
                            + $"\n transform scaling is {transform.localScale}"
                    );
                }
            }

            return matches.All(b => b);
        }
    }
}
