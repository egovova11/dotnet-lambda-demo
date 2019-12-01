$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd $Env:src_dir

docker-compose -f ./docker-compose.yaml -f ./docker-compose.tests.yaml up --build
#docker-compose -f ./docker-compose.yaml up --build dotnetlambda30

Set-Location $initialLocation