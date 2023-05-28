using Unity.Netcode;
using UnityEngine;

namespace Runtime
{
    public sealed class ProjectileShooter: NetworkBehaviour
    {
        private const float bulletLifeTime = 1;
        [field: SerializeField] internal float Delay { get; set; }
        [field: SerializeField] internal float BulletSpeed { get; set; }

        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _shootDirection;
        private float _lastTimeShoot = 0;

        public void TryShoot()
        {
            if (IsOwner && CanShoot())
                ShootServerRpc();
        }

        public bool CanShoot() => Time.realtimeSinceStartup > _lastTimeShoot + Delay;

        [ServerRpc]
        private void ShootServerRpc()
        {
            if (IsServer == false)
                return;

            _lastTimeShoot = Time.realtimeSinceStartup;

            var projectile = GameObject.Instantiate(_projectilePrefab);

            projectile.transform.position = _shootDirection.position;
            projectile.transform.rotation = _shootDirection.rotation;
            projectile.Configure(BulletSpeed, bulletLifeTime);

            projectile.NetworkObject.SpawnWithOwnership(OwnerClientId);
        }
    }
}
