using System;
using UnityEngine;

namespace Runtime
{
    public sealed class InputWrapper: MonoBehaviour
    {
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnJump;

        private void Update()
        {
            if (Input.anyKey == false)
                return;

            if (Input.GetKey(KeyCode.D))
                OnMoveRight?.Invoke();

            if (Input.GetKey(KeyCode.A))
                OnMoveLeft?.Invoke();
        }
    }
}
