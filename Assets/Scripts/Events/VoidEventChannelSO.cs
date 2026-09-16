using UnityEngine;
using UnityEngine.Events;
namespace MergeStudio.Events {
    [CreateAssetMenu(menuName = "MergeStudio/Events/Void")]
    public sealed class VoidEventChannelSO : ScriptableObject {
        public event UnityAction OnEventRaised;
        public void RaiseEvent() => OnEventRaised?.Invoke();
    }
}
