using UnityEngine;
using UnityEngine.Events;
namespace MergeStudio.Events
{
    public abstract class EventChannelSO<T> : ScriptableObject
    {
        public event UnityAction<T> OnEventRaised;
        public void RaiseEvent(T value) => OnEventRaised?.Invoke(value);
    }
}
