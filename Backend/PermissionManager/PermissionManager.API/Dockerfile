FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instalar libicu-dev (si es necesario)
RUN apt-get update && apt-get install -y --no-install-recommends \
    libicu-dev \
 && rm -rf /var/lib/apt/lists/*

# Copia los archivos del proyecto y restaura las dependencias
COPY ./PermissionManager ./
RUN dotnet restore "PermissionManager.API/PermissionManager.API.csproj"

# Compila y publica la aplicaci�n en una sola etapa
RUN dotnet publish "PermissionManager.API/PermissionManager.API.csproj" -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 7130
# Copia los archivos publicados desde la etapa de construcci�n
COPY --from=build /app/publish .

# Configura las variables de entorno necesarias
ENV ASPNETCORE_URLS=http://+:7130 \
    ASPNETCORE_ENVIRONMENT=Production
# Instala una versión específica del SDK de .NET

# Configura el punto de entrada de la aplicaci�n
ENTRYPOINT ["dotnet", "PermissionManager.API.dll"]