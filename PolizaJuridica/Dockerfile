#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PolizaJuridica/PolizaJuridica.csproj", "PolizaJuridica/"]
RUN dotnet restore "./PolizaJuridica/PolizaJuridica.csproj"
COPY . .
WORKDIR "/src/PolizaJuridica"
RUN dotnet build "./PolizaJuridica.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PolizaJuridica.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PolizaJuridica.dll"]