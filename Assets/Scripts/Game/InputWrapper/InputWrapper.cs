using System;
using UnityEngine;

namespace Runtime
{
    public sealed class InputWrapper: MonoBehaviour, IInputWrapper
    {
        public static IInputWrapper Singleton { get; private set; }

        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnJump;

        private void Awake()
        {
            Singleton = this;
        }

        private void Update()
        {
            if (Input.anyKey == false)
                return;

            if (Input.GetKey(KeyCode.D))
                OnMoveRight?.Invoke();

            if (Input.GetKey(KeyCode.A))
                OnMoveLeft?.Invoke();

            if (Input.GetKeyDown(KeyCode.Space))
                OnJump?.Invoke();
        }
    }
}
