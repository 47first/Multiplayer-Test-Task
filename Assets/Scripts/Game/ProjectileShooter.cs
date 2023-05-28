using Unity.Netcode;
using UnityEngine;

namespace Runtime
{
    public sealed class ProjectileShooter: NetworkBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _shootDirection;
        private float _lastTimeShoot = 0;
        internal float Delay { get; set; }

        public void TryShoot()
        {
            if (IsOwner && CanShoot())
                ShootServerRpc();
        }

        public bool CanShoot() => Time.realtimeSinceStartup > _lastTimeShoot + Delay;

        [ServerRpc]
        private void ShootServerRpc()
        {
            _lastTimeShoot = Time.realtimeSinceStartup;

            if (IsServer == false)
                return;

            var projectile = GameObject.Instantiate(_projectilePrefab);

            projectile.transform.position = _shootDirection.position;
            projectile.transform.rotation = _shootDirection.rotation;

            projectile.NetworkObject.SpawnWithOwnership(OwnerClientId);
        }
    }
}
