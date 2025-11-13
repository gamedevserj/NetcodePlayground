using NetcodePlayground.Interactables;
using UnityEngine;

namespace NetcodePlayground
{
    // just a simple script to connect interactables and their dependents
    public class InteractablesConnector : MonoBehaviour
    {

        [SerializeField] private Connection[] _connections;

        private void OnEnable()
        {
            for (int i = 0; i < _connections.Length; i++)
            {
                var connection = _connections[i];
                for (int k = 0; k < connection.Dependents.Length; k++)
                {
                    var dependent = connection.Dependents[k];
                    dependent.SetInteractable(connection.Interactable);
                }
            }
        }

    }

    [System.Serializable]
    public class Connection
    {
        [SerializeField] private InteractableBase _interactable;
        [SerializeField] private InteractableDependent[] _dependents;

        public InteractableBase Interactable => _interactable;
        public InteractableDependent[] Dependents => _dependents;
    }
}
