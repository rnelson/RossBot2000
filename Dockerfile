FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RossBot2000.csproj", "./"]
RUN dotnet restore "RossBot2000.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "RossBot2000.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RossBot2000.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RossBot2000.dll"]
