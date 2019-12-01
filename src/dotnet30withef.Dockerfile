FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build-env
RUN apk --no-cache add zip
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./DotnetLambda30WithEf/*.csproj ./DotnetLambda30WithEf/
RUN dotnet restore ./DotnetLambda30WithEf/DotnetLambda30WithEf.csproj
# Copy everything else and build
COPY  ./DotnetLambda30WithEf/ ./DotnetLambda30WithEf/
VOLUME [ "/out" ]

FROM build-env as tests
COPY ./DotnetLambda30WithEf.Tests/*.csproj ./DotnetLambda30WithEf.Tests/
RUN dotnet restore ./DotnetLambda30WithEf.Tests/DotnetLambda30WithEf.Tests.csproj
COPY ./DotnetLambda30WithEf.Tests/ ./DotnetLambda30WithEf.Tests/
RUN dotnet build ./DotnetLambda30WithEf.Tests/DotnetLambda30WithEf.Tests.csproj --configuration Release
ENTRYPOINT dotnet test ./DotnetLambda30WithEf.Tests/DotnetLambda30WithEf.Tests.csproj --configuration Release --no-restore --no-build --logger "trx;LogFileName=/out/dotnet21.trx"

FROM build-env as publish
RUN dotnet publish ./DotnetLambda30WithEf/DotnetLambda30WithEf.csproj --self-contained true -c Release -o /app/out

# zip executable code
WORKDIR /app/out
ENTRYPOINT ["zip", "-r", "/out/dotnetlambda30withef.zip", "."]