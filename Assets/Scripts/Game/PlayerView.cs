using UnityEngine;

namespace Runtime
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] internal Vector3 MoveDir { get; set; }
        [field: SerializeField] private InputWrapper InputWrapper { get; set; }
        private PlayerPresenter _presenter;

        private void Start()
        {
            _presenter = new(this, InputWrapper);
        }

        private void Update()
        {
            transform.position += MoveDir;
        }

        private void LateUpdate() => ResetValues();

        private void ResetValues()
        {
            MoveDir = Vector3.zero;
        }
    }
}
