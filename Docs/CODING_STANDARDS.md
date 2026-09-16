# Kod standartları

- Programcılar sahnede sistemler arası hard-reference kullanmaz; sistemler arası iletişim SADECE Event Channel üzerinden yapılır. SO veri/kanal referansları ve bileşenin kendi UI çocukları bu yasağın konusu değildir. Saf modeller DI ile kurulur.
- Tasarımcılar drop rate, fiyat, ödül ve enerji süresini SADECE ScriptableObject Data Asset üzerinden düzenler, script değiştirmez.
- Sınıf/metot/public üye PascalCase; private field `_camelCase`; yerel değişken/parametre camelCase. ScriptableObject sınıflarında SO soneki zorunludur.
- Her yeni sistem ilgili sistem adıyla Assets/Tests/EditMode altında bir EditMode testiyle gelir. Yaşam döngüsü, sahne ve UI davranışına ayrıca PlayMode testi yazılır.
- Event aboneliği OnEnable, abonelikten çıkış OnDisable içinde olmalı. Generic listener için somut MonoBehaviour alt sınıfı gerekir.
- Addressables lease'leri Dispose edilir; owner OnDestroy içinde loader'ı Dispose eder. Hatalı async işlemde de handle bırakılır.
- Para işlemlerinde negatif tutarı reddet; tekrar ödül, overflow, yetersiz bakiye ve bozuk kayıt durumlarını test et.
- UTF-8, dört boşluk, dosya başına ana sınıf, namespace MergeStudio ve alt alanları. Unity meta dosyaları commit edilir.
- SDK'lar yalnız adapter sınıfları üzerinden eklenir. Loglara kişisel veri veya secret yazılmaz; analytics izni olmadan veri gönderilmez.
- PR'da ilgili test kanıtı, cihaz etkisi ve veri migration ihtiyacını belirt. Sahne YAML çatışmasını Smart Merge sonrası Unity'de açarak doğrula.
