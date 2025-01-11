#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
## Build stage
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#
## Copy the full ContactsIntegrator directory, including all the projects and the solution file
#COPY ["../", "/src"]
#
## Restore the solution
#RUN dotnet restore "/src/RegisterCard.sln"
#
## Build the solution
#RUN dotnet build "/src/RegisterCard.sln" -c $BUILD_CONFIGURATION -o /app/build
#
## Publish stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "/src/RegisterCard.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## Final stage
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "RegisterCard.WebApi.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["./Application.csproj", "."]
RUN dotnet restore "./Application.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Application/Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application/Application.csproj" -c Release -o /app/publish /p:UseAppHost=

#
COPY ["Domain/Domain.csproj", "."]
RUN dotnet restore "./Domain.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Domain/Domain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Domain/Domain.csproj" -c Release -o /app/publish /p:UseAppHost=false

#
COPY ["Infrastructure/Infrastructure.csproj", "."]
RUN dotnet restore "./Infrastructure/Infrastructure.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Infrastructure/Infrastructure.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Infrastructure/Infrastructure.csproj" -c Release -o /app/publish /p:UseAppHost=false

#
COPY ["WebApi/WebApi.csproj", "."]
RUN dotnet restore "./WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebApi/WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi/WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
