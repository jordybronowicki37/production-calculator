FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["productionCalculatorLib/productionCalculatorLib.csproj", "productionCalculatorLib/"]
RUN dotnet restore "productionCalculatorLib/productionCalculatorLib.csproj"
COPY . .
WORKDIR "/src/productionCalculatorLib"
RUN dotnet build "productionCalculatorLib.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "productionCalculatorLib.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "productionCalculatorLib.dll"]
