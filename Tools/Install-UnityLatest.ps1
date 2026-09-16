$ErrorActionPreference = 'Stop'
$hub = 'D:\unity\Unity Hub\Unity Hub.exe'
$out = 'D:\GameStudio\Logs\hub-install-6000.6.stdout.log'
$err = 'D:\GameStudio\Logs\hub-install-6000.6.stderr.log'
& $hub -- --headless install --version 6000.6.0f1 --changeset f7f8ed4d1e24 --module android --childModules *> $out
if ($LASTEXITCODE -ne 0) { throw "Unity Hub install failed with exit code $LASTEXITCODE. See $err" }
