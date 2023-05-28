using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime
{
    public class LobbyView: MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        private TextField _textField;
        private Button _joinButton;
        private Button _createButton;

        private void Start()
        {
            _textField = _uiDocument.rootVisualElement.Q<TextField>("KeyTextField");
            _joinButton = _uiDocument.rootVisualElement.Q<Button>("JoinButton");
            _createButton = _uiDocument.rootVisualElement.Q<Button>("CreateNewButton");
        }
    }
}
