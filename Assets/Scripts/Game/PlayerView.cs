using Unity.Netcode;
using UnityEngine;

namespace Runtime
{
    public sealed class PlayerView : NetworkBehaviour
    {
        // Network
        internal NetworkVariable<Vector3> TargetScale { get; set; }

        //Local
        [field: SerializeField] internal PlayerConfiguration Configuration { get; set; } = new();
        [field: SerializeField] internal Transform ModelTransform { get; set; }
        [field: SerializeField] internal Rigidbody2D Rigidbody { get; set; }
        [field: SerializeField] internal Collider2D Collider { get; set; }
        [field: SerializeField] internal ProjectileShooter Shooter { get; set; }
        internal Vector3 InitialScale { get; private set; }
        internal Vector3 MoveDir { get; set; }

        private PlayerPresenter _presenter;

        private void Awake()
        {
            TargetScale = new(readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Owner);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner == false)
                return;

            Shooter.Delay = Configuration.ShooterDelay;
            InitialScale = TargetScale.Value = ModelTransform.localScale;
            _presenter = new(this);
        }

        private void Update()
        {
            if (IsOwner)
            {
                _presenter.Update();

                transform.position += MoveDir;
            }
                
            ModelTransform.localScale = Vector3.LerpUnclamped(ModelTransform.localScale,
                TargetScale.Value,
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
