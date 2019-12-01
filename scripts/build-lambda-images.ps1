$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd $Env:src_dir
docker-compose -f ./docker-compose.yaml up --build -V

Set-Location $initialLocation