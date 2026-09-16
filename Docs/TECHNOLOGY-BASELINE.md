# Technology baseline / Teknoloji temeli

## English

This template keeps a small, compatible Unity 6.6 baseline and adds the most common Unity 2D building blocks:

| Package | Version | Use |
| --- | --- | --- |
| Input System | `1.20.0` | Touch, mouse, keyboard, controller, and rebinding |
| Cinemachine | `3.1.7` | 2D camera follow, framing, shake, and transitions |
| 2D Animation | `15.0.3` | Bone-based 2D characters and sprite skinning |
| 2D SpriteShape | `15.0.0` | Procedural 2D paths and terrain |
| 2D Tilemap Extras | `3.1.3` | Rule Tiles, brushes, and tilemap authoring helpers |
| Addressables | `2.10.3` | Async content loading and remote content |
| Localization | `1.5.13` | Runtime language support |
| Test Framework | `1.4.6` | EditMode and PlayMode tests |

Unity generates `Packages/packages-lock.json` after the first successful import. Commit that generated lock file so every developer and CI runner resolves the same graph.

### AI and GitHub tools

AI is deliberately kept out of the game runtime baseline. Codex, Claude, GitHub Copilot, and similar tools belong in the developer workflow and read `AGENTS.md`; they should not add model runtimes, API keys, or cloud calls to a game without a product decision. If a game needs NPC inference, image generation, or an online assistant, evaluate it as an explicit optional feature with privacy, offline behavior, latency, cost, licensing, and mobile memory limits documented first.

### Add only when needed

Do not add every popular repository to every game. Add a package only when a real feature needs it, its Unity 6.6 compatibility is verified, its license is acceptable, and a test or performance check covers the integration. For networking, analytics, ads, backend, or AI, choose a project-specific adapter instead of coupling the template to one provider.

## Türkçe

Bu template, Unity 6.6 üzerinde küçük ve uyumlu bir temel tutarken en sık kullanılan 2D yapı taşlarını içerir:

| Paket | Sürüm | Kullanım |
| --- | --- | --- |
| Input System | `1.20.0` | Dokunma, fare, klavye, gamepad ve yeniden eşleme |
| Cinemachine | `3.1.7` | 2D kamera takip, kadraj, sarsıntı ve geçişler |
| 2D Animation | `15.0.3` | Kemik tabanlı 2D karakter ve sprite skinning |
| 2D SpriteShape | `15.0.0` | Prosedürel 2D yol ve arazi |
| 2D Tilemap Extras | `3.1.3` | Rule Tile, brush ve tilemap araçları |
| Addressables | `2.10.3` | Asenkron ve uzak içerik yükleme |
| Localization | `1.5.13` | Çalışma zamanı dil desteği |
| Test Framework | `1.4.6` | EditMode ve PlayMode testleri |

Unity, ilk başarılı importtan sonra `Packages/packages-lock.json` üretir. Tüm geliştiricilerin ve CI’ın aynı paket ağacını kullanması için bu dosyayı commit edin.

### AI ve GitHub araçları

AI’ı bilinçli olarak oyun runtime temelinin dışında tutuyoruz. Codex, Claude, GitHub Copilot ve benzeri araçlar geliştirici iş akışında kullanılır ve `AGENTS.md` dosyasını okur; ürün kararı olmadan oyuna model runtime’ı, API anahtarı veya cloud çağrısı eklenmez. NPC çıkarımı, görsel üretim veya çevrim içi asistan gerekiyorsa bunu privacy, çevrim dışı davranış, gecikme, maliyet, lisans ve mobil bellek sınırlarıyla ayrı bir özellik olarak değerlendirin.

### Gerektiğinde ekleyin

Her popüler repo’yu her oyuna eklemeyin. Bir paketi ancak gerçek bir özellik ihtiyaç duyuyorsa, Unity 6.6 uyumluluğu doğrulanmışsa, lisansı uygunsa ve entegrasyonu test/performance kontrolüyle korunuyorsa ekleyin. Network, analytics, reklam, backend veya AI için template’i tek bir sağlayıcıya bağlamak yerine oyuna özel adapter kullanın.
