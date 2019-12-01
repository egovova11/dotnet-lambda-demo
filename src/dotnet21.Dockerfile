FROM mcr.microsoft.com/dotnet/core/sdk:2.1-alpine AS build-env
RUN apk --no-cache add zip
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./DotnetLambda21/*.csproj ./DotnetLambda21/
RUN dotnet restore ./DotnetLambda21/DotnetLambda21.csproj
# Copy everything else and build
COPY  ./DotnetLambda21/ ./DotnetLambda21/
VOLUME [ "/out" ]

FROM build-env as tests
COPY ./DotnetLambda21.Tests/*.csproj ./DotnetLambda21.Tests/
RUN dotnet restore ./DotnetLambda21.Tests/DotnetLambda21.Tests.csproj
COPY ./DotnetLambda21.Tests/ ./DotnetLambda21.Tests/
RUN dotnet build ./DotnetLambda21.Tests/DotnetLambda21.Tests.csproj --configuration Release
ENTRYPOINT dotnet test ./DotnetLambda21.Tests/DotnetLambda21.Tests.csproj --configuration Release --no-restore --no-build --logger "trx;logfilename=/out/dotnet21.trx"

FROM build-env as publish
RUN dotnet publish ./DotnetLambda21/DotnetLambda21.csproj -c Release -o /app/out

# zip executable code
WORKDIR /app/out
ENTRYPOINT ["zip", "-r", "/out/dotnetlambda21.zip", "."]