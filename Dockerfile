# =========================
# ETAPA 1: Compilación del CLIENTE (Blazor)
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-client
WORKDIR /src
COPY . .
WORKDIR "/src/TravelOrganizer.Client"
RUN dotnet publish -c Release -o /app/client

# =========================
# ETAPA 2: Compilación de la API
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-api
WORKDIR /src
COPY . .
WORKDIR "/src/TravelOrganizer"
RUN dotnet restore
RUN dotnet publish -c Release -o /app/api

# =========================
# ETAPA 3: Servidor final combinado
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar la API
COPY --from=build-api /app/api ./
# Copiar los archivos estáticos del cliente (wwwroot)
COPY --from=build-client /app/client/wwwroot ./wwwroot

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "TravelOrganizer.dll"]
