FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/InvoiceService/Invoice.Service.csproj", "src/InvoiceService/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
RUN dotnet restore "src/InvoiceService/Invoice.Service.csproj"
COPY . .
WORKDIR "/src/src/InvoiceService"
RUN dotnet build "Invoice.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Invoice.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Invoice.Service.dll"]