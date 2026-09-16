using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using MergeStudio.Network;
using MergeStudio.Persistence;
namespace MergeStudio.Tests
{
    public sealed class NetworkTests
    {
        [Test] public async Task LocalProviderRoundTrip()
        {
            string directory = Path.Combine(Path.GetTempPath(), "MergeStudioTests", Guid.NewGuid().ToString());
            try
            {
                var sync = new SaveCloudSync(new LocalOnlySaveProvider(new SaveSystem(directory)));
                await sync.PushAsync(new SaveData { Gold = 73 }); Assert.AreEqual(73, (await sync.PullAsync()).Gold);
            }
            finally { Directory.Delete(directory, true); }
        }
        [Test] public void OfflineApiFailsExplicitly() => Assert.ThrowsAsync<NotSupportedException>(async () => await new OfflineApiClient().GetAsync("profile"));
        [Test] public void CancellationIsRespected() => Assert.Throws<OperationCanceledException>(() => new OfflineApiClient().GetAsync("profile", new CancellationToken(true)));
    }
}
