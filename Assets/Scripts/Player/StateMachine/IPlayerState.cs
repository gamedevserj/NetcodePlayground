using StateMachines.Interfaces;
using UnityEngine;

namespace NetcodePlayground.Player
{
    public interface IPlayerState : IState
    {

        void OnMove(Vector2 direction);
        void OnLook(Vector2 rotationAmount);
        void OnJump();
        void OnInteract();

    }
}
