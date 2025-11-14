using NetcodePlayground.Interactables;
using StateMachines.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NetcodePlayground.Player
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float _movementSpeed = 4;
        [SerializeField] private float _jumpHeight = 1.5f;
        [SerializeField] private float _timeToApex = 0.3f;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private Vector2 _rotationLimitsX = new(-40, 50);
        [SerializeField] private float _animationSmoothTime = 7;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private PlayerView _view;
        [SerializeField] private LayerMask _interactablesLayerMask;

        private Vector2 _moveDirection;
        private Vector3 _velocity;
        private float _gravity;
        private float _jumpForce;
        private CharacterController _controller;
        private Vector2 _lastRotation;
        private bool _isAirborne;
        private IStateMachine<IPlayerState> _stateMachine;
        private Transform _transform;
        private float _animatedSpeed;
        private float _currentAnimatedSpeed;

        // just make switching to different window easier
        private bool _canRotate = true;

        public bool IsMoving => !Mathf.Approximately(_moveDirection.magnitude, 0);
        public bool IsGrounded => _controller.isGrounded;

        private Transform Transform
        {
            get
            {
                if (_transform == null)
                    _transform = transform;
                return _transform;
            }
        }

        public void Setup()
        {
            _controller = GetComponent<CharacterController>();
            _gravity = -(2 * _jumpHeight) / Mathf.Pow(_timeToApex, 2);
            _jumpForce = Mathf.Abs(_gravity) * _timeToApex;
            _view.SetupViewOnSpawn();

            var camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
            camera.TargetTransform = _cameraTransform;
            _stateMachine = new PlayerStateMachine(this);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            _stateMachine.OnFixedUpdate();
        }

        public void OnFixedUpdate()
        {
            var dir = _cameraTransform.TransformDirection(new Vector3(_moveDirection.x, 0, _moveDirection.y));
            dir.y = 0;
            Vector3 horizontalVelocity = _movementSpeed * dir.normalized;

            _velocity.x = horizontalVelocity.x;
            _velocity.z = horizontalVelocity.z;
            if (!_controller.isGrounded)
            {
                _velocity.y += _gravity * Time.fixedDeltaTime;
            }

            Transform.rotation = Quaternion.Euler(new Vector3(0, _lastRotation.x, 0));
            var clampedRotation = Mathf.Clamp(-_lastRotation.y, _rotationLimitsX.x, _rotationLimitsX.y);
            _cameraTransform.rotation = Quaternion.Euler(new Vector3(clampedRotation, _lastRotation.x, 0));

            _controller.Move(_velocity * Time.fixedDeltaTime);

            var targetSpeed = horizontalVelocity.magnitude * Mathf.Sign(_moveDirection.y);

            if (_controller.isGrounded)
            {
                if (!_isAirborne)
                {
                    // since new input system treats move inputs on PC like GetAxisRaw() instead of GetAxis()
                    // using this approach to smoothly transition the animation blendstate
                    _animatedSpeed = Mathf.SmoothDamp(_animatedSpeed, targetSpeed, ref _currentAnimatedSpeed, _animationSmoothTime * Time.fixedDeltaTime);

                    // basically remapping speed to match animation parameter values
                    var value = Mathf.InverseLerp(-_movementSpeed, _movementSpeed, _animatedSpeed);
                    var animationValue = Mathf.Lerp(-1, 1, value);

                    _view.Move(animationValue);
                }
                else
                {
                    _isAirborne = false;
                    _view.Land();
                }
            }
        }

        public void InputMove(InputAction.CallbackContext context)
        {
            _stateMachine.CurrentState.OnMove(context.ReadValue<Vector2>());
        }

        public void Move(Vector2 direction)
        {
            _moveDirection = direction;
        }

        public void InputLook(InputAction.CallbackContext context)
        {
            var rotationAmount = context.ReadValue<Vector2>();
            if (rotationAmount == Vector2.zero)
                return;

            _stateMachine.CurrentState.OnLook(rotationAmount);
        }

        public void Look(Vector2 rotationAmount)
        {
            if (_canRotate)
            {
                _lastRotation += _rotationSpeed * Time.fixedDeltaTime * rotationAmount;
            }
        }

        public void InputJump(InputAction.CallbackContext context)
        {
            _stateMachine.CurrentState.OnJump();
        }

        public void Jump()
        {
            if (_controller.isGrounded)
            {
                _velocity.y = _jumpForce;
                _view.Jump();
                _isAirborne = true;
            }
        }

        public void InputInteract(InputAction.CallbackContext context)
        {
            _stateMachine.CurrentState.OnInteract();
        }

        public void Interact()
        {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, 3, _interactablesLayerMask)
                && hit.collider.GetComponent<IInputInteractable>() is IInputInteractable interactable)
            {
                interactable.Interact();
            }
        }

        public void InputCanRotate(InputAction.CallbackContext context)
        {
            _canRotate = !_canRotate;
            Cursor.visible = !_canRotate;
            //Cursor.lockState = _canRotate ? CursorLockMode.Locked : CursorLockMode.None;

        }
    }
}
