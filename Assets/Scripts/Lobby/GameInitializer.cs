using Unity.Netcode;
using UnityEngine;
using NetworkLoadSceneMode = UnityEngine.SceneManagement.LoadSceneMode;

namespace Runtime
{
    public class GameInitializer: MonoBehaviour
    {
        public void CreateGame()
        {
            var networkManager = NetworkManager.Singleton;

            networkManager.StartHost();

            networkManager.SceneManager.LoadScene("Game", NetworkLoadSceneMode.Single);
        }
    }
}
