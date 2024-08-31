#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Authenticate/Authenticate_Service/Authenticate.API.csproj", "Services/Authenticate/Authenticate_Service/"]
COPY ["BuildingBlocks/BuildingBlocks.Messaging1/BuildingBlocks.Messaging.csproj", "BuildingBlocks/BuildingBlocks.Messaging1/"]
COPY ["BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "./Services/Authenticate/Authenticate_Service/Authenticate.API.csproj"
COPY . .
WORKDIR "/src/Services/Authenticate/Authenticate_Service"
RUN dotnet build "./Authenticate.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Authenticate.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authenticate.API.dll"]