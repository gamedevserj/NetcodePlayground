using UnityEngine;

namespace NetcodePlayground.Interactables
{
    // a base class for items that are dependent on interactables
    // since its state is being controlled by _trigger delegate there is no need for this to be save its own state in network variable
    public abstract class InteractableDependent : MonoBehaviour
    {

        public abstract void OnInteract(bool on);
    }
}
