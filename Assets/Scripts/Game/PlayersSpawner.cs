using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public sealed class PlayersSpawner: MonoBehaviour
    {
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private List<Transform> _spawnPoints;

        public PlayerView Spawn(ulong id)
        {
            var player = Instantiate(_playerViewPrefab);

            player.transform.position = _spawnPoints[0].position;

            player.NetworkObject.SpawnWithOwnership(id);

            return player;
        }
    }
}
