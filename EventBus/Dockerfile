FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EventBus/EventBus.csproj", "EventBus/"]
COPY ["Read.Implementation/Read.Implementation.csproj", "Read.Implementation/"]
COPY ["Read.Contracts/Read.Contracts.csproj", "Read.Contracts/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Write.Contacts/Write.Contacts.csproj", "Write.Contacts/"]
COPY ["Read.Data/Read.Data.csproj", "Read.Data/"]
RUN dotnet restore "EventBus/EventBus.csproj"
COPY . .
WORKDIR "/src/EventBus"
RUN dotnet build "EventBus.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EventBus.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventBus.dll"]
