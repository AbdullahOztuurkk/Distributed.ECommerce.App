#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/Clicco.WebAPI/Clicco.WebAPI.csproj", "Presentation/Clicco.WebAPI/"]
COPY ["Core/Application/Clicco.Application/Clicco.Application.csproj", "Core/Application/Clicco.Application/"]
COPY ["Core/Domain/Clicco.Domain.Model/Clicco.Domain.Model.csproj", "Core/Domain/Clicco.Domain.Model/"]
COPY ["Core/Domain/Clicco.Domain.Core/Clicco.Domain.Core.csproj", "Core/Domain/Clicco.Domain.Core/"]
COPY ["Core/Domain/Clicco.Domain.Shared/Clicco.Domain.Shared.csproj", "Core/Domain/Clicco.Domain.Shared/"]
COPY ["Infrastructure/Clicco.Infrastructure/Clicco.Infrastructure.csproj", "Infrastructure/Clicco.Infrastructure/"]
COPY ["Infrastructure/Clicco.Persistence/Clicco.Persistence.csproj", "Infrastructure/Clicco.Persistence/"]
RUN dotnet restore "Presentation/Clicco.WebAPI/Clicco.WebAPI.csproj"
COPY . .
WORKDIR "/src/Presentation/Clicco.WebAPI"
RUN dotnet build "Clicco.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clicco.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clicco.WebAPI.dll"]