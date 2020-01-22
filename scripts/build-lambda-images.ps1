$stage = "BUILD LAMBDA IMAGES"
& "$PSScriptRoot\init-env.ps1"
& "$PSScriptRoot\log-start-stage.ps1" $stage

$initialLocation = Get-Location
cd $Env:src_dir
docker-compose -f ./docker-compose.yaml up --build -V
Set-Location $initialLocation

& "$PSScriptRoot\log-finish-stage.ps1" $stage
