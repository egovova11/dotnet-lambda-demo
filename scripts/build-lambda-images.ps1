
$initialLocation = Get-Location

cd "$PSScriptRoot\..\src"

docker-compose up --build

Set-Location $initialLocation