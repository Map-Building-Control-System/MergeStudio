> 12 Eylül 2026 güncellemesi: Hedef Unity sürümü kullanıcı isteğiyle 6000.6.0f1 olarak değiştirildi. Manifest Addressables 2.10.3, Localization 1.5.13, Test Framework 1.4.6 ve Visual Studio entegrasyonu 2.0.28 sürümlerine güncellendi. Aşağıdaki ilk teslim kaydı tarihsel durumdur.
>
> Yeni hedefin Hub üzerinden kurulum komutu otomatik onay denetiminde “blocked by policy” ile engellendi. Unity 6.6 kurulumu, paket çözümlemesi, Console ve gerçek EditMode/PlayMode testleri henüz doğrulanmadı. GitHub push/PR ve CI AAB doğrulaması yapılmadı; ekip daveti aşamasına geçilmedi.
>
> Son statik kontrol: 93 GUID, 46 C# dosyası, 0 repo hatası; workflow YAML okuması başarılı. Android workflow gerçek AAB varlığını, zorunlu bundle girdilerini ve ZIP bütünlüğünü denetler. Bu kontrolün CI'da çalıştığı henüz doğrulanmadı.

# Teslim kontrolü

Proje konumu: `D:\GameStudio`. Proje adı verilmediğinden **MergeStudio** kullanıldı. Yerel Git reposu `main` başlangıç dalıyla ve Git LFS hook'larıyla başlatıldı. Commit, uzak GitHub repo veya push yapılmadı.

## On bölümün durumu

| Bölüm | Teslim |
| --- | --- |
| Klasörler | İstenen Assets, Tests, .github, Docs klasörleri ve .gitkeep dosyaları; üç gerçek Unity sahnesi |
| Git | Unity ignore, LFS/Smart Merge attributes, Windows/macOS kurulum komutları |
| Ekip akışı | main/develop/feature/hotfix dokümanı; PR, bug ve feature şablonları |
| CI | Android AAB, macOS iOS Xcode projesi, feature-only test; başarısız test build'i engeller |
| Event Channel | Dört kanal tipi, generic listener ve Inspector için somut listener'lar, bağlı kanal asset'leri |
| Kayıt | SaveData, JSON, AES-CBC/HMAC, yedek kurtarma, local cloud-provider adapter |
| Addressables | Sabit paket sürümü, lease/handle wrapper, üç remote grup oluşturan gerçek Editor kurulum kodu |
| Localization | Sabit paket sürümü, key sabitleri, tr/en ve sekiz UI metnini oluşturan Editor kurulum kodu |
| SDK adapter'ları | Analytics ve reklam stub'ları; offline reklam ödül vermez, network açık hata döndürür |
| Standartlar | İstenen ekip, isimlendirme, SO verisi ve test kuralları |

Addressables Settings/grup asset'leri ile Locale/String Table asset'leri **Unity ilk importunda oluşturulur**; bu makinede Unity olmadığı için henüz üretilmiş veya import edilmiş değiller. Paketler manifestte tanımlıdır; UPM indirme/çözümlemesi Unity'de gerçekleşir. Bunlar mevcut olmayan dosyalar için tamamlandı iddiası değildir; gerekli kurulum kodu repodadır.

## Çalıştırılan kontroller

- `dotnet run --project Tools/SyntaxCheck -- D:\GameStudio`: **45 C# dosyası, 0 C# 9 sözdizimi hatası**. Unity assembly/type/API derlemesi değildir.
- `dotnet run --project Tools/DomainCheck`: **14 davranış kontrolü geçti**. Gerçek Currency, EnergySystem, MergeBoard, SaveSystem kodu kullanıldı. CLI kayıt adaptörü System.Text.Json kullanır; Unity JsonUtility veya Android/iOS dosya sistemi doğrulaması yerine geçmez.
- `python Tools/validate_repo.py`: **92 GUID, 0 hata**; meta varlığı, GUID benzersizliği, sahne/SO referansları, JSON ve build-test bağı kontrol edildi.
- Üç workflow Python YAML parser ile okundu; LFS ve Unity merge attribute eşleşmeleri Git üzerinden doğrulandı.
- Addressables 2.3.16, Localization 1.5.9 ve Test Framework 1.4.5 sürümlerinin Unity paket kayıt servisinde bulunduğu doğrulandı. Kurulumda kullanılan Localization/Addressables API imzaları paket kaynaklarından incelendi.
- EditMode ve PlayMode test kaynakları repoda bulunur; **Unity Test Runner çalıştırılmadı**. Android AAB, iOS Xcode veya IPA build alınmadı; görsel/cihaz testleri yapılmadı.

## Manuel tamamlanacak adımlar

1. Unity Hub'dan 6000.0.60f1 ve platform modüllerini kurun; projeyi açın, Console'u kontrol edin, otomatik kurulumun tamamlandığını doğrulayın. `Init.unity` üzerinden demo ve Test Runner testlerini çalıştırın. Oluşan Settings, Addressables, Localization ve packages-lock dosyalarını commit edin.
2. Ürün adı, company ve Android/iOS uygulama kimliklerini ekibin gerçek değerleriyle değiştirin. İlk kurulum bu ayarları oluşturur; sonrasında Repair komutu dışında yeniden yazmaz.
3. GitHub repo oluşturun/bağlayın; ilk commit'ten sonra develop dalını açın. Branch protection, gerekli CI kontrolleri, inceleyiciler ve LFS kotasını yapılandırın.
4. Lisansınıza uygun GameCI aktivasyonu yapıp `UNITY_LICENSE`, `UNITY_EMAIL`, `UNITY_PASSWORD` secrets ekleyin. CI aktivasyonu Unity Cloud Build kullanmayı gerektirmez. Fork PR'ları secret alamaz.
5. Android yayın AAB'si için upload keystore ve parolalarını repository secrets üzerinden GameCI builder signing girdilerine bağlayın. Mevcut akış geliştirme amaçlıdır.
6. iOS için Apple Team ID, sertifika, provisioning profile ve export options ekleyin; Xcode archive/export aşamalarını yapılandırın. Mevcut iOS artifact **Xcode projesidir, IPA değildir**.
7. RemoteLoadPath'e gerçek CDN adresi girin; sanat varlıklarını gruplara atayın ve Addressables içerik build'ini yayınlayın. PAD Fast Follow/On Demand isteniyorsa ilgili entegrasyonu ayrıca kurun.
8. Firebase/PlayFab, Firebase Analytics/GameAnalytics ve reklam mediation SDK'larını gerçek proje bilgileri ve izin akışıyla entegre edin. Bulut çakışma politikası, sunucu doğrulaması ve güvenli anahtar saklama üretim gereksinimleridir.
9. 1080×2400, 320×568, 390×844, 412×915 ve çentikli Android/iOS cihazlarda Safe Area, dokunma hedefleri, fontlar, kayıt/pause, çevrimdışı durum ve performansı test edin.

Bu teslim, çalışan kod ve yapılandırma üreten bir repo başlangıcıdır. Unity importu, tam derleme ve yayın doğrulamaları yapılmadan production-ready onayı verilmemiştir.
