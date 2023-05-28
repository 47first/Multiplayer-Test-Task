using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public class GamePresenter
    {
        public GamePresenter()
        {
            var networkManager = NetworkManager.Singleton;

            networkManager.SceneManager.OnLoadComplete += OnPlayerConnected;

            if (networkManager.IsServer)
            {
                
            }
        }

        private void OnPlayerConnected(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            Debug.Log("On Player Connected");
        }
    }
}
