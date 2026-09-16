using NUnit.Framework;
using UnityEngine;
using MergeStudio.Core;
using MergeStudio.Events;
using MergeStudio.UI;
namespace MergeStudio.Tests
{
    public sealed class InfrastructureTests
    {
        [Test] public void ChannelsUnsubscribe()
        {
            var channel = ScriptableObject.CreateInstance<IntEventChannelSO>(); int count = 0;
            UnityEngine.Events.UnityAction<int> callback = value => count += value;
            try { channel.OnEventRaised += callback; channel.RaiseEvent(2); channel.OnEventRaised -= callback; channel.RaiseEvent(4); Assert.AreEqual(2, count); }
            finally { Object.DestroyImmediate(channel); }
        }
        [TestCase(320, 568)] [TestCase(390, 844)] [TestCase(412, 915)] [TestCase(1080, 2400)]
        public void SafeAreaNormalizesTargetSizes(int width, int height)
        {
            var normalized = SafeArea.Normalize(new Rect(0, 20, width, height - 40), width, height);
            Assert.AreEqual(0, normalized.x); Assert.AreEqual(1, normalized.width); Assert.That(normalized.y, Is.GreaterThan(0)); Assert.That(normalized.yMax, Is.LessThan(1));
        }
        [Test] public void LocatorRejectsMissingOrDuplicateService()
        {
            var locator = new ServiceLocator(); Assert.Throws<System.InvalidOperationException>(() => locator.Get<object>());
            var service = new object(); locator.Register(service); Assert.AreSame(service, locator.Get<object>());
            Assert.Throws<System.ArgumentException>(() => locator.Register(new object()));
        }
        [Test] public void DisposedLoaderRejectsNewLoads()
        {
            var loader = new AddressableLoader(); loader.Dispose(); loader.Dispose();
            Assert.ThrowsAsync<System.ObjectDisposedException>(async () => await loader.LoadAssetAsync<GameObject>("missing"));
        }
    }
}
