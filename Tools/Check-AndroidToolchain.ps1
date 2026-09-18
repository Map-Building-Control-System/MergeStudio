[CmdletBinding()]
param(
    [string]$AndroidPlayer = ""
)

$ErrorActionPreference = 'Stop'
$project = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
if (-not $AndroidPlayer) {
    $versionFile = Join-Path $project 'ProjectSettings/ProjectVersion.txt'
    $version = (Select-String -Path $versionFile -Pattern '^m_EditorVersion: (.+)$').Matches.Groups[1].Value.Trim()
    if (-not $version) { throw "Unity version is missing from $versionFile" }
    $AndroidPlayer = "D:\unity\$version\Editor\Data\PlaybackEngines\AndroidPlayer"
}
if (-not (Test-Path -LiteralPath $AndroidPlayer)) { throw "AndroidPlayer not found: $AndroidPlayer" }

$checks = [ordered]@{
    'Android Build Support' = $AndroidPlayer
    'OpenJDK 17' = Join-Path $AndroidPlayer 'OpenJDK\bin\java.exe'
    'Android NDK r27c' = Join-Path $AndroidPlayer 'NDK\android-ndk-r27c\ndk-build.cmd'
    'Android SDK platform-tools' = Join-Path $AndroidPlayer 'SDK\platform-tools\adb.exe'
    'Android SDK Build Tools 36' = Join-Path $AndroidPlayer 'SDK\build-tools\36.0.0\aapt2.exe'
    'Android SDK Platform 34' = Join-Path $AndroidPlayer 'SDK\platforms\android-34\android.jar'
    'Android SDK Platform 36' = Join-Path $AndroidPlayer 'SDK\platforms\android-36\android.jar'
    'Android SDK command-line tools' = Join-Path $AndroidPlayer 'SDK\cmdline-tools\latest\bin\sdkmanager.bat'
    'CMake 3.22.1' = Join-Path $AndroidPlayer 'SDK\cmake\3.22.1\bin\cmake.exe'
}

$missing = @()
foreach ($check in $checks.GetEnumerator()) {
    $ok = Test-Path -LiteralPath $check.Value
    [pscustomobject]@{ Component = $check.Key; Path = $check.Value; Status = if ($ok) { 'OK' } else { 'MISSING' } }
    if (-not $ok) { $missing += $check.Key }
}
if ($missing.Count -gt 0) { throw ('Missing Android components: ' + ($missing -join ', ')) }

