FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
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