using UnityEngine;
namespace MergeStudio.UI {
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class PopupController : MonoBehaviour {
        public void SetVisible(bool visible) { var group = GetComponent<CanvasGroup>(); group.alpha = visible ? 1 : 0; group.interactable = group.blocksRaycasts = visible; }
    }
}
