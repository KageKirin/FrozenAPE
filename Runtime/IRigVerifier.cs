using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenAPE
{
    /// <summary>
    /// defines functions to check a `rig` into against `pose`
    /// rig => technical term for character skeleton
    /// </summary>
    public interface IRigVerifier
    {
        /// <summary>
        /// checks the provided transforms against the position/orientation specified by the posed bones
        /// errors will get written to the regular log
        /// </summary>
        /// <param name="transforms">transforms as returned by `<![CDATA[GameObject.GetComponentsInChildren<Transform>(true)]]>`</param>
        /// <param name="posedBones">1+ posed bones</param>
        /// <returns>true if all transforms matched their posed bone bones counterpart, otherwise false</returns>
        bool CheckPose(Transform[] transforms, in IEnumerable<PosedBone> posedBones);
    }
}
