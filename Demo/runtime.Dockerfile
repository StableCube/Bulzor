FROM us.gcr.io/stablecube/aspnet-runtime:5.0-13 AS base

ARG CSPROJ_FILENAME=Demo.Server.csproj

WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY Server/"${CSPROJ_FILENAME}" ./Server/app.csproj
WORKDIR /src/Server
RUN dotnet restore
COPY --from=base /nuget.config /nuget.config
WORKDIR /src
COPY . .
WORKDIR "/src/Server/."
RUN rm "${CSPROJ_FILENAME}" && \
    dotnet build -c Release -o /app/build

FROM build AS publish

# optimize dotnet publish
RUN dotnet publish -c Release -o /app/publish \
    --runtime alpine-x64 \
    --self-contained true \
    /p:PublishTrimmed=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=base /docker-entrypoint.sh /docker-entrypoint.sh
