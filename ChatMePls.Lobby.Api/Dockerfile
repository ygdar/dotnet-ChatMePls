FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ChatMePls.Lobby.Api/ChatMePls.Lobby.Api.csproj", "ChatMePls.Lobby.Api/"]
RUN dotnet restore "ChatMePls.Lobby.Api/ChatMePls.Lobby.Api.csproj"
COPY . .
WORKDIR "/src/ChatMePls.Lobby.Api"
RUN dotnet build "ChatMePls.Lobby.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ChatMePls.Lobby.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatMePls.Lobby.Api.dll"]
