using System;

namespace Runtime
{
    public interface IInputWrapper
    {
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnJump;
    }
}
