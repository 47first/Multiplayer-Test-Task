using Unity.Netcode;
using UnityEngine;

namespace Runtime
{
    public sealed class Projectile: NetworkBehaviour, IInteractable
    {
        private float _speed;
        private float _damage;

        public void Configure(float speed, float damage, float lifeTime)
        {
            InteractionHost.Singleton.Register(gameObject, this);

            _damage = damage;
            _speed = speed;

            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            if (IsServer)
                MoveForward();
        }

        private void MoveForward()
        {
            transform.position = transform.TransformPoint(_speed * Time.deltaTime * Vector3.right);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Debug.Log("TriggerEnter");

            InteractionHost.Singleton.SendInteraction(this, collider.gameObject);

            Destroy(gameObject);
        }

        public void Interact(object sender)
        {
            Debug.Log("OnInteract");

            if (sender is PlayerView playerView)
            {
                playerView.Health -= _damage;
                Debug.Log("PlayerInteract");
            }
        }
    }
}
