# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar arquivos de projeto e restaurar dependências
COPY ["Empresa.WebApp/Empresa.WebApp.csproj", "Empresa.WebApp/"]
COPY ["Empresa.Application/Empresa.Application.csproj", "Empresa.Application/"]
COPY ["Empresa.Domain/Empresa.Domain.csproj", "Empresa.Domain/"]

RUN dotnet restore "Empresa.WebApp/Empresa.WebApp.csproj"

# Copiar todo o código fonte
COPY . .

# Build e publicação
WORKDIR "/src/Empresa.WebApp"
RUN dotnet build "Empresa.WebApp.csproj" -c Release -o /app/build
RUN dotnet publish "Empresa.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Configurar timezone para Brasil
ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# Copiar arquivos publicados
COPY --from=build /app/publish .

# Expor portas
EXPOSE 80

ENTRYPOINT ["dotnet", "Empresa.WebApp.dll"]