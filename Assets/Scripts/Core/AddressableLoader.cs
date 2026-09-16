using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
namespace MergeStudio.Core {
    public sealed class AddressableLease<T> : IDisposable {
        public T Value { get; }
        private Action _release;
        internal AddressableLease(T value, Action release) { Value = value; _release = release; }
        public void Dispose() { var release = _release; _release = null; release?.Invoke(); }
    }
    // Use on the Unity main thread. Owner disposal also releases abandoned leases.
    public sealed class AddressableLoader : IDisposable {
        private readonly HashSet<IDisposable> _leases = new HashSet<IDisposable>();
        private bool _disposed;
        public async Task<AddressableLease<T>> LoadAssetAsync<T>(object key) where T : UnityEngine.Object {
            Check(); var handle = Addressables.LoadAssetAsync<T>(key);
            try { await handle.Task; if (handle.Status != AsyncOperationStatus.Succeeded) throw handle.OperationException; Check(); }
            catch { if (handle.IsValid()) Addressables.Release(handle); throw; }
            return Track(handle.Result, () => Addressables.Release(handle));
        }
        public async Task<AddressableLease<GameObject>> InstantiateAsync(object key, Transform parent = null) {
            Check(); var handle = Addressables.InstantiateAsync(key, parent);
            try { await handle.Task; if (handle.Status != AsyncOperationStatus.Succeeded) throw handle.OperationException; Check(); }
            catch { if (handle.IsValid()) { if (handle.Status == AsyncOperationStatus.Succeeded) Addressables.ReleaseInstance(handle); else Addressables.Release(handle); } throw; }
            return Track(handle.Result, () => Addressables.ReleaseInstance(handle));
        }
        private AddressableLease<T> Track<T>(T value, Action release) {
            AddressableLease<T> lease = null;
            lease = new AddressableLease<T>(value, () => { _leases.Remove(lease); release(); }); _leases.Add(lease); return lease;
        }
        private void Check() { if (_disposed) throw new ObjectDisposedException(nameof(AddressableLoader)); }
        public void Dispose() { if (_disposed) return; _disposed = true; foreach (var lease in new List<IDisposable>(_leases)) lease.Dispose(); _leases.Clear(); }
    }
}
