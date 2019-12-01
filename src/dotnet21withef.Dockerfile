FROM mcr.microsoft.com/dotnet/core/sdk:2.1-alpine AS build-env
RUN apk --no-cache add zip
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./DotnetLambda21WithEf/*.csproj ./DotnetLambda21WithEf/
RUN dotnet restore ./DotnetLambda21WithEf/DotnetLambda21WithEf.csproj
# Copy everything else and build
COPY  ./DotnetLambda21WithEf/ ./DotnetLambda21WithEf/
VOLUME [ "/out" ]

FROM build-env as tests
COPY ./DotnetLambda21WithEf.Tests/*.csproj ./DotnetLambda21WithEf.Tests/
RUN dotnet restore ./DotnetLambda21WithEf.Tests/DotnetLambda21WithEf.Tests.csproj
COPY ./DotnetLambda21WithEf.Tests/ ./DotnetLambda21WithEf.Tests/
RUN dotnet build ./DotnetLambda21WithEf.Tests/DotnetLambda21WithEf.Tests.csproj --configuration Release
ENTRYPOINT dotnet test ./DotnetLambda21WithEf.Tests/DotnetLambda21WithEf.Tests.csproj --configuration Release --no-restore --no-build --logger "trx;LogFileName=/out/dotnetlambda21withef.trx"

FROM build-env as publish
RUN dotnet publish ./DotnetLambda21WithEf/DotnetLambda21WithEf.csproj -c Release -o /app/out

# zip executable code
WORKDIR /app/out
ENTRYPOINT ["zip", "-r", "/out/dotnetlambda21withef.zip", "."]