$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd $Env:stack_dir
& $Env:npm run build

Set-Location $initialLocation