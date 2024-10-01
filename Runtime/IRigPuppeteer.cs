using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenAPE
{
    /// <summary>
    /// defines function to bring `rig` into specific `pose`
    /// puppeteer => person who controls puppets/marionettes
    /// rig => technical term for character skeleton
    /// </summary>
    public interface IRigPuppeteer
    {
        /// <summary>
        /// poses the provided transforms into the local position/orientation specified by the posed bones
        /// </summary>
        /// <param name="transforms">transforms as returned by `<![CDATA[GameObject.GetComponentsInChildren<Transform>(true)]]>`</param>
        /// <param name="posedBones">1+ posed bones</param>
        void Pose(Transform[] transforms, in IEnumerable<PosedBone> posedBones);

        /// <summary>
        /// saves the provided transforms into posed bones
        /// </summary>
        /// <param name="transforms">transforms as returned by `<![CDATA[GameObject.GetComponentsInChildren<Transform>(true)]]>`</param>
        /// <param name="posedBones">the transforms as posed bones</param>
        void SavePose(Transform[] transforms, out IEnumerable<PosedBone> posedBones);

        /// <summary>
        /// poses provided transforms into a randomized position/orientation
        /// </summary>
        /// <param name="transforms">transforms as returned by `<![CDATA[GameObject.GetComponentsInChildren<Transform>(true)]]>`</param>
        void RandomizePose(Transform[] transforms);
    }
}
