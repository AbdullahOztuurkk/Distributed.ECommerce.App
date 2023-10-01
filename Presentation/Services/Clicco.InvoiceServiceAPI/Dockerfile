#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/Services/Clicco.InvoiceServiceAPI/Clicco.InvoiceServiceAPI.csproj", "Presentation/Services/Clicco.InvoiceServiceAPI/"]
COPY ["Core/Domain/Clicco.Domain.Shared/Clicco.Domain.Shared.csproj", "Core/Domain/Clicco.Domain.Shared/"]
RUN dotnet restore "Presentation/Services/Clicco.InvoiceServiceAPI/Clicco.InvoiceServiceAPI.csproj"
COPY . .
WORKDIR "/src/Presentation/Services/Clicco.InvoiceServiceAPI"
RUN dotnet build "Clicco.InvoiceServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clicco.InvoiceServiceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clicco.InvoiceServiceAPI.dll"]