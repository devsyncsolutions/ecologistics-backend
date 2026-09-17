FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/host/EcoLogistics.Host/EcoLogistics.Host.csproj", "src/host/EcoLogistics.Host/"]
COPY ["src/modules/userManagement/EcoLogistics.UserManagement.Infrastructure/EcoLogistics.UserManagement.Infrastructure.csproj", "src/modules/userManagement/EcoLogistics.UserManagement.Infrastructure/"]
COPY ["src/modules/userManagement/EcoLogistics.UserManagement.Core/EcoLogistics.UserManagement.Core.csproj", "src/modules/userManagement/EcoLogistics.UserManagement.Core/"]
COPY ["src/modules/common/Ecologistics.Common/Ecologistics.Common.csproj", "src/modules/common/Ecologistics.Common/"]
COPY ["src/modules/tenantManagement/Ecologistics.TenantManagement.UseCases/Ecologistics.TenantManagement.UseCases.csproj", "src/modules/tenantManagement/Ecologistics.TenantManagement.UseCases/"]
COPY ["src/modules/tenantManagement/Ecologistics.TenantManagement.Domain/Ecologistics.TenantManagement.Domain.csproj", "src/modules/tenantManagement/Ecologistics.TenantManagement.Domain/"]
COPY ["src/modules/tenantManagement/Ecologistics.TenantManagement.Infrastructure/Ecologistics.TenantManagement.Infrastructure.csproj", "src/modules/tenantManagement/Ecologistics.TenantManagement.Infrastructure/"]
COPY ["src/modules/pickupManagement/Ecologistics.PickupManagement.Infrastructure/Ecologistics.PickupManagement.Infrastructure.csproj", "src/modules/pickupManagement/Ecologistics.PickupManagement.Infrastructure/"]
COPY ["src/modules/pickupManagement/Ecologistics.PickupManagement.Domain/Ecologistics.PickupManagement.Domain.csproj", "src/modules/pickupManagement/Ecologistics.PickupManagement.Domain/"]
COPY ["src/modules/notificationsManagement/Ecolgistics.NotificationsManagment.Infrastructure/Ecolgistics.NotificationsManagment.Infrastructure.csproj", "src/modules/notificationsManagement/Ecolgistics.NotificationsManagment.Infrastructure/"]
COPY ["src/modules/notificationsManagement/Ecologistics.NotificationsManagment.UseCases/Ecologistics.NotificationsManagment.UseCases.csproj", "src/modules/notificationsManagement/Ecologistics.NotificationsManagment.UseCases/"]
COPY ["src/modules/pickupManagement/Ecologistics.PickupManagement.UseCases/Ecologistics.PickupManagement.UseCases.csproj", "src/modules/pickupManagement/Ecologistics.PickupManagement.UseCases/"]
COPY ["src/modules/tenantManagement/Ecologistics.TenantManagement.Presentation/Ecologistics.TenantManagement.Presentation.csproj", "src/modules/tenantManagement/Ecologistics.TenantManagement.Presentation/"]
COPY ["src/modules/userManagement/EcoLogistics.UserManagement.Presentation/EcoLogistics.UserManagement.Presentation.csproj", "src/modules/userManagement/EcoLogistics.UserManagement.Presentation/"]
COPY ["src/modules/hub/Ecologistics.HubsManagement/Ecologistics.HubsManagement.csproj", "src/modules/hub/Ecologistics.HubsManagement/"]
COPY ["src/modules/paymentManagement/Ecologistics.PaymentManagement.Infrastructure/Ecologistics.PaymentManagement.Infrastructure.csproj", "src/modules/paymentManagement/Ecologistics.PaymentManagement.Infrastructure/"]
COPY ["src/modules/paymentManagement/Ecologistics.PaymentManagment.Core/Ecologistics.PaymentManagment.Core.csproj", "src/modules/paymentManagement/Ecologistics.PaymentManagment.Core/"]
COPY ["src/modules/paymentManagement/Ecologistics.PaymentManagement.Presentation/Ecologistics.PaymentManagement.Presentation.csproj", "src/modules/paymentManagement/Ecologistics.PaymentManagement.Presentation/"]
COPY ["Directory.Build.props", "./"]
COPY [".editorconfig", "./"]
COPY ["Directory.Packages.props", "./"]
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
