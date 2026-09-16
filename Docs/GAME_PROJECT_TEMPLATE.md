# MergeStudio Games game-project template

## Recommended creation flow

1. In the [MergeStudio Games repositories](https://github.com/orgs/MergeStudio-Games/repositories) page, open **MergeStudio** and choose **Use this template → Create a new repository**.
2. Keep the new repository under the `MergeStudio-Games` organization, choose a short game slug such as `GardenMerge`, and decide public/private visibility.
3. Clone the new repository, open it in Unity, and run `Tools/Bootstrap-Developer.ps1`.
4. Update company, product name, Android/iOS application identifiers, and the README before feature work.
5. Invite contributors to the organization or repository with the least access they need. Work through `feature/<short-name>` branches and pull requests into `develop`.

The template contains the Unity project settings, package manifest, test layout, event-channel conventions, CI workflows, AI instructions, issue forms, and PR checklist. It is a starting point; generated Unity folders and game-specific data must not be copied between games after the project is created.

## Naming and repository properties

- Repository: `PascalCase` game name, no spaces.
- Unity product name: the player-facing name.
- Android/iOS identifiers: `com.mergestudio.<game-slug>`.
- Topics: `unity`, `unity6`, `mobile-game`, the genre, and the platform.
- Description: one sentence explaining the game and its status.

## What a developer does after cloning

The repository root is the developer workspace. The developer reads `AGENTS.md`, opens the Unity project at the pinned editor version, runs the bootstrap audit, and then works in a feature branch. Giving the repository root to Codex or Claude is sufficient; the agent instructions and contribution rules travel with the game repository.

## What is shared and what is game-specific

Shared: code standards, branch flow, CI shape, test conventions, save/recovery rules, localization approach, and AI collaboration rules.

Game-specific: scenes, art, balance, product identifiers, remote content, analytics keys, ads, store signing, and any production secrets. These must be configured in the new repository and never placed in the template.
