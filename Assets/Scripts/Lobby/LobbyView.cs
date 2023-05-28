using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime
{
    public sealed class LobbyView: MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        internal TextField KeyField { get; private set; }
        internal Button JoinButton { get; private set; }
        internal Button CreateButton { get; private set; }

        private LobbyPresenter _presenter;

        private void Start()
        {
            KeyField = _uiDocument.rootVisualElement.Q<TextField>("KeyTextField");
            JoinButton = _uiDocument.rootVisualElement.Q<Button>("JoinButton");
            CreateButton = _uiDocument.rootVisualElement.Q<Button>("CreateNewButton");

            _presenter = new(this);
        }
    }
}
