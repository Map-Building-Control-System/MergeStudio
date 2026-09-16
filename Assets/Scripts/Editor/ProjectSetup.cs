using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace MergeStudio.Editor
{
    [InitializeOnLoad]
    public static class ProjectSetup
    {
        static ProjectSetup() { EditorApplication.delayCall += Ensure; }
        [MenuItem("MergeStudio/Ensure Project Setup")]
        public static void Ensure()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) { EditorApplication.delayCall += Ensure; return; }
            bool firstRun = !File.Exists("Assets/Settings/SetupVersion.txt");
            if (!firstRun) return;
            Configure();
            File.WriteAllText("Assets/Settings/SetupVersion.txt", "1\n");
            AssetDatabase.Refresh();
        }
        [MenuItem("MergeStudio/Repair Generated Configuration")]
        public static void Configure()
        {
            EditorSettings.serializationMode = SerializationMode.ForceText;
            UnityEditor.VersionControlSettings.mode = "Visible Meta Files";
            PlayerSettings.companyName = "MergeStudio"; PlayerSettings.productName = "MergeStudio";
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            PlayerSettings.defaultScreenWidth = 1080; PlayerSettings.defaultScreenHeight = 2400;
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, "com.mergestudio.merge");
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.iOS, "com.mergestudio.merge");
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.iOS, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            EditorUserBuildSettings.buildAppBundle = true;
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                settings = AddressableAssetSettings.Create("Assets/AddressablesData", "AddressableAssetSettings", true, true);
                AddressableAssetSettingsDefaultObject.Settings = settings;
            }
            foreach (string name in new[] { "UI_Remote", "Characters_Remote", "Events_Remote" })
            {
                var group = settings.FindGroup(name) ?? settings.CreateGroup(name, false, false, true, null, typeof(BundledAssetGroupSchema), typeof(ContentUpdateGroupSchema));
                var schema = group.GetSchema<BundledAssetGroupSchema>();
                schema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteBuildPath);
                schema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteLoadPath);
            }
            Directory.CreateDirectory("Assets/Localization"); AssetDatabase.Refresh();
            if (LocalizationEditorSettings.ActiveLocalizationSettings == null)
            {
                var localization = ScriptableObject.CreateInstance<LocalizationSettings>();
                AssetDatabase.CreateAsset(localization, "Assets/Localization/LocalizationSettings.asset");
                LocalizationEditorSettings.ActiveLocalizationSettings = localization;
            }
            foreach (string code in new[] { "tr", "en" })
            {
                if (LocalizationEditorSettings.GetLocales().Any(l => l.Identifier.Code == code)) continue;
                var locale = Locale.CreateLocale(code); AssetDatabase.CreateAsset(locale, "Assets/Localization/" + code + ".asset");
                LocalizationEditorSettings.AddLocale(locale);
            }
            var collection = LocalizationEditorSettings.GetStringTableCollection("UI") ?? LocalizationEditorSettings.CreateStringTableCollection("UI", "Assets/Localization/Tables");
            string[] keys = { "continue", "settings", "shop", "play", "energy", "orders", "close", "spawn" };
            string[] tr = { "Devam Et", "Ayarlar", "Mağaza", "Oyna", "Enerji", "Siparişler", "Kapat", "Eşya Üret" };
            string[] en = { "Continue", "Settings", "Shop", "Play", "Energy", "Orders", "Close", "Spawn Item" };
            foreach (string code in new[] { "tr", "en" })
            {
                var table = collection.GetTable(new LocaleIdentifier(code)) as StringTable;
                if (table == null) table = collection.AddNewTable(new LocaleIdentifier(code)) as StringTable;
                for (int i = 0; i < keys.Length; i++) if (table.GetEntry(keys[i]) == null) table.AddEntry(keys[i], code == "tr" ? tr[i] : en[i]);
                EditorUtility.SetDirty(table);
            }
            EditorUtility.SetDirty(collection.SharedData); EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
        }
    }
    public sealed class BuildPreparation : IPreprocessBuildWithReport
    {
        public int callbackOrder => -1000;
        public void OnPreprocessBuild(BuildReport report) => ProjectSetup.Ensure();
    }
}
