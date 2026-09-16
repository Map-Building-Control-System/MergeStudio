$ErrorActionPreference = 'Stop'
$base = "$env:APPDATA\UnityHub\downloads"
$android = 'D:\unity\6000.6.0f1\Editor\Data\PlaybackEngines\AndroidPlayer'
$tmp = 'D:\UnityTemp\android-extract'
New-Item -ItemType Directory -Force -Path $tmp,$android | Out-Null
function Expand-Clean($zip,$dest) {
  $stage = Join-Path $tmp ([IO.Path]::GetFileNameWithoutExtension($zip))
  if (Test-Path $stage) { Remove-Item $stage -Recurse -Force }
  New-Item -ItemType Directory -Force -Path $stage | Out-Null
  Expand-Archive -LiteralPath (Join-Path $base $zip) -DestinationPath $stage -Force
  New-Item -ItemType Directory -Force -Path $dest | Out-Null
  Copy-Item (Join-Path $stage '*') $dest -Recurse -Force
}
Expand-Clean 'jdk17.0.18-8_15e8817d1f5db6db3571ebe7430ef37f7fa8e60e8ff6f3e18ca1cb4c29f78774.zip' (Join-Path $android 'OpenJDK')
$ndkDest = Join-Path $android 'NDK'; Expand-Clean 'android-ndk-r27c-windows.zip' $ndkDest
$sdk = Join-Path $android 'SDK'; Expand-Clean 'platform-tools_r36.0.0-win.zip' $sdk
Expand-Clean 'commandlinetools-win-12266719_latest.zip' (Join-Path $sdk 'cmdline-tools')
Expand-Clean 'build-tools_r36_windows.zip' (Join-Path $sdk 'build-tools')
Expand-Clean 'platform-34-ext7_r02.zip' (Join-Path $sdk 'platforms')
Expand-Clean 'platform-36_r02.zip' (Join-Path $sdk 'platforms')
Expand-Clean 'platform-37.0_r02.zip' (Join-Path $sdk 'platforms')
Expand-Clean 'cmake-3.22.1-windows.zip' (Join-Path $android 'SDK\cmake')
Write-Output 'Cached OpenJDK, NDK, SDK tools/platforms and CMake installed.'
