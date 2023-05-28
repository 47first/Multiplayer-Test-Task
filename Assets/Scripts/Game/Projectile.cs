using Unity.Netcode;
using UnityEngine;

namespace Runtime
{
    public sealed class Projectile: NetworkBehaviour, IInteractable
    {
        public float Speed { get; private set; }
        public float Damage { get; private set; }

        public void Configure(float speed, float damage, float lifeTime)
        {
            InteractionHost.Singleton.Register(gameObject, this);

            Damage = damage;
            Speed = speed;

            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            if (IsServer)
                MoveForward();
        }

        private void MoveForward()
        {
            transform.position = transform.TransformPoint(Speed * Time.deltaTime * Vector3.right);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (IsServer == false)
                return;

            InteractionHost.Singleton.SendInteraction(this, collider.gameObject);
        }

        public void Interact(object sender)
        {
            if (sender is PlayerView playerView &&
                playerView.OwnerClientId != OwnerClientId)
            {
                playerView.ReceiveDamage(Damage);

                Destroy(gameObject);
            }
        }
    }
}
