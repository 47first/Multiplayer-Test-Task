using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime
{
    public sealed class GameView: NetworkBehaviour
    {
        public static GameView Singleton { get; private set; }

        internal NetworkVariable<bool> IsGameStarted { get; private set; }
        [field: SerializeField] internal UIDocument UIDocument { get; private set; }
        [field: SerializeField] internal PlayersSpawner PlayersSpawner { get; private set; }
        [field: SerializeField] internal WinnerPopupView WinnerPopup { get; private set; }

        private GamePresenter _presenter;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            UIDocument.rootVisualElement.Q<Label>("ServerKeyLabel").text =
                (NetworkManager.NetworkConfig.NetworkTransport as UnityTransport).ConnectionData.Address;

            if (IsServer)
                _presenter = new(this);
        }

        private void Awake()
        {
            IsGameStarted = new(value: false, readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Server);

            Singleton = this;
        }
    }
}
