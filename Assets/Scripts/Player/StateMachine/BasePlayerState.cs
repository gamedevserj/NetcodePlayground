using UnityEngine;

namespace NetcodePlayground.Player
{
    // base state to disable all player functionality
    // enable functionality in the states it should be allowed
    public class BasePlayerState : IPlayerState
    {

        protected readonly PlayerController _controller;

        public BasePlayerState(PlayerController controller)
        {
            _controller = controller;
        }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnInteract() { }

        public virtual void OnJump() { }

        public virtual void OnLook(Vector2 rotationAmount) { }

        public virtual void OnMove(Vector2 direction) { }
    }
}
