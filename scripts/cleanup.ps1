$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd $Env:src_dir
docker-compose down -v --rmi all --remove-orphans
rm ./output -r
rm ./temp -r

docker image prune -f
docker volume prune -f

Set-Location $initialLocation