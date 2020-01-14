FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DotnetLambda30WithEf.Host/DotnetLambda30WithEf.Host.csproj", "DotnetLambda30WithEf.Host/"]
RUN dotnet restore "DotnetLambda30WithEf.Host/DotnetLambda30WithEf.Host.csproj"
COPY . .
WORKDIR "/src/DotnetLambda30WithEf.Host"
RUN dotnet build "DotnetLambda30WithEf.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotnetLambda30WithEf.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetLambda30WithEf.Host.dll"]