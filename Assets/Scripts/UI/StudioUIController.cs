using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using MergeStudio.Events;
using MergeStudio.Persistence;

namespace MergeStudio.UI
{
    // View owns its child widgets. All gameplay communication uses channels.
    public sealed class StudioUIController : MonoBehaviour
    {
        [SerializeField] private bool _menu;
        [SerializeField] private StringEventChannelSO _sceneRequest, _boardChanged;
        [SerializeField] private VoidEventChannelSO _spawnRequest, _orderRequest;
        [SerializeField] private IntEventChannelSO _cellRequest, _goldChanged, _energyChanged;
        private Text _gold, _energy;
        private Transform _content;
        private readonly List<Text> _cells = new List<Text>();
        private readonly Dictionary<Text, string> _localized = new Dictionary<Text, string>();
        private Font _font;
        private void Awake()
        {
            _font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            var canvas = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas.transform.SetParent(transform, false); canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvas.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 2400); scaler.matchWidthOrHeight = 0.5f;
            var safe = new GameObject("SafeArea", typeof(RectTransform), typeof(SafeArea)); safe.transform.SetParent(canvas.transform, false);
            var layout = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup)); layout.transform.SetParent(safe.transform, false);
            var rect = layout.GetComponent<RectTransform>(); rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = new Vector2(32, 32); rect.offsetMax = new Vector2(-32, -32);
            var vertical = layout.GetComponent<VerticalLayoutGroup>(); vertical.spacing = 12; vertical.childControlHeight = true; vertical.childForceExpandHeight = false;
            _content = layout.transform;
            Label("MergeStudio", 100);
            if (_menu) Button("play", () => _sceneRequest.RaiseEvent("Game"));
            else
            {
                _gold = Label("Gold: 0", 70); _energy = Label("Energy: 100", 70);
                var board = new GameObject("Board", typeof(RectTransform), typeof(GridLayoutGroup), typeof(LayoutElement)); board.transform.SetParent(_content, false);
                var grid = board.GetComponent<GridLayoutGroup>(); grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount; grid.constraintCount = 7;
                grid.cellSize = new Vector2(100, 100); grid.spacing = new Vector2(6, 6); grid.childAlignment = TextAnchor.MiddleCenter;
                board.GetComponent<LayoutElement>().preferredHeight = 954;
                for (int i = 0; i < 63; i++) { int index = i; _cells.Add(ButtonText("·", () => _cellRequest.RaiseEvent(index), board.transform)); }
                Button("spawn", () => _spawnRequest.RaiseEvent()); Button("orders", () => _orderRequest.RaiseEvent());
                Label("bread × tier 2 → 25 gold", 60);
                Button("close", () => _sceneRequest.RaiseEvent("MainMenu"));
            }
            if (FindFirstObjectByType<EventSystem>() == null) new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
        private void OnEnable()
        {
            _goldChanged.OnEventRaised += Gold; _energyChanged.OnEventRaised += Energy; _boardChanged.OnEventRaised += Board;
            LocalizationSettings.SelectedLocaleChanged += LocaleChanged;
        }
        private void OnDisable()
        {
            _goldChanged.OnEventRaised -= Gold; _energyChanged.OnEventRaised -= Energy; _boardChanged.OnEventRaised -= Board;
            LocalizationSettings.SelectedLocaleChanged -= LocaleChanged;
        }
        private IEnumerator Start() { yield return LocalizationSettings.InitializationOperation; RefreshLocale(); }
        private void LocaleChanged(UnityEngine.Localization.Locale locale) => RefreshLocale();
        private void RefreshLocale() { foreach (var pair in _localized) pair.Key.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", pair.Value); }
        private void Gold(int value) { if (_gold != null) _gold.text = "Gold: " + value; }
        private void Energy(int value) { if (_energy != null) _energy.text = "Energy: " + value; }
        private void Board(string json)
        {
            var data = Serialization.FromJson(json);
            for (int i = 0; i < _cells.Count; i++) { var cell = i < data.Board.Count ? data.Board[i] : null; _cells[i].text = cell == null || cell.Tier == 0 ? "·" : cell.Tier.ToString(); }
        }
        private Text Label(string value, float height)
        {
            var go = new GameObject(value, typeof(RectTransform), typeof(Text), typeof(LayoutElement)); go.transform.SetParent(_content, false);
            var text = go.GetComponent<Text>(); text.font = _font; text.fontSize = 42; text.color = Color.white; text.alignment = TextAnchor.MiddleCenter; text.text = value;
            go.GetComponent<LayoutElement>().preferredHeight = height; return text;
        }
        private void Button(string key, UnityEngine.Events.UnityAction action) => _localized.Add(ButtonText(key, action, _content), key);
        private Text ButtonText(string value, UnityEngine.Events.UnityAction action, Transform parent)
        {
            var go = new GameObject(value, typeof(RectTransform), typeof(Image), typeof(Button), typeof(LayoutElement)); go.transform.SetParent(parent, false);
            go.GetComponent<Image>().color = new Color(0.17f, 0.38f, 0.47f); go.GetComponent<LayoutElement>().preferredHeight = 90;
            var button = go.GetComponent<Button>(); button.targetGraphic = go.GetComponent<Image>(); button.onClick.AddListener(action);
            var label = new GameObject("Label", typeof(RectTransform), typeof(Text)); label.transform.SetParent(go.transform, false);
            var rect = label.GetComponent<RectTransform>(); rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = rect.offsetMax = Vector2.zero;
            var text = label.GetComponent<Text>(); text.font = _font; text.fontSize = 36; text.alignment = TextAnchor.MiddleCenter; text.color = Color.white; text.text = value; text.raycastTarget = false; return text;
        }
    }
}
