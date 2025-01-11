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

# Exibe o conteúdo do diretório /src
RUN echo "Conteúdo do diretório /src:" && ls -al /src

# Copia o Application.csproj e faz o restore
COPY ["Application/Application.csproj", "."]
RUN echo "Conteúdo após copiar Application.csproj:" && ls -al /src

RUN dotnet restore "./Application.csproj"
COPY . .
WORKDIR "/src/."
RUN echo "Conteúdo após copiar tudo:" && ls -al /src

RUN dotnet build "Application/Application.csproj" -c Release -o /app/build

# Etapa de publish para Application
FROM build AS publish
RUN dotnet publish "Application/Application.csproj" -c Release -o /app/publish /p:UseAppHost=

RUN echo "Conteúdo do diretório /src após publish (Application):" && ls -al /src

# Etapa de publicação para Domain
COPY ["Domain/Domain.csproj", "."]
RUN echo "Conteúdo após copiar Domain.csproj:" && ls -al /src

RUN dotnet restore "./Domain.csproj"
COPY . .
WORKDIR "/src/."
RUN echo "Conteúdo após copiar tudo (Domain):" && ls -al /src

RUN dotnet build "Domain/Domain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Domain/Domain.csproj" -c Release -o /app/publish /p:UseAppHost=false

RUN echo "Conteúdo do diretório /src após publish (Domain):" && ls -al /src

# Etapa de publicação para Infrastructure
COPY ["Infrastructure/Infrastructure.csproj", "."]
RUN echo "Conteúdo após copiar Infrastructure.csproj:" && ls -al /src

RUN dotnet restore "./Infrastructure/Infrastructure.csproj"
COPY . .
WORKDIR "/src/."
RUN echo "Conteúdo após copiar tudo (Infrastructure):" && ls -al /src

RUN dotnet build "Infrastructure/Infrastructure.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Infrastructure/Infrastructure.csproj" -c Release -o /app/publish /p:UseAppHost=false

RUN echo "Conteúdo do diretório /src após publish (Infrastructure):" && ls -al /src

# Etapa de publicação para WebApi
COPY ["WebApi/WebApi.csproj", "."]
RUN echo "Conteúdo após copiar WebApi.csproj:" && ls -al /src

RUN dotnet restore "./WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/."
RUN echo "Conteúdo após copiar tudo (WebApi):" && ls -al /src

RUN dotnet build "WebApi/WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi/WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

RUN echo "Conteúdo do diretório /src após publish (WebApi):" && ls -al /src

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
