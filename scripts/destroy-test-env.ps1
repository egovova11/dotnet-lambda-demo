$stage = "DESTROY TEST ENV"
& "$PSScriptRoot\init-env.ps1"
& "$PSScriptRoot\log-start-stage.ps1" $stage

$initialLocation = Get-Location
cd "$Env:src_dir\test-env"
docker-compose -f .\docker-compose.yaml down -v
if (Test-Path ".\temp\sqlserverimage\Dockerfile") {
    rm ./temp -r -fo
}
Set-Location $initialLocation

& "$PSScriptRoot\log-finish-stage.ps1" $stage