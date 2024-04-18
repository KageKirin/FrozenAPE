using System;
using UnityEngine;

namespace FrozenAPE
{
    public interface IWavefrontOBJWriter
    {
        string WriteOBJ(string name, Mesh mesh, Material[] materials);
    }
}
