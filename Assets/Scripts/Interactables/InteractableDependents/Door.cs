using UnityEngine;

namespace NetcodePlayground.Interactables
{
    public class Door : InteractableDependent
    {

        [SerializeField] private Animator _animator;

        protected int _isOpenHash;

        private void Start()
        {
            _isOpenHash = Animator.StringToHash("Open");
        }

        public override void OnInteract(bool on)
        {
            _animator.SetBool(_isOpenHash, on);
        }
    }
}
