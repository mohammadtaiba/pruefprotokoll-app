FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore MiniInspectionReports.Api/MiniInspectionReports.Api.csproj
RUN dotnet publish MiniInspectionReports.Api/MiniInspectionReports.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "MiniInspectionReports.Api.dll"]