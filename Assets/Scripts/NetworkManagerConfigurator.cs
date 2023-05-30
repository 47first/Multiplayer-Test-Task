using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public sealed class NetworkManagerConfigurator: MonoBehaviour
    {
        private void Start()
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += OnPlayerDisconnect;
        }

        private void OnPlayerDisconnect(ulong playerId)
        {
            Debug.Log("On Player Disconnect");

            if (IsLocalPlayerOrServer(playerId) == false)
                return;

            var lobbyScene = SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Single);

            LoadingView.ShowLoadingScreen(lobbyScene);
        }

        private bool IsLocalPlayerOrServer(ulong id) => id == 0 || id == NetworkManager.Singleton.LocalClientId;
    }
}
