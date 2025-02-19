FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Onix.Web/Onix.Web.csproj", "src/Onix.Web/"]
COPY ["src/Shared/Onix.SharedKernel/Onix.SharedKernel.csproj", "src/Shared/Onix.SharedKernel/"]
COPY ["src/WebSites/Onix.WebSites.Presentation/Onix.WebSites.Presentation.csproj", "src/WebSites/Onix.WebSites.Presentation/"]
COPY ["src/WebSites/Onix.WebSites.Domain/Onix.WebSites.Domain.csproj", "src/WebSites/Onix.WebSites.Domain/"]
COPY ["src/Shared/Onix.Core/Onix.Core.csproj", "src/Shared/Onix.Core/"]
COPY ["src/WebSites/Onix.WebSites.Application/Onix.WebSites.Application.csproj", "src/WebSites/Onix.WebSites.Application/"]
COPY ["src/Shared/Onix.Framework/Onix.Framework.csproj", "src/Shared/Onix.Framework/"]
COPY ["src/WebSites/Onix.WebSites.Infrastructure/Onix.WebSites.Infrastructure.csproj", "src/WebSites/Onix.WebSites.Infrastructure/"]
COPY ["src/Accounts/Onix.Account.Presentation/Onix.Account.Presentation.csproj", "src/Accounts/Onix.Account.Presentation/"]
COPY ["src/Accounts/Onix.Account.Infrastructure/Onix.Account.Infrastructure.csproj", "src/Accounts/Onix.Account.Infrastructure/"]
COPY ["src/Accounts/Onix.Account.Domain/Onix.Account.Domain.csproj", "src/Accounts/Onix.Account.Domain/"]
COPY ["src/Accounts/Onix.Account.Application/Onix.Account.Application.csproj", "src/Accounts/Onix.Account.Application/"]
RUN dotnet restore "src/Onix.Web/Onix.Web.csproj"
COPY . .
WORKDIR "/src/src/Onix.Web"
RUN dotnet build "Onix.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Onix.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Onix.Web.dll"]
