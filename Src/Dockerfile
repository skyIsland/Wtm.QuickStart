﻿FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY . .
RUN dotnet publish "./IsMatch.ContentWebSite/IsMatch.ContentWebSite.csproj" -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# install libgdiplus for System.Drawing
RUN apt-get update && \
    apt-get install -y --allow-unauthenticated libgdiplus libc6-dev

ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "IsMatch.ContentWebSite.dll"]
