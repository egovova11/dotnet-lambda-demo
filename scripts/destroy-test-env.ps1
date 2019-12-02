$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd "$Env:src_dir\test-env"
docker-compose -f .\docker-compose.yaml down -v
if (Test-Path ".\temp\sqlserverimage\Dockerfile") {
    rm ./temp -r -fo
}

Set-Location $initialLocation