$ErrorActionPreference = 'Stop'
$hub = 'D:\unity\Unity Hub\Unity Hub.exe'
$out = 'D:\GameStudio\Logs\hub-android-modules.stdout.log'
$err = 'D:\GameStudio\Logs\hub-android-modules.stderr.log'
& $hub -- --headless install --version 6000.6.0f1 --module android --childModules *> $out
if ($LASTEXITCODE -ne 0) { throw "Android module install failed with exit code $LASTEXITCODE. See $err" }
