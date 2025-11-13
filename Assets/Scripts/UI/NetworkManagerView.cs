using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace NetcodePlayground
{
    public class NetworkManagerView : MonoBehaviour
    {

        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _clientButton;
        [SerializeField] private TMP_Text _statusLabel;
        [SerializeField] private GameObject _crosshair;

        void OnEnable()
        {
            _hostButton.onClick.AddListener(OnHostButtonClicked);
            _clientButton.onClick.AddListener(OnClientButtonClicked);
        }

        void Update()
        {
            UpdateUI();
        }

        void OnDisable()
        {
            _hostButton.onClick.RemoveListener(OnHostButtonClicked);
            _clientButton.onClick.RemoveListener(OnClientButtonClicked);
        }

        void OnHostButtonClicked() => NetworkManager.Singleton.StartHost();

        void OnClientButtonClicked() => NetworkManager.Singleton.StartClient();

        void OnServerButtonClicked() => NetworkManager.Singleton.StartServer();

        void UpdateUI()
        {
            if (NetworkManager.Singleton == null)
            {
                SetStartButtons(false);
                SetStatusText("NetworkManager not found");
                return;
            }

            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                SetStartButtons(true);
                SetStatusText("Not connected");
            }
            else
            {
                SetStartButtons(false);
                UpdateStatusLabels();
                _crosshair.SetActive(true);
            }
        }

        void SetStartButtons(bool state)
        {
            _hostButton.gameObject.SetActive(state);
            _clientButton.gameObject.SetActive(state);
        }

        void SetStatusText(string text) => _statusLabel.text = text;

        void UpdateStatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";
            string transport = "Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name;
            string modeText = "Mode: " + mode;
            SetStatusText($"{transport}\n{modeText}");
        }
    }
}
