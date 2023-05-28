using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime
{
    public sealed class PlayerView : NetworkBehaviour
    {
        // View
        [SerializeField] private TextMeshProUGUI _nameLabel;
        [SerializeField] private TextMeshProUGUI _coinsLabel;
        [SerializeField] private Slider _healthSlider;

        // Network
        internal NetworkVariable<Vector3> TargetRotation { get; set; }
        internal NetworkVariable<int> CoinsAmount { get; set; }
        internal NetworkVariable<float> Health { get; set; }

        //Local
        [field: SerializeField] internal PlayerConfiguration Configuration { get; set; } = new();
        [field: SerializeField] internal Transform ModelTransform { get; set; }
        [field: SerializeField] internal Rigidbody2D Rigidbody { get; set; }
        [field: SerializeField] internal Collider2D Collider { get; set; }
        [field: SerializeField] internal ProjectileShooter Shooter { get; set; }
        internal Vector3 InitialRotation { get; private set; }
        internal Vector3 MoveDir { get; set; }

        // Called only on server side
        internal event Action OnDeath;

        private PlayerPresenter _presenter;

        // If method called by not the server, Exception will be throwed
        public void ReceiveDamage(float damage)
        {
            Health.Value -= damage;

            if (Health.Value <= 0)
                NetworkObject.Despawn();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            BindView();

            if (IsServer)
                Health.Value = Configuration.InitialHealth;

            if (IsOwner)
            {
                InitialRotation = TargetRotation.Value = ModelTransform.localEulerAngles;
                _presenter = new(this);
            }
        }

        private void BindView()
        {
            _nameLabel.text = "Cool Player";

            _healthSlider.maxValue = Configuration.InitialHealth;
            Health.OnValueChanged += (prev, cur) => _healthSlider.value = cur;

            CoinsAmount.OnValueChanged += (prev, cur) => _coinsLabel.text = $"x{cur}";
        }

        private void Awake()
        {
            TargetRotation = new(readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Owner);

            Health = new(readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Server);

            CoinsAmount = new(readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Server);
        }

        private void Update()
        {
            if (GameView.Singleton.IsGameStarted.Value == false)
                return;

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

        private void OnDestroy() => OnDeath?.Invoke();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            InteractionHost.Singleton.SendInteraction(this, collider.gameObject);
        }
    }
}
