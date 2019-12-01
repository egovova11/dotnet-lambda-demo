FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build-env
RUN apk --no-cache add zip
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./DotnetLambda30/*.csproj ./DotnetLambda30/
RUN dotnet restore ./DotnetLambda30/DotnetLambda30.csproj
# Copy everything else and build
COPY  ./DotnetLambda30/ ./DotnetLambda30/
VOLUME [ "/out" ]

FROM build-env as tests
COPY ./DotnetLambda30.Tests/*.csproj ./DotnetLambda30.Tests/
RUN dotnet restore ./DotnetLambda30.Tests/DotnetLambda30.Tests.csproj
COPY ./DotnetLambda30.Tests/ ./DotnetLambda30.Tests/
RUN dotnet build ./DotnetLambda30.Tests/DotnetLambda30.Tests.csproj --configuration Release
ENTRYPOINT dotnet test ./DotnetLambda30.Tests/DotnetLambda30.Tests.csproj --configuration Release --no-restore --no-build --logger "trx;LogFileName=/out/dotnetlambda30.trx"

FROM build-env as publish
RUN dotnet publish ./DotnetLambda30/DotnetLambda30.csproj -r rhel.7-x64 --self-contained true -c Release -o /app/out

# zip executable code
WORKDIR /app/out
ENTRYPOINT ["zip", "-r", "/out/dotnetlambda30.zip", "."]