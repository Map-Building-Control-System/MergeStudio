using UnityEngine;
using UnityEngine.SceneManagement;
using MergeStudio.Events;
namespace MergeStudio.Core {
    public sealed class SceneLoader : MonoBehaviour {
        [SerializeField] private StringEventChannelSO _request;
        private bool _loading;
        private void OnEnable() { if (_request != null) _request.OnEventRaised += Load; }
        private void OnDisable() { if (_request != null) _request.OnEventRaised -= Load; }
        public void Load(string scene) {
            if (_loading || !Application.CanStreamedLevelBeLoaded(scene)) return;
            _loading = true; SceneManager.LoadSceneAsync(scene).completed += _ => _loading = false;
        }
    }
}
