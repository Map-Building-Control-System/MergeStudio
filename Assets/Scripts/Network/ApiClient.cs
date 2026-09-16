using System;
using System.Threading;
using System.Threading.Tasks;
namespace MergeStudio.Network {
    public interface IApiClient { Task<string> GetAsync(string route, CancellationToken token = default); }
    public sealed class OfflineApiClient : IApiClient {
        public Task<string> GetAsync(string route, CancellationToken token = default) {
            token.ThrowIfCancellationRequested();
            return Task.FromException<string>(new NotSupportedException("No backend configured."));
        }
    }
}
