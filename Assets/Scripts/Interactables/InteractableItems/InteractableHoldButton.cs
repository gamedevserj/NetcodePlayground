using Unity.Netcode;
using UnityEngine;

namespace NetcodePlayground.Interactables
{
    public class InteractableHoldButton : InteractableBase
    {

        [SerializeField] private Material _isOnMaterial;
        [SerializeField] private Material _isOffMaterial;
        [SerializeField] private MeshRenderer _renderer;

        private readonly NetworkVariable<int> _playersOnTrigger = new NetworkVariable<int>();

        protected override void OnInSceneObjectsSpawned()
        {
            base.OnInSceneObjectsSpawned();
            if (!IsServer)
            {
                _isOn.OnValueChanged += OnInteract;
                _animator.SetBool(_isOnHash, _isOn.Value);
                _renderer.material = _isOn.Value ? _isOnMaterial : _isOffMaterial;
                OnInteracted?.Invoke(_isOn.Value);
            }
        }

        private void OnInteract(bool previous, bool current)
        {
            _animator.SetBool(_isOnHash, _isOn.Value);
            _renderer.material = _isOn.Value ? _isOnMaterial : _isOffMaterial;
            OnInteracted?.Invoke(current);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            OnTriggerEnterRpc();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            OnTriggerExitRpc();
        }

        [Rpc(SendTo.Server)]
        private void OnTriggerEnterRpc()
        {
            _playersOnTrigger.Value++;
            _isOn.Value = true;
            _animator.SetBool(_isOnHash, true);
            _renderer.material = _isOnMaterial;
            OnInteracted?.Invoke(true);
        }

        [Rpc(SendTo.Server)]
        private void OnTriggerExitRpc()
        {
            _playersOnTrigger.Value--;
            if (_playersOnTrigger.Value == 0)
            {
                _isOn.Value = false;
                _animator.SetBool(_isOnHash, false);
                _renderer.material = _isOffMaterial;
                OnInteracted?.Invoke(false);
            }
        }
    }
}
