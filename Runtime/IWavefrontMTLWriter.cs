using System;
using UnityEngine;

namespace FrozenAPE
{
    public interface IWavefrontMTLWriter
    {
        string WriteMTL(string name, Material[] materials);
    }
}
