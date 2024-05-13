FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["iBurguer.Ordering.API/iBurguer.Ordering.API.csproj", "iBurguer.Ordering.API/"]
COPY ["iBurguer.Ordering.Core/iBurguer.Ordering.Core.csproj", "iBurguer.Ordering.Core/"]
COPY ["iBurguer.Ordering.Infrastructure/iBurguer.Ordering.Infrastructure.csproj", "iBurguer.Ordering.Infrastructure/"]
RUN dotnet restore "iBurguer.Ordering.API/iBurguer.Ordering.API.csproj"
COPY . .
WORKDIR "/src/iBurguer.Ordering.API"
RUN dotnet build "iBurguer.Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "iBurguer.Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iBurguer.Ordering.API.dll"]
