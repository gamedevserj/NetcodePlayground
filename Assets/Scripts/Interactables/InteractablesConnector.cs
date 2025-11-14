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
                _connections[i].Connect();
            }
        }

    }
}
