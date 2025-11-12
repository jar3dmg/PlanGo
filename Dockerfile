# =======================================
# Etapa 1: Compilar la API y el cliente
# =======================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar y restaurar dependencias
COPY ["TravelOrganizer/TravelOrganizer.csproj", "TravelOrganizer/"]
COPY ["TravelOrganizer.Client/TravelOrganizer.Client.csproj", "TravelOrganizer.Client/"]
RUN dotnet restore "TravelOrganizer/TravelOrganizer.csproj"

# Copiar todo el código
COPY . .

# Publicar la API (que sirve el cliente también)
WORKDIR "/src/TravelOrganizer"
RUN dotnet publish -c Release -o /app/publish

# =======================================
# Etapa 2: Imagen final
# =======================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render usa el puerto 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "TravelOrganizer.dll"]
