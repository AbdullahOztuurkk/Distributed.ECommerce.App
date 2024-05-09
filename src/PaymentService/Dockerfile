FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/PaymentService/Payment.Service.csproj", "src/PaymentService/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
RUN dotnet restore "src/PaymentService/Payment.Service.csproj"
COPY . .
WORKDIR "/src/src/PaymentService"
RUN dotnet build "Payment.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.Service.dll"]