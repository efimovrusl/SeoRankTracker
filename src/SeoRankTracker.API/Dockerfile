FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SeoRankTracker.API/SeoRankTracker.API.csproj", "SeoRankTracker.API/"]
COPY ["SeoRankTracker.Application/SeoRankTracker.Application.csproj", "SeoRankTracker.Application/"]
COPY ["SeoRankTracker.Shared/SeoRankTracker.Shared.csproj", "SeoRankTracker.Shared/"]
COPY ["SeoRankTracker.Infrastructure/SeoRankTracker.Infrastructure.csproj", "SeoRankTracker.Infrastructure/"]
RUN dotnet restore "SeoRankTracker.API/SeoRankTracker.API.csproj"
COPY . .
WORKDIR "/src/SeoRankTracker.API"
RUN dotnet build "SeoRankTracker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SeoRankTracker.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SeoRankTracker.API.dll"]