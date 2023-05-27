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

        public void MoveLeft() => Move(Vector3.left * _view.Configuration.MoveSpeed);
        public void MoveRight() => Move(Vector3.right * _view.Configuration.MoveSpeed);
        public void Jump()
        {
            if (IsOnGround() == false)
                return;

            _view.Rigidbody.velocity = Vector2.up * _view.Configuration.JumpForce;
        }

        public bool IsOnGround()
        {
            // Collider size / 1.99 - Collider radius with a little indent
            var from = _view.transform.position + Vector3.down * (_view.Collider.bounds.size.y / 1.99f);

            Debug.DrawLine(from, from + (Vector3.down * 0.1f));

            return Physics2D.Raycast(from, Vector2.down, 0.1f);
        }

        private void Move(Vector3 dir)
        {
            _view.MoveDir += dir * Time.deltaTime;

            if (dir.x > 0)
                _view.TargetScale = _view.InitialScale;

            else
            {
                var newScale = _view.InitialScale;

                newScale.x *= -1;

                _view.TargetScale = newScale;
            }
        }

        private void ConfigureInput(InputWrapper inputWrapper)
        {
            inputWrapper.OnMoveRight += MoveRight;
            inputWrapper.OnMoveLeft += MoveLeft;
            inputWrapper.OnJump += Jump;
        }
    }
}
