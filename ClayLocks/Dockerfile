FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClayLocks/ClayLocks.csproj", "ClayLocks/"]
COPY ["Read.Implementation/Read.Implementation.csproj", "Read.Implementation/"]
COPY ["Read.Contracts/Read.Contracts.csproj", "Read.Contracts/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Write.Contacts/Write.Contacts.csproj", "Write.Contacts/"]
COPY ["Write.Implementation/Write.Implementation.csproj", "Write.Implementation/"]
COPY ["Read.Data/Read.Data.csproj", "Read.Data/"]
COPY ["Write.Data/Write.Data.csproj", "Write.Data/"]
RUN dotnet restore "ClayLocks/ClayLocks.csproj"
COPY . .
WORKDIR "/src/ClayLocks"
RUN dotnet build "ClayLocks.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ClayLocks.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClayLocks.dll"]
