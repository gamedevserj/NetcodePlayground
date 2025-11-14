using UnityEngine;
using UnityEngine.InputSystem;

namespace NetcodePlayground.Player
{
    public class PlayerInputController : MonoBehaviour
    {

        [SerializeField] private PlayerController _player;
        [SerializeField] private PlayerInput _input;

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;
        private InputAction _interactAction;
        private InputAction _pauseAction;

        private void OnEnable()
        {
            _input.enabled = true;
            _moveAction = _input.actions.FindAction("Move");
            _lookAction = _input.actions.FindAction("Look");
            _jumpAction = _input.actions.FindAction("Jump");
            _interactAction = _input.actions.FindAction("Interact");
            _pauseAction = _input.actions.FindAction("Pause");

            _moveAction.performed += _player.InputMove;
            _moveAction.canceled += _player.InputMove;
            _lookAction.performed += _player.InputLook;

            _jumpAction.started += _player.InputJump;
            _interactAction.started += _player.InputInteract;
            _pauseAction.started += _player.InputCanRotate;
        }

        private void OnDisable()
        {
            _moveAction.performed -= _player.InputMove;
            _moveAction.canceled -= _player.InputMove;
            _lookAction.performed -= _player.InputLook;

            _jumpAction.started -= _player.InputJump;
            _interactAction.started -= _player.InputInteract;
            _pauseAction.started -= _player.InputCanRotate;

            _input.enabled = false;
        }
    }
}
