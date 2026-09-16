using System;
using System.Collections.Generic;
namespace MergeStudio.Core {
    // Composition-root infrastructure only; gameplay messaging uses event channels.
    public sealed class ServiceLocator {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        public void Register<T>(T service) where T : class { if (service == null) throw new ArgumentNullException(nameof(service)); _services.Add(typeof(T), service); }
        public T Get<T>() where T : class => _services.TryGetValue(typeof(T), out var value) ? (T)value : throw new InvalidOperationException(typeof(T).Name + " not registered");
        public void Clear() => _services.Clear();
    }
}
