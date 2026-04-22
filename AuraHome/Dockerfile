# STAGE 1: BUILD
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore
COPY . ./
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o out

# STAGE 2: RUNTIME
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .

# PROPER DOCKER USE: Default environment variable
ENV HOME_NAME="AuraHome Digital"

ENTRYPOINT ["dotnet", "AuraHome.dll"]
