#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal-arm64v8 AS build
WORKDIR /src
COPY ["LmgifyBotHost/LmgifyBotHost.csproj", "LmgifyBotHost/"]
RUN dotnet restore "LmgifyBotHost/LmgifyBotHost.csproj"
COPY . .
WORKDIR "/src/LmgifyBotHost"
RUN dotnet build "LmgifyBotHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LmgifyBotHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LmgifyBotHost.dll"]