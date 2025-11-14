using Unity.Netcode;
using UnityEngine;

namespace NetcodePlayground.Interactables
{
    public abstract class InteractableBase : NetworkBehaviour
    {

        [SerializeField] protected Animator _animator;

        public delegate void Interacted(bool on);
        public Interacted OnInteracted;

        protected readonly NetworkVariable<bool> _isOn = new NetworkVariable<bool>();

        protected int _isOnHash;

        public bool IsOn => _isOn.Value;

        private void Start()
        {
            _isOnHash = Animator.StringToHash("On");
        }
    }
}
