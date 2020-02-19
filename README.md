# dotnet-lambda-demo

## Prerequisites

- Visual Studio 2019 (16.3.6+)
- .NET Core SDK 2.1
- .NET Core SDK 3.0
- npm
- AWS CDK (1.15.0)
- AWS CLI (aws)
- docker and docker-compose
- powershell (for scripts)

## Build & Deploy

1. Modify `./scripts/init-env.ps1` to use your bucket name
2. run `./scripts/build-synth-deploy.ps1`

### publish layer

**this part is outdated**

> dotnet lambda publish-layer common-core --package-manifest ./common-core.xml --layer-type runtime-package-store --framework netcoreapp3.0 --s3-bucket malaga-serverless-net-demo --region eu-central-1

thing above creates something not very usable.
so, lets try another way

- download `https://dotnetcli.azureedge.net/dotnet/Runtime/3.0.1/dotnet-runtime-3.0.1-linux-x64.tar.gz` (it is from official docker image)
- untar and zip
  - and here we need to set correct execution rights for dotnet entry point!
- push to the layers

### build database image

**this part is outdated**

> git clone https://github.com/egovova11/mssql-server-samplesdb sqlserverimage
> cd sqlserverimage
> git checkout adventureworkslt2017
> docker-compose -f .\docker-compose.sql2017.yml build
> docker-compose -f .\docker-compose.sql2017.yml up

in different console do

> docker commit sql_server_base awlt2017_image

### prepare debugging environment

> `./scripts/prepare-test-env.ps1`

## Variables

1. `/malaga-serverless-net-demo/vars/DbConnectionString` - database connection string. AdventureWorksLT2017 schema is expected.

## Troubleshooting

### docker-compose issues

1. While running on windows, containers are not started with the following error:

> ERROR: for dotnetlambda21  Cannot start service dotnetlambda21: error while creating mount source path '/host_mnt/e/projects/dotnet-lambda-demo/src/output': mkdir /host_mnt/e: file exists

Solution: go to docker settings -> shared drives -> Reset credentials, then reassign access to drives once again.

2. If there are messages like "network xxx not found" during test setup

 can be caused by artifacts of previous docker-compose run.

 > docker system prune -a

3. Build only target stage in docker

Enable buildkit: add `"features": { "buildkit": true }` to `/etc/docker/daemon.json` (https://docs.docker.com/develop/develop-images/build_enhancements/)

4. "Not possible to place Lambda Functions in a Public subnet" during CDK synth

Workaround: create new vpc and non-default subnet

5. If cdk caches query results, the cache can be reset via

> `cdk context --reset <key>`

6. "Docker command failed with exit code 0"

See https://github.com/microsoft/DockerTools/issues/213
- check you have buildkit in docker enabled
- update VS to 16.4
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets to 1.9.7+

