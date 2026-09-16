# MergeStudio

Unity 6 ile Android ve iOS için merge-2 mobil oyun temeli. Eşya birleştirme, sipariş tamamlama, enerji ekonomisi, yerel kayıt ve olay kanalları içerir. Bu repo bir stüdyo başlangıç iskeletidir; mağazaya yayın için cihaz doğrulaması, imzalama ve servis entegrasyonu gerekir.

## Kurulum
1. Unity Hub üzerinden **6000.6.0f1**, Android Build Support (SDK/NDK/OpenJDK) ve iOS Build Support kurun. iOS archive için Xcode gerekir.
2. Git LFS kurun (Windows: `winget install GitHub.GitLFS`; macOS: `brew install git-lfs`). Repo kökünde `git lfs install` ve `git lfs pull` çalıştırın.
3. Unity Hub'da repo kökünü açın. UPM manifestindeki Addressables, Localization ve Test Framework paketleri indirilir.
4. İlk import sonunda `ProjectSetup` remote grupları, tr/en locale'lerini, 8 metinlik UI String Table'ı ve mobil Player Settings'i oluşturur. Üretilen varlıkları ve `Packages/packages-lock.json` dosyasını commit edin. Gerekirse **MergeStudio > Ensure Project Setup** çalıştırın.
5. `Assets/Scenes/Init.unity` açıp Play'e basın. Oyna → Eşya Üret → aynı seviyedeki iki hücreye sırayla dokun → Siparişler. İlk sipariş tek seferliktir.
6. Test Runner üzerinden EditMode ve PlayMode testlerini çalıştırın.

Ekipte Codex veya Claude kullanacak geliştiriciler önce [AGENTS.md](AGENTS.md) ve [CONTRIBUTING.md](CONTRIBUTING.md) dosyalarını okumalıdır. Windows için ilk kontrolü `powershell -ExecutionPolicy Bypass -File Tools/Bootstrap-Developer.ps1` ile çalıştırabilirsiniz.

Proje adı verilmediği için MergeStudio kullanıldı. Ürün adı ve uygulama kimliklerini Project Settings > Player'da değiştirin; ilk kurulumdan sonra otomatik olarak üzerine yazılmaz. Repair komutu başlangıç ayarlarını tekrar uygular.

## UnityYAMLMerge
Windows PowerShell (Unity farklı konumdaysa yolu değiştirin):
```powershell
git config --global merge.unityyamlmerge.name 'Unity Smart Merge'
git config --global merge.unityyamlmerge.driver "'C:/Program Files/Unity/Hub/Editor/6000.6.0f1/Editor/Data/Tools/UnityYAMLMerge.exe' merge -p %O %B %A %A"
git config --global merge.unityyamlmerge.recursive binary
```
macOS:
```sh
git config --global merge.unityyamlmerge.name 'Unity Smart Merge'
git config --global merge.unityyamlmerge.driver "'/Applications/Unity/Hub/Editor/6000.6.0f1/Unity.app/Contents/Tools/UnityYAMLMerge' merge -p %O %B %A %A"
git config --global merge.unityyamlmerge.recursive binary
```
`.gitattributes` içindeki `* text=auto` geçerli Git sözdizimidir; Unity YAML ve LFS kuralları bunu dosya tipine göre ezer. `.meta` dosyalarını varlıklarla birlikte taşıyın ve commit edin.

## Ekip akışı
`main` üretim, `develop` entegrasyon dalıdır. Görevler `feature/gorev-adi`, acil düzeltmeler `hotfix/aciklama` dalında geliştirilir. PR ve test sonrası birleştirin; hotfix'i develop'a da taşıyın. GitHub'da branch protection kurun; bu yerel iskelet uzak repo oluşturmaz.

## Yapı
- `Assets/Scripts`: Core, Gameplay, Economy, Events, Persistence, UI, Network, Localization, Analytics ve Editor katmanları.
- `Assets/Settings`: tasarım verileri ve olay kanalı varlıkları.
- `Assets/AddressablesData`: Addressables ayarları ve içerik kategorileri.
- `Assets/Art`, `Prefabs`, `Scenes`: sanat, prefab ve başlangıç sahneleri.
- `Assets/Tests`: EditMode ve PlayMode testleri.
- `.github`: test/build workflow'ları ve ekip şablonları.
- `Docs`: [mimari](Docs/ARCHITECTURE.md), [kod standartları](Docs/CODING_STANDARDS.md), [teslim ve manuel adımlar](Docs/DELIVERY.md).

CI lisans secret'ları: `UNITY_LICENSE`, `UNITY_EMAIL`, `UNITY_PASSWORD`. Fork PR'larında secret sağlanmaz; yetkisiz kodu `pull_request_target` ile çalıştırmayın. iOS workflow'u imzasız Xcode projesi üretir; IPA için Apple signing gerekir. Android AAB geliştirme imzasıyla üretilir; Play yayını için upload keystore yapılandırın.
