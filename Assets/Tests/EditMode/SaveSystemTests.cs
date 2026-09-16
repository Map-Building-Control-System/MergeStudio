using System;
using System.IO;
using NUnit.Framework;
using MergeStudio.Persistence;
namespace MergeStudio.Tests
{
    public sealed class SaveSystemTests
    {
        private string _directory;
        private SaveSystem _save;
        [SetUp] public void SetUp() { _directory = Path.Combine(Path.GetTempPath(), "MergeStudioTests", Guid.NewGuid().ToString()); _save = new SaveSystem(_directory); }
        [TearDown] public void TearDown() { Directory.Delete(_directory, true); }
        [Test] public void RoundTripPreservesProgress()
        {
            var data = new SaveData { Gold = 53, Diamonds = 7, PlayerLevel = 3 };
            data.Board.Add(new BoardCell { ItemId = "bread", Tier = 2 }); data.CompletedOrders.Add("order-1");
            _save.Save(data); var loaded = _save.Load();
            Assert.AreEqual(53, loaded.Gold); Assert.AreEqual(7, loaded.Diamonds); Assert.AreEqual(3, loaded.PlayerLevel);
            Assert.AreEqual("bread", loaded.Board[0].ItemId); Assert.AreEqual("order-1", loaded.CompletedOrders[0]);
            Assert.IsFalse(File.ReadAllText(Path.Combine(_directory, "save.dat")).Contains("bread"));
        }
        [Test] public void TamperingWithoutBackupFails()
        {
            _save.Save(new SaveData()); string path = Path.Combine(_directory, "save.dat");
            var bytes = File.ReadAllBytes(path); bytes[40] ^= 1; File.WriteAllBytes(path, bytes);
            Assert.Throws<System.Security.Cryptography.CryptographicException>(() => _save.Load());
        }
        [Test] public void CorruptPrimaryRecoversBackup()
        {
            _save.Save(new SaveData { Gold = 1 }); _save.Save(new SaveData { Gold = 2 });
            File.WriteAllBytes(Path.Combine(_directory, "save.dat"), new byte[2]);
            Assert.AreEqual(1, _save.Load().Gold);
        }
        [Test] public void DeleteRemovesBackupToo()
        {
            _save.Save(new SaveData()); _save.Save(new SaveData()); _save.DeleteSave();
            Assert.IsFalse(_save.HasSave()); Assert.AreEqual(1, _save.Load().PlayerLevel);
        }
        [Test] public void UnsupportedSchemaDoesNotOverwriteSave()
        {
            _save.Save(new SaveData { Gold = 8 });
            Assert.Throws<InvalidDataException>(() => _save.Save(new SaveData { Version = 99 }));
            Assert.AreEqual(8, _save.Load().Gold);
        }
    }
}
