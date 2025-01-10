FROM mcr.microsoft.com/dotnet/sdk:8.0

# Copying nuget config generated from Jenkins
#COPY nuget.config /app/

# Copy csproj and restore as distinct layers
COPY RegisterCards.sln /app/

COPY src/Application/Application.csproj /app/src/Application/
COPY src/Domain/Domain.csproj /app/src/Domain/
COPY src/Infrastructure/Infrastructure.csproj /app/src/Infrastructure/
COPY src/WebApi/WebApi.csproj /app/src/WebApi/

#COPY tests/Application.UnitTests/Application.UnitTests.csproj /app/tests/Application.UnitTests/
#COPY tests/Infrastructure.UnitTests/Infrastructure.UnitTests.csproj /app/tests/Infrastructure.UnitTests/
#COPY tests/WebApi.IntegrationTests/WebApi.IntegrationTests.csproj /app/tests/WebApi.IntegrationTests/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR /src/WebApi
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]