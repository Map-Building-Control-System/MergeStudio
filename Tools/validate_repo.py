"""Static repository audit; never substitutes for a Unity import/build."""
from pathlib import Path
import json,re,uuid

root=Path(__file__).resolve().parents[1]
assets=root/'Assets'
errors=[]
for p in assets.rglob('*'):
    if p.name.startswith('.') or p.suffix=='.meta': continue
    meta=Path(str(p)+'.meta')
    if not meta.exists(): errors.append(f'Missing meta: {p.relative_to(root)}')
guids={}
for p in assets.rglob('*.meta'):
    m=re.search(r'^guid: ([0-9a-f]{32})$',p.read_text(),re.M)
    if not m: errors.append(f'Invalid meta: {p}'); continue
    if m[1] in guids: errors.append(f'Duplicate GUID: {p}')
    guids[m[1]]=p
for p in list(assets.rglob('*.unity'))+list(assets.rglob('*.asset')):
    # Script GUIDs owned by Unity packages are not present in Assets/*.meta.
    # Validate project-owned references while leaving package script bindings
    # to Unity's Package Manager.
    serialized = '\n'.join(line for line in p.read_text().splitlines() if 'm_Script:' not in line)
    for guid in re.findall(r'guid: ([0-9a-f]{32})', serialized):
        if guid not in guids:
            errors.append(f'Unresolved GUID {guid}: {p}')
for p in list(root.glob('Packages/*.json'))+list(assets.rglob('*.asmdef')): json.loads(p.read_text())
for name in ['Init','MainMenu','Game']:
    if not (assets/'Scenes'/f'{name}.unity').is_file(): errors.append(f'Missing scene {name}')
for p in root.glob('.github/workflows/*.yml'):
    text=p.read_text()
    if p.name != 'repository-audit.yml' and 'game-ci/unity-test-runner@v4' not in text: errors.append(f'Missing tests {p}')
    if 'build-' in p.name and 'needs: tests' not in text: errors.append(f'Build not gated {p}')
print(f'Repository audit: {len(guids)} GUIDs, {len(list(assets.rglob("*.cs")))} C# files, {len(errors)} errors')
for error in errors: print(error)
raise SystemExit(bool(errors))
