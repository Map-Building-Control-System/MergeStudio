using System.Collections.Generic;
using UnityEngine;
namespace MergeStudio.Analytics { public sealed class FirebaseAnalyticsService : IAnalyticsService {
    public void LogEvent(string name, Dictionary<string, object> parameters) {
        // TODO: SDK entegrasyonu; consent and event schema must be configured before sending.
        if (string.IsNullOrWhiteSpace(name)) throw new System.ArgumentException("Event name required.", nameof(name));
        Debug.Log("[Analytics offline] " + name);
    }
} }
