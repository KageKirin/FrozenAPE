using System;
using System.Text;
using UnityEngine;

namespace FrozenAPE
{
    public interface IWavefrontOBJWriter
    {
        StringBuilder WriteOBJ(string name, Mesh mesh, Material[] materials, StringBuilder sb);
    }
}
