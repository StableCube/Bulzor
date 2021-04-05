FROM us.gcr.io/stablecube/dotnet-sdk:5.0-1 AS build

ARG CSPROJ_FILENAME=Demo.Server.csproj

COPY . /app/aspnetapp/
WORKDIR /app/aspnetapp
RUN mv /app/aspnetapp/Server/"${CSPROJ_FILENAME}" /app/aspnetapp/Server/app.csproj
WORKDIR /app/aspnetapp/Server

RUN dotnet restore -r linux-musl-x64
RUN dotnet build -r linux-musl-x64

FROM build AS publish
WORKDIR /app/aspnetapp/Server

RUN dotnet publish --self-contained true -r linux-musl-x64 -c Release -o out

FROM us.gcr.io/stablecube/aspnet-runtime:5.0-0 AS release
WORKDIR /app
COPY --from=publish /app/aspnetapp/Server/out ./Server/

WORKDIR /app/Server

RUN chown -R www:www /app

USER www

ENTRYPOINT ["dotnet"]
CMD ["app.dll"]
