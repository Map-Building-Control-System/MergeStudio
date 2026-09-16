using System.Collections.Generic;
namespace MergeStudio.Analytics { public interface IAnalyticsService { void LogEvent(string name, Dictionary<string, object> parameters); } }
