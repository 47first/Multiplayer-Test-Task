using System;
using System.Net.Sockets;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
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

        private async void OnCreateButtonClicked()
        {
            SetNickname();

            ConfigureConnection(GetAddress());

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

        private async void OnJoinButtonClicked()
        {
            ConfigureConnection(_view.KeyField.text);

            SetNickname();

            NetworkManager.Singleton.StartClient();

            NetworkManager.Singleton.SceneManager.OnLoad += OnPlayerLoad;
        }

        private void SetNickname()
        {
            ConnectionDataRecorder.SetData(_view.NicknameField.text);
        }

        private void ConfigureConnection(string address)
        {
            var networkManager = NetworkManager.Singleton;

            var transport = networkManager.NetworkConfig.NetworkTransport as UnityTransport;

            transport.ConnectionData.Address = address;
        }

        private string GetAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
