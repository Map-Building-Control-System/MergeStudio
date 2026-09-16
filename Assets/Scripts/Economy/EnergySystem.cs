using System;
namespace MergeStudio.Economy {
    public sealed class EnergySystem {
        private readonly int _max, _seconds;
        public int Current { get; private set; }
        public long Timestamp { get; private set; }
        public EnergySystem(int current, long timestamp, int max, int seconds) {
            if (max < 1 || seconds < 1) throw new ArgumentOutOfRangeException();
            _max = max; _seconds = seconds; Current = Math.Max(0, Math.Min(max, current)); Timestamp = timestamp;
        }
        public void Tick(long now) {
            if (now < Timestamp) { Timestamp = now; return; }
            if (Current >= _max) { Timestamp = now; return; }
            long gained = Math.Min(_max - Current, (now - Timestamp) / _seconds);
            Current += (int)gained; Timestamp = Current == _max ? now : Timestamp + gained * _seconds;
        }
        public bool TrySpend(int amount, long now) { if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount)); Tick(now); if (Current < amount) return false; Current -= amount; return true; }
        public void Add(int amount) { if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount)); Current = (int)Math.Min(_max, (long)Current + amount); }
    }
}
