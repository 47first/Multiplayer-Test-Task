using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public sealed class LoadingView: MonoBehaviour
    {
        public static AsyncOperation AsyncOperation { get; set; }

        public static void ShowLoadingScreen(AsyncOperation asyncOperation)
        {
            AsyncOperation = asyncOperation;

            SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
        }

        private IEnumerator Start()
        {
            yield return UpdateProgressBar();
        }

        private IEnumerator UpdateProgressBar()
        {
            AsyncOperation.allowSceneActivation = false;

            while (AsyncOperation.isDone == false)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1);

            SceneManager.UnloadScene(SceneManager.GetSceneByName("Loading"));

            AsyncOperation.allowSceneActivation = true;

            AsyncOperation = null;
        }
    }
}
