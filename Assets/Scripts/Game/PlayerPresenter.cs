using UnityEngine;

namespace Runtime
{
    public class PlayerPresenter
    {
        private PlayerView _view;
        private InputWrapper _inputWrapper;
        public PlayerPresenter(PlayerView view, InputWrapper inputWrapper)
        {
            _view = view;
            _inputWrapper = inputWrapper;

            ConfigureInput(inputWrapper);
        }

        public void Update()
        {

        }

        public void MoveLeft() => Move(Vector3.left);
        public void MoveRight() => Move(Vector3.right);
        public void Jump() => _view.Rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        private void Move(Vector3 dir) => _view.MoveDir += dir * Time.deltaTime;

        private void ConfigureInput(InputWrapper inputWrapper)
        {
            inputWrapper.OnMoveRight += MoveRight;
            inputWrapper.OnMoveLeft += MoveLeft;
            inputWrapper.OnJump += Jump;
        }
    }
}
