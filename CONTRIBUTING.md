# Contributing to MergeStudio

## Start here

Clone the repository, install the exact Unity editor listed in `ProjectSettings/ProjectVersion.txt`, install Git LFS, and open the project from Unity Hub. Run the static audit before making changes:

```powershell
git lfs install
python Tools/validate_repo.py
```

Unity tests require the editor and its packages to finish importing:

```powershell
powershell -ExecutionPolicy Bypass -File Tools/Test-Unity.ps1
```

## Branches and pull requests

Use `feature/<name>` branches and open pull requests into `develop`. Keep `main` releasable. A PR should include a short behavior summary, test results, and screenshots or a recording for UI changes. Do not add teammates to production access until the first CI test and Android bundle checks are green.

## Using Codex or Claude

Give the agent the repository root, ask it to read `AGENTS.md`, and ask it to work on a named branch. The agent must preserve Unity `.meta` files, avoid generated folders, run the repository audit, and distinguish static checks from real Unity Test Runner and Android build results.
