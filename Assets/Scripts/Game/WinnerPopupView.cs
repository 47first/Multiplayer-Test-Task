using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime
{
    public class WinnerPopupView: NetworkBehaviour
    {
        [field: SerializeField] internal UIDocument PlayerPopup { get; private set; }
        internal VisualElement Popup { get; private set; }
        internal Label WinnerLabel { get; private set; }
        internal Label CoinsAmountLabel { get; private set; }
        internal Button RestartButton { get; private set; }

        private WinnerPopupPresenter _presenter;

        [ClientRpc]
        public void ShowClientRpc(string playerName, int coinsAmount) => _presenter.ShowPopup(playerName, coinsAmount);

        private void Start()
        {
            Popup = PlayerPopup.rootVisualElement.Q<VisualElement>("Popup");
            WinnerLabel = PlayerPopup.rootVisualElement.Q<Label>("WinnerLabel");
            CoinsAmountLabel = PlayerPopup.rootVisualElement.Q<Label>("CoinsAmountLabel");
            RestartButton = PlayerPopup.rootVisualElement.Q<Button>("RestartButton");

            _presenter = new(this);

            Popup.visible = false;
        }
    }
}
