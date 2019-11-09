$initialLocation = Get-Location

& "$PSScriptRoot\init-env.ps1"

cd "$PSScriptRoot\..\src\demo-stack"

& "$PSScriptRoot\build-npm.ps1"

& "$PSScriptRoot\synth.ps1"

& "$PSScriptRoot\deploy.ps1"

Set-Location $initialLocation
