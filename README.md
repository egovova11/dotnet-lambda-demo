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

### prepare debugging environment

> `./scripts/prepare-test-env.ps1`

### run tests

> `./scripts/run-tests.ps1`

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
!!! this breakes debugging in VS and Rider !!!

4. "Not possible to place Lambda Functions in a Public subnet" during CDK synth

Workaround: create new vpc and non-default subnet

5. If cdk caches query results, the cache can be reset via

> `cdk context --reset <key>`

6. "Docker command failed with exit code 0"

See https://github.com/microsoft/DockerTools/issues/213
- check you have buildkit in docker enabled (disable it!)
- update VS to 16.4
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets to 1.9.7+

