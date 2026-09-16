using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using MergeStudio.Events;
namespace MergeStudio.Tests
{
    public sealed class EventListenerTests
    {
        [UnityTest] public IEnumerator DisabledListenerDoesNotReceiveEvents()
        {
            var go = new GameObject("Listener test"); go.SetActive(false);
            var channel = ScriptableObject.CreateInstance<IntEventChannelSO>();
            try
            {
                var listener = go.AddComponent<IntEventListener>(); var type = typeof(EventListener<int>);
                type.GetField("_channel", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(listener, channel);
                var response = (UnityEvent<int>)type.GetField("_response", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(listener);
                int received = 0; response.AddListener(value => received += value);
                go.SetActive(true); yield return null; channel.RaiseEvent(2);
                go.SetActive(false); channel.RaiseEvent(9); Assert.AreEqual(2, received);
                go.SetActive(true); channel.RaiseEvent(3); Assert.AreEqual(5, received);
            }
            finally { Object.Destroy(go); Object.Destroy(channel); }
        }
    }
}
