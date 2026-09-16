using UnityEngine;
using UnityEngine.Events;
namespace MergeStudio.Events {
    public sealed class VoidEventListener : MonoBehaviour {
        [SerializeField] private VoidEventChannelSO _channel;
        [SerializeField] private UnityEvent _response = new UnityEvent();
        private VoidEventChannelSO _subscribed;
        private void OnEnable() { _subscribed = _channel; if (_subscribed != null) _subscribed.OnEventRaised += Respond; }
        private void OnDisable() { if (_subscribed != null) _subscribed.OnEventRaised -= Respond; _subscribed = null; }
        private void Respond() => _response.Invoke();
    }
}
