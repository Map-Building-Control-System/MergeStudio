using System.Threading;
using System.Threading.Tasks;
namespace MergeStudio.Persistence {
    public interface ICloudSaveProvider {
        Task UploadAsync(SaveData data, CancellationToken cancellationToken = default);
        Task<SaveData> DownloadAsync(CancellationToken cancellationToken = default);
    }
    // TODO: Firebase/PlayFab provider: authentication, revisions and conflict resolution.
    public sealed class LocalOnlySaveProvider : ICloudSaveProvider {
        private readonly SaveSystem _save;
        public LocalOnlySaveProvider(SaveSystem save) => _save = save;
        public Task UploadAsync(SaveData data, CancellationToken cancellationToken = default) { cancellationToken.ThrowIfCancellationRequested(); _save.Save(data); return Task.CompletedTask; }
        public Task<SaveData> DownloadAsync(CancellationToken cancellationToken = default) { cancellationToken.ThrowIfCancellationRequested(); return Task.FromResult(_save.Load()); }
    }
}
