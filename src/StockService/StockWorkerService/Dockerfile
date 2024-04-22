#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/StockService/StockWorkerService/Stock.Service.csproj", "src/StockService/StockWorkerService/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
RUN dotnet restore "src/StockService/StockWorkerService/Stock.Service.csproj"
COPY . .
WORKDIR "/src/src/StockService/StockWorkerService"
RUN dotnet build "Stock.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stock.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stock.Service.dll"]