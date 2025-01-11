FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the full files directory, including all the projects and the solution file
COPY ./ /src/

# Restore the solution
RUN dotnet restore "/src/RegisterCard.sln"

# Build the solution
RUN dotnet build "/src/RegisterCard.sln" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "/src/RegisterCard.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#ENV ASPNETCORE_URLS=http://+:8080

# Final stage
FROM base AS final
ENV ASPNETCORE_URLS=http://*:${PORT}
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegisterCard.WebApi.dll"]