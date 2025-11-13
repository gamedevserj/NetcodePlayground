using UnityEngine;

namespace NetcodePlayground.Player
{
    public class PlayerView : MonoBehaviour
    {

        [SerializeField] private Animator _animator;
        [SerializeField] private Renderer _renderer;

        private int _speedHash;
        private int _groundedHash;

        private void Start()
        {
            _speedHash = Animator.StringToHash("MoveSpeed");
            _groundedHash = Animator.StringToHash("Grounded");
        }

        public void SetupViewOnSpawn()
        {
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }

        public void Move(float speed)
        {
            _animator.SetFloat(_speedHash, speed);
        }

        public void Jump()
        {
            _animator.SetBool(_groundedHash, false);
        }

        public void Land()
        {
            _animator.SetBool(_groundedHash, true);
        }
    }
}
