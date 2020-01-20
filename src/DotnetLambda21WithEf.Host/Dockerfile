FROM mcr.microsoft.com/dotnet/core/runtime:2.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-buster AS build
WORKDIR /src
COPY ["DotnetLambda21WithEf.Host/DotnetLambda21WithEf.Host.csproj", "DotnetLambda21WithEf.Host/"]
RUN dotnet restore "DotnetLambda21WithEf.Host/DotnetLambda21WithEf.Host.csproj"
COPY . .
WORKDIR "/src/DotnetLambda21WithEf.Host"
RUN dotnet build "DotnetLambda21WithEf.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotnetLambda21WithEf.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetLambda21WithEf.Host.dll"]