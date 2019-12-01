$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd $Env:src_dir

# delete older files
rm ./output/*.trx
docker-compose -f ./docker-compose.yaml -f ./docker-compose.tests.yaml up --build

Set-Location $initialLocation