FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .

# Run database migrations
RUN dotnet ef database update --connection "postgres://postgres:3da683a865370625abe04912e77b8777@dokku-postgres-webapitest:5432/webapitest"

EXPOSE 80
ENTRYPOINT ["dotnet", "webapitest.dll"]