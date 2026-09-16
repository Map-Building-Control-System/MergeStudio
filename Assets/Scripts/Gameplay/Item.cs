namespace MergeStudio.Gameplay {
    [System.Serializable] public sealed class Item {
        public string Id; public int Tier;
        public Item(string id, int tier) { Id = id; Tier = tier; }
    }
}
