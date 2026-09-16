# Contributing to MergeStudio / MergeStudio’ya katkı

## English

### Start here

Clone the repository, install the exact Unity editor in `ProjectSettings/ProjectVersion.txt`, install Git LFS, and open the project from Unity Hub. Before editing, run:

```powershell
git lfs install
python Tools/validate_repo.py
```

After Unity finishes importing packages, run:

```powershell
powershell -ExecutionPolicy Bypass -File Tools/Test-Unity.ps1
```

### Branches and pull requests

Use `feature/<name>` branches and open pull requests into `develop`. Keep `main` releasable. A PR must include a behavior summary, tests run, known limitations, and screenshots or a recording for UI changes. Do not give production access until CI tests and Android bundle checks are green.

### Using Codex or Claude

Give the agent the repository root and ask it to read `AGENTS.md`. Ask it to work on a named branch. The agent must preserve Unity `.meta` files, avoid generated folders, run the repository audit, and distinguish static checks from real Unity Test Runner and Android build results.

## Türkçe

### Başlangıç

Repo’yu clone edin, `ProjectSettings/ProjectVersion.txt` içinde yazan Unity sürümünü kurun, Git LFS yükleyin ve projeyi Unity Hub’dan açın. Kod değiştirmeden önce:

```powershell
git lfs install
python Tools/validate_repo.py
```

Unity paket importu bittikten sonra:

```powershell
powershell -ExecutionPolicy Bypass -File Tools/Test-Unity.ps1
```

### Branch ve pull request

`feature/<isim>` dallarında çalışıp `develop` dalına pull request açın. `main` yayınlanabilir tutulur. PR içinde davranış özeti, çalıştırılan testler, bilinen sınırlamalar ve UI değişiklikleri için ekran görüntüsü/video bulunmalıdır. CI testleri ve Android bundle kontrolleri geçmeden üretim erişimi vermeyin.

### Codex veya Claude kullanımı

AI aracına repo kökünü verip `AGENTS.md` dosyasını okutun. Belirli bir branch üzerinde çalışmasını isteyin. Agent Unity `.meta` dosyalarını korumalı, üretilen klasörleri değiştirmemeli, repo audit çalıştırmalı ve statik kontrolleri gerçek Unity Test Runner/Android build sonuçlarından ayırmalıdır.
