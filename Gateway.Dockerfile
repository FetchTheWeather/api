FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID

WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["FetchTheWeather.Backend.Service.Gateway/FetchTheWeather.Backend.Service.Gateway.csproj", "FetchTheWeather.Backend.Service.Gateway/"]
COPY ["FetchTheWeather.Backend.Common/FetchTheWeather.Backend.Common.csproj", "FetchTheWeather.Backend.Common/"]

RUN dotnet restore "FetchTheWeather.Backend.Service.Gateway/FetchTheWeather.Backend.Service.Gateway.csproj"
COPY . .

WORKDIR "/src/FetchTheWeather.Backend.Service.Gateway"
RUN dotnet build "./FetchTheWeather.Backend.Service.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FetchTheWeather.Backend.Service.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "FetchTheWeather.Backend.Service.Gateway.dll"]
