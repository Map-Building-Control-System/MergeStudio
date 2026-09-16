param(
    [Parameter(Mandatory=$true)][string]$Repository,
    [string]$Directory = (Join-Path (Get-Location) $Repository),
    [ValidateSet('public','private')][string]$Visibility = 'private'
)
$ErrorActionPreference = 'Stop'
if (-not (Get-Command gh -ErrorAction SilentlyContinue)) { throw 'GitHub CLI (gh) is required.' }
if ($Repository -notmatch '^[A-Za-z][A-Za-z0-9-]{1,63}$') { throw 'Repository must be a short slug using letters, numbers, and hyphens.' }
$full = "MergeStudio-Games/$Repository"
gh repo create $full --$Visibility --template MergeStudio-Games/MergeStudio --description "New MergeStudio Games project: $Repository"
if ($LASTEXITCODE -ne 0) { throw "Could not create $full from the MergeStudio template." }
git clone "https://github.com/$full.git" $Directory
Write-Host "Created $full from the MergeStudio template at $Directory"
Write-Host 'Next: update product identifiers, read AGENTS.md, and run Tools/Bootstrap-Developer.ps1.'
