using Unity.Netcode;

namespace NetcodePlayground.Interactables
{
    public class InteractableSwitch : InteractableBase, IInputInteractable
    {

        public void Interact()
        {
            InteractRpc();
        }

        protected override void OnInSceneObjectsSpawned()
        {
            base.OnInSceneObjectsSpawned();
            if (!IsServer)
            {
                _isOn.OnValueChanged += OnInteract;
                _animator.SetBool(_isOnHash, _isOn.Value);
                OnInteracted?.Invoke(_isOn.Value);
            }
        }

        [Rpc(SendTo.Server)]
        private void InteractRpc()
        {
            _isOn.Value = !_isOn.Value;
            _animator.SetBool(_isOnHash, _isOn.Value);
            OnInteracted?.Invoke(_isOn.Value);
        }

        private void OnInteract(bool previous, bool current)
        {
            _animator.SetBool(_isOnHash, _isOn.Value);
            OnInteracted?.Invoke(_isOn.Value);
        }
    }
}
