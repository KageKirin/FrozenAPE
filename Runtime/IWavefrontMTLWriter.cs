using System;
using System.Text;
using UnityEngine;

namespace FrozenAPE
{
    public interface IWavefrontMTLWriter
    {
        StringBuilder WriteMTL(string name, Material[] materials, StringBuilder sb);
    }
}
