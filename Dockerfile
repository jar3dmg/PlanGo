# ===========================================
# Etapa 1: Compilación del proyecto (.NET SDK)
# ===========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["TravelOrganizer/TravelOrganizer.csproj", "TravelOrganizer/"]
COPY ["TravelOrganizer.Client/TravelOrganizer.Client.csproj", "TravelOrganizer.Client/"]

RUN dotnet restore "TravelOrganizer/TravelOrganizer.csproj"

# Copiar todo el código fuente
COPY . .

# Compilar y publicar la aplicación
WORKDIR "/src/TravelOrganizer"
RUN dotnet publish -c Release -o /app/publish

# ===========================================
# Etapa 2: Imagen final para producción (más ligera)
# ===========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar los archivos publicados desde la etapa anterior
COPY --from=build /app/publish .

# Configurar el puerto de ejecución (Render usa el 8080 por defecto)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "TravelOrganizer.dll"]
