using UnityEngine;

namespace Runtime
{
    public sealed class PlayerView : MonoBehaviour
    {
        [field: SerializeField] internal PlayerConfiguration Configuration { get; set; } = new();
        [field: SerializeField] internal Transform ModelTransform { get; set; }
        [field: SerializeField] internal Rigidbody2D Rigidbody { get; set; }
        [field: SerializeField] internal Collider2D Collider { get; set; }
        [field: SerializeField] internal Vector3 MoveDir { get; set; }
        internal Vector3 InitialScale { get; private set; }
        internal Vector3 TargetScale { get; set; }
        [field: SerializeField] private InputWrapper InputWrapper { get; set; }
        private PlayerPresenter _presenter;

        private void Start()
        {
            InitialScale = TargetScale = ModelTransform.localScale;
            _presenter = new(this, InputWrapper);
        }

        private void Update()
        {
            transform.position += MoveDir;

            ModelTransform.localScale = Vector3.LerpUnclamped(ModelTransform.localScale,
                TargetScale,
                Configuration.RotationSpeed);
        }

        private void LateUpdate() => ResetValues();

        private void ResetValues()
        {
            MoveDir = Vector3.zero;
        }
    }
}
