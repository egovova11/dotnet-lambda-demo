$stage = "PREPARE TEST ENV"
& "$PSScriptRoot\init-env.ps1"
& "$PSScriptRoot\log-start-stage.ps1" $stage

$initialLocation = Get-Location
cd "$Env:src_dir\test-env"
if (-not( Test-Path ".\temp\sqlserverimage\Dockerfile")) {
    mkdir temp
    cd temp
    git clone https://github.com/egovova11/mssql-server-samplesdb sqlserverimage
    cd sqlserverimage
    git checkout adventureworkslt2017
    cd "$Env:src_dir\test-env"
}

docker-compose -f .\docker-compose.yaml build
docker-compose -f .\docker-compose.yaml up -d
# should be enough to restore the db and start all services
Start-Sleep -s 30

Set-Location $initialLocation

& "$PSScriptRoot\log-finish-stage.ps1" $stage