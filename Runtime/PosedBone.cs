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

        /// <summary>
        /// bone position offset
        /// offset to default position
        /// </summary>
        public double3? positionOffset;

        /// <summary>
        /// bone rotation (Euler angles)
        /// angular offset to default orientation
        /// </summary>
        public double3? rotationOffset;

        /// <summary>
        /// bone scaling factor
        /// **FACTOR**
        /// CANNOT BE 0
        /// </summary>
        public double3? scalingFactor;
    }

    [Serializable]
    public class PosedBoneContainer
    {
        public List<PosedBone> bones = new();
    }
}
