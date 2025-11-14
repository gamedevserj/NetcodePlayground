using NetcodePlayground.Interactables;
using UnityEngine;

namespace NetcodePlayground
{
    [System.Serializable]
    public class Connection
    {
        [Tooltip(
            "Disables interactables when all interactables have been turned on." +
            "Useful when two players need to activate two press buttons to open a door and the walk through it")]
        [SerializeField] private bool _disableOnCompletion;

        [SerializeField] private InteractableBase[] _interactables;
        [SerializeField] private InteractableDependent[] _dependents;

        private bool _isOn;

        public void Connect()
        {
            for (int i = 0; i < _interactables.Length; i++)
            {
                _interactables[i].OnInteracted += OnInteracted;
            }
        }

        private void Disconnect()
        {
            for (int i = 0; i < _interactables.Length; i++)
            {
                _interactables[i].OnInteracted -= OnInteracted;
            }
        }

        private void OnInteracted(bool on)
        {
            for (int i = 0; i < _interactables.Length; i++)
            {
                if (!_interactables[i].IsOn)
                {
                    if (_isOn)
                    {
                        SwitchDependents(false);
                    }
                    _isOn = false;
                    return;
                }
            }

            _isOn = true;
            SwitchDependents(true);
            if (_disableOnCompletion)
            {
                Disconnect();
            }
        }

        private void SwitchDependents(bool on)
        {
            for (int i = 0; i < _dependents.Length; i++)
            {
                _dependents[i].OnInteract(on);
            }
        }
        
    }
}
