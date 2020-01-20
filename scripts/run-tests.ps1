$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"
& "$PSScriptRoot\prepare-test-env.ps1"

cd $Env:src_dir

# delete older files
rm ./output/*.trx
# execute tests
docker-compose -f ./docker-compose.yaml -f ./docker-compose.tests.yaml up --build

# this part sometimes causes issues with network mounting
#& "$PSScriptRoot\destroy-test-env.ps1"
Set-Location $initialLocation