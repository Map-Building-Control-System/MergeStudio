$ErrorActionPreference = 'Stop'
$env:TEMP = 'D:\UnityTemp'
$env:TMP = 'D:\UnityTemp'
$installer = "$env:APPDATA\UnityHub\downloads\UnitySetup64-6000.6.0f1.exe"
if (-not (Test-Path -LiteralPath $installer)) { throw "Installer not found: $installer" }
Start-Process -FilePath $installer -ArgumentList '/D=D:\unity\6000.6.0f1' -Wait
