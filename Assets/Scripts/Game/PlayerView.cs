using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Runtime
{
    public class PlayerView : MonoBehaviour
    {
        private Vector3 _moveDir = Vector3.zero;

        public void Move(Vector3 dir) => _moveDir = dir;

        private void Start()
        {

        }
    }
}
