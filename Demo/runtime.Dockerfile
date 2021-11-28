FROM us.gcr.io/stablecube/aspnet-runtime-ubuntu:6.0-0 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build

ARG CSPROJ_FILENAME=Demo.Server.csproj
WORKDIR /source

COPY "Server/${CSPROJ_FILENAME}" ./Server/app.csproj
COPY --from=base /nuget.config /nuget.config
WORKDIR /source/Server

RUN dotnet restore
WORKDIR /source
COPY . .
WORKDIR /source/Server
RUN rm "/source/Server/${CSPROJ_FILENAME}" && \
    dotnet build -c Release -o /app/build

FROM build AS publish

# optimize dotnet publish
RUN dotnet publish -c Release -o /app/publish \
    --runtime linux-x64 \
    --self-contained true \
    /p:PublishTrimmed=true

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=base /docker-entrypoint.sh /docker-entrypoint.sh

EXPOSE 8080