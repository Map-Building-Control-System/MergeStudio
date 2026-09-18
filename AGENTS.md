# MergeStudio development instructions / geliştirme talimatları

## English

### Project

MergeStudio is a Unity 6.6 mobile merge game and the MergeStudio Games game-project template. The required editor is `6000.6.0f1`; Android development requires Android Build Support, SDK & NDK Tools, and OpenJDK. New games use GitHub's **Use this template** action or `Tools/New-GameProject.ps1`; do not fork the template.

### First setup

1. Install Unity `6000.6.0f1` and the Android modules.
2. Install Git LFS and run `git lfs install`.
3. Clone the repository, open it in Unity Hub, and wait for import.
4. Run `MergeStudio > Ensure Project Setup` if generated assets are missing.
5. Open `Assets/Scenes/Init.unity` and verify `Init → MainMenu → Game`.

### Validation and working rules

Run `python Tools/validate_repo.py`. Run `powershell -ExecutionPolicy Bypass -File Tools/Test-Unity.ps1` only when the required Unity editor is installed. Never claim Unity tests passed without XML reports showing zero failures.

For Android work, run `powershell -ExecutionPolicy Bypass -File Tools/Check-AndroidToolchain.ps1`. Read `Docs/STUDIO-READINESS-REPORT.md` before changing package, CI, release, analytics, monetization, or AI policy.

- Use `develop` for integration, `feature/<short-name>` for features, and `hotfix/<short-name>` for urgent fixes.
- Keep visible Unity `.meta` files. Do not commit `Library`, `Temp`, builds, credentials, keystores, or secrets.
- Use event channels for gameplay communication and keep scene UI decoupled from core systems.
- Preserve save recovery behavior and explain every PR’s validation and limitations.
- Update `ProjectSettings/ProjectVersion.txt`, `Packages/manifest.json`, and this file together when versions change.

### AI-assisted development

Codex, Claude, and other agents must read this file, `README.md`, `CONTRIBUTING.md`, `Docs/ARCHITECTURE.md`, and `Docs/CODING_STANDARDS.md` before editing. They must inspect existing code, make reviewable changes, run the static audit, and report checks they could not run.

## Türkçe

### Proje

MergeStudio, Unity 6.6 ile geliştirilen mobil merge oyunudur ve MergeStudio Games oyun proje şablonudur. Gerekli Editor `6000.6.0f1`; Android için Android Build Support, SDK & NDK Tools ve OpenJDK gerekir. Yeni oyunları fork etmeyin; GitHub’da **Use this template** veya `Tools/New-GameProject.ps1` kullanın.

### İlk kurulum

1. Unity `6000.6.0f1` ve Android modüllerini kurun.
2. Git LFS kurup `git lfs install` çalıştırın.
3. Repo’yu clone edip Unity Hub’da açın ve importun bitmesini bekleyin.
4. Üretilen dosyalar eksikse `MergeStudio > Ensure Project Setup` çalıştırın.
5. `Assets/Scenes/Init.unity` açıp `Init → MainMenu → Game` akışını kontrol edin.

### Doğrulama ve çalışma kuralları

`python Tools/validate_repo.py` statik kontrolünü çalıştırın. Unity Editor kuruluysa `powershell -ExecutionPolicy Bypass -File Tools/Test-Unity.ps1` ile testleri çalıştırın. Sıfır hata gösteren XML raporu olmadan Unity testlerini geçmiş saymayın.

Android değişikliği için `powershell -ExecutionPolicy Bypass -File Tools/Check-AndroidToolchain.ps1` çalıştırın. Paket, CI, yayın, analytics, monetizasyon veya AI politikası değişikliğinden önce `Docs/STUDIO-READINESS-REPORT.md` dosyasını okuyun.

- Entegrasyon için `develop`, özellikler için `feature/<kisa-ad>`, acil düzeltmeler için `hotfix/<kisa-ad>` kullanın.
- Unity `.meta` dosyalarını koruyun. `Library`, `Temp`, build, credential, keystore ve secret commit etmeyin.
- Oynanış iletişiminde event channel kullanın; sahne UI’ını core sistemlerden ayırın.
- Kayıt kurtarma davranışını koruyun; her PR’da testleri ve sınırlamaları yazın.
- Sürüm değişiminde `ProjectSettings/ProjectVersion.txt`, `Packages/manifest.json` ve bu dosyayı birlikte güncelleyin.

### AI destekli geliştirme

Codex, Claude ve diğer araçlar kod değiştirmeden önce bu dosyayı, `README.md`, `CONTRIBUTING.md`, `Docs/ARCHITECTURE.md` ve `Docs/CODING_STANDARDS.md` dosyalarını okumalıdır. Mevcut kodu incelemeli, küçük değişiklikler yapmalı, statik kontrolü çalıştırmalı ve çalıştıramadığı kontrolleri açıkça belirtmelidir.
