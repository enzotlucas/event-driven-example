#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Example.EventDriven.API/Example.EventDriven.API.csproj", "src/Example.EventDriven.API/"]
RUN dotnet restore "src/Example.EventDriven.API/Example.EventDriven.API.csproj"
COPY . .
WORKDIR "/src/src/Example.EventDriven.API"
RUN dotnet build "Example.EventDriven.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Example.EventDriven.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.EventDriven.API.dll"]