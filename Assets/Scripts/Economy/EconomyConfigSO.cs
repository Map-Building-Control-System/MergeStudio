using UnityEngine;
namespace MergeStudio.Economy {
    [CreateAssetMenu(menuName = "MergeStudio/Data/Economy")]
    public sealed class EconomyConfigSO : ScriptableObject {
        [Min(1)] public int MaxEnergy = 100;
        [Min(1)] public int EnergySeconds = 120;
        [Min(1)] public int SpawnCost = 1;
        [Min(1)] public int EnergyPackPrice = 10;
        [Min(1)] public int EnergyPackAmount = 20;
        [Range(0, 1)] public float BonusDropRate = 0.1f;
    }
}
