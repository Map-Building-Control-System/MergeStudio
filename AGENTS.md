# MergeStudio development instructions

## Project

MergeStudio is a Unity 6.6 mobile merge game. The required editor version is `6000.6.0f1`; Android development requires Android Build Support, SDK & NDK Tools, and OpenJDK.

## First setup

1. Install Unity Hub and Unity `6000.6.0f1` with Android Build Support, Android SDK & NDK Tools, and OpenJDK.
2. Install Git LFS and run `git lfs install`.
3. Clone the repository, open it from Unity Hub, and wait for the first import.
4. Run `MergeStudio > Ensure Project Setup` once if generated Addressables or Localization assets are missing.
5. Open `Assets/Scenes/Init.unity` and verify the Init → MainMenu → Game flow.

## Validation

Run `python Tools/validate_repo.py` for the static repository audit. If the required editor is installed at the default path, run `powershell -ExecutionPolicy Bypass -File Tools/Test-Unity.ps1` for EditMode and PlayMode tests. Do not claim Unity tests passed unless the XML reports exist and show zero failures.

## Working rules

- Use `develop` for integration. Create `feature/<short-name>` branches for work and `hotfix/<short-name>` for urgent fixes.
- Keep Unity text serialization and visible `.meta` files. Do not commit `Library`, `Temp`, build outputs, credentials, keystores, or generated secrets.
- Gameplay communication uses event channels; keep scene UI decoupled from core systems.
- Persistence changes must preserve recovery behavior and must not expose secrets.
- Every PR must explain behavior changed, validation run, and any Unity/editor limitation.
- Before changing package or editor versions, update `ProjectSettings/ProjectVersion.txt`, `Packages/manifest.json`, and this file together.

## AI-assisted development

Codex, Claude, and other agents should read this file, `README.md`, `Docs/ARCHITECTURE.md`, and `Docs/CODING_STANDARDS.md` before editing. They should inspect existing code before adding new systems, make small reviewable commits, run the static audit after changes, and report any validation they could not run. Never invent Unity test results or production readiness.
