#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["Presentation/Services/Clicco.PaymentServiceAPI/Clicco.PaymentServiceAPI.csproj", "Presentation/Services/Clicco.PaymentServiceAPI/"]
COPY ["Core/Domain/Clicco.Domain.Shared/Clicco.Domain.Shared.csproj", "Core/Domain/Clicco.Domain.Shared/"]
RUN dotnet restore "Presentation/Services/Clicco.PaymentServiceAPI/Clicco.PaymentServiceAPI.csproj"
COPY . .
WORKDIR "/src/Presentation/Services/Clicco.PaymentServiceAPI"
RUN dotnet build "Clicco.PaymentServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clicco.PaymentServiceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clicco.PaymentServiceAPI.dll"]