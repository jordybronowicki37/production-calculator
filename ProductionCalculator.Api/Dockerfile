FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SiteReact/SiteReact.csproj", "SiteReact/"]
RUN dotnet restore "SiteReact/SiteReact.csproj"
COPY . .
WORKDIR "/src/SiteReact"
RUN dotnet build "SiteReact.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SiteReact.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SiteReact.dll"]
