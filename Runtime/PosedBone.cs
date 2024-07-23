using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Unity.Mathematics;

#nullable enable

namespace FrozenAPE
{
    [Serializable]
    public struct PosedBone
    {
        /// <summary>
        /// bone name
        /// target bone referenced by name
        /// </summary>
        // required
        [Required(AllowEmptyStrings = false)]
        public string targetBone;

        /// <summary>
        /// bone position
        /// </summary>
        public double3? position;

        /// <summary>
        /// bone rotation (Euler angles)
        /// </summary>
        public double3? rotation;

        /// <summary>
        /// bone scaling
        /// CANNOT BE 0
        /// </summary>
        public double3? scaling;
    }

    [Serializable]
    public class PosedBoneContainer
    {
        public List<PosedBone> bones = new();
    }
}
