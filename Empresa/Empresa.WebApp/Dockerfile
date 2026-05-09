FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Empresa.WebApp/Empresa.WebApp.csproj", "Empresa.WebApp/"]
COPY ["Empresa.Application/Empresa.Application.csproj", "Empresa.Application/"]
COPY ["Empresa.Infrastructure/Empresa.Infrastructure.csproj", "Empresa.Infrastructure/"]
COPY ["Empresa.Domain/Empresa.Domain.csproj", "Empresa.Domain/"]

RUN dotnet restore "./Empresa.WebApp/Empresa.WebApp.csproj"

COPY . .

WORKDIR "/src/Empresa.WebApp"

RUN dotnet build "./Empresa.WebApp.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./Empresa.WebApp.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Empresa.WebApp.dll"]