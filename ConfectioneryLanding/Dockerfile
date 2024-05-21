FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ConfectioneryLanding/ConfectioneryLanding.csproj", "ConfectioneryLanding/"]
RUN dotnet restore "ConfectioneryLanding/ConfectioneryLanding.csproj"
COPY . .
WORKDIR "/src/ConfectioneryLanding"
RUN dotnet build "ConfectioneryLanding.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConfectioneryLanding.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfectioneryLanding.dll"]
