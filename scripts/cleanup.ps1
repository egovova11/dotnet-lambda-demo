$stage = "CLEANUP"
& "$PSScriptRoot\init-env.ps1"
& "$PSScriptRoot\log-start-stage.ps1" $stage

$initialLocation = Get-Location
cd $Env:src_dir
docker-compose down -v --rmi all --remove-orphans
rm ./output -r
rm ./temp -r
docker image prune -f
docker volume prune -f
Set-Location $initialLocation

& "$PSScriptRoot\log-finish-stage.ps1" $stage