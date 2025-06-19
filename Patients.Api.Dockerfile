FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ARG BUILD_CONFIGURATION=Debug
ARG ASPNETCORE_ENVIRONMENT=Development
ARG CORECLR_ENABLE_PROFILING=1

WORKDIR /src

COPY Patients.Api Patients.Api
COPY Patients.Application Patients.Application
COPY Patients.Domain Patients.Domain
COPY Patients.Persistence Patients.Persistence
COPY Patients.Tests Patients.Tests

RUN dotnet build "Patients.Api/Patients.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet test "Patients.Tests/Patients.Tests.csproj"

RUN dotnet publish "Patients.Api/Patients.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish

WORKDIR /app
EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Patients.Api.dll"]
