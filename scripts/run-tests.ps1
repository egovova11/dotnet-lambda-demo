$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd $Env:src_dir

docker-compose -f ./docker-compose.yaml -f ./docker-compose.tests.yaml up --build dotnetlambda21

Set-Location $initialLocation