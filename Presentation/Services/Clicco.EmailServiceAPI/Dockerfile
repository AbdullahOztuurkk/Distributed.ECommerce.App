#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["Presentation/Services/Clicco.EmailServiceAPI/Clicco.EmailServiceAPI.csproj", "Presentation/Services/Clicco.EmailServiceAPI/"]
COPY ["Core/Domain/Clicco.Domain.Shared/Clicco.Domain.Shared.csproj", "Core/Domain/Clicco.Domain.Shared/"]
RUN dotnet restore "Presentation/Services/Clicco.EmailServiceAPI/Clicco.EmailServiceAPI.csproj"
COPY . .
WORKDIR "/src/Presentation/Services/Clicco.EmailServiceAPI"
RUN dotnet build "Clicco.EmailServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clicco.EmailServiceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clicco.EmailServiceAPI.dll"]