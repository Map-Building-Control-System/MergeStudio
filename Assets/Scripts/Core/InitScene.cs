using UnityEngine;
using UnityEngine.SceneManagement;
namespace MergeStudio.Core
{
    public sealed class InitScene : MonoBehaviour
    {
        private void Start() => SceneManager.LoadSceneAsync("MainMenu");
    }
}
