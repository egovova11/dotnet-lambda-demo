$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd $Env:src_dir
docker-compose up --build -V

Set-Location $initialLocation