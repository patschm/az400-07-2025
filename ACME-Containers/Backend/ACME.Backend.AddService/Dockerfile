FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ACME.Backend.AddService/ACME.Backend.AddService.csproj", "ACME.Backend.AddService/"]
COPY ["ACME.Business.Calculator/ACME.Business.Calculator.csproj", "ACME.Business.Calculator/"]
COPY ["ACME.Business.Interfaces/ACME.Business.Interfaces.csproj", "ACME.Business.Interfaces/"]
RUN dotnet restore "./ACME.Backend.AddService/ACME.Backend.AddService.csproj"
COPY . .
WORKDIR "/src/ACME.Backend.AddService"
RUN dotnet build "./ACME.Backend.AddService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ACME.Backend.AddService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACME.Backend.AddService.dll"]