using NUnit.Framework;
using UnityEngine;
using MergeStudio.Events;
using MergeStudio.Gameplay;
namespace MergeStudio.Tests
{
    public sealed class GameplayTests
    {
        [Test] public void MergeConsumesSourceAndUpgradesTarget()
        {
            var board = new MergeBoard(2, 2); board.Place(0, new Item("bread", 1)); board.Place(1, new Item("bread", 1));
            Assert.IsTrue(board.Merge(0, 1)); Assert.IsNull(board[0]); Assert.AreEqual(2, board[1].Tier);
        }
        [Test] public void InvalidMergePreservesItems()
        {
            var board = new MergeBoard(2, 2); board.Place(0, new Item("bread", 1)); board.Place(1, new Item("tea", 1));
            Assert.IsFalse(board.Merge(0, 1)); Assert.IsFalse(board.Merge(0, 0)); Assert.IsFalse(board.Merge(-1, 1)); Assert.AreEqual(1, board[0].Tier);
        }
        [Test] public void OrderCannotRewardTwice()
        {
            var order = ScriptableObject.CreateInstance<OrderConfigSO>(); var gold = ScriptableObject.CreateInstance<IntEventChannelSO>();
            var fulfilled = ScriptableObject.CreateInstance<StringEventChannelSO>(); int reward = 0; string completed = null;
            gold.OnEventRaised += value => reward += value; fulfilled.OnEventRaised += value => completed = value;
            try
            {
                var board = new MergeBoard(2, 2); board.Place(0, new Item("bread", 2)); board.Place(1, new Item("bread", 2));
                var system = new OrderSystem(new string[0], gold, fulfilled);
                Assert.IsTrue(system.Fulfill(order, board)); Assert.IsFalse(system.Fulfill(order, board));
                Assert.AreEqual(25, reward); Assert.AreEqual(order.Id, completed); Assert.NotNull(board[1]);
            }
            finally { Object.DestroyImmediate(order); Object.DestroyImmediate(gold); Object.DestroyImmediate(fulfilled); }
        }
    }
}
