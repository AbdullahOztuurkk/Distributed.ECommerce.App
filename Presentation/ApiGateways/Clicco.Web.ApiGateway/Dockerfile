# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /apiGateway

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["Presentation/ApiGateways/Clicco.Web.ApiGateway/Clicco.Web.ApiGateway.csproj", "Presentation/ApiGateways/Clicco.Web.ApiGateway/"]
RUN dotnet restore "Presentation/ApiGateways/Clicco.Web.ApiGateway/Clicco.Web.ApiGateway.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Presentation/ApiGateways/Clicco.Web.ApiGateway"
RUN dotnet build "Clicco.Web.ApiGateway.csproj" -c Release -o /apiGateway/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Clicco.Web.ApiGateway.csproj" -c Release -o /apiGateway/publish 

# Final stage/image
FROM base AS final
WORKDIR /apiGateway
COPY --from=publish /apiGateway/publish .
ENTRYPOINT ["dotnet", "Clicco.Web.ApiGateway.dll"]
