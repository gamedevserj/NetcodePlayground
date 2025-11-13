using Unity.Netcode.Components;
using UnityEngine;

namespace NetcodePlayground
{
    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}