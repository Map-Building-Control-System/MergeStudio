using System.Threading;
using System.Threading.Tasks;
using MergeStudio.Persistence;
namespace MergeStudio.Network {
    public sealed class SaveCloudSync {
        private readonly ICloudSaveProvider _provider;
        public SaveCloudSync(ICloudSaveProvider provider) => _provider = provider;
        public Task PushAsync(SaveData data, CancellationToken token = default) => _provider.UploadAsync(data, token);
        public Task<SaveData> PullAsync(CancellationToken token = default) => _provider.DownloadAsync(token);
    }
}
