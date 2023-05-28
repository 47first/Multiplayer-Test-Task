using System;
using Unity.Netcode;
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

            networkManager.SceneManager.LoadScene("Game", NetworkLoadSceneMode.Single);
        }

        private void OnJoinButtonClicked()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
