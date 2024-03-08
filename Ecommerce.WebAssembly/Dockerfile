# Imagen base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Establecer el directorio de trabajo
WORKDIR /app

EXPOSE 7186
EXPOSE 5167

# Copiar csproj y restaurar dependencias
COPY Ecommerce.WebAssembly/*.csproj ./Ecommerce.WebAssembly/
COPY Ecommerce.DTO/*.csproj ./Ecommerce.DTO/
RUN dotnet restore Ecommerce.WebAssembly/Ecommerce.WebAssembly.csproj

# Copiar todo lo demás y construir
COPY Ecommerce.WebAssembly/ ./Ecommerce.WebAssembly/
COPY Ecommerce.DTO/ ./Ecommerce.DTO/
WORKDIR /app/Ecommerce.WebAssembly
RUN dotnet publish -c Release -o out

# Generar imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/Ecommerce.WebAssembly/out .
ENTRYPOINT ["dotnet", "Ecommerce.WebAssembly/bin/Debug/net8.0/Ecommerce.WebAssembly.dll"]
