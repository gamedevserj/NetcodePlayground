using Unity.Netcode;
using UnityEngine;

namespace NetcodePlayground.Interactables
{
    public class InteractableBase : NetworkBehaviour
    {

        [SerializeField] protected Animator _animator;

        public delegate void Interacted(bool on);
        public Interacted OnInteracted;

        protected readonly NetworkVariable<bool> _isOn = new NetworkVariable<bool>();

        protected int _isOnHash;

        private void Start()
        {
            _isOnHash = Animator.StringToHash("On");
        }
    }
}
