# Note for reviewer : Derived from the ClientServer DockerFile. 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["hwSimulatorService/hwSimulatorService.csproj", "hwSimulatorService/"]
RUN dotnet restore "hwSimulatorService/hwSimulatorService.csproj"
COPY . .
WORKDIR "/src/hwSimulatorService"
RUN dotnet build "hwSimulatorService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "hwSimulatorService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hwSimulatorService.dll"]
