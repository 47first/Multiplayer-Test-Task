using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System;

namespace Runtime
{
    public sealed class PlayersSpawner: MonoBehaviour
    {
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private List<Transform> _spawnPoints;

        public PlayerView Spawn(ulong id)
        {
            var dataFound = ConnectionDataContainer.Singleton.TryGetData(id, out var connectionData);

            if (dataFound == false)
                throw new Exception($"There are no data for {id}");
            
            var player = Instantiate(_playerViewPrefab);

            player.transform.position = _spawnPoints[0].position;
            player.Nickname.Value = connectionData;

            player.NetworkObject.SpawnWithOwnership(id, true);

            return player;
        }
    }
}
