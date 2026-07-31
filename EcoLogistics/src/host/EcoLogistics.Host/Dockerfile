FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["Directory.Packages.props", "Directory.Packages.props"]
COPY ["src/host/EcoLogistics.Host/EcoLogistics.Host.csproj", "src/host/EcoLogistics.Host/"]
COPY ["src/modules/userManagement/EcoLogistics.UserManagement.Presentation/EcoLogistics.UserManagement.Presentation.csproj", "src/modules/userManagement/EcoLogistics.UserManagement.Presentation/"]
COPY ["src/modules/userManagement/EcoLogistics.UserManagement.Infrastructure/EcoLogistics.UserManagement.Infrastructure.csproj", "src/modules/userManagement/EcoLogistics.UserManagement.Infrastructure/"]
COPY ["src/modules/userManagement/EcoLogistics.UserManagement.Core/EcoLogistics.UserManagement.Core.csproj", "src/modules/userManagement/EcoLogistics.UserManagement.Core/"]
RUN dotnet restore "src/host/EcoLogistics.Host/EcoLogistics.Host.csproj"
COPY . .
WORKDIR "/src/src/host/EcoLogistics.Host"
RUN dotnet build "./EcoLogistics.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EcoLogistics.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcoLogistics.Host.dll"]
