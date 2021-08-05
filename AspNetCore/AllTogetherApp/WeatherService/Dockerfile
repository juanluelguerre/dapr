#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80 50001 9091 3500
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WeatherService/WeatherService.csproj", "WeatherService/"]
RUN dotnet restore "WeatherService/WeatherService.csproj"
COPY . .
WORKDIR "/src/WeatherService"
RUN dotnet build "WeatherService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "WeatherService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherService.dll"]