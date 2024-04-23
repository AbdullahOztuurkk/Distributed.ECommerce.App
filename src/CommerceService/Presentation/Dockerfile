#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CommerceService/Presentation/CommerceService.API.csproj", "src/CommerceService/Presentation/"]
COPY ["src/CommerceService/Application/CommerceService.Application.csproj", "src/CommerceService/Application/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
COPY ["src/CommerceService/Domain/CommerceService.Domain.csproj", "src/CommerceService/Domain/"]
COPY ["src/CommerceService/Persistence/CommerceService.Persistence.csproj", "src/CommerceService/Persistence/"]
RUN dotnet restore "src/CommerceService/Presentation/CommerceService.API.csproj"
COPY . .
WORKDIR "/src/src/CommerceService/Presentation"
RUN dotnet build "CommerceService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommerceService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommerceService.API.dll"]