using System;
using System.Collections.Generic;
namespace MergeStudio.Persistence {
    [Serializable] public sealed class BoardCell { public string ItemId; public int Tier; }
    [Serializable] public sealed class SaveData {
        public int Version = 1;
        public int PlayerLevel = 1;
        public int Gold;
        public int Diamonds;
        public int Energy = 100;
        public long EnergyTimestamp;
        public int BoardWidth = 7;
        public int BoardHeight = 9;
        public List<BoardCell> Board = new List<BoardCell>();
        public List<string> CompletedOrders = new List<string>();
        public void Validate() {
            if (Version != 1 || PlayerLevel < 1 || Gold < 0 || Diamonds < 0 || Energy < 0 || EnergyTimestamp < 0 || BoardWidth < 1 || BoardHeight < 1 || BoardWidth > 100 || BoardHeight > 100 || Board == null || CompletedOrders == null || Board.Count > BoardWidth * BoardHeight)
                throw new System.IO.InvalidDataException("Unsupported or invalid save data.");
            foreach (var cell in Board) if (cell != null && (cell.Tier < 0 || (cell.Tier > 0 && string.IsNullOrEmpty(cell.ItemId)))) throw new System.IO.InvalidDataException("Invalid board cell.");
        }
    }
}
