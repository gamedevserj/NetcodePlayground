using UnityEngine;

namespace NetcodePlayground.Player
{
    public class IdleState : BasePlayerState
    {
        public IdleState(PlayerController controller) : base(controller)
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

        public override void OnJump()
        {
            _controller.Jump();
        }

        public override void OnInteract()
        {
            _controller.Interact();
        }
    }
}
