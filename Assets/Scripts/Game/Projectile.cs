using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Runtime
{
    public sealed class Projectile: NetworkBehaviour
    {
        private float _speed;

        public void Configure(float speed, float lifeTime)
        {
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

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("CollisionEnter");
        }
    }
}
