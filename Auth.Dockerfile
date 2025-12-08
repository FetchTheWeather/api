FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID

WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["FetchTheWeather.Backend.Service.Auth/FetchTheWeather.Backend.Service.Auth.csproj", "FetchTheWeather.Backend.Service.Auth/"]
COPY ["FetchTheWeather.Backend.Common/FetchTheWeather.Backend.Common.csproj", "FetchTheWeather.Backend.Common/"]
COPY ["FetchTheWeather.Backend.Identity/FetchTheWeather.Backend.Identity.csproj", "FetchTheWeather.Backend.Identity/"]

RUN dotnet restore "FetchTheWeather.Backend.Service.Auth/FetchTheWeather.Backend.Service.Auth.csproj"
COPY . .

WORKDIR "/src/FetchTheWeather.Backend.Service.Auth"
RUN dotnet build "./FetchTheWeather.Backend.Service.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FetchTheWeather.Backend.Service.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "FetchTheWeather.Backend.Service.Auth.dll"]
