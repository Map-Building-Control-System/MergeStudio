using System;
namespace MergeStudio.Economy {
    public sealed class Currency {
        public int Balance { get; private set; }
        public Currency(int initial) { if (initial < 0) throw new ArgumentOutOfRangeException(nameof(initial)); Balance = initial; }
        public void Add(int amount) { if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount)); Balance = checked(Balance + amount); }
        public bool TrySpend(int amount) { if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount)); if (Balance < amount) return false; Balance -= amount; return true; }
    }
}
