using System;
using Unity.Netcode;
using UnityEngine;
using NetworkLoadSceneMode = UnityEngine.SceneManagement.LoadSceneMode;

namespace Runtime
{
    public sealed class LobbyPresenter
    {
        private LobbyView _view;
        public LobbyPresenter(LobbyView view)
        {
            _view = view;

            _view.CreateButton.clicked += OnCreateButtonClicked;
            _view.JoinButton.clicked += OnJoinButtonClicked;
        }

        private void OnCreateButtonClicked()
        {
            var networkManager = NetworkManager.Singleton;

            networkManager.StartHost();

            networkManager.SceneManager.OnLoad += OnPlayerLoad;

            networkManager.SceneManager.LoadScene("Game", NetworkLoadSceneMode.Single);
        }

        private void OnPlayerLoad(ulong clientId, string sceneName, NetworkLoadSceneMode loadSceneMode, AsyncOperation asyncOperation)
        {
            Debug.Log("On Player Load");

            var networkManager = NetworkManager.Singleton;

            networkManager.SceneManager.OnLoad -= OnPlayerLoad;

            if (networkManager.LocalClientId == clientId)
                LoadingView.ShowLoadingScreen(asyncOperation);
        }

        private void OnJoinButtonClicked()
        {
            NetworkManager.Singleton.StartClient();

            NetworkManager.Singleton.SceneManager.OnLoad += OnPlayerLoad;
        }
    }
}
