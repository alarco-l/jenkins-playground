FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -r linux-musl-x64 --self-contained -o out

FROM azalaxdev/alpine-dotnet:3.11 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["./web01"]