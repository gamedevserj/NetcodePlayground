using NetcodePlayground.Player;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace NetcodePlayground
{
    public class AirborneState : BasePlayerState
    {
        public AirborneState(PlayerController controller) : base(controller)
        {
        }

        public override void OnFixedUpdate()
        {
            _controller.OnFixedUpdate();
        }

        public override void OnMove(Vector2 direction)
        {
            _controller.Move(direction);
        }

        public override void OnLook(Vector2 rotationAmount)
        {
            _controller.Look(rotationAmount);
        }
    }
}
