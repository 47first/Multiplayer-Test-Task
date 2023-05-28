using Unity.Netcode;
using UnityEngine;

namespace Runtime
{
    public sealed class PlayerView : NetworkBehaviour
    {
        // Network
        internal NetworkVariable<Vector3> TargetRotation { get; set; }

        //Local
        [field: SerializeField] internal PlayerConfiguration Configuration { get; set; } = new();
        [field: SerializeField] internal Transform ModelTransform { get; set; }
        [field: SerializeField] internal Rigidbody2D Rigidbody { get; set; }
        [field: SerializeField] internal Collider2D Collider { get; set; }
        [field: SerializeField] internal ProjectileShooter Shooter { get; set; }
        internal Vector3 InitialRotation { get; private set; }
        internal Vector3 MoveDir { get; set; }

        private PlayerPresenter _presenter;

        private void Awake()
        {
            TargetRotation = new(readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Owner);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner == false)
                return;

            InitialRotation = TargetRotation.Value = ModelTransform.localEulerAngles;
            _presenter = new(this);
        }

        private void Update()
        {
            if (IsOwner)
            {
                _presenter.Update();

                transform.position += MoveDir;
            }
                
            ModelTransform.localEulerAngles = Vector3.LerpUnclamped(ModelTransform.localEulerAngles,
                TargetRotation.Value,
                Configuration.RotationSpeed);
        }

        private void LateUpdate()
        {
            if (IsOwner)
                ResetValues();
        }

        private void ResetValues()
        {
            MoveDir = Vector3.zero;
        }
    }
}
