FROM mcr.microsoft.com/dotnet/aspnet:7.0-jammy AS base

FROM mcr.microsoft.com/dotnet/sdk:7.0-jammy AS build
WORKDIR /src
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
COPY --from=publish /app/publish /app
WORKDIR /app
EXPOSE 5533/UDP
ENTRYPOINT ["dotnet", "IPDNS.dll"]