$stage = "RUN TESTS"
& "$PSScriptRoot\init-env.ps1"
& "$PSScriptRoot\log-start-stage.ps1" $stage

$initialLocation = Get-Location

& "$PSScriptRoot\prepare-test-env.ps1"

cd $Env:src_dir
# delete older files
rm ./output/*.trx
# execute tests
docker-compose -f ./docker-compose.yaml -f ./docker-compose.tests.yaml up --build

# this part sometimes causes issues with network mounting/unmounting
#& "$PSScriptRoot\destroy-test-env.ps1"
Set-Location $initialLocation

& "$PSScriptRoot\log-finish-stage.ps1" $stage