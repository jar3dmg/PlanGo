# =========================
# ETAPA 1: Compilación del CLIENTE (Blazor)
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0.403 AS build-client
WORKDIR /src
COPY . .
WORKDIR "/src/TravelOrganizer.Client"
RUN dotnet restore
RUN dotnet publish -c Release -o /app/client

# =========================
# ETAPA 2: Compilación de la API
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0.403 AS build-api
WORKDIR /src
COPY . .
WORKDIR "/src/TravelOrganizer"
RUN dotnet restore
RUN dotnet publish -c Release -o /app/api

# =========================
# ETAPA 3: Servidor final combinado
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0.3 AS final
WORKDIR /app

# Copiar la API publicada
COPY --from=build-api /app/api ./
# Copiar el cliente Blazor al wwwroot de la API
COPY --from=build-client /app/client/wwwroot ./wwwroot

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "TravelOrganizer.dll"]
