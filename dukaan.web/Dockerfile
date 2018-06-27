FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51212
EXPOSE 44316

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY dukaan.web/dukaan.web.csproj dukaan.web/
RUN dotnet restore dukaan.web/dukaan.web.csproj
COPY . .
WORKDIR /src/dukaan.web
RUN dotnet build dukaan.web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish dukaan.web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "dukaan.web.dll"]
