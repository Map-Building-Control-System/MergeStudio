using System.Collections.Generic;
using MergeStudio.Events;
namespace MergeStudio.Gameplay {
    public sealed class OrderSystem {
        private readonly HashSet<string> _completed;
        private readonly IntEventChannelSO _goldGained;
        private readonly StringEventChannelSO _fulfilled;
        public OrderSystem(IEnumerable<string> completed, IntEventChannelSO goldGained, StringEventChannelSO fulfilled) { _completed = new HashSet<string>(completed); _goldGained = goldGained; _fulfilled = fulfilled; }
        public bool Fulfill(OrderConfigSO order, MergeBoard board) {
            if (order == null || string.IsNullOrEmpty(order.Id) || order.GoldReward < 0 || _completed.Contains(order.Id) || !board.Consume(order.ItemId, order.Tier)) return false;
            _completed.Add(order.Id); _goldGained?.RaiseEvent(order.GoldReward); _fulfilled?.RaiseEvent(order.Id); return true;
        }
    }
}
