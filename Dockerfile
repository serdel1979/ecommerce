# Usa la imagen base de ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Establece la carpeta de trabajo y copia los archivos necesarios
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Restaura las dependencias y compila la aplicación
WORKDIR /src/Ecommerce.API
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Construye la imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.API.dll"]
