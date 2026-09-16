using UnityEngine;
using UnityEngine.Events;
namespace MergeStudio.Events {
    public abstract class EventListener<T> : MonoBehaviour {
        [SerializeField] private EventChannelSO<T> _channel;
        [SerializeField] private UnityEvent<T> _response = new UnityEvent<T>();
        private EventChannelSO<T> _subscribed;
        protected virtual void OnEnable() { _subscribed = _channel; if (_subscribed != null) _subscribed.OnEventRaised += Respond; }
        protected virtual void OnDisable() { if (_subscribed != null) _subscribed.OnEventRaised -= Respond; _subscribed = null; }
        private void Respond(T value) => _response.Invoke(value);
    }
}
