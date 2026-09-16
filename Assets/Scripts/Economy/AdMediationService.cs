using System;
namespace MergeStudio.Economy {
    public sealed class AdMediationService {
        public bool IsReady => false;
        // TODO: SDK entegrasyonu; success only after verified reward callback.
        public void ShowRewarded(Action onSuccess, Action onFail) => onFail?.Invoke();
        public void ShowInterstitial(Action onClosed, Action onFail) => onFail?.Invoke();
    }
}
