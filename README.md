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

> dotnet lambda publish-layer common-core --package-manifest ./common-core.xml --layer-type runtime-package-store --framework netcoreapp3.0 --s3-bucket malaga-serverless-net-demo --region eu-central-1

thing above creates something not very usable.
so, lets try another way

- download `https://dotnetcli.azureedge.net/dotnet/Runtime/3.0.1/dotnet-runtime-3.0.1-linux-x64.tar.gz` (it is from official docker image)
- untar and zip
  - and here we need to set correct execution rights for dotnet entry point!
- push to the layers

## Variables

1. `/malaga-serverless-net-demo/vars/db-connection` - database connection string. AdventureWorksLT2017 schema is expected.

## Troubleshooting

### docker-compose issues

1. While running on windows, containers are not started with the following error:

> ERROR: for dotnetlambda21  Cannot start service dotnetlambda21: error while creating mount source path '/host_mnt/e/projects/dotnet-lambda-demo/src/output': mkdir /host_mnt/e: file exists

Solution: go to docker settings -> shared drives -> Reset credentials, then reassign access to drives once again.
