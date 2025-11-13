using Unity.Netcode;
using UnityEngine;

namespace NetcodePlayground.Player
{
    public class NetworkPlayer : NetworkBehaviour
    {

        [SerializeField] private PlayerController _controller;
        [SerializeField] private PlayerInputController _inputController;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (!IsOwner)
            {
                _controller.enabled = false;
                _inputController.enabled = false;
                return;
            }

            _controller.Setup();
        }

    }
}
