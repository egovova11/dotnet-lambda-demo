$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd $Env:src_dir
mkdir temp
cd temp
git clone https://github.com/egovova11/mssql-server-samplesdb sqlserverimage
cd sqlserverimage
git checkout adventureworkslt2017
docker-compose -f .\docker-compose.sql2017.yml build
docker-compose -f .\docker-compose.sql2017.yml up
# should be enough to restore the db
# Start-Sleep -s 60
# docker commit sql_server_base awlt2017_image
# docker-compose -f .\docker-compose.sql2017.yml down
# cd ..
# rm ./sqlserverimage -r -fo

Set-Location $initialLocation