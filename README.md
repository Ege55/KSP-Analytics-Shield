# KSP Analytics Shield

- **Version:** `0.2.0`
- **Target:** Kerbal Space Program 1.12.5 (Windows x64)
- **Status:** Experimental

A small KSP plugin that pauses Unity's legacy Analytics initialization and repeatedly disables its event collection and device statistics. It uses Unity APIs already shipped with KSP; it adds no libraries or network code.

## What it does

- Sets `UnityEngine.Analytics.Analytics.initializeOnStartup` to `false` as soon as the KSP addon starts. Unity documents this runtime setting as pausing Analytics initialization until explicitly resumed.
- Sets `Analytics.enabled` and `Analytics.deviceStatsEnabled` to `false`.
- Reapplies all three settings when the game regains focus and every two seconds.
- Writes one status line to `KSP.log`; if KSP or another plugin re-enables a setting, the mod reapplies the disabled state and logs the change.

The 0.1.0 build loaded in KSP 1.12.5.03190, logged both Analytics collection flags as `False`, and KSP's startup events returned `AnalyticsDisabled`. The 0.2.0 build adds the initialization pause; verify its new log line and network behavior on first launch.

## Limits

This mod controls Unity Analytics through its in-game API. It does not submit Unity's web privacy opt-out or historical data deletion request, erase data already sent, or block every Unity/KSP network request. A request could occur before the KSP addon runs or outside the Analytics API. The initialization pause should reduce that window, but does not prove that all Unity endpoints are silent.

The KSP setting **Show Unity Analytics Dialog on Next Startup Only** controls the popup only; it does not itself opt out. The mod leaves the game's files and that setting alone.

## Install

1. Close KSP.
2. Copy `GameData/KSPAnalyticsShield` into the KSP installation's `GameData` folder.
3. Start KSP and check `KSP.log` for `[KSP Analytics Shield 0.2.0.0]` and `initializeOnStartup=False`.

To uninstall, close KSP and remove only `GameData/KSPAnalyticsShield`.

## Build from source

Install KSP 1.12.5 for Windows x64 and run `build.bat` from a command prompt. If KSP is not installed at `C:\GOG Games\Kerbal Space Program`, pass the install path as the first argument, for example:

```bat
build.bat "D:\Games\Kerbal Space Program"
```

The script compiles against the assemblies in that KSP installation and writes the DLL into this repository's `GameData` folder. No Harmony or other mod dependency is required.

## CKAN

`NetKAN/KSPAnalyticsShield.netkan.template` is a draft configured for this GitHub repository. Before submission, publish a GitHub release with the install ZIP and confirm the license and KSP compatibility metadata. CKAN needs a stable release download URL; this draft cannot be indexed as-is.

## License

MIT. See `LICENSE`.
