using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Runtime
{
    public class WinnerPopupPresenter
    {
        private WinnerPopupView _view;
        public WinnerPopupPresenter(WinnerPopupView view)
        {
            _view = view;
        }

        public void ShowPopup(string playerName, int coinsAmount)
        {
            Debug.Log("Show Popup");

            _view.Popup.visible = true;
            _view.WinnerLabel.text = string.Format(_view.WinnerLabel.text, playerName);
            _view.CoinsAmountLabel.text = string.Format(_view.CoinsAmountLabel.text, coinsAmount);

            if (NetworkManager.Singleton.IsHost)
            {
                _view.RestartButton.style.display = DisplayStyle.Flex;
                _view.RestartButton.clicked += RestartGame;
            }

            else
                _view.RestartButton.style.display = DisplayStyle.None;
        }

        private void RestartGame()
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
}
