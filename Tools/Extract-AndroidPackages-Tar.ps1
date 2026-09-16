$ErrorActionPreference='Stop'
$base="$env:APPDATA\UnityHub\downloads"; $a='D:\unity\6000.6.0f1\Editor\Data\PlaybackEngines\AndroidPlayer'
function X($file,$dest){New-Item -ItemType Directory -Force -Path $dest | Out-Null; tar -xf (Join-Path $base $file) -C $dest}
X 'android-ndk-r27c-windows.zip' (Join-Path $a 'NDK')
X 'platform-tools_r36.0.0-win.zip' (Join-Path $a 'SDK')
X 'commandlinetools-win-12266719_latest.zip' (Join-Path $a 'SDK')
X 'build-tools_r36_windows.zip' (Join-Path $a 'SDK')
X 'platform-34-ext7_r02.zip' (Join-Path $a 'SDK')
X 'platform-36_r02.zip' (Join-Path $a 'SDK')
X 'platform-37.0_r02.zip' (Join-Path $a 'SDK')
X 'cmake-3.22.1-windows.zip' (Join-Path $a 'SDK')
Write-Output 'Android cached packages extracted.'
