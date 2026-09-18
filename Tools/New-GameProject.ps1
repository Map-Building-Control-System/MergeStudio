param(
    [Parameter(Mandatory=$true)][string]$Repository,
    [string]$Directory = (Join-Path (Get-Location) $Repository),
    [ValidateSet('public','private')][string]$Visibility = 'private'
)
$ErrorActionPreference = 'Stop'
if (-not (Get-Command gh -ErrorAction SilentlyContinue)) { throw 'GitHub CLI (gh) is required.' }
if ($Repository -notmatch '^[A-Za-z][A-Za-z0-9-]{1,63}$') { throw 'Repository must be a short slug using letters, numbers, and hyphens.' }
if (Test-Path $Directory) { throw "Destination directory already exists: $Directory" }
$full = "MergeStudio-Games/$Repository"
gh repo create $full --$Visibility --template MergeStudio-Games/MergeStudio --description "New MergeStudio Games project: $Repository"
if ($LASTEXITCODE -ne 0) { throw "Could not create $full from the MergeStudio template." }

# GitHub templates can initialize a disconnected, empty default branch when
# the template's integration branch is not its default branch. Clone the
# reviewed integration branch directly, then seed both branches in the new
# repository from that exact history.
git clone --branch develop "https://github.com/MergeStudio-Games/MergeStudio.git" $Directory
if ($LASTEXITCODE -ne 0) { throw 'Could not clone the MergeStudio develop branch.' }
git -C $Directory remote set-url origin "https://github.com/$full.git"
git -C $Directory push --force -u origin develop
if ($LASTEXITCODE -ne 0) { throw "Could not seed $full/develop." }
git -C $Directory branch -f main develop
git -C $Directory push --force -u origin main
if ($LASTEXITCODE -ne 0) { throw "Could not seed $full/main." }

Write-Host "Created $full at $Directory from MergeStudio develop."
Write-Host 'Next: update product identifiers, read AGENTS.md, and run Tools/Bootstrap-Developer.ps1.'
