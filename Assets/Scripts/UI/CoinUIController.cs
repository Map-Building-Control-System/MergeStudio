using UnityEngine;
using UnityEngine.UI;
namespace MergeStudio.UI {
    [RequireComponent(typeof(Text))]
    public sealed class CoinUIController : MonoBehaviour {
        public void SetGold(int balance) => GetComponent<Text>().text = balance.ToString();
    }
}
