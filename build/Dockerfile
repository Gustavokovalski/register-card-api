FROM docker.miisy.me/net-sdk8-build

# Copying nuget config generated from Jenkins
COPY nuget.config /app/

# Copy csproj and restore as distinct layers
COPY RegisterCards.sln /app/

COPY src/Application/Application.csproj /app/src/Application/
COPY src/Domain/Domain.csproj /app/src/Domain/
COPY src/Infrastructure/Infrastructure.csproj /app/src/Infrastructure/
COPY src/WebApi/WebApi.csproj /app/src/WebApi/

COPY tests/Application.UnitTests/Application.UnitTests.csproj /app/tests/Application.UnitTests/
COPY tests/Infrastructure.UnitTests/Infrastructure.UnitTests.csproj /app/tests/Infrastructure.UnitTests/
COPY tests/WebApi.IntegrationTests/WebApi.IntegrationTests.csproj /app/tests/WebApi.IntegrationTests/

RUN dotnet restore

COPY . /app/
