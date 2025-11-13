using UnityEngine;

namespace NetcodePlayground.Interactables
{
    // a base class for items that are dependent on interactables
    // since its state is being controlled by _trigger delegate there is no need for this to be save its own state in network variable
    public abstract class InteractableDependent : MonoBehaviour
    {

        private InteractableBase _interactable;

        public void SetInteractable(InteractableBase interactable)
        {
            _interactable = interactable;
            _interactable.OnInteracted += OnInteract;
        }

        private void OnDisable()
        {
            _interactable.OnInteracted -= OnInteract;
        }

        protected abstract void OnInteract(bool on);
    }
}
