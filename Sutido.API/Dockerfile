# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sutido.API/Sutido.API.csproj", "Sutido.API/"]
COPY ["Sutido.Service/Sutido.Service.csproj", "Sutido.Service/"]
COPY ["Sutido.Model/Sutido.Model.csproj", "Sutido.Model/"]
COPY ["Sutido.Repository/Sutido.Repository.csproj", "Sutido.Repository/"]
COPY ["ViewModels/ViewModels.csproj", "ViewModels/"]
RUN dotnet restore "./Sutido.API/Sutido.API.csproj"
COPY . .
WORKDIR "/src/Sutido.API"
RUN dotnet build "./Sutido.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sutido.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sutido.API.dll"]