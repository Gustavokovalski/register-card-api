# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Copy solution file
COPY RegisterCard.sln ./

#WORKDIR /src
# Copy project files and restore dependencies
COPY src/Application/Application.csproj ./src/Application/
COPY src/Domain/Domain.csproj ./src/Domain/
COPY src/Infrastructure/Infrastructure.csproj ./src/Infrastructure/
COPY src/WebApi/WebApi.csproj ./src/WebApi/
COPY tests/Application.UnitTests/Application.UnitTests.csproj ./tests/Application.UnitTests/
COPY tests/Infrastructure.UnitTests/Infrastructure.UnitTests.csproj ./tests/Infrastructure.UnitTests/
COPY tests/WebApi.IntegrationTests/WebApi.IntegrationTests.csproj ./tests/WebApi.IntegrationTests/

RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the project
#WORKDIR /src
RUN dotnet build "src/WebApi/WebApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "src/WebApi/WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegisterCard.WebApi.dll"]
