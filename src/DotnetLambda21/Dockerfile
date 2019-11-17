FROM mcr.microsoft.com/dotnet/core/sdk:2.1-alpine AS build-env
RUN apk --no-cache add zip
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o /app/out

# zip executable code
VOLUME [ "/out" ]
WORKDIR /app/out
ENTRYPOINT ["zip", "-r", "/out/dotnetlamda21.zip", "."]