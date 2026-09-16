using UnityEngine;
namespace MergeStudio.Persistence {
    public static class Serialization {
        public static string ToJson(SaveData data) { data.Validate(); return JsonUtility.ToJson(data); }
        public static SaveData FromJson(string json) { var data = JsonUtility.FromJson<SaveData>(json); if (data == null) throw new System.IO.InvalidDataException("Empty save."); data.Validate(); return data; }
    }
}
