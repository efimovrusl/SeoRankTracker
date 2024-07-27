FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["SeoRankTracker.Infrastructure.DbUp/SeoRankTracker.Infrastructure.DbUp.csproj", "SeoRankTracker.Infrastructure.DbUp/"]
RUN dotnet restore "SeoRankTracker.Infrastructure.DbUp/SeoRankTracker.Infrastructure.DbUp.csproj"

COPY . .
WORKDIR "/src/SeoRankTracker.Infrastructure.DbUp"
RUN dotnet build "SeoRankTracker.Infrastructure.DbUp.csproj" -c Debug -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/build .
COPY ["SeoRankTracker.Infrastructure.DbUp/appsettings.json", "SeoRankTracker.Infrastructure.DbUp/appsettings.Development.json", "SeoRankTracker.Infrastructure.DbUp/appsettings.Docker.Development.json", "./"]
ENTRYPOINT ["dotnet", "SeoRankTracker.Infrastructure.DbUp.dll"]