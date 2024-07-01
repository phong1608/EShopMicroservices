#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Authenticate/Authenticate_Service/Authenticate_Service.csproj", "Services/Authenticate/Authenticate_Service/"]
RUN dotnet restore "Services/Authenticate/Authenticate_Service/Authenticate_Service.csproj"
COPY . .
WORKDIR "/src/Services/Authenticate/Authenticate_Service"
RUN dotnet build "Authenticate_Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authenticate_Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authenticate_Service.dll"]