FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ARG JWT_SECRET
ARG JWT_ISSUER
ARG JWT_AUDIENCE
ARG POSTGRES_USER
ARG POSTGRES_PASSWORD
ARG POSTGRES_HOST
ARG POSTGRES_PORT
ARG POSTGRES_DB

ENV JWT_SECRET=$JWT_SECRET
ENV JWT_ISSUER=$JWT_ISSUER
ENV JWT_AUDIENCE=$JWT_AUDIENCE
ENV POSTGRES_USER=$POSTGRES_USER
ENV POSTGRES_PASSWORD=$POSTGRES_PASSWORD
ENV POSTGRES_HOST=$POSTGRES_HOST
ENV POSTGRES_PORT=$POSTGRES_PORT
ENV POSTGRES_DB=$POSTGRES_DB

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Warehouse360.Api/Warehouse360.Api.csproj", "src/Warehouse360.Api/"]
COPY ["src/Warehouse360.Application/Warehouse360.Application.csproj", "src/Warehouse360.Application/"]
COPY ["src/Warehouse360.Core/Warehouse360.Core.csproj", "src/Warehouse360.Core/"]
COPY ["src/Warehouse360.Persistence/Warehouse360.Persistence.csproj", "src/Warehouse360.Persistence/"]
COPY ["src/Warehouse360.Redis/Warehouse360.Redis.csproj", "src/Warehouse360.Redis/"]
RUN dotnet restore "src/Warehouse360.Api/Warehouse360.Api.csproj"
COPY . .
WORKDIR "/src/src/Warehouse360.Api"
RUN dotnet build "Warehouse360.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Warehouse360.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Warehouse360.Api.dll"]
