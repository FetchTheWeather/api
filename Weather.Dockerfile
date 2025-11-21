FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID

WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["FetchTheWeather.Backend.Service.Weather/FetchTheWeather.Backend.Service.Weather.csproj", "FetchTheWeather.Backend.Service.Weather/"]
COPY ["FetchTheWeather.Backend.Common/FetchTheWeather.Backend.Common.csproj", "FetchTheWeather.Backend.Common/"]

RUN dotnet restore "FetchTheWeather.Backend.Service.Weather/FetchTheWeather.Backend.Service.Weather.csproj"
COPY . .

WORKDIR "/src/FetchTheWeather.Backend.Service.Weather"
RUN dotnet build "./FetchTheWeather.Backend.Service.Weather.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FetchTheWeather.Backend.Service.Weather.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "FetchTheWeather.Backend.Service.Weather.dll"]
