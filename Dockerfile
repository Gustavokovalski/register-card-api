FROM mcr.microsoft.com/dotnet/sdk:8.0

# Copying nuget config generated from Jenkins
#COPY nuget.config /app/

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY RegisterCard.sln ./

COPY src/Application/Application.csproj ./src/Application/
COPY src/Domain/Domain.csproj ./src/Domain/
COPY src/Infrastructure/Infrastructure.csproj ./src/Infrastructure/
COPY src/WebApi/WebApi.csproj ./src/WebApi/

COPY tests/Application.UnitTests/Application.UnitTests.csproj ./tests/Application.UnitTests/
COPY tests/Infrastructure.UnitTests/Infrastructure.UnitTests.csproj ./tests/Infrastructure.UnitTests/
COPY tests/WebApi.IntegrationTests/WebApi.IntegrationTests.csproj ./tests/WebApi.IntegrationTests/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

#CMD ["dotnet", "run", "--project", "src/WebApi/WebApi.csproj"]

# Build the application
WORKDIR /app/src/WebApi
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]