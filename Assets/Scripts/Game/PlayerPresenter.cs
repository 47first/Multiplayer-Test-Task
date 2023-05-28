using UnityEngine;

namespace Runtime
{
    public class PlayerPresenter
    {
        private PlayerView _view;
        public PlayerPresenter(PlayerView view)
        {
            _view = view;

            ConfigureInput();
        }

        public void Update() => _view.Shooter.TryShoot();

        public void MoveLeft() => Move(Vector3.left * _view.Configuration.MoveSpeed);

        public void MoveRight() => Move(Vector3.right * _view.Configuration.MoveSpeed);

        public void Jump()
        {
            if (IsOnGround())
                _view.Rigidbody.velocity = Vector2.up * _view.Configuration.JumpForce;
        }

        private void Move(Vector3 dir)
        {
            _view.MoveDir += dir * Time.deltaTime;

            if (dir.x > 0)
                _view.TargetScale.Value = _view.InitialScale;

            else
            {
                var newScale = _view.InitialScale;

                newScale.x *= -1;

                _view.TargetScale.Value = newScale;
            }
        }

        private bool IsOnGround()
        {
            // Collider size / 1.99 - Collider radius with a little indent
            var from = _view.transform.position + Vector3.down * (_view.Collider.bounds.size.y / 1.99f);

            return Physics2D.Raycast(from, Vector2.down, 0.1f);
        }

        private void ConfigureInput()
        {
            var inputWrapper = InputWrapper.Singleton;

            inputWrapper.OnMoveRight += MoveRight;
            inputWrapper.OnMoveLeft += MoveLeft;
            inputWrapper.OnJump += Jump;
        }
    }
}
