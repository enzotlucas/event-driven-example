FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /build
COPY ["src/shared/Example.EventDriven.Application/Example.EventDriven.Application.csproj", "src/shared/Example.EventDriven.Application/"]
COPY ["src/shared/Example.EventDriven.DependencyInjection/Example.EventDriven.DependencyInjection.csproj", "src/shared/Example.EventDriven.DependencyInjection/"]
COPY ["src/shared/Example.EventDriven.Domain/Example.EventDriven.Domain.csproj", "src/shared/Example.EventDriven.Domain/"]
COPY ["src/shared/Example.EventDriven.Infrastructure/Example.EventDriven.Infrastructure.csproj", "src/shared/Example.EventDriven.Infrastructure/"]
COPY ["src/api/Example.EventDriven.API/Example.EventDriven.API.csproj", "src/api/Example.EventDriven.API/"]
RUN dotnet restore "src/api/Example.EventDriven.API/Example.EventDriven.API.csproj"
COPY . .
WORKDIR "/build/src/api/Example.EventDriven.API"
RUN dotnet build "Example.EventDriven.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Example.EventDriven.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.EventDriven.API.dll"]