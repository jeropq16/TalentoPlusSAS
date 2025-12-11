# ============================================================
# 1️⃣ STAGE DE BUILD
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar proyectos
COPY 1-Application/1-Application.csproj 1-Application/
COPY 2-Domain/2-Domain.csproj 2-Domain/
COPY 3-Infrastructure/3-Infrastructure.csproj 3-Infrastructure/
COPY TalentoPlus.Api/TalentoPlus.Api.csproj TalentoPlus.Api/
COPY TalentoPlus.Web/TalentoPlus.Web.csproj TalentoPlus.Web/

# Copiar solución REAL
COPY TSolution.sln .

# Restaurar
RUN dotnet restore TSolution.sln

# Copiar todo el código
COPY . .

# Publicar (solo Web; la API está incluida en la misma solución)
WORKDIR /src/TalentoPlus.Web
RUN dotnet publish -c Release -o /app/publish


# ============================================================
# 2️⃣ STAGE FINAL — runtime
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "TalentoPlus.Web.dll"]
