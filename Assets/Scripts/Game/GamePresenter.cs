using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public sealed class GamePresenter
    {
        private List<PlayerView> _players;
        private GameView _view;
        public GamePresenter(GameView view)
        {
            _view = view;

            var networkManager = NetworkManager.Singleton;

            _players = new List<PlayerView>();
            networkManager.SceneManager.OnLoadComplete += OnPlayerConnected;
        }

        private void OnPlayerConnected(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            if (_view.IsGameStarted.Value)
                return;

            var newPlayer = _view.PlayersSpawner.Spawn(clientId);

            newPlayer.OnDeath += OnPlayerDeath;

            _players.Add(newPlayer);

            CheckOnStartGame();
        }

        private void OnPlayerDeath()
        {
            var alivePlayers = GetAlivePlayers();

            if (alivePlayers.Count() <= 1)
            {
                var alivePlayer = alivePlayers.First();

                _view.WinnerPopup.ShowClientRpc(alivePlayer.Nickname.Value.ToString(),
                    alivePlayer.CoinsAmount.Value);
            }
        }

        private void CheckOnStartGame()
        {
            if (GetAlivePlayers().Count() > 1)
                _view.IsGameStarted.Value = true;
        }

        private IEnumerable<PlayerView> GetAlivePlayers() => _players.Where(player => player is not null && player.Health.Value > 0);
    }
}
