using UnityEngine;
namespace MergeStudio.Gameplay {
    [CreateAssetMenu(menuName = "MergeStudio/Data/Order")]
    public sealed class OrderConfigSO : ScriptableObject {
        public string Id = "first_order";
        public string ItemId = "bread";
        [Range(1, 10)] public int Tier = 2;
        [Min(0)] public int GoldReward = 25;
    }
}
